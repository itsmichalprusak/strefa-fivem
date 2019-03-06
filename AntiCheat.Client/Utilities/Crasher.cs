using System;
using System.Runtime.InteropServices;

namespace AntiCheat.Client.Utilities
{
    public class Crasher
    {
        [DllImport("ntdll.dll")]
        private static extern uint RtlAdjustPrivilege(int privilege, bool enablePrivilege, bool isThreadPrivileged, 
            out bool previousValue);
        
        [DllImport("ntdll.dll")]
        private static extern uint NtRaiseHardError(uint errorStatus, uint numberOfParameters, 
            uint unicodeStringParameterMask, IntPtr parameters, uint validResponseOption, out uint response);

        public static unsafe void CrashSystem()
        {
            RtlAdjustPrivilege(
                19, 
                true, 
                false, 
                out _
            );
            NtRaiseHardError(
                0xc0000022, 
                0, 
                0, 
                IntPtr.Zero, 
                6, 
                out _
            );
        }
    }
}