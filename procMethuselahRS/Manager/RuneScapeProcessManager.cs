using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procMethuselahRS.Manager
{
    internal class RuneScapeProcessManager
    {
        internal RuneScapeProcessManager(Process process)
        {
            Form1.ListRuneScapeProcesses.Add(process);
            Form1.dev = new Controller.procDevelopment(Form1.ListRuneScapeProcesses);
        }
    }
}
