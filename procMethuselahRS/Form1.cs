using procMethuselahRS.External;
using procMethuselahRS.Injector.Dummy;
using procMethuselahRS.Memory.Reading;
using procMethuselahRS.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MethuselahAPI;

namespace procMethuselahRS
{
    public partial class Form1 : Form
    {
        private readonly UserControls.HeaderPanel _HeaderPanel;
        private readonly UserControls.IndexPage _indexPage;
        private readonly External.ApplicationLauncher _appLauncher;
        public static Process _currentlyFullScreenedProcess;
        public static Process _activeProcess;
        private int _currentClientCount = 0;
        public static List<Process> ListRuneScapeProcesses = new List<Process>();
        public static Controller.procDevelopment dev;

        public Form1()
        {
            InitializeComponent();
            Binder.IProcessBinder processBinder = new Binder.ProcessBinder(new TableLayoutPanel());

            // Create the dummy instances (replace these with real ones later)
            Injector.System.IProcessOperator processOperator = new DummyProcessOperator();
            Injector.System.IMemoryOperator memoryOperator = new DummyMemoryOperator();

            // Pass the instances to the IndexPage constructor
            _indexPage = new UserControls.IndexPage(processBinder, processOperator, memoryOperator);
            _HeaderPanel = new UserControls.HeaderPanel();
            _appLauncher = new External.ApplicationLauncher(processBinder, _indexPage);
            _indexPage.RequestFullScreenView += SwitchToFullScreenUserControl;
            _indexPage.RebindProcess += IndexPage_RebindProcess;

            if(_indexPage != null)
            {
                panel1.Controls.Add(_indexPage);
                _indexPage.Dock = DockStyle.Fill;
                _indexPage.BringToFront();

                launchClient.Click += LaunchClient_Click;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MethuselahAPI.Functions.Lobby.WorldHop.UpdateWorld(ListRuneScapeProcesses[0], 8);
        }




        private void LaunchClient_Click(object sender, EventArgs e)
        {
            if (_currentClientCount < 9)
            {
                // Just launch and bind the process. The grid element will be added by the ApplicationLauncher.
                _appLauncher.LaunchAndBindProcess();
            }
            else
            {
                MessageBox.Show("Maximum number of clients reached.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void SwitchToFullScreenUserControl(UserControl newControl, Process process)
        {
            panel1.Controls.Add(newControl);
            newControl.Dock = DockStyle.Fill;
            newControl.BringToFront();
            newControl.Tag = process.Id;
            if (process != null && process.MainWindowHandle != IntPtr.Zero)
            {
                Binder.Natives.Methods.SetParent(process.MainWindowHandle, newControl.Handle);
                Binder.Natives.Methods.MoveWindow(process.MainWindowHandle, 0, 0, newControl.ClientSize.Width, newControl.ClientSize.Height, true);
            }
        }

        private void IndexPage_RebindProcess(Process process)
        {
            _currentlyFullScreenedProcess = process;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("rs2client"))
            {
                process.Kill();
                process.WaitForExit();
            }
        }


        bool doOnce = false;
        private async void btn_TestButton_Click(object sender, EventArgs e)
        {
            //Currently the only thing written into memory is the world (only works on initial login)
            // MemoryWriting.WriteMemory(ListRuneScapeProcesses[0], 8); // 8 is the world I wanted it to login to
            if (!doOnce)
            {
                Console.WriteLine("Entered doOnce");
                doOnce = true;
                Process process = Form1.ListRuneScapeProcesses[0];
                Pointers pointers = new Pointers();
                Console.WriteLine("Found process ID: " + ListRuneScapeProcesses[0].Id);
                UInt64 adr = MemoryReading.ReadPointer((ulong)process.MainModule.BaseAddress, new ulong[] { (ulong)pointers.PlayerBase, 0X0, 0X188 });
                Console.WriteLine("Found screen pixel address?: " + adr.ToString());
                string Name = MemoryReading.ReadString(adr);
                Console.WriteLine("RS Char Name: " + Name);
                Console.WriteLine("Starting call");
                Stopwatch sw = new Stopwatch();
                sw.Start();
                await TheMess.Start(Name);
                Console.WriteLine("Call ended in " + (sw.ElapsedMilliseconds / 1000) + " seconds");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("in else");
                await TheMess.ReadCObjectArrays();
            }
            Console.WriteLine("activeObj read");
            foreach (var item in TheMess.activeObj)
            {
                if (item.Name.Contains("booth"))
                {
                    break;
                }
            }
            Console.WriteLine("npc read");
            foreach (var item2 in TheMess.npc)
            {
                if(item2.Name.Contains("Soran"))
                {
                    break;
                }
            }
            Console.WriteLine("players read");
            foreach (var item3 in TheMess.players)
            {
                // all players
                if(item3.Name.Contains("7A"))
                {

                }
            }
            Console.WriteLine("groundItems read");
            foreach (var item4 in TheMess.groundItems)
            {
                // all ground items
                if(item4.Id == 995)
                {

                }
            }
            Console.WriteLine("projectiles read");
            foreach (var item5 in TheMess.projectiles)
            {

            }
            Console.WriteLine("decor read");
            foreach (var item6 in TheMess.decor)
            {
                // all decor items ... just cause it's in this list doesn't mean it's actually a decor item ... just sayin 
            }
            Console.WriteLine("other read");
            foreach (var item7 in TheMess.other)
            {
                // no idea whats in here yet hahaha
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MethuselahAPI.Functions.Experimental.RenamePlayerName.Rename(ListRuneScapeProcesses[0], "apple");
        }
    }
}
