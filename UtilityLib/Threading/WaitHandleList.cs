using System;
using System.Collections.Generic;
using System.Threading;

namespace Libraries.UtilityLib.Threading
{
    public class WaitHandleList : List<WaitHandle>
    {
        public WaitHandleList()
            : base()
        {
        }

        public WaitHandleList(IEnumerable<WaitHandle> waitHandles)
            : base(waitHandles)
        {
        }

        public int WaitAnyValid()
        {
            return WaitAnyValid(ToArray());
        }

        public int WaitAny()
        {
            return WaitHandle.WaitAny(ToArray());
        }

        public bool AreValid()
        {
            return AreValid(ToArray());
        }

        public static int WaitAnyValid(WaitHandle[] waitHandles)
        {
            if (! AreValid(waitHandles))
            {
                return -1;
            }

            return WaitHandle.WaitAny(waitHandles);
        }       

        public static bool AreValid(WaitHandle[] waitHandles)
        {
            foreach (var wh in waitHandles)
            {
                if (wh.SafeWaitHandle.IsClosed || wh.SafeWaitHandle.IsInvalid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}

