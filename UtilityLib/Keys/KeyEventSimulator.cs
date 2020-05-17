using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Libraries.UtilityLib.Windows;


namespace Libraries.UtilityLib.Keys
{
    public static class KeyEventSimulator
    {            
        public static uint SimulateKeyDown(Win32API.VirtualKey vkCode, Win32API.ScanCode scanCode, bool extended)
        {
            Win32API.KeyEventFlags flags = 0;
            if (extended)
            {
                flags |= Win32API.KeyEventFlags.EXTENDEDKEY;
            }

            return SimulateKeyEvent(vkCode, scanCode, flags, UIntPtr.Zero);
        }

        public static bool SimulateKeyDownAsync(Win32API.VirtualKey vkCode, Win32API.ScanCode scanCode, bool extended)
        {
            return ThreadPool.QueueUserWorkItem(new WaitCallback(SimulateKeyDownInvoker), new KeyPress(vkCode, scanCode, extended));
        }

        public static uint SimulateKeyUp(Win32API.VirtualKey vkCode, Win32API.ScanCode scanCode, bool extended)
        {
            Win32API.KeyEventFlags flags = Win32API.KeyEventFlags.KEYUP;
            if (extended)
            {
                flags |= Win32API.KeyEventFlags.EXTENDEDKEY;
            }

            return SimulateKeyEvent(vkCode, scanCode, Win32API.KeyEventFlags.KEYUP, UIntPtr.Zero);
        }

        public static bool SimulateKeyUpAsync(Win32API.VirtualKey vkCode, Win32API.ScanCode scanCode, bool extended)
        {
            return ThreadPool.QueueUserWorkItem(new WaitCallback(SimulateKeyUpInvoker), new KeyPress(vkCode, scanCode, extended));
        }

        public static uint SimulateKeyPress(Win32API.VirtualKey vkCode, Win32API.ScanCode scanCode, bool extended)
        {
            uint ret = 0;

            ret = SimulateKeyDown(vkCode, scanCode, extended);
            if (ret == 1)
            {
                ret = SimulateKeyUp(vkCode, scanCode, extended);
            }

            return ret;
        }

        public static bool SimulateKeyPressAsync(Win32API.VirtualKey vkCode, Win32API.ScanCode scanCode, bool extended)
        {
            return ThreadPool.QueueUserWorkItem(new WaitCallback(SimulateKeyPressInvoker), new KeyPress(vkCode, scanCode, extended));
        }       

        private static void SimulateKeyDownInvoker(object data)
        {
            KeyPress key = data as KeyPress;
            if (key != null)
            {
                SimulateKeyDown(key.VkCode, key.ScanCode, key.Extended);
            }
        }

        private static void SimulateKeyUpInvoker(object data)
        {
            KeyPress key = data as KeyPress;
            if (key != null)
            {
                SimulateKeyUp(key.VkCode, key.ScanCode, key.Extended);
            }
        }

        private static void SimulateKeyPressInvoker(object data)
        {
            KeyPress key = data as KeyPress;
            if (key != null)
            {
                SimulateKeyPress(key.VkCode, key.ScanCode, key.Extended);
            }
        }    

        private static uint SimulateKeyEvent(Win32API.VirtualKey vkCode, Win32API.ScanCode scanCode, Win32API.KeyEventFlags flags, UIntPtr dwExtraInfo)
        {
#if (SIMULATE_KEYS_SENDINPUT)
            Win32API.INPUT[] inputs = new Win32API.INPUT[]
            {
                new Win32API.INPUT 
                {
                    type = Win32API.INPUT_TYPE.INPUT_KEYBOARD,
                    U = new Win32API.InputUnion
                    {
                        ki = new Win32API.KEYBDINPUT
                        {
                            wVk = vkCode,
                            wScan = scanCode,
                            dwFlags = flags,
                            time = 0,
                            dwExtraInfo = dwExtraInfo
                        }
                    }
                }              
            };                                  

            return Win32API.SendInput((uint) inputs.Length, inputs, Win32API.INPUT.Size);            
#else
            Win32API.keybd_event((uint) vkCode, (uint) scanCode, flags, dwExtraInfo);
            return 0;
#endif
        }       
    }
}
