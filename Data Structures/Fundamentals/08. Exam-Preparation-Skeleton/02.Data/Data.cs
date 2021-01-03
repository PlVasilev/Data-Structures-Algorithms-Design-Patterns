using System.Linq;
using Wintellect.PowerCollections;

namespace _02.Data
{
    using _02.Data.Interfaces;
    using System;
    using System.Collections.Generic;

    public class Data : IRepository
    {
        private OrderedBag<IEntity> data;
        

        public Data()
        {
            data = new OrderedBag<IEntity>(Decending);
        }
        
        public Data(Data copy)
        {
            this.data = copy.data;
        }

        public int Size => data.Count;

        public void Add(IEntity entity)
        {
            data.Add(entity);

            var parent = GetById((int) entity.ParentId);
            if (parent != null)
            {
                parent.Children.Add(entity);
            }
        }

        public IRepository Copy()
        {

            var a = (Data)this.MemberwiseClone();
            return new Data(a);
            // var result = new Data();
            // result.data = data;
            // return result;
        }

        public IEntity DequeueMostRecent()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
            return data.RemoveFirst();
        }

        public List<IEntity> GetAll()
        {
            return data.AsList().ToList();
        }

        public List<IEntity> GetAllByType(string type)
        {
            // var myType = data[0].GetType().Name;
            if (type == "Invoice" || type == "StoreClient" || type == "User")
            {
               // var currentType = "_02.Data.Models." + type;
                return data.Where(x => x.GetType().Name == type).ToList();
            }

            throw new InvalidOperationException("Invalid type: " + type);
        }

        public IEntity GetById(int id)
        {
           if (id < 0 || id >= Size)
           {
               return null;
           }
          
           return data[Size - 1 - id];
            // return data.FirstOrDefault(x => x.Id == id);
        }

        public List<IEntity> GetByParentId(int parentId)
        {
             // return data.Where(x => x.ParentId == parentId).ToList();

             var parent = GetById(parentId);
             return parent.Children;
        }

        public IEntity PeekMostRecent()
        {
            if (Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
            return data.GetFirst();
        }

        int Decending(IEntity first, IEntity second)
        {
            return second.Id - first.Id;
        }

  
    }
}
