using procMethuselahRS.Memory.Reading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procMethuselahRS.Controller
{
    public class procDevelopment
    {
        internal static List<Process> ListRuneScapeProcesses;

        public procDevelopment(List<Process> listRuneScapeProcesses) 
        { 
            ListRuneScapeProcesses = listRuneScapeProcesses; 
        }

        public void WorldHop()
        {
            MethuselahAPI.Functions.Lobby.WorldHop.UpdateWorld(ListRuneScapeProcesses[0], 8);
        }

    }
}
