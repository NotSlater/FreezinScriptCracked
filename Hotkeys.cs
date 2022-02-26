using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Steam
{
    class Hotkeys
    {
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        private static volatile MessageWindow _wnd;
        private static volatile IntPtr _hwnd;
        private static ManualResetEvent _windowReadyEvent = new ManualResetEvent(false);
        private static event EventHandler<HotKeyEventArgs> HotKeyPressed;

        private class MessageWindow : Form
        {
            public MessageWindow()
            {
                _wnd = this;
                _hwnd = this.Handle;
                _windowReadyEvent.Set();
            }

            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_HOTKEY)
                {
                    HotKeyEventArgs e = new HotKeyEventArgs(m.LParam);
                    Hotkey.OnHotKeyPressed(e);
                }
                base.WndProc(ref m);
            }

            protected override void SetVisibleCore(bool value)
            { base.SetVisibleCore(false); }
            private const int WM_HOTKEY = 0x312;
        }

        static Hotkeys()
        {
            Thread messageLoop = new Thread(delegate ()
            { Application.Run(new MessageWindow()); });
            messageLoop.Name = "HotKeyMessageThread";
            messageLoop.IsBackground = true;
            messageLoop.Start();
        }

        [Flags] public enum KeyModifiers { Control = 2 }

        public class HotKeyEventArgs : EventArgs
        {
            public readonly Keys Key;
            public readonly KeyModifiers Modifiers;

            public HotKeyEventArgs(IntPtr hotKeyParam)
            {
                uint param = (uint)hotKeyParam.ToInt64();
                Key = (Keys)((param & 0xffff0000) >> 16);
                Modifiers = (KeyModifiers)(param & 0x0000ffff);
            }
        }

        public class Initiate
        {
            private static int _id = 0;
            public static int RegisterHotKey(Keys key, KeyModifiers modifiers)
            {
                _windowReadyEvent.WaitOne();
                int id = Interlocked.Increment(ref _id);
                _wnd.Invoke(new RegisterHotKeyDelegate(RegisterHotKeyInternal), _hwnd, id, (uint)modifiers, (uint)key);
                return id;
            }

            delegate void RegisterHotKeyDelegate(IntPtr hwnd, int id, uint modifiers, uint key);

            private static void RegisterHotKeyInternal(IntPtr hwnd, int id, uint modifiers, uint key)
            {  DLLImports.RegisterHotKey(hwnd, id, modifiers, key); }

            public static void InitiateHotKeys()
            {
                RegisterHotKey(Keys.Tab, KeyModifiers.Control);
                RegisterHotKey(Keys.F1, KeyModifiers.Control); // AK
                RegisterHotKey(Keys.F2, KeyModifiers.Control); // LR300
                RegisterHotKey(Keys.F3, KeyModifiers.Control); // Semi
                RegisterHotKey(Keys.F4, KeyModifiers.Control); // Custom
                RegisterHotKey(Keys.F5, KeyModifiers.Control); // MP5
                RegisterHotKey(Keys.F6, KeyModifiers.Control); // Thompson
                RegisterHotKey(Keys.F7, KeyModifiers.Control); // M39
                RegisterHotKey(Keys.F8, KeyModifiers.Control); // M92
                RegisterHotKey(Keys.F9, KeyModifiers.Control); // M249
                RegisterHotKey(Keys.Left, KeyModifiers.Control); // Smoothness Down
                RegisterHotKey(Keys.Right, KeyModifiers.Control); // Smoothness Up
                RegisterHotKey(Keys.Up, KeyModifiers.Control); // Randomness Up
                RegisterHotKey(Keys.Down, KeyModifiers.Control); // Randomness Down
                RegisterHotKey(Keys.Enter, KeyModifiers.Control); // Scopes
                RegisterHotKey(Keys.RShiftKey, KeyModifiers.Control); // Barrels
                RegisterHotKey(Keys.Insert, KeyModifiers.Control);
                HotKeyPressed += new EventHandler<HotKeyEventArgs>(Hotkey.HotKeys_HotKeyPressed);
            }
        }

        public class Hotkey
        {

            public static void OnHotKeyPressed(HotKeyEventArgs e)
            { HotKeyPressed?.Invoke(null, e); }

            public static void HotKeys_HotKeyPressed(object sender, HotKeyEventArgs e)
            {
                switch (e.Key)
                {
                    case Keys.Insert:
                        Variables.Windex();
                        break;
                    case Keys.Tab:
                        Variables.Menu.setEnabled(!Variables.Menu.isEnabled());
                        break;
                    case Keys.F1:
                        Weapons.setVariables(1);
                        break;
                    case Keys.F2:
                        Weapons.setVariables(2);
                        break;
                    case Keys.F3:
                        Weapons.setVariables(3);
                        break;
                    case Keys.F4:
                        Weapons.setVariables(4);
                        break;
                    case Keys.F5:
                        Weapons.setVariables(5);
                        break;
                    case Keys.F6:
                        Weapons.setVariables(6);
                        break;
                    case Keys.F7:
                        Weapons.setVariables(7);
                        break;
                    case Keys.F8:
                        Weapons.setVariables(8);
                        break;
                    case Keys.F9:
                        Weapons.setVariables(9);
                        break;
                    case Keys.Left:
                        Variables.Weapon.setRandomness(-1);
                        break;
                    case Keys.Right:
                        Variables.Weapon.setRandomness(1);
                        break;
                    case Keys.Up:
                        Variables.Settings.setSensitivity(1);
                        break;
                    case Keys.Down:
                        Variables.Settings.setSensitivity(-1);
                        break;
                    case Keys.Enter:
                        Variables.Weapon.scopeIndex++;
                        Variables.Weapon.changeScope();
                        break;
                    case Keys.RShiftKey:
                        Variables.Weapon.barrelIndex++;
                        Variables.Weapon.changeBarrel();
                        break;
                }
                Console.Clear();
                Display.PrintHeader();
            }
        }
    }
}
