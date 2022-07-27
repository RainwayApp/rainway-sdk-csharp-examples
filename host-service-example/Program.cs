using Microsoft.Win32;
using Rainway.SDK;
using Rainway.SDK.Options;
using ServiceExample;
using ServiceExample.Windows;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using Warden.Core;

// This example application demonstrates how to use the Rainway SDK in a C#
// console app that behaves as both a standard process and a service. The
// benefit of this design is that your application has a single primary
// executable. It also allows your process to interact with parts of the desktop
// that are normally blocked, such as UAC prompts.
//
// A few notes:
//
// * We do not recommend running a GUI (WinForm, WPF, etc.) inside of this
//   process. Basic elements such as the Windows file picker will be broken.
// * When running under SYSTEM, certain calls (such as usage of the Registry
//   class) need to be called with impersonation.

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
    // First we check if the current process was launched by the Service Control Manager (SCM).
    if (WardenProcess.GetCurrentProcess().IsWindowsService())
    {
        // The 'BasicService' class handles the creation of an interactive process.
        // Please see the documentation inside the class for more information.
        ServiceBase.Run(new BasicService());
        return;
    }
    else
    {
        // This the primary entry point for the app and indicates an interactive
        // context. There are two ways to get here:
        //
        //  1. 'service-example.exe' was run from the command line or was
        //     double-clicked.
        //  2. The SCM launched 'service-example.exe', and then 'BasicService'
        //     recreated the process in the interactive terminal.
        //
        // In the first case, there is nothing special about the process. In the
        // second case, the process is on the interactive desktop, and has the
        // environment and privileges of SYSTEM. In this case there are various
        // properties about the current process and its environment we need to
        // configure to ensure a stable runtime.

        // First: we disable native Windows error prompts. When we arrive here
        // because of the SCM, before the process is forced into the interactive
        // terminal these prompts will cause the program to hang forever, as
        // there is no way to interact with them.
        ErrorHandling.DisableErrorPrompts();

        // Second: we try to attach a console to the current process. If the
        // process was launched normally, this will return false. When we arrive
        // here because of the SCM, however, the process will not have any
        // console.
        if (ApplicationConsole.TryAssociateConsole())
        {
            Console.WriteLine("Console associated with current process");
        }
        // Third: we disable Quick Edit mode. When this mode is enabled,
        // selecting text in the console causes Windows to pause your
        // application's execution.
        if (ApplicationConsole.TryDisableQuickEdit())
        {
            Console.WriteLine("Quick edit mode disabled");
        }
        // Fourth: we forcefully disconnect any Remote Desktop sessions. This is
        // critical. If `RainwayRuntime.Initialize` is called while an RDP
        // session is active, it may not be able to locate discrete GPUs and
        // properly initialize the environment for use. This call will end RDP
        // sessions and reattach the interactive console.
        //
        // TODO: remove this line as it will be handled done by the runtime in
        // the next release.
        if (TerminalSession.TryDisconnectRDP())
        {
            Console.WriteLine("Forced Windows into the client terminal session.");
        }

        // Set up a callback for Rainway SDK logging
        RainwaySDK.LogLevel = LogLevel.Info;
        RainwaySDK.LogHandler += (ev) => Console.WriteLine($"{ev.Data.Level} [{ev.Data.Target}] {ev.Data.Message}");

        // Connect to Rainway services. Modify this to contain your API key
        var connection = await RainwaySDK.ConnectAsync(new ConnectOptions() {
            // your publishable API key should go here
            ApiKey = "YOUR_API_KEY",
            // any string identifying a user or entity within your app (optional)
            ExternalId = "Rainway SDK C# service-example",
        });

        // auto accepts all connection requests
        connection.PeerRequest += async (_, request) =>
        {
            var peer = await request.AcceptAsync();

            peer.StreamRequest += async (_, request) =>
            {
                // auto accepts all stream request and gives full input permissions to the remote peer
                var stream = await request.AcceptAsync(new OutboundStreamOptions()
                {
                    Type = StreamType.FullDesktop,
                    Permissions = InputLevel.All,
                });

                // attach an event listener for when data channels are created
                // this lets us interact with the data channel (and it's messages)
                peer.DataChannel += (_, ev) =>
                {
                    // obtain the created data channel
                    var channel = ev.Data;

                    // Example handler for messages from a remote peer: reverse UTF-8 data and echo it back
                    channel.Message += (_, ev) =>
                    {
                        channel.Send(ReverseString(ev.Data));
                    };
                };
            };
        };

        Console.WriteLine($"whoami: {WardenImpersonator.Username}");
        Console.WriteLine($"Needs Impersonation?: {WardenImpersonator.NeedsImpersonation}");
        if (WardenImpersonator.NeedsImpersonation)
        {
            WardenImpersonator.RunAsUser(() =>
            {
                // Services that change impersonation should call RegDisablePredefinedCache
                // before using any of the predefined handles
                // TODO: remove once Warden does this for the user in the next version
                if (RegDisablePredefinedCacheEx() != 0)
                {
                    Console.WriteLine($"RegDisablePredefinedCacheEx returned non-zero value. ErrorCode: [{Marshal.GetLastWin32Error()}]");
                }
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    // If RegDisablePredefinedCacheEx is not called and the SCM
                    // launched this process, then the below code will not work.
                    // `key` will be null.
                    using var key = Registry.CurrentUser.OpenSubKey("Volatile Environment");
                    if (key is not null)
                    {
                        foreach (var name in key.GetValueNames())
                        {
                            var value = key.GetValue(name);
                            Console.WriteLine($"{name}:{value}");
                        }
                    }
                }
                Console.WriteLine($"Is Thread Impersonating?: {WardenImpersonator.IsThreadImpersonating}");
            });
        }


        // Inform the user about some Rainway state
        Console.WriteLine($"Rainway SDK Version: {RainwaySDK.Version}");
        Console.WriteLine($"Peer Id: {connection.Id}");
        Console.WriteLine("Press Ctrl+C To Terminate");

        // Prevent the console app from closing right away.
        var closeEvent = new AutoResetEvent(false);
        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            eventArgs.Cancel = true;
            closeEvent.Set();
        };
        closeEvent.WaitOne();
    }
}


static string ReverseString(byte[] data)
{
    var str = Encoding.UTF8.GetString(data);
    var charArray = str.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}

[DllImport("Advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
static extern int RegDisablePredefinedCacheEx();
