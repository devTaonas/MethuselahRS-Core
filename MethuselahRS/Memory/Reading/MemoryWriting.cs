using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    internal class MemoryWriting
    {
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess,
               bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(int hProcess, ulong lpBaseAddress,
          byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        public static void WriteMemory(Process process, int world)
        {
            Pointers pointers = new Pointers();
            UInt64 adr = MemoryReading.ReadPointer((ulong)process.MainModule.BaseAddress, new ulong[]
            {
                
                (ulong)pointers.WorldBase, 0X38, 0X960, 0X0, 0X20, 0X8
            }, process);
            IntPtr processHandle = OpenProcess(0x1F0FFF, false, process.Id);
            int bytesWritten = 0;
            byte[] bytes = BitConverter.GetBytes(world); // to write int
            WriteProcessMemory((int)processHandle, adr, bytes, bytes.Length, ref bytesWritten);
        }
    }
}
