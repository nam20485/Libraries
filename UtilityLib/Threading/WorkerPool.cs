using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;

namespace Libraries.UtilityLib.Threading
{
    public abstract class WorkerPool<TWorkItem>
    {
        protected readonly SynchronizedList<TWorkItem> WorkItems = new SynchronizedList<TWorkItem>();
        protected readonly List<Worker<TWorkItem>> Workers = new List<Worker<TWorkItem>>();

        public int WorkerCount { get; set; }

        public WorkerPool()
            : this(5)
        {
        }

        public WorkerPool(int workerCount)
        {
            WorkerCount = workerCount;
        }

        protected abstract void WorkItemConsumer(TWorkItem workItem);

        public void AddWorkItem(TWorkItem workItem)
        {
            WorkItems.Add(workItem);
        }      

        public void AddWorkItems(IEnumerable<TWorkItem> workItems)
        {
            WorkItems.AddRange(workItems);
        }      

        public void Start()
        {
            while (Workers.Count < WorkerCount)
            {
                DelegatedWorker<TWorkItem> worker = new DelegatedWorker<TWorkItem>(new DelegatedWorker<TWorkItem>.WorkItemConsumer(WorkItemConsumer), 
                                                                                    WorkItems);
                Workers.Add(worker);
                worker.Start();
            }
        }

        public void Stop()
        {
            foreach (var worker in Workers)
            {
                worker.Stop();
            }
            Workers.Clear();
        }    
    }
}

