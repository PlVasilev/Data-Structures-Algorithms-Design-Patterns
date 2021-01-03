namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] _items;

        public int Capacity { get; private set; }

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity = DefaultCapacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this._items = new T[capacity];
            this.Capacity = capacity;
        }

        

        public T this[int index]
        {
            get
            {
                this.ValidateIndex(Count - index - 1);
                return this._items[Count - index - 1];
            }
            set
            {
                this.ValidateIndex(index );
                this._items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.ResizeIfNecessary();
            this._items[this.Count++] = item;
        }

        public bool Contains(T item)
        {
            return this.IndexOf(item) >= 0;
        }

        public int IndexOf(T item)
        {
            for (var i = 0; i < this.Count; i++)
            {
                if ((item == null && this._items[i] == null) || this._items[i].Equals(item))
                {
                    return Count - i - 1;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.ValidateIndex(index);
            this.ResizeIfNecessary();

            for (var i = this.Count; i > this.Count - index; i--)
            {
                this._items[i] = this._items[i - 1];
            }
            this._items[this.Count - index] = item;
            this.Count++;
        }

        // O(n)
        public bool Remove(T item)
        {
            int index = 0;
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(_items[i]))
                {
                    index = i;
                }
            }

            if (index == 0)
            {
                return false;
            }
            for (int i = index; i < Count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }

            Count--;
            return true;
        }

        public void RemoveAt(int index)
        {
            this.ValidateIndex(index);

            for (var i = Count - index - 1; i < Count ; i++)
            {
                this._items[i] = this._items[i + 1];
            }
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = this.Count - 1 ; i >= 0; i--)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ResizeIfNecessary()
        {
            if (this.Count == this.Capacity)
            {
                this.Resize();
            }
        }

        // O(n)
        private void Resize()
        {
            var newCapacity = this.Capacity * 2;
            var newArray = new T[newCapacity];
            for (var i = 0; i < this.Count; i++)
            {
                newArray[i] = this._items[i];
            }

            this._items = newArray;
            this.Capacity = newCapacity;
        }

        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}