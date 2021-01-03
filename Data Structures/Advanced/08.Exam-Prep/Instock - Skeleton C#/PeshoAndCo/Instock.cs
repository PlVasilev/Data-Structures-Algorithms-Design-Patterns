using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Wintellect.PowerCollections;
using System.Linq;
using System.Runtime.CompilerServices;

public class Instock : IProductStock
{
    private int counter = 0;
    private List<Product> _listProducts = new List<Product>();
    private SortedSet<Product> _setProducts = new SortedSet<Product>(new CompareByNameNumber());
    private Dictionary<string, Product> _byLabel = new Dictionary<string, Product>();
    private Dictionary<int, HashSet<Product>> _byQuant = 
        new Dictionary<int, HashSet<Product>>();
    public int Count => _listProducts.Count;

    public void Add(Product product)
    {

        if (!Contains(product))
        {
            product.Time = counter;
            _setProducts.Add(product);
            _listProducts.Add(product);
            _byLabel[product.Label] = product;
            if (!_byQuant.ContainsKey(product.Quantity))
            {
                _byQuant[product.Quantity] = new HashSet<Product>();
            }
            _byQuant[product.Quantity].Add(product);
            counter++;
        }

    }

    public void ChangeQuantity(string product, int quantity)
    {
        var current = FindByLabel(product);
        if (quantity == current.Quantity)
        {
            return;
        }
        counter++;
        
        _byQuant[current.Quantity].Remove(current);

        if (_byQuant[current.Quantity].Count == 0)
        {
            _byQuant.Remove(current.Quantity);
        }
        current.Quantity = quantity;
        current.Time = counter;
        if (!_byQuant.ContainsKey(quantity))
        {
            _byQuant[quantity] = new HashSet<Product>();
        }
        _byQuant[quantity].Add(current);

    }

    public bool Contains(Product product)
    {
        return _byLabel.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (index < 0 || index >= _listProducts.Count)
        {
            throw new IndexOutOfRangeException();
        }
        return _listProducts[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        var result = _listProducts.Where(x => x.Price == price);
        return result;
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        if (!_byQuant.ContainsKey(quantity))
        {
            return Enumerable.Empty<Product>();
        }
        
        return _byQuant[quantity].OrderBy(x => x.Time);
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        return _setProducts.Reverse().Where(x => x.Price > lo && x.Price <= hi);
    }

    public Product FindByLabel(string label)
    {
        if (!_byLabel.ContainsKey(label))
        {
            throw new ArgumentException();
        }

        return _byLabel[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (_byLabel.Count < count)
        {
            throw new ArgumentException();
        }
        var result = _byLabel.Values.Take(count).ToList();
        return result;
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (_byLabel.Count < count)
        {
            throw new ArgumentException();
        }
        return _setProducts.OrderByDescending(x => x.Price).Take(count);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        return _listProducts.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    internal class CompareByNameNumber : IComparer<Product>
    {
    
        public int Compare(Product x, Product y)
        {
            var cmp = y.Quantity.CompareTo(x.Quantity);
            return cmp == 0 ? x.Time.CompareTo(y.Time) : cmp;
        }
    }
    //
    //internal class ComputerComparerDouble : IComparer<Product>
    //{
    //
    //    public int Compare(double x, double y)
    //    {
    //        return y.CompareTo(x);
    //    }
    //}
}
