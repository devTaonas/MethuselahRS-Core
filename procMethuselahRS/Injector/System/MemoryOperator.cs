using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace procMethuselahRS.Injector.System
{
    public class MemoryOperator : IMemoryOperator
    {

        public IntPtr AllocateMemory(IntPtr processHandle, string dllPath)
        {
            const uint AllocationType = 0x1000 | 0x2000;
            const uint Protect = 0x40;
            IntPtr memoryAddress = Binder.Natives.Methods.VirtualAllocEx(processHandle, IntPtr.Zero, (IntPtr)dllPath.Length, AllocationType, Protect);
            if (memoryAddress == IntPtr.Zero)
                throw new InvalidOperationException("Failed to allocate memory.");

            return memoryAddress;
        }

        public bool WriteMemory(IntPtr processHandle, IntPtr address, byte[] data)
        {
            return Binder.Natives.Methods.WriteProcessMemory(processHandle, address, data, (uint)data.Length, out _) != 0;
        }

        public IntPtr CreateRemoteThread(IntPtr processHandle, IntPtr loadLibraryAddress, IntPtr address)
        {
            IntPtr threadHandle = Binder.Natives.Methods.CreateRemoteThread(processHandle, IntPtr.Zero, IntPtr.Zero, loadLibraryAddress, address, 0, IntPtr.Zero);
            if (threadHandle == IntPtr.Zero)
                throw new InvalidOperationException("Failed to create remote thread.");

            return threadHandle;
        }
    }
}
