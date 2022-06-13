# rainway-sdk-csharp-examples

Various examples of how to use the Rainway SDK with C#:

* **host-example-core** is the business logic for a simple Rainway application that hosts streams to all incoming peers: `var core = new Core(); core.Start()` starts the application and `core.Stop()` terminates it.
  * **host-example-cli** and **host-example-gui** are demo applications built on top of this core, which you may find useful when testing Rainway hosting.
* **service-example** demonstrates how to use the Rainway SDK in a C# console app that can act as both a standard process and a service.
