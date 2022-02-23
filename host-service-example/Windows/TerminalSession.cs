using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ServiceExample.Windows
{
    public static class TerminalSession
    {
        /// <summary>
        /// Tries to forcefully end any RDP sessions and reattaches interactive console without locking the desktop.
        /// </summary>
        /// <returns>true if the function succeeds, otherwise false.</returns>
        public static bool TryDisconnectRDP()
        {
            try
            {
                if (TerminalServerSession)
                {
                    var tscon = Environment.ExpandEnvironmentVariables(Path.Combine("%WINDIR%", "System32", "tscon.exe"));
                    if (!string.IsNullOrWhiteSpace(tscon) && File.Exists(tscon))
                    {
                        using var currentProcess = Process.GetCurrentProcess();
                        using var tsconProcess = Process.Start(tscon, $"{currentProcess.SessionId} /dest:console");
                        if (tsconProcess is not null)
                        {
                            tsconProcess.WaitForExit();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static bool TerminalServerSession => (uint)(GetSystemMetrics(4096) & 1) > 0U;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int GetSystemMetrics(int nIndex);

    }
}
