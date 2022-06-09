using Rainway.SDK;
using System.Text;

namespace Rainway.HostExample
{
    public class Core
    {
        private readonly Action<string> logCallback;
        private RainwayRuntime? runtime;

        public Core(Action<string> logCallback)
        {
            this.logCallback = logCallback;
        }

        public async Task Start(string rainwayApiKey)
        {
            RainwayRuntime.SetLogLevel(RainwayLogLevel.Info, null);
            RainwayRuntime.SetLogSink((level, target, message) =>
            {
                logCallback($"{level} [{target}] {message}");
            });
            // the runtime configuration
            var config = new RainwayConfig
            {
                // your publishable API key read from command line arguments above
                ApiKey = rainwayApiKey,
                // any string identifying a user or entity within your app (optional)
                ExternalId = "Rainway C# Host Example",
                OnConnectionRequest = (runtime, request) => {
                    if (AcceptIncoming)
                        request.Accept();
                    else
                        request.Reject("accept setting is off");
                },
                // auto accepts all stream request and gives full input privileges to the remote peer
                OnStreamRequest = (runtime, requests) => requests.Accept(new RainwayStreamConfig()
                {
                    StreamType = RainwayStreamType.FullDesktop,
                    InputLevel = inputLevel,
                    IsolateProcessIds = Array.Empty<uint>()
                }),
                // reverses the data sent by a peer over a channel and echos it back
                OnPeerMessage = (runtime, peer, channel, data) => HandlePeerMessage(peer, channel, data),
                // logs peer state changes, including connect and disconnect
                OnPeerStateChange = (runtime, peer, state) =>
                {
                    logCallback($"Peer {peer.PeerId} changed states to {state}");
                }
            };
            runtime = await RainwayRuntime.Initialize(config);
        }

        public RainwayPeerId? PeerId => runtime?.PeerId;
        public Version? Version => runtime?.Version;

        public bool AcceptIncoming { get; set; } = true;
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
            inputLevel = (mouse ? RainwayInputLevel.Mouse : 0)
                | (keyboard ? RainwayInputLevel.Keyboard : 0)
                | (gamepad ? RainwayInputLevel.GamepadPortAll : 0);
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
