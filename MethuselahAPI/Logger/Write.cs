using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethuselahAPI.Logger
{
    public static class WriteLog
    {
        public static void Debug(string message)
        {
            Log("   " + message, ConsoleColor.Yellow, "DEBUG");
        }

        public static void Information(string message)
        {
            Log("    " + message, ConsoleColor.Green, "INFO");
        }

        public static void Error(string message)
        {
            Log("   " + message, ConsoleColor.Red, "ERROR");
        }

        public static void Critical(string message)
        {
            Log("   " + message, ConsoleColor.DarkRed, "CRITICAL");
        }

        public static void Action(string message)
        {
            Log("  " + message, ConsoleColor.Cyan, "ACTION");
        }

        private static void Log(string message, ConsoleColor color, string level)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"[{DateTime.Now:MM-dd HH:mm:ss}] [{level}] {"   " + message}");
            Console.ResetColor();
        }
    }
}
