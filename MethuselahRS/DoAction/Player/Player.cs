using System;
using static MethuselahRS.Memory.Reading.Global;

namespace MethuselahRS.DoAction.Player
{
    public class Player
    {

        internal static void SetLocalPlayerAddress(ulong localplayeraddress)
        {
            var addr = GetProcAddress(Form1.lib, "SetLocalPlayerAddress");
            Form1.rs2clientProcess.Call(addr, (IntPtr)localplayeraddress);
        }
    }
}
