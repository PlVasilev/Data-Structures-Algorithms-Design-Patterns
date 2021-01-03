using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _01.RoyaleArena
{
    public class Table<TValue> : IEnumerable<TValue> where TValue: BattleCard
    {
        private Index<double> _index;

        private Dictionary<IComparable, List<TValue>> _records;

        public Table(Index<double> index)
        {
            _records = new Dictionary<IComparable, List<TValue>>();
            _index = index;
        }

        public void Add(TValue item)
        {
            var key = _index.GetKey(item);
            if (!_records.ContainsKey(key))
                _records[key] = new List<TValue>();
            
            _records[key].Add(item);
            _index.Add(key);
        }

        public void Remove(TValue item)
        {
            var key = _index.GetKey(item);
            if (!_records.ContainsKey(key))
                return;
            _records[key].Remove(item);
            if (_records[key].Count == 0)
            {
                _records.Remove(key);
                _index.Remove(key);
            }
        }

        public IEnumerable<TValue> GetViewBetween(double min, double max)
        {
            var set = _index.GetViewBetween(min, max);
            return set.SelectMany(key => _records[key]);
        }

        public IEnumerable<TValue> GetFirstN(int n, Func<TValue, object> orderBy)
        {
            int count = 0;
            foreach (var key in _index.Take(n))
            {
                foreach (var item in _records[key].OrderBy(orderBy))
                {
                    if (count < n)
                    {
                        yield return item;
                        count++;
                    }
                    else
                        break;
                }
            }
        }


        public IEnumerator<TValue> GetEnumerator() => 
            _records.Values.SelectMany(list => list).GetEnumerator();
        

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public List<TValue> Max
        {
            get
            {
                if (_index.Count > 0)
                {
                    return _records[_index.Max];
                }
                else
                    return  new List<TValue>();
            }
        }

        public List<TValue> Min
        {
            get
            {
                if (_index.Count > 0)
                {
                    return _records[_index.Min];
                }
                else
                    return new List<TValue>();
            }
        }

        public double MaxKey => _index.Max;

        public double MinKey => _index.Min;

    }
}
