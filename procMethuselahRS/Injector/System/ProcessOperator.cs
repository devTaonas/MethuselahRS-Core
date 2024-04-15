using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace procMethuselahRS.Injector.System
{
    public class ProcessOperator : IProcessOperator
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int CloseHandle(IntPtr hObject);

        public IntPtr OpenProcess(Process proc)
        {
            const uint DesiredAccess = 0x2 | 0x8 | 0x10 | 0x20 | 0x400;
            IntPtr handle = OpenProcess(DesiredAccess, 1, (uint)proc.Id);
            if (handle == IntPtr.Zero)
                throw new InvalidOperationException("Failed to open process.");

            return handle;
        }

        public bool CloseHandleMain(IntPtr handle)
        {
            return CloseHandle(handle) != 0;
        }
    }
}
