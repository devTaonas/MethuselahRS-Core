using MethuselahRS.Injector.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Injector
{
    public class DllInjector
    {
        private readonly IProcessOperator _processOperator;
        private readonly IMemoryOperator _memoryOperator;

        public DllInjector(IProcessOperator processOperator, IMemoryOperator memoryOperator)
        {
            _processOperator = processOperator;
            _memoryOperator = memoryOperator;
        }

        public void Inject(int processId)
        {
            string username = Environment.GetEnvironmentVariable("USERNAME") ?? "default_user";
            string dllPath = $@"C:\Users\{username}\Documents\MemoryError\MemoryError.dll";

            Process process = Process.GetProcessById(processId);
            IntPtr processHandle = _processOperator.OpenProcess(process);
            try
            {
                IntPtr loadLibraryAddress = Binder.Natives.Methods.GetProcAddress(Binder.Natives.Methods.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                IntPtr memoryAddress = _memoryOperator.AllocateMemory(processHandle, dllPath);
                byte[] dllPathBytes = Encoding.ASCII.GetBytes(dllPath);

                if (!_memoryOperator.WriteMemory(processHandle, memoryAddress, dllPathBytes))
                    throw new InvalidOperationException("Failed to write memory.");

                _memoryOperator.CreateRemoteThread(processHandle, loadLibraryAddress, memoryAddress);
            }
            finally
            {
                _processOperator.CloseHandleMain(processHandle);
            }
        }
    }
}
