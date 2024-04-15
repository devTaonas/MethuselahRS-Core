using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace procMethuselahRS.Injector.System
{
    public interface IProcessOperator
    {
        IntPtr OpenProcess(Process proc);
        bool CloseHandleMain(IntPtr handle);
    }
}
