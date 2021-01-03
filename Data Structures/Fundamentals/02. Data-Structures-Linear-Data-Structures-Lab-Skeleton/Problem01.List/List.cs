using System.Globalization;

namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity = DEFAULT_CAPACITY)
        {
            if (capacity < 0)
            {
                throw new IndexOutOfRangeException(nameof(capacity));
            }
            _items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }
                return _items[index];
            }
            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }
                _items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            if (Count == _items.Length)
            {
                T[] arr = new T[Count*2];
                for (int i = 0; i < Count; i++)
                {
                    arr[i] = _items[i];
                }
                _items = arr;
            }
            
            _items[Count] = item;
            Count++;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(_items[i]))
                {
                    return true;
                }
            }

            return false;
        }


        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(_items[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
            if (Count == _items.Length)
            {
                T[] arr = new T[Count * 2];
                for (int i = 0; i < Count; i++)
                {
                    arr[i] = _items[i];
                }
                _items = arr;
            }
            
            for (int i = Count; i > index; i--)
            {
                _items[i] = _items[i - 1];
            }
            _items[index] = item;
            Count++;
        }

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
            for (int i = index; i < Count; i++)
            {
                _items[i] = _items[i + 1];
            }

            Count--;
            return true;

        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException(nameof(index));
            }
            for (int i = index; i < Count; i++)
            {
                _items[i] = _items[i + 1];
            }
            Count--;
            _items[Count] = default;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _items[i];
                
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}