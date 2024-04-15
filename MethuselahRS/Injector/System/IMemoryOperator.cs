using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MethuselahRS.Injector.System
{
    public interface IMemoryOperator
    {
        IntPtr AllocateMemory(IntPtr processHandle, string dllPath);
        bool WriteMemory(IntPtr processHandle, IntPtr address, byte[] data);
        IntPtr CreateRemoteThread(IntPtr processHandle, IntPtr loadLibraryAddress, IntPtr address);
    }
}
