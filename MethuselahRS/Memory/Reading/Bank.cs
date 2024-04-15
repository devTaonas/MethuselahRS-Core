using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    internal class Bank
    {
        public static bool IsBankOpen()//BankOpen2
        {
            InterfaceComp5 interf0 = Interfaces.GetIbystring("Bank1");
            List<InterfaceComp5> interf = new List<InterfaceComp5>();
            interf.Add(interf0);
            List<IInfo> aim = Interfaces.ScanForInterfaceTest2Get(false, interf);
            if (aim != null)
            {
                if (aim.Count > 0)
                {
                    int blah = BitConverter.ToUInt16(MemoryReading.Read<int>(aim[0].memloc + Interfaces.I_x4), 0);
                    if (blah > 0)
                    {
                        Console.WriteLine("Bank is open");
                        return true;
                    }
                }
            }
            return false;
        }
        public static Tuple<List<IInfo>, List<IInfo>> BankWindowData(bool Get_bank = true, bool Get_inv = true)
        {
            if (Get_bank)
            {
                Interfaces.BankWArr2.Clear();
                List<IInfo> Inter0_dynamic = Interfaces.GetInterfaceByIdsUnder(
                    new List<InterfaceComp5>
                    {
                        new InterfaceComp5(517, 0, 65535, 65535, 0),
                        new InterfaceComp5(517, 2, 65535, 0, 0),
                        new InterfaceComp5(517, 146, 65535, 2, 0),
                        new InterfaceComp5(517, 147, 65535, 146, 0),
                        new InterfaceComp5(517, 187, 65535, 147, 0),
                        new InterfaceComp5(517, 193, 65535, 187, 0),
                        new InterfaceComp5(517, 195, 65535, 193, 0)
                    },
                    0,
                    true
                );
                if (Inter0_dynamic != null)
                {
                    if (Inter0_dynamic.Count() > 0)
                    {
                        for (int i = 0; i < Inter0_dynamic.Count(); i++)
                        {
                            Interfaces.BankWArr2.Add(new IInfo());
                            Interfaces.BankWArr2[i].memloc = Inter0_dynamic[i].memloc;
                            Interfaces.BankWArr2[i].textitem = Inter0_dynamic[i].textitem;
                            Interfaces.BankWArr2[i].itemid1 = Inter0_dynamic[i].itemid1;
                            Interfaces.BankWArr2[i].itemid1_size = Inter0_dynamic[i].itemid1_size;
                            Interfaces.BankWArr2[i].x = Inter0_dynamic[i].x + Inter0_dynamic[i].xs / 2;
                            Interfaces.BankWArr2[i].y = Inter0_dynamic[i].y + Inter0_dynamic[i].ys / 2;
                            Interfaces.BankWArr2[i].notvisible = Inter0_dynamic[i].notvisible;
                            Interfaces.BankWArr2[i].id1 = Inter0_dynamic[i].id1;
                            Interfaces.BankWArr2[i].id2 = Inter0_dynamic[i].id2;
                            Interfaces.BankWArr2[i].id3 = Inter0_dynamic[i].id3;
                            Interfaces.BankWArr2[i].id4 = Inter0_dynamic[i].id4;
                        }
                    }
                }
            }


            if (Get_inv)
            {
                Interfaces.BankWInv2.Clear();
                List<IInfo> Inter0_dynamic = Interfaces.ScanForInterfaceTest2Get(true, new List<InterfaceComp5>
                {
                    new InterfaceComp5(517, 0, 65535, 65535, 0),
                    new InterfaceComp5(517, 2, 65535, 0, 0),
                    new InterfaceComp5(517, 3, 65535, 2, 0),
                    new InterfaceComp5(517, 4, 65535, 3, 0),
                    new InterfaceComp5(517, 5, 65535, 4, 0),
                    new InterfaceComp5(517, 7, 65535, 5, 0),
                    new InterfaceComp5(517, 8, 65535, 7, 0),
                    new InterfaceComp5(517, 11, 65535, 8, 0),
                    new InterfaceComp5(517, 12, 65535, 11, 0),
                    new InterfaceComp5(517, 13, 65535, 12, 0),
                    new InterfaceComp5(517, 15, 65535, 13, 0)
                });
                if (Inter0_dynamic != null)
                {
                    if (Inter0_dynamic.Count() > 0)
                    {
                        for (int i = 0; i < Inter0_dynamic.Count(); i++)
                        {
                            Interfaces.BankWInv2.Add(new IInfo());
                            Interfaces.BankWInv2[i].memloc = Inter0_dynamic[i].memloc;
                            Interfaces.BankWInv2[i].id3 = Inter0_dynamic[i].id3;
                            Interfaces.BankWInv2[i].textitem = Inter0_dynamic[i].textitem;
                            Interfaces.BankWInv2[i].itemid1 = Inter0_dynamic[i].itemid1;
                            Interfaces.BankWInv2[i].itemid1_size = Inter0_dynamic[i].itemid1_size;
                            Interfaces.BankWInv2[i].x = Inter0_dynamic[i].x + Inter0_dynamic[i].xs / 2;
                            Interfaces.BankWInv2[i].y = Inter0_dynamic[i].y + Inter0_dynamic[i].ys / 2;
                            Interfaces.BankWInv2[i].notvisible = Inter0_dynamic[i].notvisible;
                            Interfaces.BankWInv2[i].id1 = Inter0_dynamic[i].id1;
                            Interfaces.BankWInv2[i].id2 = Inter0_dynamic[i].id2;
                            Interfaces.BankWInv2[i].id3 = Inter0_dynamic[i].id3;
                            Interfaces.BankWInv2[i].id4 = Inter0_dynamic[i].id4;
                        }
                    }
                }
            }
            return new Tuple<List<IInfo>, List<IInfo>>(Interfaces.BankWArr2, Interfaces.BankWInv2);
        }


    }
}
