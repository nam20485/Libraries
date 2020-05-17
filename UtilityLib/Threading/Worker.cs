using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;

namespace Libraries.UtilityLib.Threading
{
    public abstract class Worker<TWorkItem> : ThreadLoop
    {             
        protected readonly SynchronizedList<TWorkItem> WorkItems;

        protected Worker()
            : this(new SynchronizedList<TWorkItem>())
        {
        }
   
        public Worker(SynchronizedList<TWorkItem> workItems)
            : base(workItems.NotifyEvent)
        {
            WorkItems = workItems;
        }

        protected abstract void ConsumeWorkItem(TWorkItem workItem);

        protected void ProduceWorkItem(TWorkItem workItem)
        {
            WorkItems.Add(workItem);
        }

        protected override void Callback()
        {
            List<TWorkItem> workItems = WorkItems.FetchAll(true);

            for (int i = 0; i < workItems.Count; i++)
            {
                ConsumeWorkItem(workItems[i]);
            }
        }
    }
}