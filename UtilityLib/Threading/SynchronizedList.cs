using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;

namespace Libraries.UtilityLib.Threading
{
    public class SynchronizedList<TItem> : List<TItem>
    {
        public readonly AutoResetEvent NotifyEvent = new AutoResetEvent(false);

        protected object SyncRoot
        { 
            get { return (this as ICollection).SyncRoot; }
        }

        protected void SignalNotifyEvent()
        {
            if (NotifyEvent != null)
            {
                NotifyEvent.Set();
            }
        }

        public new void Add(TItem item)
        {
            lock (SyncRoot)
            {
                base.Add(item);
            }

            SignalNotifyEvent();
        }

        public new void AddRange(IEnumerable<TItem> items)
        {
            lock (SyncRoot)
            {
                base.AddRange(items);
            }

            SignalNotifyEvent();
        }

        public new void Remove(TItem item)
        {
            lock (SyncRoot)
            {
                base.Remove(item);
            }
        }

        public new TItem this [int index]
        {
            get
            {
                lock (SyncRoot)
                {
//                    if (Count == 0)
//                    {
//                        // wait for an item to become available...
//                        NotifyEvent.WaitOne();
//                    }

                    return base[index];
                }
            }
            set
            {
                lock (SyncRoot)
                {
                    base[index] = value;
                }
            }
        }

        public List<TItem> FetchAll(bool remove = false)
        {
            lock (SyncRoot)
            {
                if (Count > 0)
                {
                    List<TItem> allItems = GetRange(0, Count);
                    if (remove)
                    {
                        Clear();
                    }
                    return allItems;
                }

                return new List<TItem>();
            }

        }

        // Count
    }
}

