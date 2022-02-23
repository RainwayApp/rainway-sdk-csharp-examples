# Service Example

This example demonstrates how to use the Rainway SDK in a C# console app that can act as both a standard process and a service.

## Running as a process
1. Edit [Program.cs](Program.cs) to replace `YOUR_API_KEY` with your Rainway SDK API key, obtained from the [Rainway Hub](https://hub.rainway.com/).
2. In Visual Studio, build and run the _service-example_ project contained in this solution.

The process will accept any incoming stream requests and yield remote control of the host desktop. You can edit the `RainwayConfig` object in Program.cs to modify this behavior.

You can watch the logs for mention of a PeerID, and use the [Web SDK demo app](https://sdk-builds.rainway.com/demos/web/) (which is a hosted build created from [this repository](https://github.com/RainwayApp/rainway-sdk-web-demo)) to connect to it remotely.

## Running as a service
See [Program.cs](Program.cs#L11) for a description of how the application creates an interactive process for itself when it detects that it was launched as a service.

Two PowerShell scripts are supplied to help run this example app as a service:

- `install-service.ps1` will build the project and install it as a Windows service. Requires Administrator access.
- `start-service.ps1` will start the service and pass through any arguments supplied to the script.

