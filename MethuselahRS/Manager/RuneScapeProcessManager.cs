using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Manager
{
    internal class RuneScapeProcessManager
    {
        internal RuneScapeProcessManager(Process process)
        {
            Form1.ListRuneScapeProcesses.Add(process);
        }
    }
}
