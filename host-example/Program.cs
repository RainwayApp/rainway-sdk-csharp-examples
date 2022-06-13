using System;
using System.Text;
using System.Threading;

// retrieve the cli arguments
var cliArgs = Environment.GetCommandLineArgs();

// if we don't have a valid apiKey as the only argument, throw an exception
if (cliArgs.Length != 2 || string.IsNullOrEmpty(cliArgs[1]))
{
    throw new ArgumentException("Expected <apiKey> as the first (and only) command line argument");
}

var apiKey = cliArgs[1];
var core = new Rainway.HostExample.Core(Console.WriteLine);

// initalize the runtime
await core.Start(apiKey);
Console.WriteLine($"Rainway SDK Version: {core.Version}");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"Peer ID: {core.PeerId}");
Console.ResetColor();
Console.WriteLine("Press Ctrl+C To Terminate");

var closeEvent = new AutoResetEvent(false);

Console.CancelKeyPress += (sender, eventArgs) =>
{
    eventArgs.Cancel = true;
    closeEvent.Set();
};

closeEvent.WaitOne();
