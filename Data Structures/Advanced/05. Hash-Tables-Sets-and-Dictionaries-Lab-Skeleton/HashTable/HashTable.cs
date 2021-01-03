using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int InitialCapacity = 100;
        private List<KeyValue<TKey, TValue>>[] buckets;


        public int Count { get; private set; }

        public int Capacity => buckets.Length;

        public HashTable() :this(InitialCapacity) { }

        public HashTable(int capacity)
        {
            buckets = new List<KeyValue<TKey, TValue>>[capacity];
        }

        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException();

            var item = new KeyValue<TKey,TValue>(key,value);
            AddItem(item);
            Count++;
            ResizeAndRefresh();
        }

        private void AddItem(KeyValue<TKey, TValue> item)
        {
            var index = GetIndex(item.Key);
            if (buckets[index] == null)
                buckets[index] = new List<KeyValue<TKey, TValue>>();
            buckets[index].Add(item);
            
        }

        private int GetIndex(TKey key)
        {
            var hash = key.GetHashCode();
            return Math.Abs(hash % Capacity);
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            var item = Find(key);
            if (item != null)
                item.Value = value;
            else
                Add(key,value);
            return true;
        }

        public TValue Get(TKey key) => GetItemOrThrowExcep(key);
        

        private TValue GetItemOrThrowExcep(TKey key)
        {
            var item = Find(key);
            if (item != null)
                return item.Value;

            throw new KeyNotFoundException();
        }

        public TValue this[TKey key]
        {
            get => GetItemOrThrowExcep(key);
            set => AddOrReplace(key, value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            value = default(TValue);
            var item = Find(key);
            if (item != null)
                value = item.Value;
           
            return item != null;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            var index = GetIndex(key);
            return buckets[index]?
                .FirstOrDefault(item => item.Key.Equals(key));
        }

        public bool ContainsKey(TKey key) => Find(key) != null;
        

        public bool Remove(TKey key)
        {
            var item = Find(key);
            if (item != null)
            {
                var index = GetIndex(key);
                buckets[index].Remove(item);
                buckets[index] = buckets[index].Count == 0 ? null : buckets[index];
                Count--;
                return true;
            }
            return false;
        }

        public void Clear()
        {
            buckets = new List<KeyValue<TKey, TValue>>[InitialCapacity];
            Count = 0;
        }

        public IEnumerable<TKey> Keys =>
            buckets.Where(item => item != null)
                .SelectMany(bucket => bucket.Select(item => item.Key));

        public IEnumerable<TValue> Values =>
            buckets.Where(item => item != null)
                .SelectMany(bucket => bucket.Select(item => item.Value));

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var bucket in buckets)
            {
                if (bucket == null) continue;

                foreach (var item in bucket)
                    yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private void ResizeAndRefresh()
        {
            if (Count / (double)Capacity >= 0.75)
            {
                var oldBuckets = buckets;
                buckets = new List<KeyValue<TKey, TValue>>[Capacity * 2];
                foreach (var bucket in oldBuckets)
                {
                    if (bucket == null) continue;

                    foreach (var item in bucket)
                    {
                        AddItem(item);
                    }
                }
            }
        }
    }
}
