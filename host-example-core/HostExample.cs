using Rainway.SDK;
using Rainway.SDK.Identifiers;
using System.Text;

namespace Rainway.HostExample
{
    /// <summary>
    /// A Rainway application that streams the whole desktop to all clients, and
    /// responds to Data Channel messages.
    /// </summary>
    public class Core
    {
        private readonly Action<string> logCallback;
        private RainwayConnection? conn;

        /// <summary>
        /// Instantiate the application using the given callback for logging.
        /// </summary>
        /// <param name="logCallback">A string-consuming callback.</param>
        public Core(Action<string> logCallback)
        {
            this.logCallback = logCallback;
        }

        /// <summary>
        /// Connect to Rainway and start accepting connections.
        /// </summary>
        /// <param name="rainwayApiKey">The Rainway API key to use.</param>
        /// <returns>A task that completes when the connection is
        /// established.</returns>
        public async Task Start(string rainwayApiKey)
        {
            // First, set up logging so that we'll get descriptive log events if
            // initialization fails.
            RainwaySDK.LogLevel = LogLevel.Info;
            RainwaySDK.LogHandler = (ev) =>
            {
                logCallback($"{ev.Data.Level} [{ev.Data.Target}] {ev.Data.Message}");
            };

            // Initialize Rainway with the config. This task completes when
            // we're successfully connected to the Rainway Network.
            conn = await RainwaySDK.ConnectAsync(new SDK.Options.ConnectOptions() { ApiKey = rainwayApiKey, ExternalId = "Rainway C# Host Example" });

            conn.PeerRequest += async (_, req) =>
            {
                if (AcceptIncoming)
                {
                    var peer = await req.AcceptAsync();
                    peer.StateChange += (_, ev) =>
                    {
                        logCallback($"Peer {peer.Id} changed states to {ev.Data}");
                    };
                    peer.StreamRequest += async (_, req) =>
                    {
                        var stream = await req.AcceptAsync(new SDK.Options.OutboundStreamOptions() { Type = StreamType.FullDesktop, Permissions = inputLevel });
                    };
                    peer.DataChannel += (_, ev) =>
                    {
                        var dc = ev.Data;
                        dc.Message += (_, ev) =>
                        {
                            HandleMessage(dc, ev.Data);
                        };
                    };
                }
                else
                {
                    await req.RejectAsync("accept setting is off");
                }
            };
        }

        /// <summary>
        /// If connected: get our own Peer ID.
        /// </summary>
        public PeerId? Id => conn?.Id;

        /// <summary>
        /// If connected: get the Rainway SDK version.
        /// </summary>
        public Version? Version => new Version(RainwaySDK.Version);

        /// <summary>
        /// A publicly settable property that determines whether to accept
        /// incoming connection requests from other peers.
        /// </summary>
        public bool AcceptIncoming { get; set; } = true;

        /// <summary>
        /// Are we currently connected?
        /// </summary>
        public bool Connected => conn != null;

        /// <summary>
        /// Disconnect from all peers and from the Rainway Network.
        /// </summary>
        public void Stop()
        {
            conn?.Dispose();
            conn = null;
        }

        private void HandleMessage(DataChannel ch, byte[] data)
        {
            var chars = Encoding.UTF8.GetString(data).ToCharArray();
            Array.Reverse(chars);
            ch.Send(new string(chars));
        }

        private InputLevel inputLevel = InputLevel.Mouse | InputLevel.Keyboard | InputLevel.GamepadPortAll;

        public void SetInputLevel(bool mouse, bool keyboard, bool gamepad)
        {
            // Set it for future connections...
            inputLevel = (mouse ? InputLevel.Mouse : 0)
                | (keyboard ? InputLevel.Keyboard : 0)
                | (gamepad ? InputLevel.GamepadPortAll : 0);

            // And if connected, update it in active streams:
            if (conn == null) return;
            foreach (var peer in conn.Peers)
            {
                foreach (var stream in peer.OutboundStreams)
                {
                    stream.Value.Permissions = inputLevel;
                }
            }
        }
    }
}
