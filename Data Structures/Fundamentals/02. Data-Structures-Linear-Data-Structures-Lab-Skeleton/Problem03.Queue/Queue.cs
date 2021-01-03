namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;
            while (current != null)
            {
                if (current.Element.Equals(item))
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
            
            var result = this._head.Element;
            this._head = _head.Next;
            Count--;
            return result;
        }

        public void Enqueue(T item)
        {
            var newNode = new Node<T>(item)
            {
                Element = item,
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
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }
            return this._head.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;
            while (current != null)
            {
                yield return current.Element;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
    }
}