using System;
using System.Text;
using System.Threading;
using Rainway.SDK;

// the first and only command line argument should be your rainway api key
var apiKey = Environment.GetCommandLineArgs()[1];

// if we don't have a valid apiKey, throw an exception
if (string.IsNullOrEmpty(apiKey))
{
    throw new ArgumentException("Expected <apiKey> as the first (and only) command line argument");
}

static string ReverseString(byte[] data)
{
    var str = Encoding.UTF8.GetString(data);
    var charArray = str.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}

// configure logging
RainwayRuntime.SetLogLevel(RainwayLogLevel.Info, null);
RainwayRuntime.SetLogSink((level, target, message) =>
{
    Console.WriteLine($"{level} [{target}] {message}");
});

// the runtime configuration
var config = new RainwayConfig
{
    // your publishable API key read from command line arguments above
    ApiKey = apiKey,
    // any string identifying a user or entity within your app (optional)
    ExternalId = string.Empty,
    // audo accepts all connection request
    OnConnectionRequest = (request) => request.Accept(),
    // auto accepts all stream request and gives full input privileges to the remote peer 
    OnStreamRequest = (requests) => requests.Accept(new RainwayStreamConfig() {
        InputLevel = RainwayInputLevel.Mouse | RainwayInputLevel.Keyboard | RainwayInputLevel.GamepadPortAll,
        IsolateProcessIds = Array.Empty<uint>()
    }),
    // reverses the data sent by a peer over a channel and echos it back
    OnPeerMessage = (peer, channel, data) => peer.Send(channel, ReverseString(data))
};

// initalize the runtime
using var runtime = await RainwayRuntime.Initialize(config);
Console.WriteLine($"Rainway SDK Version: {runtime.Version}");
Console.WriteLine($"Peer Id: {runtime.PeerId}");
Console.WriteLine("Press Ctrl+C To Terminate");

var closeEvent = new AutoResetEvent(false);

Console.CancelKeyPress += (sender, eventArgs) =>
{
    eventArgs.Cancel = true;
    closeEvent.Set();
};

closeEvent.WaitOne();
