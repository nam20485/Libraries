using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.Disposeable;
using Libraries.UtilityLib.Keys;
using Libraries.UtilityLib.Logger;

namespace Libraries.WindowsHookLib
{
    public class KeyRemapper : ManagedResource
    {
        protected KeyPressDictionary mappings = new KeyPressDictionary();

        protected LowLevelKeyboardHook pfnLowLevelKeyboardHook = null;

        public KeyPress this[KeyPress remappedKey]
        {
            get { return mappings[remappedKey]; }
            set { mappings[remappedKey] = value; }                
        }       

        public bool StartRemappingKeys()
        {
            pfnLowLevelKeyboardHook = new LowLevelKeyboardHook(LowLevelKeyboardHookProc);
            if (pfnLowLevelKeyboardHook != null)
            {
                // make sure our hook gets disposed (unhooked)
                Resource = pfnLowLevelKeyboardHook;
                return true;
            }
            return false;
        }

        public void StopRemappingKeys()
        {
            if (pfnLowLevelKeyboardHook != null)
            {
                pfnLowLevelKeyboardHook.Dispose();
                pfnLowLevelKeyboardHook = null;
            }
        }

        protected virtual IntPtr LowLevelKeyboardHookProc(HookCode hc, KeyEvent keyEvent, KeyEventInfo info)
        {
            try
            {
                string line = "\r\nHC: [" + hc.ToString() + "]\r\nKEY_EVENT: [" + keyEvent.ToString() + "]\r\n" + info.ToString();
                LocalStaticLogger.WriteLine(line);
                Console.Out.WriteLine(line);

                foreach (var remappedKey in mappings.Keys)
                {
                    // we fudge equality here, comparing a KeyPress to a KeyEvent, technically 
                    // they are not so much equal as equivalent
                    if (info == remappedKey)
                    {
                        var mappedKey = mappings[remappedKey];
                        if (mappedKey != null)
                        {
                            if (keyEvent == KeyEvent.WM_KEYDOWN)
                            {
                                if (KeyEventSimulator.SimulateKeyDownAsync(mappedKey.VkCode, mappedKey.ScanCode, mappedKey.Extended))
                                {
                                    return LowLevelKeyboardHook.HANDLED;                                                               
                                }                                
                            }
                            else if (keyEvent == KeyEvent.WM_KEYUP)
                            {
                                if (KeyEventSimulator.SimulateKeyUpAsync(mappedKey.VkCode, mappedKey.ScanCode, mappedKey.Extended))
                                {
                                    return LowLevelKeyboardHook.HANDLED;
                                }                                
                            }                    
                        }   
                    }                            
                }          
            }
            catch (Exception e)
            {
                LocalStaticLogger.WriteLine(e.ToString());
            }

            return IntPtr.Zero;
        }

        protected class KeyPressDictionary : Dictionary<KeyPress, KeyPress>
        {
            // convenient for type safety, kind of like a type alias
        }        
    }
}