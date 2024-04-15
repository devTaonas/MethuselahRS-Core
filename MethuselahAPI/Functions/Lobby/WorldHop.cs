using MethuselahAPI.Logger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahAPI.Functions.Lobby
{
    public class WorldHop
    {

        public static void UpdateWorld(Process process, int world)
        {
            WriteLog.Critical(">>>>>>>>>>>>>>>>>>> Executing API Function <<<<<<<<<<<<<<<<<<<" + Environment.NewLine);
            WriteLog.Action("[ ############### Begin UpdateWorld to " + world + " on process " + process.Id + " ]");
            if (process == null)
            {
                throw new ArgumentNullException(nameof(process), "Process cannot be null");
            }

            UInt64 address = ReadWorldBasePointer(process);
            IntPtr processHandle = OpenProcessWithFullAccess(process.Id);

            try
            {
                WriteIntToMemory(processHandle, address, world);
            }
            finally
            {
                CloseProcessHandle(processHandle);
            }
            WriteLog.Action("[ ############### End UpdateWorld method on process " + process.Id + " ############### ]");
        }

        private static UInt64 ReadWorldBasePointer(Process process)
        {
            var offsets = new ulong[] { (ulong)Memory.Statics.Addresses.WorldBase, 0X38, 0X960, 0X0, 0X20, 0X8 };
            WriteLog.Information("[ ReadWorldBasePointer Method Start");
            WriteLog.Debug("[  - Process base address: " + (ulong)process.MainModule.BaseAddress);
            int offsetNumber = 1;
            foreach (var x in offsets)
            {
                WriteLog.Debug("[  - Offset " + offsetNumber + ": " + x);
                offsetNumber++;
            }
            WriteLog.Information("[ ReadWorldBasePointer Method End" + Environment.NewLine + "[ ---------------------------------------------------------------------------------------");
            return Memory.Reading.ReadPointer((ulong)process.MainModule.BaseAddress, offsets, process);
        }

        private static IntPtr OpenProcessWithFullAccess(int processId)
        {
            WriteLog.Information("[ OpenProcessWithFullAccess Method Start");
            const int ProcessAllAccess = 0x1F0FFF;
            IntPtr processHandle = MethuselahAPI.Natives.Methods.OpenProcess(ProcessAllAccess, false, processId);
            WriteLog.Debug("[ Process Handle: " + processHandle);
            if (processHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Unable to open process with full access");
            }
            WriteLog.Information("[ ReadWorldBasePointer Method End" + Environment.NewLine + "[ ---------------------------------------------------------------------------------------");
            return processHandle;
        }

        private static void WriteIntToMemory(IntPtr processHandle, UInt64 address, int value)
        {
            WriteLog.Information("[ WriteIntToMemory Method Start");
            byte[] bytes = BitConverter.GetBytes(value);
            int byteNumber = 1;
            foreach (var x in bytes)
            {
                WriteLog.Debug("[  - Offset " + byteNumber + ": " + x);
            }
            int bytesWritten = 0;
            bool success = MethuselahAPI.Natives.Methods.WriteProcessMemory((int)processHandle, address, bytes, bytes.Length, ref bytesWritten);
            WriteLog.Debug("[  - Written Successfully: " + success);
            if (!success || bytesWritten != bytes.Length)
            {
                throw new InvalidOperationException("Failed to write to process memory");
            }
            WriteLog.Information("[ WriteIntToMemory Method End" + Environment.NewLine + "[ ---------------------------------------------------------------------------------------");
        }

        private static void CloseProcessHandle(IntPtr processHandle)
        {
            WriteLog.Information("[ CloseProcessHandle Method Start");
            MethuselahAPI.Natives.Methods.CloseHandle(processHandle);
            WriteLog.Information("[ WriteIntToMemory Method End" + Environment.NewLine + "[ ---------------------------------------------------------------------------------------");
        }
    }
}
