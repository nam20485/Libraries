using System;
using System.Threading;

namespace Libraries.UtilityLib.Threading
{
    public class DelegatedWorker<TWorkItem> : Worker<TWorkItem>
    {
        public delegate void WorkItemConsumer(TWorkItem workItem);
        public readonly WorkItemConsumer OnConsumeWorkItem;           

        public DelegatedWorker(WorkItemConsumer workItemConsumer)
            : base()
        {
            OnConsumeWorkItem = workItemConsumer;
        }

        public DelegatedWorker(WorkItemConsumer workItemConsumer, SynchronizedList<TWorkItem> workItems)
            : base(workItems)
        {
            OnConsumeWorkItem = workItemConsumer;
        }

        protected override void ConsumeWorkItem(TWorkItem workItem)
        {
            if (OnConsumeWorkItem != null)
            {
                OnConsumeWorkItem(workItem);
            }
        }
    }
}

