using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    internal class MemoryReading
    {
        [DllImport("Kernel32.dll")]
        internal static extern int GetLastError();

        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        // Windows API Functions
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);


        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool ReadProcessMemory(
            IntPtr hProcess,
            UInt64 lpBaseAddress,
            byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);
        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        internal static byte[] Read<T>(UInt64 adress, Process process = null)
        {
            if(process == null) { process = Form1.ListRuneScapeProcesses[0]; }
            IntPtr processHandle = OpenProcess(0x0010, false, process.Id);
            byte[] buffer = new byte[Marshal.SizeOf<T>()];
            IntPtr bytesRead = IntPtr.Zero;
            try
            {
                if (ReadProcessMemory(processHandle, adress, buffer, buffer.Length, out bytesRead) == false)
                {
                }
                CloseHandle(processHandle);

                return buffer;
            }
            catch { Console.WriteLine("Issue with ReadProcessMemory Read"); Console.WriteLine(new System.Diagnostics.StackTrace() + "\r\n\r\n"); }
            return null;
        }
        internal static UInt64 ReadPointer(UInt64 baseAddres, UInt64[] offsets, Process process = null)
        {
            if(process == null) { process = Process.GetProcessesByName("rs2client")[0]; }
            try
            {
                byte[] f;
                for (int i = 0; i <= offsets.Length - 2; i++)
                {
                    f = Read<UInt64>(baseAddres + offsets[i], process);
                    baseAddres = BitConverter.ToUInt64(f, 0);
                }
                return baseAddres + offsets[offsets.Length - 1];
            }
            catch { Console.WriteLine("Issue with Pointer Read"); Console.WriteLine(new System.Diagnostics.StackTrace() + "\r\n\r\n"); }
            return 0;
        }

        internal static string ReadString(ulong Addres, Process process = null)
        {
            if(process == null) { process = Form1.ListRuneScapeProcesses[0]; }
            try
            {
                int count = 0;
                for (ulong i = 0; Convert.ToChar(Read<char>(Addres + i, process)[0]) != '\0'; i++)
                {
                    count += 1;
                }

                byte[] stringByteBuffer = new byte[count];
                for (ulong i = 0; i < (ulong)stringByteBuffer.Length; i++)
                {
                    stringByteBuffer[i] = Read<byte>(Addres + i, process)[0];
                }
                string returnstring = Encoding.UTF8.GetString(stringByteBuffer);
                return returnstring;
            }
            catch { Console.WriteLine("Issue with String Read"); Console.WriteLine(new System.Diagnostics.StackTrace() + "\r\n\r\n"); }
            return null;
        }



        // Permissions
        const uint PROCESS_VM_READ = 0x0010;
        const uint PROCESS_QUERY_INFORMATION = 0x0400;

        public static IntPtr SearchMemory(int processId, byte[] searchBytes)
        {
            IntPtr processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_QUERY_INFORMATION, false, processId);

            if (processHandle == IntPtr.Zero)
                return IntPtr.Zero;

            // Assuming you are searching in a reasonable memory range
            IntPtr startAddress = new IntPtr(0x00000000);
            IntPtr endAddress = new IntPtr(0x7fffffff);

            byte[] buffer = new byte[4096]; // Adjust the size as needed
            int bytesRead;

            for (IntPtr address = startAddress; address.ToInt64() < endAddress.ToInt64(); address = new IntPtr(address.ToInt64() + buffer.Length))
            {
                if (ReadProcessMemory(processHandle, address, buffer, buffer.Length, out bytesRead))
                {
                    int index = IndexOfSequence(buffer, searchBytes, bytesRead);
                    if (index != -1)
                    {
                        CloseHandle(processHandle);
                        return new IntPtr(address.ToInt64() + index);
                    }
                }
            }

            CloseHandle(processHandle);
            return IntPtr.Zero;
        }

        private static int IndexOfSequence(byte[] buffer, byte[] pattern, int bytesRead)
        {
            for (int i = 0; i < bytesRead - pattern.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (buffer[i + j] != pattern[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                    return i;
            }
            return -1;
        }
    }
}
