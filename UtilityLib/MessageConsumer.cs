using System;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using Libraries.UtilityLib.Threading;

namespace Libraries.UtilityLib
{
    public abstract class MessageConsumer<T> : ThreadLoop
    {
        protected List<T> messages = new List<T>();
              
        protected abstract void ConsumeMessage(T message);

        protected void ProduceMessage(T message)
        {
            lock((messages as ICollection).SyncRoot)
            {
                messages.Add(message);                
            }

            SignalExecuteCallback();
        }

        protected override void Callback()
        {
            // process all messages currently in the queue
            lock((messages as ICollection).SyncRoot)
            {
                while (messages.Count > 0)
                {
                    var message = messages[0];
                    ConsumeMessage(message);
                    messages.Remove(message);
                }
            }
        }
    }
}

