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
                ExternalId = string.Empty,
                // audo accepts all connection request
                OnConnectionRequest = (runtime, request) => request.Accept(),
                // auto accepts all stream request and gives full input privileges to the remote peer
                OnStreamRequest = (runtime, requests) => requests.Accept(new RainwayStreamConfig()
                {
                    StreamType = RainwayStreamType.FullDesktop,
                    InputLevel = RainwayInputLevel.Mouse | RainwayInputLevel.Keyboard | RainwayInputLevel.GamepadPortAll,
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
    }
}
