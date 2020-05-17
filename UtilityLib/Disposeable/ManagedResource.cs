using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.UtilityLib.Disposeable
{
    public abstract class ManagedResource : DisposeableResource<IDisposable>
    {
        public ManagedResource(IDisposable managedResource)
            : base(managedResource)
        {
        }

        public ManagedResource()
        {
        }

        protected override void FreeManagedResources()
        {
            // free managed resources
            if (Resource != null)
            {
                Resource.Dispose();
                Resource = null;
            }
        }
    }
}
