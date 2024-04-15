using MinHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    public class HookExample
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }
        [Flags]
        public enum AllocationType : uint
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection : uint
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hhk);
        public static bool UnHookRs3Hook()
        {
            var rsexestart = TheMess.RSExeStart;
            Process p = Process.GetProcessesByName("rs2client")[0];
            var maybeaddress = p.MainModule.BaseAddress + 0x6a0ac0;

            ulong baseaddress = (ulong)maybeaddress;
            ulong instruction_size = 0x7;
            ulong instruction_start = baseaddress + 0x7b;
            var instruction_address = BitConverter.ToInt32(MemoryReading.Read<int>(instruction_start + 0x3), 0);
            ulong Final_Unhook = instruction_size + instruction_start + (ulong)instruction_address;
            ulong hook = Interfaces.ReadUINT64(Final_Unhook);
            bool unhooked = UnhookWindowsHookEx((IntPtr)hook);
            return unhooked;
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flNewProtect, out uint lpflOldProtect);

        const uint PROCESS_ALL_ACCESS = 0x1F0FFF;
        const uint PAGE_EXECUTE_READWRITE = 0x40;

        public static void WriteJmp(IntPtr processHandle, IntPtr targetAddress, IntPtr newAddress)
        {
            long offset = newAddress.ToInt64() - targetAddress.ToInt64() - 5;


            // Check if offset is within the range of a 32-bit signed integer
            if (offset > int.MaxValue || offset < int.MinValue)
            {
                throw new InvalidOperationException("Offset is too large for a 32-bit relative jump.");
            }

            // Convert offset to a 32-bit integer
            int offset32 = (int)offset;

            // JMP instruction (0xE9) followed by the offset
            byte[] jmpInstruction = new byte[] { 0xE9, (byte)(offset32 & 0xFF), (byte)((offset32 >> 8) & 0xFF),
                (byte)((offset32 >> 16) & 0xFF), (byte)((offset32 >> 24) & 0xFF) };


            VirtualProtectEx(processHandle, targetAddress, (uint)jmpInstruction.Length, PAGE_EXECUTE_READWRITE, out uint oldProtect);

            WriteProcessMemory(processHandle, targetAddress, jmpInstruction, (uint)jmpInstruction.Length, out int bytesWritten);

            VirtualProtectEx(processHandle, targetAddress, (uint)jmpInstruction.Length, oldProtect, out _);
        }

        public static void HookFrameTickEngine()
        {
            Process p = Process.GetProcessesByName("rs2client")[0];
            IntPtr targetAddress = p.MainModule.BaseAddress + 0xDB970;

            IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, p.Id);

            if (processHandle != IntPtr.Zero)
            {
                // code cave new function and get address to insert into new location (0x7ffc597e0fc7)
                WriteJmp(processHandle, targetAddress, new IntPtr(0X7FF72AFB0FD3)); // places a jump to the new address
            }
            else
            {
                Console.WriteLine("Failed to open process");
            }
        }
        public static void tick_detour()
        {
            Get_D_A_Q();
        }

        public static List<PrepFunctions> prepFunctions = new List<PrepFunctions>(5);
        public static void Get_D_A_Q()
        {
            foreach (PrepFunctions P in prepFunctions)
            {
                if (P.Done)
                {
                    if (P.Func != null)
                    {
                        P.Func();
                        Thread.Sleep(1); 
                        P.Func = null;
                        P.Done = false;
                        break; 
                    }
                }
            }
        }

        internal static void OpenTheProcess()
        {
            Process p = Process.GetProcessesByName("rs2client")[0];
            IntPtr processHandle = OpenProcess(ProcessAccessFlags.VirtualMemoryRead | ProcessAccessFlags.VirtualMemoryWrite | ProcessAccessFlags.VirtualMemoryOperation, false, p.Id);

            if (processHandle == IntPtr.Zero)
            {
                int errorCode = Marshal.GetLastWin32Error();
                Console.WriteLine($"Failed to open process. Error code: {errorCode}");
            }
            else
            {
                Console.WriteLine("Process opened successfully.");
                CloseHandle(processHandle);
            }
        }

        internal static IntPtr AllocateTheMemory(byte[] codeBytes)
        {
            Process p = Process.GetProcessesByName("rs2client").FirstOrDefault();
            if (p == null)
            {
                Console.WriteLine("Process not found.");
                return IntPtr.Zero;
            }

            IntPtr processHandle = OpenProcess(ProcessAccessFlags.All, false, p.Id);
            if (processHandle == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open process.");
                return IntPtr.Zero;
            }

            IntPtr remoteMemoryAddress = VirtualAllocEx(processHandle, IntPtr.Zero, (uint)codeBytes.Length, AllocationType.Commit | AllocationType.Reserve, MemoryProtection.ExecuteReadWrite);
            if (remoteMemoryAddress == IntPtr.Zero)
            {
                Console.WriteLine("Failed to allocate memory.");
            }
            else
            {
                Console.WriteLine($"Memory allocated at: {remoteMemoryAddress.ToString("X")}");
            }

            return remoteMemoryAddress;
        }

        internal static void WriteCodeToMemory(IntPtr remoteMemoryAddress, byte[] codeBytes)
        {
            Process p = Process.GetProcessesByName("rs2client").FirstOrDefault();
            if (p == null)
            {
                Console.WriteLine("Process not found.");
                return;
            }

            IntPtr procHandle = OpenProcess(0x001F0FFF, false, p.Id);
            if (procHandle == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open process.");
                return;
            }

            bool result = WriteProcessMemory(procHandle, remoteMemoryAddress, codeBytes, (uint)codeBytes.Length, out int bytesWritten);
            if (!result)
            {
                Console.WriteLine("Failed to write to process memory.");
            }
            else
            {
                Console.WriteLine("Memory write successful. Bytes written: " + bytesWritten);
            }

            CloseHandle(procHandle);
        }
    }
}
public class PrepFunctions
{
    public Action Func { get; set; } = null;
    public bool Done { get; set; } = false;
}
