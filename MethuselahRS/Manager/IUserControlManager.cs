using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MethuselahRS.Manager
{
    public interface IUserControlManager
    {
        event Action<UserControl, Process> RequestFullScreenView;
        event Action<Process> RebindProcess;
        void RebindProcessToPanel(Process process);
    }
}
