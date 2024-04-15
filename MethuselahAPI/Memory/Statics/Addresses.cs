using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahAPI.Memory.Statics
{
    internal class Addresses
    {
        public static int PlayerBase { get; private set; } = 0xD44A18;
        public static int GameStatusBase { get; private set; } = 0xD26198;
        public static int WorldBase { get; private set; } = 0xD26198;
        public static int NPCBase { get; private set; } = 0xD44EC0;
        public static int DGameObjectBase { get; private set; } = 0xD44EC0;
        public static int AGameObjectBase { get; private set; } = 0xD44EC0;
        public static int RefVarp444 { get; private set; } = 0xD26198;
        public static int ScanForInterface { get; private set; } = 0x0;
        public static int Compass22 { get; private set; } = 0xD47978;
    }
}
