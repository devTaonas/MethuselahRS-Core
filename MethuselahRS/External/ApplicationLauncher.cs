using MethuselahRS.Binder;
using MethuselahRS.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MethuselahRS.External
{
    public class ApplicationLauncher
    {
        private readonly IProcessBinder _processBinder;
        private readonly IndexPage _indexPage;
        private const int TimeoutMilliseconds = 30000; // 30 seconds

        public ApplicationLauncher(IProcessBinder processBinder, IndexPage indexPage)
        {
            _processBinder = processBinder ?? throw new ArgumentNullException(nameof(processBinder));
            _indexPage = indexPage ?? throw new ArgumentNullException(nameof(indexPage));
        }

        public void LaunchAndBindProcess()
        {
            var processStartInfo = new ProcessStartInfo
            {
                //FileName = "C:\\Program Files (x86)\\Jagex Launcher\\Games\\RuneScape",
                FileName = "C:\\ProgramData\\Jagex\\launcher\\rs2client.exe"
        //        Arguments = "https://world1.runescape.com/jav_config.ws?binaryType=2",
         //       UseShellExecute = false
            };

            Process process = Process.Start(processStartInfo);

            Thread.Sleep(8000);
            Process p = Process.GetProcessesByName("rs2client")[0];
            Form1.rs2clientProcess = p; 
            //if (process != null)
            //{

            //    bool isBound = WaitForProcessAndBind(process);
            //    if (!isBound)
            //    {
            //        MessageBox.Show("No client with the specified Main Window Title was detected.",
            //                        "Client Detection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
        }

        public static Panel Main;
        private bool WaitForProcessAndBind(Process process)
        {
            var stopwatch = Stopwatch.StartNew();
            while (stopwatch.ElapsedMilliseconds < TimeoutMilliseconds)
            {
                process.Refresh(); // Refresh the process information
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    if (process.MainWindowTitle.Equals("NxtApp", StringComparison.OrdinalIgnoreCase))
                    {
                        _indexPage.Invoke((Action)(() =>
                        {
                            // Pass the process here so that it gets bound to the panel
                            Main = new Panel();
                            Main = _indexPage.AddGridElement(process);
                        }));
                        return true;
                    }
                }
                Thread.Sleep(500); // Wait for half a second before checking again
            }
            return false; // Timeout reached without finding a window with the correct title
        }
    }
}
