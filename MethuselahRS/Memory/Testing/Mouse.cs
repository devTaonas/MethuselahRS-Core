using MethuselahRS.Memory.Reading;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Testing
{
    internal class Mouse
    {
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;
        const int WM_RBUTTONDOWN = 0x0204;
        const int WM_RBUTTONUP = 0x0205;
        const int WM_MOUSEMOVE = 0x0200;
        const int WM_MOUSEHWHEEL = 0x020E;

        [DllImport("User32.DLL")]
        static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        internal static IntPtr MakeLParam(int x, int y) => (IntPtr)((y << 16) | (x & 0xFFFF));
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        public struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        internal static async void MouseMove(int x, int y, int xMax = 1, int yMax = 1)
        {
            //r.Left + item.PixXmid-25, r.Bottom - item.PixYmid
            Process rs = Form1.ListRuneScapeProcesses[0];
            IntPtr rs2window = FindWindowEx(rs.MainWindowHandle, IntPtr.Zero, "JagRenderView", null);
            var r = new RECT();
            GetWindowRect(rs.MainWindowHandle, out r);
      //      var pointPtr = getRandClick(x, r.Bottom - y-220, xMax, yMax);
            var pointPtr = getRandClick(r.Left + x, r.Top + y, xMax, yMax);
            SendMessage(rs2window, WM_MOUSEMOVE, IntPtr.Zero, pointPtr);

            await Task.Delay(1);

        }
        public static async Task leftClick(int x, int y)
        {
            Process rs = Form1.ListRuneScapeProcesses[0];
            IntPtr rsWindow = FindWindowEx(rs.MainWindowHandle, IntPtr.Zero, "JagRenderView", null);
            var r = new RECT();
            GetWindowRect(rs.MainWindowHandle, out r);

            var pointPtr = MakeLParam(r.Left + x - 58, r.Top + y - 223);

            SendMessage(rsWindow, WM_MOUSEMOVE, IntPtr.Zero, pointPtr);
            await Task.Delay(200);
            SendMessage(rsWindow, WM_LBUTTONDOWN, IntPtr.Zero, pointPtr);
            await Task.Delay(200);
            SendMessage(rsWindow, WM_LBUTTONUP, IntPtr.Zero, pointPtr);
        }
        internal static IntPtr getRandClick(int x, int y, int xMax, int yMax)
        {
            Random randX = new Random();
            Random randY = new Random();
            x = x + randX.Next(0, xMax);
            y = y + randY.Next(0, yMax);
            var point = MakeLParam(x, y);
            return point;
        }






        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private static void DrawOnWindow(Point position)
        {
            IntPtr windowHandle = Form1.ListRuneScapeProcesses[0].MainWindowHandle;
            if (windowHandle != IntPtr.Zero)
            {
                IntPtr hdc = GetWindowDC(windowHandle);
                if (hdc != IntPtr.Zero)
                {
                    using (Graphics g = Graphics.FromHdc(hdc))
                    {
                        // Draw something on the window at the specified position
                        g.FillEllipse(Brushes.Red, new Rectangle(position.X, position.Y, 10, 10));
                    }
                    ReleaseDC(windowHandle, hdc);
                }
            }
        }


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // Delegate to filter windows
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        private static IntPtr FindWindowByProcessId(int processId)
        {
            IntPtr windowHandle = IntPtr.Zero;
            EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                uint windowProcessId;
                GetWindowThreadProcessId(hWnd, out windowProcessId);
                if (windowProcessId == processId)
                {
                    windowHandle = hWnd;
                    return false; // Found the window, stop enumeration
                }
                return true; // Continue enumeration
            }, IntPtr.Zero);

            return windowHandle;
        }
    }
}
