using System.Linq;

namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> _entities;

        public Loader()
        {
            _entities = new List<IEntity>();
        }

        public int EntitiesCount => _entities.Count;

        public void Add(IEntity entity)
        {
            _entities.Add(entity);
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
          return GetById(entity.Id) != null;
        }

        public IEntity Extract(int id)
        {
            IEntity result = GetById(id);
            if (result != null)
            {
                _entities.Remove(result);
            }
            return result;
        }

        private IEntity GetById(int id)
        {
            for (int i = 0; i < EntitiesCount; i++)
            {
                var current = _entities[i];
                if (current.Id == id)
                {
                    return current;
                }
            }
            return null;
        }

        public IEntity Find(IEntity entity)
        {
            return GetById(entity.Id);
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(_entities);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        public void RemoveSold()
        {
            _entities.RemoveAll(x => x.Status == BaseEntityStatus.Sold);
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            int indxOld = _entities.IndexOf(oldEntity);
            if (indxOld == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }
            _entities[indxOld] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            return _entities.Where(x => x.Status >= lowerBound && x.Status <= upperBound).ToList();
        }

        public void Swap(IEntity first, IEntity second)
        {
            int f = _entities.IndexOf(first);
            if (f < 0)
            {
                throw new InvalidOperationException("Entity not found");
            }
            int s = _entities.IndexOf(second);
            if (s < 0)
            {
                throw new InvalidOperationException("Entity not found");
            }

            
            _entities[f] = second;
            _entities[s] = first;

        }

        public IEntity[] ToArray()
        {
            return _entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            for (int i = 0; i < EntitiesCount; i++)
            {
                var current = _entities[i];
                if (current.Status == oldStatus)
                {
                    current.Status = newStatus;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
