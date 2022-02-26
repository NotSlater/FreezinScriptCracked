using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using KeyAuth;

namespace Steam
{
    class Display
    {
        static string name = ""; // application name. right above the blurred text aka the secret on the licenses tab among other tabs
        static string ownerid = ""; // ownerid, found in account settings. click your profile picture on top right of dashboard and then account settings.
        static string secret = ""; // app secret, the blurred text on licenses tab and other tabs
        static string version = "1.0"; // leave alone unless you've changed version on website

        public static api KeyAuthApp = new api(name, ownerid, secret, version);

            [DllImport("Kernel32.dll")]
            private static extern IntPtr GetConsoleWindow();
            [DllImport("User32.dll")]
            private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
            public static DateTime UnixTimeToDateTime(long unixtime)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        public static void PrintHeader()
        {
            Console.Title = "Steam";
            Console.SetWindowSize(84, 35);
            Console.SetBufferSize(84, 35);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            Console.WriteLine(@"    _______ ______ _______ _______ _______ ____ _____     ___ ___ ___ ___ _______ ");
            Console.WriteLine(@"   |    ___|  __  |    ___|    ___|__     |    |   | |   |   |   |   |   |__     |");
            Console.WriteLine(@"   |    ___|     -|    ___|    ___|     __|    |     |__ |-     -|_     _|     __|");
            Console.WriteLine(@"   |___|   |___|__|_______|_______|_______|____|_|___|__||___|___| |___| |_______|");
            Console.WriteLine();
            Console.WriteLine(@"              ╔════════════════════════════════════════════════════╗ ");     
            Console.WriteLine(@"               Version: 2.5 | Made by: NotSlater | Release: Cracked  ");
            Console.WriteLine(@"              ╚════════════════════════════════════════════════════╝ ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"                            Status: Probably Detected                   ");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("                Enabled: ");
            Console.ResetColor();
            Console.Write(Variables.Menu.isEnabled() + " / ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Weapon: ");
            Console.ResetColor();
            Console.Write(Variables.Weapon.getName() + " / ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Scope: ");
            Console.ResetColor();
            Console.Write(Variables.Weapon.getActiveScope());
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"     ╔═════Weapons═════╗       ╔══Settings══╗       ╔═══Scopes═════╗ ");
            Console.WriteLine(@"       F1 = Ak-47                                                     ");
            Console.WriteLine(@"       F2 = LR-300              Humanization              Simple        ");
            Console.WriteLine(@"       F3 = SAR                     {0:0.0}                              ", Variables.Weapon.getRandomness());
            Console.WriteLine(@"       F4 = CUSTOM                                        Holo          ");
            Console.WriteLine(@"       F5 = MP5                                                        ");
            Console.WriteLine(@"       F6 = Thompson            Sensitivity               8x            ");
            Console.WriteLine(@"       F7 = M39                     {0:0.0}                             ", Variables.Settings.getSensitivity());
            Console.WriteLine(@"       F8 = M92                                           16x           ");
            Console.WriteLine(@"       F9 = M249                                                      ");
            Console.WriteLine(@"     ╚════════════════╝        ╚════════════╝       ╚══════════════╝ ");
            Console.WriteLine(@"      (CTRL + Bind)               (Sens) ");
            Console.WriteLine(@"                                CTRL + ↑/↓");
            Console.WriteLine(@"                                  (Human) ");
            Console.WriteLine(@"                                CTRL + </>");
            Console.WriteLine();
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            Console.Title = "Steam";
            Console.WriteLine("\n [-] Connecting...");
            Thread.Sleep(250);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n [freezin] Enter license: ");
            Thread.Sleep(200);
            Console.ResetColor();
            Variables.LoadBypass();
            Thread.Sleep(80);
            Variables.LoadDrivers();
            Thread.Sleep(50);
            Console.Clear();
            PrintHeader();
            Hotkeys.Initiate.InitiateHotKeys();
            Recoil.RecoilLoop();
        }
    }
}
