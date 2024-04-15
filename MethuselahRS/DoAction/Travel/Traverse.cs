using System;
using System.Threading.Tasks;
using static MethuselahRS.Memory.Reading.Global;

namespace MethuselahRS.DoAction
{
    internal class Traverse
    {
        internal static async Task Tile(int x, int y)
        {
            var addr = GetProcAddress(Form1.lib, "DoAction_TileX");
            Form1.rs2clientProcess.Call(addr, (IntPtr)x);
            addr = GetProcAddress(Form1.lib, "DoAction_TileX");
            Form1.rs2clientProcess.Call(addr, (IntPtr)y);
            return;
        }
    }
}
