namespace Problem01.FasterQueue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    // This is the code from the Lab exercise on writing a simple Queue.
    public class SlowQueue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;
            while (current != null)
            {
                if (current.Item.Equals(item))
                {
                    return true;
                }
                current = current.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            var result = this._head.Item;
            this._head = _head.Next;
            Count--;
            return result;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item)
            {
                Item = item,
                Next = null,
            };
            if (this._head == null)
            {
                this._head = newNode;
                this._tail = newNode;
            }
            else
            {
                this._tail.Next = newNode;
                this._tail = newNode;
            }
            Count++;
        }

        public T Peek()
        {
            this.EnsureNotEmpty();

            return this._head.Item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;
            while (current != null)
            {
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() 
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException();
        }
    }
}