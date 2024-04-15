using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MethuselahRS.Memory.Testing
{
    internal class Keyboard
    {

        [DllImport("User32.dll", EntryPoint = "PostMessageA")]
        private static extern int PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        const uint WM_KEYDOWN = 0x100;
        const uint WM_KEYUP = 0x101;

        public async static Task sendString(string word)
        {
            string[] character = Regex.Split(word, string.Empty);

            foreach (string ss in character)
            {
                await sendKey(ss);
            }
        }
        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        public static async Task SenKey(char c, int delay = 10)
        {
            Process rs = Form1.ListRuneScapeProcesses[0];
            IntPtr rsWindow = FindWindowEx(rs.MainWindowHandle, IntPtr.Zero, "JagOpenGLView", null);

            await Task.Delay(delay);
            PostMessage(rsWindow, 0x102, c, 1);
        }
        public static async void HoldTwoKeys(string key1, string key2, int HoldForHowLong = 100)
        {
            await sendKey(key1, HoldForHowLong);
            await sendKey(key2, HoldForHowLong);
        }
        public static async Task sendKey(string key, int HoldForHowLong = 100)
        {
            Process rs = Form1.ListRuneScapeProcesses[0];
            IntPtr rsWindow = FindWindowEx(rs.MainWindowHandle, IntPtr.Zero, "JagOpenGLView", null);

            if (key == "a" || key == "A")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x41, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x41, 0);
            }

            if (key == "b" || key == "B")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x42, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x42, 0);
            }
            if (key == "c" || key == "C")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x43, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x43, 0);
            }
            if (key == "d" || key == "D")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x44, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x44, 0);
            }
            if (key == "e" || key == "E")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x45, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x45, 0);
            }
            if (key == "f" || key == "F")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x46, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x46, 0);
            }
            if (key == "g" || key == "G")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x47, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x47, 0);
            }
            if (key == "h" || key == "H")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x48, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x48, 0);
            }
            if (key == "i" || key == "I")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x49, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x49, 0);
            }
            if (key == "j" || key == "J")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x4A, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x4A, 0);
            }
            if (key == "k" || key == "K")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x4B, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x4B, 0);
            }
            if (key == "l" || key == "L")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x4C, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x4C, 0);
            }
            if (key == "m" || key == "M")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x4D, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x4D, 0);
            }
            if (key == "n" || key == "N")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x4E, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x4E, 0);
            }
            if (key == "o" || key == "O")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x4F, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x4F, 0);
            }
            if (key == "p" || key == "P")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x50, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x50, 0);
            }
            if (key == "q" || key == "Q")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x51, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x51, 0);
            }
            if (key == "r" || key == "R")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x52, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x52, 0);
            }
            if (key == "s" || key == "S")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0X53, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x53, 0);
            }
            if (key == "t" || key == "T")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x54, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x54, 0);
            }
            if (key == "u" || key == "U")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x55, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x55, 0);
            }
            if (key == "v" || key == "V")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x56, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x56, 0);
            }
            if (key == "w" || key == "W")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x57, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x57, 0);
            }
            if (key == "x" || key == "X")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x58, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x58, 0);
            }
            if (key == "y" || key == "Y")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x59, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x59, 0);
            }
            if (key == "z" || key == "Z")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x5A, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x5A, 0);
            }
            if (key == "0")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x60, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x60, 0);
            }
            if (key == "1")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x61, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x61, 0);
            }
            if (key == "2")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x62, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x62, 0);
            }
            if (key == "3")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x63, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x63, 0);
            }
            if (key == "4")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x64, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x64, 0);
            }
            if (key == "5")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x65, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x65, 0);
            }
            if (key == "6")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x66, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x66, 0);
            }
            if (key == "7")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x67, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x67, 0);
            }
            if (key == "8")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x68, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x68, 0);
            }
            if (key == "9")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x69, 0);
                await Task.Delay(HoldForHowLong);
                PostMessage(rsWindow, WM_KEYUP, 0x69, 0);
            }
            if (key == "up" || key == "Up" || key == "UP")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x26, 0);
                await Task.Delay(200);
                PostMessage(rsWindow, WM_KEYUP, 0x26, 0);
            }
            if (key == "left" || key == "Left" || key == "LEFT")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x25, 0);
                await Task.Delay(200);
                PostMessage(rsWindow, WM_KEYUP, 0x25, 0);
            }
            if (key == "right" || key == "Right" || key == "RIGHT")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x27, 0);
                await Task.Delay(200);
                PostMessage(rsWindow, WM_KEYUP, 0x27, 0);
            }
            if (key == "down" || key == "Down" || key == "DOWN")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x28, 0);
                await Task.Delay(200);
                PostMessage(rsWindow, WM_KEYUP, 0x28, 0);
            }
            if (key == "esc" || key == "ESC" || key == "Esc" || key == "escape" || key == "Escape" || key == "ESCAPE")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x1B, 0);
                await Task.Delay(200);
                PostMessage(rsWindow, WM_KEYUP, 0X1B, 0);
            }
            if (key == "enter" || key == "Enter" || key == "ENTER")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x0D, 0);
                await Task.Delay(200);
                PostMessage(rsWindow, WM_KEYUP, 0x0D, 0);
            }
            if (key == "space" || key == "Space" || key == "SPACE")
            {
                PostMessage(rsWindow, WM_KEYDOWN, 0x20, 0);
                await Task.Delay(200);
                PostMessage(rsWindow, WM_KEYUP, 0x20, 0);
            }
        }
    }
}
