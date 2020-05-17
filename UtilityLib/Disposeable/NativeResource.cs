using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.UtilityLib.Disposeable
{
    public abstract class NativeResource : DisposeableResource<IntPtr>
    {
        public NativeResource()
            : base()
        {
        }

        public NativeResource(IntPtr nativeResource)
            : base(nativeResource)
        {
        }

        protected override void FreeNativeResources()
        {
            // free native resources if there are any.
            if (Resource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(Resource);
                Resource = IntPtr.Zero;
            }
        }
    }
}
