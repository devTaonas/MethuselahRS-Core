using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procMethuselahRS.Memory.Reading
{
    internal class Pointers
    {
        public int PlayerBase { get; private set; }
        public int GameStatusBase { get; private set; }
        public int WorldBase { get; private set; }
        public int NPCBase { get; private set; }
        public int DGameObjectBase { get; private set; }
        public int AGameObjectBase { get; private set; }
        public int RefVarp444 { get ; private set; }
        public int ScanForInterface { get;private set; }
        public int Compass22 { get; private set; }
        public Pointers()
        {
            InitializePointers();
        }

        private void InitializePointers()
        {
            PlayerBase = 0xD44A18;
            GameStatusBase = 0xD26198;
            WorldBase = 0xD26198;
            NPCBase = 0xD44EC0;
            DGameObjectBase = 0xD44EC0;
            AGameObjectBase = 0xD44EC0;
            RefVarp444 = 0xD26198;
            ScanForInterface = 0x0; // not sure about this one yet
            Compass22 = 0x0;        // not sure about this one yet
        }
    }
}
