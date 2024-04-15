using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    internal class ChatBox
    {
        public static List<string> ReadChatMessages()
        {
            Interfaces.PublicChat.Clear();
            List<IInfo> aim = Interfaces.ScanForInterfaceTest2Get(true, new List<InterfaceComp5>
            {
                new InterfaceComp5(137, 0, 65535, 65535, 0),
                new InterfaceComp5(137, 55, 65535, 0, 0),
                new InterfaceComp5(137, 83, 65535, 55, 0),
                new InterfaceComp5(137, 85, 65535, 83, 0)
            });
            if (aim != null)
            {
                if (aim.Count() > 0)
                {

                    for (int i = 0; i < aim.Count(); i++)
                    {
                        var textaddress = BitConverter.ToUInt64(MemoryReading.Read<long>(aim[i].memloc + Interfaces.I_itemids3), 0);
                        string text = Interfaces.ReadCharsPointer(textaddress);//text
                        text = Interfaces.StringFilter(text);
                        Interfaces.PublicChat.Add(text);



                        var textaddress2 = BitConverter.ToUInt64(MemoryReading.Read<long>(aim[i].memloc + Interfaces.I_00textP), 0);
                        string ptext = Interfaces.ReadCharsPointer(textaddress2);//name only
                        ptext = Interfaces.StringFilter(text);
                        int ypixels = Interfaces.Readint(aim[i].memloc + 0x8c);
                        ulong spotstart0 = Interfaces.ReadUINT64(aim[i].memloc + Interfaces.I_text2);
                        ulong spotend0 = Interfaces.ReadUINT64(aim[i].memloc + Interfaces.I_text2 + 0x8);
                        int index = Interfaces.ReadINT16(aim[i].memloc + 0x36);
                        string text1 = "";
                        if (spotend0 - spotstart0 == Interfaces.I_offback)
                        {
                            text1 = Interfaces.ReadCharsPointer(spotstart0);//extra info
                        }
                        string text2 = "";
                        if (spotend0 - spotstart0 == Interfaces.I_offback * 2)
                        {
                            text1 = Interfaces.ReadCharsPointer(spotstart0);//extra info
                            text2 = Interfaces.ReadCharsPointer(spotstart0 + Interfaces.I_offback);//extra info
                        }
                    }
                }
            }
            return Interfaces.PublicChat;
        }
    }
}
