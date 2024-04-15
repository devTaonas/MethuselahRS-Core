using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    internal class Offsets
    {
        public static List<ulong> Bank = new List<ulong> { 366, 368 };
        public static int BankPreset = 9932;

        public static List<ulong> Fletching = new List<ulong> { 347, 349 };
        public static List<ulong> Smithing = new List<ulong> { 357 };
        public static List<ulong> ChooseATool = new List<ulong> { 347 };
        public static List<ulong> Crafting = new List<ulong> { 347, 348 };
        public static List<ulong> Lodestone = new List<ulong> { 359 };
        public static List<ulong> TradeWindow = new List<ulong> { 352 };

    }
}
