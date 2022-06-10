using Rainway.SDK;
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
        private RainwayRuntime? runtime;

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
            RainwayRuntime.SetLogLevel(RainwayLogLevel.Info, null);
            RainwayRuntime.SetLogSink((level, target, message) =>
            {
                logCallback($"{level} [{target}] {message}");
            });

            // Define callbacks for Rainway events:
            var config = new RainwayConfig
            {
                ApiKey = rainwayApiKey,
                ExternalId = "Rainway C# Host Example",
                OnConnectionRequest = (runtime, request) =>
                {
                    if (AcceptIncoming) request.Accept();
                    else request.Reject("accept setting is off");
                },
                OnStreamRequest = (runtime, request) =>
                {
                    request.Accept(new RainwayStreamConfig()
                    {
                        StreamType = RainwayStreamType.FullDesktop,
                        InputLevel = inputLevel,
                        IsolateProcessIds = Array.Empty<uint>()
                    });
                },
                OnPeerMessage = (runtime, peer, channel, data) => HandlePeerMessage(peer, channel, data),
                OnPeerStateChange = (runtime, peer, state) =>
                {
                    logCallback($"Peer {peer.PeerId} changed states to {state}");
                }
            };

            // Initialize Rainway with the config. This task completes when
            // we're successfully connected to the Rainway Network.
            runtime = await RainwayRuntime.Initialize(config);
        }

        /// <summary>
        /// If connected: get our own Peer ID.
        /// </summary>
        public RainwayPeerId? PeerId => runtime?.PeerId;

        /// <summary>
        /// If connected: get the Rainway SDK version.
        /// </summary>
        public Version? Version => runtime?.Version;

        /// <summary>
        /// A publicly settable property that determines whether to accept
        /// incoming connection requests from other peers.
        /// </summary>
        public bool AcceptIncoming { get; set; } = true;

        /// <summary>
        /// Are we currently connected?
        /// </summary>
        public bool Connected => runtime != null;

        public void Stop()
        {
            runtime?.Dispose();
            runtime = null;
        }

        private void HandlePeerMessage(RainwayPeer peer, string channel, byte[] data)
        {
            var chars = Encoding.UTF8.GetString(data).ToCharArray();
            Array.Reverse(chars);
            peer.Send(channel, new string(chars));
        }

        private RainwayInputLevel inputLevel = RainwayInputLevel.Mouse | RainwayInputLevel.Keyboard | RainwayInputLevel.GamepadPortAll;

        public void SetInputLevel(bool mouse, bool keyboard, bool gamepad)
        {
            // Set it for future connections...
            inputLevel = (mouse ? RainwayInputLevel.Mouse : 0)
                | (keyboard ? RainwayInputLevel.Keyboard : 0)
                | (gamepad ? RainwayInputLevel.GamepadPortAll : 0);

            // And if connected, update it in active streams:
            if (runtime == null) return;
            foreach (var peer in runtime.Peers.Values)
            {
                foreach (var stream in peer.Streams)
                {
                    stream.Permissions = inputLevel;
                }
            }
        }
    }
}
