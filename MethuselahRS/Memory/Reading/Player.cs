using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    internal class Player
    {
        public string Name { get; set; }
        public Point WorldCoords { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int Prayer { get; set; }
        public int MaxPrayer { get; set; }

        public string GetPlayerName(Process process = null)
        {
            //if(process == null) { process = Form1.ListRuneScapeProcesses[0]; }

            //Pointers pointers = new Pointers();
            //UInt64 adr = MemoryReading.ReadPointer((ulong)process.MainModule.BaseAddress, new ulong[]
            //{ (ulong)pointers.PlayerBase, 0X0, 0X188 });                // SCREEN PIXEL

            return MemoryReading.ReadString(TheMess.LocalPlayer + 0x148);
        }
        public async Task<(float x, float y)> GetPlayerCoords()
        {
            float x = 0f, y = 0f;
            if (await TheMess.PlayerLoggedIn())
            {
                x = BitConverter.ToSingle(MemoryReading.Read<float>(TheMess.LocalPlayer + TheMess.npcoff_X_tile), 0);
                y = BitConverter.ToSingle(MemoryReading.Read<float>(TheMess.LocalPlayer + TheMess.npcoff_Y_tile), 0);
            }
            return (x / 512, y / 512);
        }
        public async Task<(int, int)> GetHP()
        {
            int placeh = 659;
            if (await TheMess.PlayerLoggedIn())
            {
                if (TheMess.HPBit == 0)
                {
                    settings value = await TheMess.FindPSett(placeh);
                    if (value == null) { return (0, 0); }
                    TheMess.HPBit = value.SettingsAddr + 0x38;
                    var balh = (value.SettingsAddr & 0xffff) / 2;
                    var hp = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.HPBit), 0) / 2;
                    var maxhp = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.HPBit), 0);
                    return (hp, maxhp);
                }
                else
                {
                    var hp = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.HPBit), 0) / 2;
                    var maxhp = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.HPBit + 0x2), 0);
                    return (hp, maxhp);
                }
            }
            return (0, 0);
        }
        internal async Task<(int, int)> GetPray()
        {
            int placeh = 3274;
            if (await TheMess.PlayerLoggedIn())
            {
                if (TheMess.PrayBit == 0)
                {
                    settings value = await TheMess.FindPSett(placeh);
                    if (value == null) { return (0, 0); }
                    TheMess.PrayBit = value.SettingsAddr;
                    var pray = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.PrayBit + 0x8), 0);
                    var maxpray = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.PrayBit + 0xA), 0);
                    return (pray, maxpray);
                }
                else
                {
                    var pray = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.PrayBit + 0x8), 0) / 10;
                    var maxpray = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.PrayBit + 0xA), 0) * 10;
                    return (pray, maxpray);
                }
            }
            return (0, 0);
        }
        internal async Task<int> GetSlayerCount()
        {
            int placeh = 183;
            if (await TheMess.PlayerLoggedIn())
            {
                if (TheMess.SlayerCountBit == 0)
                {
                    settings value = await TheMess.FindPSett(placeh);
                    if (value == null) { return 0; }
                    TheMess.SlayerCountBit = value.SettingsAddr;
                    var slay = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.SlayerCountBit + 0x38), 0);
                    return slay;
                }
                else
                {
                    var slay = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.SlayerCountBit + 0x38), 0);
                    return slay;
                }
            }
            else
            {
                TheMess.SlayerCountBit = 0;
            }
            return 0;
        }
        public static int olvalue = 1;
        public static int curvalue = 1;
        public static int olvalueMax = 1;
        public static int curvalueMax = 1;
        internal async Task<(int, int)> GetSumm()
        {
            int placeh = 8040;

            if (await TheMess.PlayerLoggedIn())
            {
                if (olvalue != curvalue)
                {
                    settings value = await TheMess.FindPSett(placeh);
                    if (value == null) { return (0,0); }
                    TheMess.SummBit = value.SettingsAddr;
                }
                if (TheMess.SummBit == 0)
                {
                    settings value = await TheMess.FindPSett(placeh);
                    if (value == null) { return (0,0); }
                    TheMess.SummBit = value.SettingsAddr;
                    olvalue = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.SummBit + 0x8), 0);
                }
                else
                {
                    var value = BitConverter.ToInt32(MemoryReading.Read<int>(TheMess.SummBit + 0x8), 0);
                    var maxsumm = BitConverter.ToInt32(MemoryReading.Read<int>(TheMess.SummBit + 0xA), 0);
                    var summ = (value & 0xffff) - 0x8000;
                    if (summ < 0)
                    {
                        summ = (summ + 0x8000);
                    }
                    summ = summ / 10;
                    maxsumm = maxsumm * 10;
                    return (summ, maxsumm);
                }
            }
            return (0,0);
        }
        public async Task<int> GetAddreline()
        {
            int placeh = 679;
            if (await TheMess.PlayerLoggedIn())
            {
                if (TheMess.AddrelineBit == 0)
                {
                    settings value = await TheMess.FindPSett(placeh);
                    if (value == null) { return 0; }
                    TheMess.AddrelineBit = value.SettingsAddr;
                    var slay = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.AddrelineBit + 0x8), 0);
                    return slay / 10;
                }
                else
                {
                    var slay = BitConverter.ToInt16(MemoryReading.Read<int>(TheMess.AddrelineBit + 0x8), 0);
                    return slay / 10;
                }
            }
            else
            {
                TheMess.AddrelineBit = 0;
            }
            return 0;
        }
        internal async Task<bool> PInArea(int xc, int rangex, int yc, int rangey, int floorz)
        {
            Player pp = new Player();
            var pc = await pp.GetPlayerCoords();
            var px = pc.x;
            var py = pc.y;
            if (xc > px - rangex && xc < px + rangex)
            {
                if (yc > py - rangey && yc < py + rangey)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
