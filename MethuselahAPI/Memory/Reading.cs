using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MethuselahAPI.Memory
{
    internal class Reading
    {
        internal static byte[] Read<T>(UInt64 address, Process process)
        {
            if (process == null)
            {
                throw new ArgumentNullException(nameof(process), "Process cannot be null");
            }

            IntPtr processHandle = MethuselahAPI.Natives.Methods.OpenProcess(0x0010, false, process.Id);
            byte[] buffer = new byte[Marshal.SizeOf<T>()];
            IntPtr bytesRead;

            if (!MethuselahAPI.Natives.Methods.ReadProcessMemory(processHandle, address, buffer, buffer.Length, out bytesRead))
            {
                throw new InvalidOperationException("Failed to read process memory");
            }

            MethuselahAPI.Natives.Methods.CloseHandle(processHandle);
            return buffer;
        }

        internal static UInt64 ReadPointer(UInt64 baseAddress, UInt64[] offsets, Process process)
        {
            if (process == null)
            {
                throw new ArgumentNullException(nameof(process), "Process cannot be null");
            }

            foreach (var offset in offsets.Take(offsets.Length - 1))
            {
                var buffer = Read<UInt64>(baseAddress + offset, process);
                baseAddress = BitConverter.ToUInt64(buffer, 0);
            }
            Console.WriteLine(baseAddress + offsets.Last());
            return baseAddress + offsets.Last();
        }

        internal static string ReadString(ulong address, Process process)
        {
            if (process == null)
            {
                throw new ArgumentNullException(nameof(process), "Process cannot be null");
            }

            var stringByteBuffer = new List<byte>();

            while (true)
            {
                byte[] buffer = Read<byte>(address + (ulong)stringByteBuffer.Count, process);
                if (buffer[0] == 0) break;
                stringByteBuffer.Add(buffer[0]);
            }

            return Encoding.UTF8.GetString(stringByteBuffer.ToArray());
        }
    }
}