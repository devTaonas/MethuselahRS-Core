using System;
using static MethuselahRS.Memory.Reading.Global;

namespace MethuselahRS.DoAction
{
    internal class DoActionGlobal
    {
        internal static void SetHandle(int gameHandle)
        {
            var addr = GetProcAddress(Form1.lib, "SetHandle");
            uint processId = (uint)gameHandle; // Cast to uint
            Form1.rs2clientProcess.Call(addr, processId);
        }
        internal static void SetCompass2(ulong Compass2)
        {
            var addr = GetProcAddress(Form1.lib, "SetCompass2");
            Form1.rs2clientProcess.Call(addr, (IntPtr)Compass2);
        }
    }
}
