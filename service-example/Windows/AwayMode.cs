using System.Runtime.InteropServices;

namespace ServiceExample.Windows
{
    /// <summary>
    ///     A helper class for enabling and disabling away-mode.
    /// </summary>
    public static class AwayMode
    {

        /// <summary>
        ///     Prevents the system from entering sleep or turning off the display
        /// </summary>
        /// <remarks>
        ///  Windows with energy 
        /// </remarks>
        public static ExecutionState Enable()
        {
            return SetThreadExecutionState(ExecutionState.AwayModeRequired | ExecutionState.SystemRequired | ExecutionState.Continuous);
        }

        /// <summary>
        ///     Allows the system from entering sleep or turning off the display
        /// </summary>
        public static ExecutionState Disable()
        {
            return SetThreadExecutionState(ExecutionState.Continuous);
        }

        #region Windows API
        /// <summary>
        ///     Enables an application to inform the system that it is in use, thereby preventing the system from entering sleep or
        ///     turning off the display while the application is running.
        /// </summary>
        /// <param name="esFlags">The thread's execution requirements.</param>
        /// <returns>If the function succeeds, the return value is the previous thread execution state.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum ExecutionState : uint
        {
            /// <summary>
            ///     Enables away mode. This value must be specified with <see cref="Continuous"/>.
            ///     <para>
            ///     Away mode should be used only by media-recording and media-distribution applications that must perform critical background processing on desktop computers while the computer appears to be sleeping.
            ///     </para>
            /// </summary>
            AwayModeRequired = 0x00000040,

            /// <summary>
            ///     Informs the system that the state being set should remain in effect until the next call that uses
            ///     <see cref="Continuous"/> and one of the other state flags is cleared.
            /// </summary>
            Continuous = 0x80000000,

            /// <summary>
            ///     Forces the display to be on by resetting the display idle timer.
            /// </summary>
            DisplayRequired = 0x00000002,

            /// <summary>
            ///     Forces the system to be in the working state by resetting the system idle timer.
            /// </summary>
            SystemRequired = 0x00000001
        }

        #endregion
    }
}
