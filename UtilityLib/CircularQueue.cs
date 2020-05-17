using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
namespace Libraries.UtilityLib
{
    public class CircularQueue<T> : ICollection<T>, ICollection
    {
        private readonly T[] _buffer;

        public int Capacity { get; protected set; }

        private int _head;
        private int _tail;

        public T LastItem => _buffer[_head];
        public T FirstItem => _buffer[_tail];

        public int Count { get; protected set; }
        public bool IsSynchronized => false;

        public bool IsReadOnly => false;

        private object _syncRoot;
        public object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    Interlocked.CompareExchange(ref _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
        }

        public CircularQueue() : this(10)   { }
        public CircularQueue(int capacity)
        {
            if (Capacity < 0)
            {
                throw new ArgumentException("Capacity must be greater than 0", "capacity");
            }

            Capacity = capacity;
            _buffer = new T[Capacity];
        }

        public IEnumerator<T> GetEnumerator()
        {
            var bufferIndex = _head;
            for (var i = 0; i < Count; i++, bufferIndex++)
            {
                if (bufferIndex == Capacity)
                {
                    bufferIndex = 0;
                }

                yield return _buffer[bufferIndex];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            _buffer[_tail] = item;
            if (++_tail == Capacity)
            {
                _tail = 0;
            }
            Count++;
        }

        public void Clear()
        {
            Capacity = 0;
            _head = 0;
            _tail = 0;
        }

        public bool Contains(T item)
        {
            int bufferIndex = _head;
            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < Count; i++, bufferIndex++)
            {
                if (bufferIndex == Capacity)
                    bufferIndex = 0;

                if (item == null && _buffer[bufferIndex] == null)
                    return true;
                else if ((_buffer[bufferIndex] != null) &&
                    comparer.Equals(_buffer[bufferIndex], item))
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();

            //if (Count > Capacity)
            //    throw new ArgumentOutOfRangeException("count", "Count too large");

            //int bufferIndex = _head;
            //for (int i = 0; i < Count; i++, bufferIndex++, arrayIndex++)
            //{
            //    if (bufferIndex == Capacity)
            //        bufferIndex = 0;
            //    _buffer[arrayIndex] = _buffer[bufferIndex];
            //}
        }

        public void CopyTo(Array array, int index)
        {
           
        }
    }
}
