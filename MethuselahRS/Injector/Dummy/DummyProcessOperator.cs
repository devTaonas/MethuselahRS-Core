using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Injector.Dummy
{
    public class DummyProcessOperator : Injector.System.IProcessOperator
    {
        public IntPtr OpenProcess(Process proc)
        {
            // Dummy implementation - Replace with actual code to open a process
            return IntPtr.Zero;
        }

        public bool CloseHandleMain(IntPtr handle)
        {
            // Dummy implementation - Replace with actual code to close a process handle
            return true;
        }
    }
}
