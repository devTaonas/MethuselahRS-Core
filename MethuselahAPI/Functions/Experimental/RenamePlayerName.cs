using MethuselahAPI.Memory.Statics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahAPI.Functions.Experimental
{
    public class RenamePlayerName
    {


        public static void Rename(Process process, string name)
        {
            Console.WriteLine();
            UInt64 adr = Memory.Reading.ReadPointer((ulong)process.MainModule.BaseAddress, new ulong[] { (ulong) Addresses.PlayerBase, 0X0, 0X188 }, process);
            Console.WriteLine("Found screen pixel address?: " + adr.ToString());
            string Name = Memory.Reading.ReadString(adr, process);
            Console.WriteLine("RS Char Name: " + Name);

            Console.WriteLine("Changing Name");
            IntPtr processHandle = MethuselahAPI.Natives.Methods.OpenProcess(0x1F0FFF, false, process.Id);
            byte[] bytes = Encoding.UTF8.GetBytes(name);
            int bytesWritten = bytes.Length;
            Console.WriteLine("Writing bytes: " + bytes.Length + " - " + Addresses.PlayerBase.ToString());
            //adr = Memory.Reading.ReadPointer((ulong)_gameClient.MainModule.BaseAddress, new ulong[] { (ulong) Addresses.PlayerBase, 03E39850 }, process);
            Natives.Methods.WriteProcessMemory((int)processHandle, adr, bytes, bytes.Length, ref bytesWritten);
            Console.WriteLine("Written");

            Name = Memory.Reading.ReadString(adr, process);
            Console.WriteLine("RS Char Name: " + Name);
            //03E39850
        }
    }
}
