using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
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
        public int SkillsBase { get; private set; }
        public Pointers()
        {
            InitializePointers();
        }

        private void InitializePointers()
        {
            PlayerBase = 0xD44A18;          // not used any longer
            GameStatusBase = 0xD26198;      // not used any longer
            NPCBase = 0xD44EC0;             // not used any longer
            DGameObjectBase = 0xD44EC0;     // not used any longer
            AGameObjectBase = 0xD44EC0;     // not used any longer

            ScanForInterface = 0xD54500;
            //FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00
            // Sometimes it's only 1 result and that is the one you want
            // sometimes its 100 + but so far it's always been the very last address haha

            WorldBase = 0xD34928;
            //?? 00 00 00 00 00 00 00 77 6F 72 6C 64
            // Should receive 5 or below results and its the only one that changes when selecting a different world

            Compass22 = 0xD535E8;
            //C8 1F 23 E3 F6 7F 00 00 98 1F 23 E3 F6 7F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 20
            //Should only see two results it's the top result the second result is plus 0x6B00 (first start up not logged in)

            RefVarp444 = 0xD349A8;
            //64 69 72 65 63 74 6C 6F 67 69 6E 00 00 00 00//directlogin
            //Search for this AOB in CE and pointer scan will get you RefVarp444

            SkillsBase = 0xD34928;
        }
    }
}
