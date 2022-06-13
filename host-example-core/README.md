# host-example-core

This is an example of a simple C# codebase that uses the Rainway SDK.

It contains the business logic for an application that connects to Rainway and accepts all incoming client connections, streaming the full desktop when requested.

It exposes a class `Core` with two simple methods: `Start()` and `Stop()`.

Both the [CLI](../host-example) and [GUI](../host-example-gui) demos for C# are powered by this "core", so you can change the code and build either of those apps to play around with Rainway.

Good luck!
