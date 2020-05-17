using System;

namespace Libraries.UtilityLib.Threading
{
    public class DelegatedWorkerPool<TWorkItem> : WorkerPool<TWorkItem>
    {
        protected readonly DelegatedWorker<TWorkItem>.WorkItemConsumer OnConsumeWorkItem;

        public DelegatedWorkerPool(DelegatedWorker<TWorkItem>.WorkItemConsumer workItemConsumer)
        {
            OnConsumeWorkItem = workItemConsumer;
        }

        #region implemented abstract members of Libraries.UtilityLib.Threading.WorkerPool
        protected override void WorkItemConsumer(TWorkItem workItem)
        {
            if (OnConsumeWorkItem != null)
            {
                OnConsumeWorkItem(workItem);
            }
        }
        #endregion

    }
}

