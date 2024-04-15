using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Reading
{
    internal class Inventory
    {
        public static bool InventroyFull()
        {
            int count = 0;

            List<IInfo> Inventory = new List<IInfo>();
            Inventory = Interfaces.ReadInvArrays33();

            for (int i = 0; i < Interfaces.InventoryList.Count(); i++)
            {
                if (Interfaces.InventoryList[i].itemid1 != -1)
                {
                    count++;
                }
            }
            if (count == 28)
            {
                return true;
            }
            return false;
        }
        public static List<IInfo> GetInventroy()
        {
            List<IInfo> Inventory = new List<IInfo>();
            return Interfaces.ReadInvArrays33();
        }
    }
}
