using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace ServiceExample.Windows
{
    /// <summary>
    ///     A class that assist with various win32 console operations.
    /// </summary>
    public static class ApplicationConsole
    {
        /// <summary>
        /// Attempts to associate the current process with a console.
        /// </summary>
        /// <returns>if the function succeeds, the return value is true.</returns>
        /// <remarks>
        /// if this function returns false, call <see cref="Marshal.GetLastWin32Error"/> for the error code.
        /// </remarks>
        public static bool TryAssociateConsole()
        {
            // if the process has a parent process with a console we can attach to that.
            if (AttachConsole(AttachParentProcess))
            {
                return true;
            }
            // otherwise a new console must be allocated. 
            return AllocConsole();
        }

        /// <summary>
        ///     Attempts to disable  Quick Edit mode for the current console. Disabling the Quick Edit feature of the Win32 console
        ///     can prevent unintentional blocking of the main thread when it becomes selected by the mouse.
        /// </summary>
        /// <returns>if the function succeeds, the return value is true.</returns>
        /// <remarks>
        /// if this function returns false, call <see cref="Marshal.GetLastWin32Error"/> for the error code.
        /// </remarks>
        public static bool TryDisableQuickEdit()
        {
            using var consoleHandle = GetStdHandle(StdInputHandle);
            // grab the current input mode of the console.
            // if this fails one is not attached to the current process.
            if (!GetConsoleMode(consoleHandle, out var consoleMode))
            {
                return false;
            }
            // Clear the quick edit bit in the mode flags
            consoleMode &= ~EnableQuickEdit;
            // Set the new mode
            return SetConsoleMode(consoleHandle, consoleMode);
        }

        #region Consts

        /// <summary>
        ///     Use the console of the parent of the current process.
        /// </summary>
        private const int AttachParentProcess = -1;

        /// <summary>
        ///     This flag enables the user to use the mouse to select and edit text.
        /// </summary>
        private const uint EnableQuickEdit = 0x0040;

        /// <summary>
        ///     The standard input device.
        /// </summary>
        private const int StdInputHandle = -10;

        #endregion

        #region Windows API

        /// <summary>
        ///     Allocates a new console for the calling process.
        /// </summary>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        /// <summary>
        ///     Attaches the calling process to the console of the specified process as a client application.
        /// </summary>
        /// <param name="dwProcessId">The identifier of the process whose console is to be used.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AttachConsole(int dwProcessId);

        /// <summary>
        ///     Retrieves a handle to the specified standard device (standard input, standard output, or standard error).
        /// </summary>
        /// <param name="nStdHandle">The standard device.</param>
        /// <returns>If the function succeeds, the return value is a handle to the specified device.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern SafeFileHandle GetStdHandle(int nStdHandle);

        /// <summary>
        ///     Retrieves the current input mode of a console's input buffer or the current output mode of a console screen buffer.
        /// </summary>
        /// <param name="hConsoleHandle">A handle to the console input buffer or the console screen buffer.</param>
        /// <param name="lpMode">A pointer to a variable that receives the current mode of the specified buffer.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool GetConsoleMode(SafeFileHandle hConsoleHandle, out uint lpMode);

        /// <summary>
        ///     Sets the input mode of a console's input buffer or the output mode of a console screen buffer.
        /// </summary>
        /// <param name="hConsoleHandle">A handle to the console input buffer or a console screen buffer.</param>
        /// <param name="dwMode">The input or output mode to be set.</param>
        /// <returns>If the function succeeds, the return value is nonzero.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleMode(SafeFileHandle hConsoleHandle, uint dwMode);

        #endregion
    }
}
