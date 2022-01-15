using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceExample.Windows
{
    /// <summary>
    /// A convenient wrapper for errhandlingapi.h
    /// </summary>
    public static class ErrorHandling
    {
        /// <summary>
        ///     Best practice is that all applications call this at startup. This is to prevent error mode dialogs from hanging the application in the event of an unexpected error / crash.
        /// </summary>
        public static ErrorMode DisableErrorPrompts()
        {
            return SetErrorMode(SetErrorMode(ErrorMode.SystemDefault) | ErrorMode.NoGPFaultErrorBox | ErrorMode.FailCriticalErrors | ErrorMode.NoOpenFileErrorBox);
        }

        #region Windows API

        [Flags]
        public enum ErrorMode : uint
        {
            /// <summary>
            ///     Use the system default, which is to display all error dialog boxes.
            /// </summary>
            SystemDefault = 0x0,

            /// <summary>
            ///     The system does not display the critical-error-handler message box. Instead, the system sends the error to the
            ///     calling process.
            /// </summary>
            FailCriticalErrors = 0x0001,

            /// <summary>
            ///     The system does not display the Windows Error Reporting dialog.
            /// </summary>
            NoGPFaultErrorBox = 0x0002,

            /// <summary>
            ///     The system automatically fixes memory alignment faults and makes them invisible to the application. It does this
            ///     for the calling process and any descendant processes. This feature is only supported by certain processor
            ///     architectures.
            /// </summary>
            NoAlignmentFaultExcept = 0x0004,

            /// <summary>
            ///     The OpenFile function does not display a message box when it fails to find a file. Instead, the error is returned
            ///     to the caller. This error mode overrides the OF_PROMPT flag.
            /// </summary>
            NoOpenFileErrorBox = 0x8000
        }

        [DllImport("kernel32.dll")]
        private static extern ErrorMode SetErrorMode(ErrorMode mode);

        #endregion
    }
}
