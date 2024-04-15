using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MethuselahRS.Binder
{
    public interface IProcessBinder
    {
        void BindProcesses(string processName);
        void RebindProcessToPanel(Process process, Panel panel);
        void NextProcess();
        void UpdatePanels();
    }
}
