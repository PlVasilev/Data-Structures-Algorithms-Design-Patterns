//namespace _01.Loader
//{
//    using _01.Loader.Interfaces;
//    using _01.Loader.Models;
//    using System;
//    using System.Collections;
//    using System.Collections.Generic;

//    public class Loader : IBuffer
//    {
//        private const int InitalSize = 10;
//        private IEntity[] _items = new IEntity[InitalSize];

//        public Loader()
//        {
//        }

//        public int EntitiesCount { get; private set; }


//        public void Add(IEntity entity)
//        {
//            if (EntitiesCount == _items.Length)
//            {
//                IEntity[] arr = new IEntity[EntitiesCount * 2];
//                for (int i = 0; i < EntitiesCount; i++)
//                {
//                    arr[i] = _items[i];
//                }
//                _items = arr;
//            }

//            _items[EntitiesCount] = entity;
//            EntitiesCount++;
//        }

//        public void Clear()
//        {
//            _items = new IEntity[InitalSize];
//            EntitiesCount = 0;

//        }

//        public bool Contains(IEntity entity)
//        {
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if (entity.Equals(_items[i]))
//                {
//                    return true;
//                }
//            }

//            return false;
//        }

//        public IEntity Extract(int id)
//        {
//            IEntity result = null;
//            int replacedAt = 0;

//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if (_items[i].Id == id)
//                {
//                    result = _items[i];
//                    EntitiesCount--;
//                    replacedAt = i;
//                }
//            }

//            for (int i = replacedAt; i < EntitiesCount - 1; i++)
//            {
//                _items[i] = _items[i + 1];
//            }

//            return result;


//        }

//        public IEntity Find(IEntity entity)
//        {
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if (entity.Equals(_items[i]))
//                {
//                    return _items[i];
//                }
//            }

//            return null;
//        }

//        public List<IEntity> GetAll()
//        {
//            var result = new List<IEntity>();
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if (_items[i] != null)
//                {
//                    result.Add(_items[i]);
//                }
//            }

//            return result;
//        }

//        public IEnumerator<IEntity> GetEnumerator()
//        {
//            for (var i = 0; i < this.EntitiesCount; i++)
//            {
//                yield return this._items[i];
//            }
//        }

//        public void RemoveSold()
//        {
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if (_items[i].Status == BaseEntityStatus.Sold)
//                {
//                    EntitiesCount--;
//                    _items[i] = null;
//                }
//            }
//        }

//        public void Replace(IEntity oldEntity, IEntity newEntity)
//        {
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if (oldEntity.Equals(_items[i]))
//                {
//                    _items[i] = newEntity;
//                    return;
//                }
//            }

//            throw new InvalidOperationException("Entity not found");

//        }

//        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
//        {
//            var result = new List<IEntity>(_items);
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if (_items[i].Status < lowerBound && _items[i].Status > upperBound)
//                {
//                    result.Remove(_items[i]);
//                }

//            }

//            return result;
//        }

//        public void Swap(IEntity first, IEntity second)
//        {
//            var firstEntIndex = IndexOf(first);
//            var SecondEntIndex = IndexOf(second);

//            if (firstEntIndex == -1 || SecondEntIndex == -1)
//            {
//                throw new InvalidOperationException("Entity not found");
//            }

//            _items[firstEntIndex] = second;
//            _items[SecondEntIndex] = first;
//        }

//        private int IndexOf(IEntity item)
//        {
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if (item.Id.Equals(_items[i].Id))
//                {
//                    return i;
//                }
//            }
//            return -1;
//        }

//        public IEntity[] ToArray()
//        {
//            var result = new IEntity[EntitiesCount];
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                result[i] = _items[i];
//            }
//            return result;
//        }

//        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
//        {
//            for (int i = 0; i < EntitiesCount; i++)
//            {
//                if ( _items[i].Status == oldStatus)
//                {
//                    _items[i].Status = newStatus;
//                }
//            }
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return this.GetEnumerator();
//        }


//    }
//}
