using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libraries.UtilityLib.Disposeable
{
    public abstract class DisposeableResource<T> : IDisposable
    {
        protected T Resource = default(T);

        public DisposeableResource()
        {
            // don't need to cache the resource
        }

        public DisposeableResource(T resource)
        {
            Resource = resource;
        }

        protected void DisposeResource(bool disposing)
        {
            if (disposing)
            {
                FreeManagedResources();
            }     

            FreeNativeResources();
        }

        ~DisposeableResource()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        protected virtual void FreeManagedResources()
        {
        }

        protected virtual void FreeNativeResources()
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        

        protected virtual void Dispose(bool disposing)
        {
            DisposeResource(disposing);                       
        }     
    }
}
