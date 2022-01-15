# Service Example

This example demonstrates how to use the Rainway SDK in a C# console app that behaves as both a standard process and a service.

For detailed explanation of how everything works read the comments in [Program.cs](Program.cs#L11), all the corresponding documentation.

To test this project provide your API key to the `RainwayConfig` and then use the two scripts included:
- `install-service.ps1` which will build the project and install it as a Windows service
- `start-service.ps1` which will start the service and passthrough any arguments supplied to the script

