using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace procMethuselahRS.Memory.Reading
{
    internal class ScanAllObjs
    {
        internal static ulong bll;
        internal static SYSTEM_INFO system_info;
        internal static MEMORY_BASIC_INFORMATION mbi;
        internal ScanAllObjs()
        {
            system_info = new SYSTEM_INFO();
            mbi = new MEMORY_BASIC_INFORMATION();
        }
        internal static MEMORY_BASIC_INFORMATION ShowMemory()
        {
            ScanAllObjects();
            return mbi;
        }
        internal static async Task ScanAllObjects(Process process = null)
        {
            if(process == null) { process = Form1.ListRuneScapeProcesses[0]; }
            ulong currentAddress = 0x10000;
            ulong maxAddress = 0x7FFFFFFEFFFF;
            var MEM_COMMIT = 0x00001000;
            var PAGE_NOACCESS = 0x01;
            ulong totalBytes = 0;
            MEMORY_BASIC_INFORMATION MI;
            bool debug = false;
            ulong size_aim = 0x78e000;
            for (ulong memptr = currentAddress; memptr < maxAddress; memptr = (ulong)mbi.BaseAddress + (ulong)mbi.RegionSize)
            {
                VirtualQueryEx(process.Handle, (IntPtr)memptr, out mbi, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION)));
                if (mbi.State != MEM_COMMIT || mbi.Protect == PAGE_NOACCESS)
                    continue;
                if (!(mbi.Type == 0x20000 || mbi.Type == 0x1000000))
                    continue;
                // Filter regions
                if (!(mbi.Protect == 0x4 || mbi.Protect == 0x20))
                    continue;
                if ((ulong)mbi.RegionSize == size_aim)
                {
                    string bl = mbi.BaseAddress.ToString();
                    bll = ulong.Parse(bl);
                    Console.WriteLine("AllocationBase: {0:X}", ulong.Parse(mbi.AllocationBase.ToString()));
                    Console.WriteLine("AllocationProtect:" + mbi.AllocationProtect);
                    Console.WriteLine("BaseAddress: {0:X}", ulong.Parse(mbi.BaseAddress.ToString()));
                    Console.WriteLine("Protect:" + mbi.Protect);
                    Console.WriteLine("New BaseAddress: {0:X}", ulong.Parse((mbi.BaseAddress + 0x10).ToString()));
                    Console.WriteLine("RegionSize:" + mbi.RegionSize.ToString("X"));
                    Console.WriteLine("State:" + mbi.State.ToString("X"));
                    Console.WriteLine("Type: " + mbi.Type.ToString("X"));
                    break;
                }
            }

            //    Console.WriteLine("Lets verify that we have the correct address");
            ulong address = (bll + 0x10 + 0x2c);
            int ips = BitConverter.ToInt32(MemoryReading.Read<int>(address), 0);
            //       Console.WriteLine("BaseAddress + 2c should equal 67108864 data from rs = " + ips.ToString());
            if (ips == 67108864)
            {
                //             Console.WriteLine("All possible matches found.");
            }
            TheMess.ScAdd1 = (long)bll + 0x10;
            Console.WriteLine("All Obj ScAdd1: {0:X}", ulong.Parse(TheMess.ScAdd1.ToString()));

            TheMess.ScAdd1End = BitConverter.ToInt64(MemoryReading.Read<long>(bll), 0);
            Console.WriteLine("All Obj MEGlobalVariable.ScAdd1End: {0:X}", ulong.Parse(TheMess.ScAdd1End.ToString()));
            var alloff2 = 528;
            var calcPlaceholder = (TheMess.ScAdd1End - TheMess.ScAdd1 - 0x10000) / alloff2;
            Console.WriteLine("AllObj chunks to read: " + calcPlaceholder);

        }
        [DllImport("kernel32.dll")]
        private static extern int VirtualQuery(ref uint lpAddress,
            ref MEMORY_BASIC_INFORMATION lpBuffer,
            int dwLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("kernel32.dll")]
        private static extern void GetSystemInfo(
            [MarshalAs(UnmanagedType.Struct)] ref SYSTEM_INFO lpSystemInfo);

        [StructLayout(LayoutKind.Sequential)]
        internal struct MEMORY_BASIC_INFORMATION
        {
            public UIntPtr BaseAddress;
            public UIntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct SYSTEM_INFO
        {
            internal PROCESSOR_INFO_UNION p;
            public uint dwPageSize;
            public IntPtr lpMinimumApplicationAddress;
            public IntPtr lpMaximumApplicationAddress;
            public UIntPtr dwActiveProcessorMask;
            public uint dwNumberOfProcessors;
            public uint dwProcessorType;
            public uint dwAllocationGranularity;
            public ushort wProcessorLevel;
            public ushort wProcessorRevision;
        };

        [StructLayout(LayoutKind.Explicit)]
        internal struct PROCESSOR_INFO_UNION
        {
            [FieldOffset(0)] internal uint dwOemId;
            [FieldOffset(0)] internal ushort wProcessorArchitecture;
            [FieldOffset(2)] internal ushort wReserved;
        }
    }
}
