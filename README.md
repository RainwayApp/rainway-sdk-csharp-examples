<div align="center">

![A multi-colored spiral on a dark background with the Rainway logo and the text "Node Examples" in the center](.github/header.jpg)

[💯 Stable Branch](https://github.com/RainwayApp/rainway-sdk-csharp-examples/tree/main) - [🚀 Beta Branch](https://github.com/RainwayApp/rainway-sdk-csharp-examples/tree/beta) - [📘 Read the Docs](https://docs.rainway.com/docs/net-getting-started) - [🎁 Get Started](https://hub.rainway.com/landing)

[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/rainway-sdk)](https://www.nuget.org/packages/rainway-sdk)
[![Nuget](https://img.shields.io/nuget/dt/rainway-sdk)](https://www.nuget.org/packages/rainway-sdk)

[![Host Example CI](https://github.com/RainwayApp/rainway-sdk-csharp-examples/actions/workflows/host-example-ci.yml/badge.svg)](https://github.com/RainwayApp/rainway-sdk-csharp-examples/actions/workflows/host-example-ci.yml)
[![Host Service Example CI](https://github.com/RainwayApp/rainway-sdk-csharp-examples/actions/workflows/host-service-example-ci.yml/badge.svg)](https://github.com/RainwayApp/rainway-sdk-csharp-examples/actions/workflows/host-service-example-ci.yml)
[![CD](https://github.com/RainwayApp/rainway-sdk-csharp-examples/actions/workflows/cd.yml/badge.svg)](https://github.com/RainwayApp/rainway-sdk-csharp-examples/actions/workflows/cd.yml)

</div>

# rainway-sdk-csharp-examples

Various examples of how to use the Rainway SDK with .NET:

- [host-example](./host-example/) - A CLI example that allows all web clients to connect and stream the desktop.
- [host-example-gui](./host-example-gui/) - A GUI example that allows all web clients to connect and stream the desktop.
- [host-service-example](./host-service-example/) - A CLI example that can act as both a standard process and a windows service.

Note: Both `host-example` and `host-example-gui` consume a shared base project, [host-example-core](./host-example-core/) which exposes a small wrapper for allowing clients to connect and stream.
