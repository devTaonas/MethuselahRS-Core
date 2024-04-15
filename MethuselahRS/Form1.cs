using MethuselahRS.External;
using MethuselahRS.Injector.Dummy;
using MethuselahRS.Memory.Reading;
using MethuselahRS.Memory.Testing;
using MethuselahRS.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MethuselahRS.Memory.Testing.Mouse;

namespace MethuselahRS
{
    public partial class Form1 : Form
    {
        private readonly UserControls.HeaderPanel _HeaderPanel;
        private readonly UserControls.IndexPage _indexPage;
        private readonly External.ApplicationLauncher _appLauncher;
        public static Process _currentlyFullScreenedProcess;
        public static Process _activeProcess;
        private int _currentClientCount = 0;
        internal static List<Process> ListRuneScapeProcesses = new List<Process>();
        public static CheckBox cb_Overlay_NPC, cb_Overlay_GameObject, cb_Overlay_Player, cb_Overlay_Inventory, cb_Overlay_Bank;

        public static string fileName = "MethuselahCPP.dll";
        public static string dllPath = Application.StartupPath + "\\MethuselahCPP\\" + fileName;

        public static IntPtr lib;
        public static Process rs2clientProcess;

        public Form1()
        {
            InitializeComponent();
            lib = Global.LoadLibrary(dllPath);
            
            cb_Overlay_NPC = cb_OverlayNPC;
            cb_Overlay_GameObject = cb_OverlayGameObject;
            cb_Overlay_Player = cb_OverlayPlayer;
            cb_Overlay_Inventory = cb_OverlayInventory;
            cb_Overlay_Bank = cb_OverlayBank;

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

            if (_indexPage != null)
            {
                panel1.Controls.Add(_indexPage);
                _indexPage.Dock = DockStyle.Fill;
                _indexPage.BringToFront();

                launchClient.Click += LaunchClient_Click;
            }


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


        /// <summary>
        ///                                                         ONLY FOR TESTING 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        bool doOnce = false;
        private async void btn_TestButton_Click(object sender, EventArgs e)
        {
            ulong[] adr = new ulong[1];
            Pointers pointers = new Pointers();
            Process process = Process.GetProcessesByName("rs2client")[0];
            adr[0] = MemoryReading.ReadPointer((ulong)process.MainModule.BaseAddress, new ulong[]
                { (ulong)pointers.ScanForInterface, 0X0, 0X1F8, 0X90, 0X330, 0X40,  unchecked((ulong)(-0x3c50)) });
            Interfaces.Interface_Address_start_pointer = adr[0];

            string Name = "";
            TheMess.Start(Name);
            doOnce = true;
            Interfaces.LoadKnownInterfacesIn();

        }

        public async Task ShowPlayerInfo()
        {
            Player p = new Player();
            var COORDS = p.GetPlayerCoords();
            var hp = p.GetHP().Result.Item1;
            var maxhp = p.GetHP().Result.Item2;
            var pray = p.GetPray().Result.Item1;
            var maxpray = p.GetPray().Result.Item2;
            var slay = p.GetSlayerCount().Result;
            var name = p.GetPlayerName();
            var summ = p.GetSumm().Result.Item1;
            var maxsumm = p.GetSumm().Result.Item2;
            var addreline = p.GetAddreline().Result;
            lbl_Test.Text = "Player Name: " + name + "\r\n " +
                "Player Coords : " + COORDS.Result.ToString() + "\r\n " +
                "HP: " + hp.ToString() + " / " + maxhp.ToString() + "\r\n " +
                "Prayer : " + pray + " / " + maxpray + "\r\n " +
                "Slayer Count : " + slay.ToString() + "\r\n " +
                "Summing Points : " + summ.ToString() + " / " + maxsumm.ToString() + "\r\n " +
                "Addreline :" + addreline.ToString();
            lbl_MouseOverText.Text = Interfaces.FindMouseOverText();
            lbl_playeraddress.Text = TheMess.LocalPlayer.ToString("X");
            await Task.Delay(100);
        }
        private async void btn_TestButtonTwo_Click(object sender, EventArgs e)
        {
            var THEID = rs2clientProcess.Id;
            IntPtr intPtr = (IntPtr)THEID;
            var blah = TheMess.Compass2;
            if (TheMess.LocalPlayer != 0)
            {
                DoAction.Player.Player.SetLocalPlayerAddress(TheMess.LocalPlayer);
                DoAction.DoActionGlobal.SetHandle(rs2clientProcess.Id);
                DoAction.DoActionGlobal.SetCompass2(TheMess.Compass2);
            }

            //int CurrentItemPrice = TheMess.ItemPrice(225); // limpwurt root

            //Point destination = new Point(3186, 3442);
            //await Interfaces.Map_Walker1(destination, 5);

            //string blah = Interfaces.FindMouseOverText();
            //listBox1.BringToFront();
            //List<string> allPublicChat = ChatBox.ReadChatMessages();
            //listBox1.Items.Clear();
            //foreach (var item in allPublicChat)
            //{
            //    listBox1.Items.Add(item);
            //}


            //List<Skills> skills = await Skills.GetSkills();
            //foreach (var skill in skills)
            //{
            //    Console.WriteLine(skill.Name + " | " + skill.XP + " | " + skill.CurrentLevel + " / " + skill.MaxLevel);
            //}

            //bool open = Bank.IsBankOpen();
            //bool full = Inventory.InventroyFull();
            //Tuple<List<IInfo>,List<IInfo>> AllBankInfo = Bank.BankWindowData();


            // bool open = await Interfaces.Smithing_interface_open();
            // bool open2 = await Interfaces.BankOpen_();

        }
        public static async Task<string> GetGameStatus()
        {
            var GameStatusSetting = await TheMess.FindPSett(1800);
            if (GameStatusSetting != null)
            {
                var GameStatusAddress = GameStatusSetting.SettingsAddr + 0XA;
                var GameStatusValue = BitConverter.ToInt16(MemoryReading.Read<int>(GameStatusAddress), 0);
                ulong[] adrr = new ulong[1];
                Pointers pointers = new Pointers();
                adrr[0] = MemoryReading.ReadPointer((ulong)ListRuneScapeProcesses[0].MainModule.BaseAddress, new ulong[]
                { (ulong)pointers.ScanForInterface, 0X0, 0X1F8, 0X90, 0X330, 0X40, 0XA18 });
                var Value = BitConverter.ToUInt16(MemoryReading.Read<long>(adrr[0] - 0x278), 0);
                if (Value > 200)
                {
                    return "In Game";
                }
                else
                {
                    if (GameStatusValue == -1)
                    {
                        return "Logged Out";
                    }
                    else
                    {
                        return "In Lobby";
                    }
                }
            }
            return "";
        }

        private void btn_Inject_Click(object sender, EventArgs e)
        {
            if (System.IO.File.Exists(dllPath))
            {
                FileInfo fi = new FileInfo(dllPath);
                fi.Attributes = FileAttributes.Hidden;
                Injection.Injection.InjectDLL(rs2clientProcess.Id, dllPath);
                var addr = Global.GetProcAddress(Form1.lib, "StartThread");
                Form1.rs2clientProcess.Call(addr, (IntPtr)0);
            }
        }

        public static async Task<bool> InterfaceOpen(List<ulong> Offsets)
        {
            List<ulong> results = new List<ulong>();

            ulong[] adr = new ulong[1];
            Pointers pointers = new Pointers();
            adr[0] = MemoryReading.ReadPointer((ulong)ListRuneScapeProcesses[0].MainModule.BaseAddress, new ulong[]
                { (ulong)pointers.ScanForInterface, 0X0, 0X1F8, 0X90, 0X330, 0X40, 0XA18 });
            var Value = BitConverter.ToUInt16(MemoryReading.Read<long>(adr[0]-0x278), 0);
            foreach (var offsetValue in Offsets)
            {
                if (Value == offsetValue)
                {
                    return true;
                }
            }
            return false;
        }
    
        private void btn_ShowOverlay_Click(object sender, EventArgs e)
        {
            Overlay overlayForm = new Overlay();
            overlayForm.Show();
        }

        private async void btn_MouseOverTest_Click(object sender, EventArgs e)
        {
            if (!cb_npc.Checked && !cb_go.Checked) { MessageBox.Show("One of the checkboxes need to be checked."); return; }
            if (cb_npc.Checked && cb_go.Checked) { MessageBox.Show("Only one can be checked at a time."); return; }

            if (cb_loop.Checked)
            {
                while (cb_loop.Checked)
                {
                    await TheMess.ReadCObjectArrays();
                    if (cb_npc.Checked)
                    {
                        foreach (var item in TheMess.npc)
                        {
                            if (item.Name.ToLower().Contains(tb_objectname.Text.ToLower()))
                            {
                                Mouse.MouseMove(item.PixXmid, item.PixYmid);
                                break;
                            }
                        }
                    }
                    if (cb_go.Checked)
                    {
                        bool found = false;
                        foreach (var item in TheMess.activeObj)
                        {
                            if (item.Name.ToLower().Contains(tb_objectname.Text.ToLower()))
                            {
                                Mouse.MouseMove(item.PixXmid, item.PixYmid);
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            foreach (var item in TheMess.decor)
                            {
                                if (item.Name.ToLower().Contains(tb_objectname.Text.ToLower()))
                                {
                                    Mouse.MouseMove(item.PixXmid, item.PixYmid);
                                    break;
                                }
                            }
                        }
                    }
                    await Task.Delay(1000);
                }
            }
            else
            {
                await TheMess.ReadCObjectArrays();
                if (cb_npc.Checked)
                {
                    foreach (var item in TheMess.npc)
                    {
                        if (item.Name.ToLower().Contains(tb_objectname.Text.ToLower()))
                        {
                            Mouse.MouseMove(item.PixXmid, item.PixYmid);
                            break;
                        }
                    }
                }
                if (cb_go.Checked)
                {
                    bool found = false;
                    foreach (var item in TheMess.activeObj)
                    {
                        if (item.Name.ToLower().Contains(tb_objectname.Text.ToLower()))
                        {
                            Mouse.MouseMove(item.PixXmid, item.PixYmid);
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        foreach (var item in TheMess.decor)
                        {
                            if (item.Name.ToLower().Contains(tb_objectname.Text.ToLower()))
                            {
                                Mouse.MouseMove(item.PixXmid, item.PixYmid);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            if (TheMess.LocalPlayer == 0)
            {
                string status = await GetGameStatus();
                if (status == "In Game")
                { 
                    //Currently the only thing written into memory is the world (only works on initial login)
                    // MemoryWriting.WriteMemory(ListRuneScapeProcesses[0], 8); // 8 is the world I wanted it to login to
                    if (!doOnce)
                    {
                        doOnce = true;
                        //Process process = Form1.ListRuneScapeProcesses[0];
                        //Pointers pointers = new Pointers();
                        //UInt64 adr = MemoryReading.ReadPointer((ulong)process.MainModule.BaseAddress, new ulong[]
                        //{ (ulong)pointers.PlayerBase, 0X0, 0X188 });  
                        //string Name = MemoryReading.ReadString(adr);
                        string Name = "";
                        await Task.Delay(1000);
                        await TheMess.Start(Name);
                    }
                    await Task.Delay(1);
                    await ShowPlayerInfo();

                    //else
                    //{
                    //    await TheMess.ReadCObjectArrays();
                    //}
                    //foreach (var item in TheMess.activeObj)
                    //{
                    //    if (item.Name.Contains("booth"))
                    //    {
                    //        break;
                    //    }
                    //}
                    //foreach (var item2 in TheMess.npc)
                    //{
                    //    if (item2.Name.Contains("Soran"))
                    //    {
                    //        break;
                    //    }
                    //}
                    //foreach (var item3 in TheMess.players)
                    //{
                    //    // all players
                    //    if (item3.Name.Contains("7A"))
                    //    {

                    //    }
                    //}
                    //foreach (var item4 in TheMess.groundItems)
                    //{
                    //    // all ground items
                    //    if (item4.Id == 995)
                    //    {

                    //    }
                    //}
                    //foreach (var item5 in TheMess.projectiles)
                    //{

                    //}
                    //foreach (var item6 in TheMess.decor)
                    //{
                    //    // all decor items ... just cause it's in this list doesn't mean it's actually a decor item ... just sayin 
                    //}
                    //foreach (var item7 in TheMess.other)
                    //{
                    //    // no idea whats in here yet hahaha
                    //}
                    //lbl_ready.Text = "READY";
                }
            }
            if (TheMess.LocalPlayer != 0)
            {
                Point mousePos;
                GetCursorPos(out mousePos);

                Process p = new Process();
                p = ListRuneScapeProcesses[0];
                RECT windowRect;
                GetWindowRect(p.MainWindowHandle, out windowRect);

                lbl_NPCScreenCoords.Text = "Screen Coords (" + (mousePos.X - windowRect.Left) + ", " + (windowRect.Bottom - mousePos.Y) + ") \r\n " +
                    " " + mousePos.X + " , " + mousePos.Y;

                bool open1 = Bank.IsBankOpen();
                bool open2 = await InterfaceOpen(Offsets.Fletching);
                bool open3 = await InterfaceOpen(Offsets.Smithing);
                bool open4 = await InterfaceOpen(Offsets.ChooseATool);
                bool open6 = await InterfaceOpen(Offsets.Crafting);
                bool open7 = await InterfaceOpen(Offsets.Lodestone);
                int open8 = await TheMess.BankPreset(Offsets.BankPreset);
                bool open9 = await InterfaceOpen(Offsets.TradeWindow);
                lbl_InterfaceOpen.Text = "Bank: " + open1.ToString() + "\r\n" +
                                         "Fletching: " + open2.ToString() + "\r\n" +
                                         "Smithing: " + open3.ToString() + "\r\n" +
                                         "Choose A Tool: " + open4.ToString() + "\r\n" +
                                         "Crafting: " + open6.ToString() + "\r\n" +
                                         "Lodestone Window: " + open7.ToString() + "\r\n" +
                                         "Last Bank Preset Used: " + open8.ToString() + "\r\n" +
                                         "Trade Window Open: " + open9.ToString();

                await Task.Delay(100);
                lbl_testlabel.Text = await GetGameStatus();

                ShowPlayerInfo();
            }
        }

    }
}
