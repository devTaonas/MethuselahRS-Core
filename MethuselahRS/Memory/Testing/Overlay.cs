using MethuselahRS.Memory.Reading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MethuselahRS.Memory.Testing
{
    public partial class Overlay : Form
    {
        private Timer _positionUpdater;
        private Timer _OverlayTimer;
        public Overlay()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.TopMost = true;
            this.BackColor = Color.Magenta; // Example color
            this.TransparencyKey = this.BackColor;
            
            _positionUpdater = new Timer();
            _positionUpdater.Interval = 500; // Update every 500 milliseconds
            _positionUpdater.Tick += PositionUpdater_Tick;
            _positionUpdater.Start();

        }



        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_TRANSPARENT = 0x20;
                const int WS_EX_LAYERED = 0x80000;
                const int WS_EX_TOPMOST = 0x8;
                const int WS_EX_COMPOSITED = 0x02000000; // This helps with flickering

                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST | WS_EX_COMPOSITED;
                return cp;
            }
        }
        public Rectangle GetWindowRectangle(int processId)
        {
            Process process = Process.GetProcessById(processId);
            if (process == null || process.MainWindowHandle == IntPtr.Zero)
                return Rectangle.Empty;

            NativeMethods.RECT rect;
            if (NativeMethods.GetWindowRect(process.MainWindowHandle, out rect))
            {
                return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            }

            return Rectangle.Empty; 
        }
        private void PositionUpdater_Tick(object sender, EventArgs e)
        {
            var r = new Mouse.RECT();
            Mouse.GetWindowRect(Form1.ListRuneScapeProcesses[0].MainWindowHandle, out r);
            this.Location = new Point(r.Left, r.Top);
            this.Size = new Size(r.Right - r.Left, r.Bottom - r.Top);
            Refresh();
        }

        private async void Overlay_Paint(object sender, PaintEventArgs e)
        {
            if (Form1.cb_Overlay_NPC.Checked) 
            {
                await TheMess.ReadCObjectArrays();
                foreach (var item in TheMess.npc)
                {
                    using (Font font = new Font("Arial", 12))
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        Mouse.RECT r = new Mouse.RECT();
                        Mouse.GetWindowRect(Form1.ListRuneScapeProcesses[0].MainWindowHandle, out r);
                        var _textPosition = this.PointToClient(new Point(r.Left + item.PixXmid-25, r.Bottom - item.PixYmid));
                        e.Graphics.DrawString(item.Id + " | " + item.Name + "\r\n" + item.Life, font, brush, _textPosition);
                    }
                }
            }
            if (Form1.cb_Overlay_GameObject.Checked)
            {
                await TheMess.ReadCObjectArrays();
                foreach (var item in TheMess.activeObj)
                {
                    using (Font font = new Font("Arial", 12))
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        Mouse.RECT r = new Mouse.RECT();
                        Mouse.GetWindowRect(Form1.ListRuneScapeProcesses[0].MainWindowHandle, out r);
                        var _textPosition = this.PointToClient(new Point(r.Left + item.PixXmid-25, r.Bottom - item.PixYmid));
                        e.Graphics.DrawString(item.Id + " | " + item.Name, font, brush, _textPosition);
                    }
                }
                foreach (var item in TheMess.decor)
                {
                    using (Font font = new Font("Arial", 12))
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        Mouse.RECT r = new Mouse.RECT();
                        Mouse.GetWindowRect(Form1.ListRuneScapeProcesses[0].MainWindowHandle, out r);
                        var _textPosition = this.PointToClient(new Point(r.Left + item.PixXmid - 25, r.Bottom - item.PixYmid));
                        e.Graphics.DrawString(item.Id + " | " + item.Name, font, brush, _textPosition);
                    }
                }
            }
            if (Form1.cb_Overlay_Player.Checked)
            {
                await TheMess.ReadCObjectArrays();
                foreach (var item in TheMess.players)
                {
                    using (Font font = new Font("Arial", 12))
                    using (Brush brush = new SolidBrush(Color.White))
                    {
                        Mouse.RECT r = new Mouse.RECT();
                        Mouse.GetWindowRect(Form1.ListRuneScapeProcesses[0].MainWindowHandle, out r);
                        var _textPosition = this.PointToClient(new Point(r.Left + item.PixXmid-25, r.Bottom - item.PixYmid));
                        e.Graphics.DrawString(item.Name, font, brush, _textPosition);
                    }
                }
            }
            if (Form1.cb_Overlay_Inventory.Checked)
            {
                List<IInfo> Inventory = new List<IInfo>();
                Inventory = Interfaces.ReadInvArrays33();
                if (Inventory != null)
                {
                    foreach (var item in Inventory)
                    {
                        using (Font font = new Font("Arial", 10))
                        using (Brush brush = new SolidBrush(Color.White))
                        {
                            Mouse.RECT r = new Mouse.RECT();
                            Mouse.GetWindowRect(Form1.ListRuneScapeProcesses[0].MainWindowHandle, out r);
                            var _textPosition = this.PointToClient(new Point(r.Left + item.x - 10, r.Top + item.y + 23));
                            var _textPosition2 = this.PointToClient(new Point(r.Left + item.x - 15, r.Top + item.y + 33));
                            e.Graphics.DrawString(item.itemid1.ToString(), font, brush, _textPosition);
                            e.Graphics.DrawString(item.textitem.ToString(), font, brush, _textPosition2);
                        }
                    }
                }
            }
            if (Form1.cb_Overlay_Bank.Checked)
            {
                Tuple<List<IInfo>, List<IInfo>> AllBankInfo = Bank.BankWindowData();
                if (AllBankInfo != null)
                {
                    foreach (var item in AllBankInfo.Item1)//item 1 is bank window
                    {
                        using (Font font = new Font("Arial", 10))
                        using (Brush brush = new SolidBrush(Color.White))
                        {
                            Mouse.RECT r = new Mouse.RECT();
                            Mouse.GetWindowRect(Form1.ListRuneScapeProcesses[0].MainWindowHandle, out r);
                            var _textPosition = this.PointToClient(new Point(r.Left + item.x - 10, r.Top + item.y + 23));
                            var _textPosition2 = this.PointToClient(new Point(r.Left + item.x - 15, r.Top + item.y + 33));
                            e.Graphics.DrawString(item.itemid1.ToString(), font, brush, _textPosition);
                            e.Graphics.DrawString(item.textitem.ToString(), font, brush, _textPosition2);
                        }
                    }
                    foreach (var item in AllBankInfo.Item2)//item 2 is inventory in bank window
                    {
                        using (Font font = new Font("Arial", 10))
                        using (Brush brush = new SolidBrush(Color.White))
                        {
                            Mouse.RECT r = new Mouse.RECT();
                            Mouse.GetWindowRect(Form1.ListRuneScapeProcesses[0].MainWindowHandle, out r);
                            var _textPosition = this.PointToClient(new Point(r.Left + item.x - 10, r.Top + item.y + 23));
                            var _textPosition2 = this.PointToClient(new Point(r.Left + item.x - 15, r.Top + item.y + 33));
                            e.Graphics.DrawString(item.itemid1.ToString(), font, brush, _textPosition);
                            e.Graphics.DrawString(item.textitem.ToString(), font, brush, _textPosition2);
                        }
                    }
                }
            }
        }
    }
}
public class NativeMethods
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
