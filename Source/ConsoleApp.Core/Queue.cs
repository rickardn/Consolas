using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp.Core
{
    public class Queue<T> : IEnumerable<T>, ICollection
    {
        private readonly List<T> _queue = new List<T>();

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            ((ICollection)_queue).CopyTo(array, index);
        }

        public int Count { get { return _queue.Count; } }
        public object SyncRoot { get { return ((ICollection) _queue).SyncRoot; } }
        public bool IsSynchronized { get { return ((ICollection) _queue).IsSynchronized; } }

        public void Clear()
        {
            _queue.Clear();
        }

        public void Enqueue(T item)
        {
            _queue.Add(item);
        }

        public T Dequeue()
        {
            ValidateCount();
            var item = _queue[0];
            _queue.RemoveAt(0);
            return item;
        }

        public void DeDequeue(T item)
        {
            _queue.Insert(0, item);
        }

        public T Peek()
        {
            ValidateCount();
            return _queue[0];
        }

        private void ValidateCount()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }
        }

        public T[] ToArray()
        {
            return _queue.ToArray();
        }
    }
}