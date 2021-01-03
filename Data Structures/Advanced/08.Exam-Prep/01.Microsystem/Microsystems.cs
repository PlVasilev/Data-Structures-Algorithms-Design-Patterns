using System.Globalization;
using System.Linq;

namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;

    public class Microsystems : IMicrosystem
    {
        private HashSet<Computer> computers = 
            new HashSet<Computer>();
        private Dictionary<int, Computer> _byNuber = new Dictionary<int, Computer>();
        private Dictionary<Brand, HashSet<Computer>> _byBrand = 
            new Dictionary<Brand, HashSet<Computer>>();
        Dictionary<string, HashSet<Computer>> _byColor = 
            new Dictionary<string, HashSet<Computer>>();
       


        public void CreateComputer(Computer computer)
        {
            if (_byNuber.ContainsKey(computer.Number))
            {
               throw new ArgumentException(); 
            }
            _byNuber[computer.Number] = computer;

            if (!_byBrand.ContainsKey(computer.Brand))
                _byBrand[computer.Brand] = new HashSet<Computer>();
            _byBrand[computer.Brand].Add(computer);

            if (!_byColor.ContainsKey(computer.Color))
                _byColor[computer.Color] = new HashSet<Computer>();
            _byColor[computer.Color].Add(computer);

            computers.Add(computer);

        }

        public bool Contains(int number)
        {
            return _byNuber.ContainsKey(number);
        }

        public int Count()
        {
            return computers.Count;
        }

        public Computer GetComputer(int number)
        {
            if (!_byNuber.ContainsKey(number))
            {
                throw new ArgumentException();
            }
            return _byNuber[number];
        }

        public void Remove(int number)
        {
            if (!_byNuber.ContainsKey(number))
                throw new ArgumentException();

            var computer = _byNuber[number];

            computers.Remove(computer);
            _byNuber.Remove(number);
            _byBrand[computer.Brand].Remove(computer);
            _byColor[computer.Color].Remove(computer);

            if (_byBrand[computer.Brand].Count == 0)
                _byBrand.Remove(computer.Brand);

            if (_byColor[computer.Color].Count == 0)
                _byColor.Remove(computer.Color);
            
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!_byBrand.ContainsKey(brand))
            {
                throw new ArgumentException();
            }
            var brands = _byBrand[brand].Select(c => c.Number).ToList();
            foreach (var computer in brands) 
                Remove(computer);
        }

        public void UpgradeRam(int ram, int number)
        {
            var computer = GetComputer(number);
            if (ram > computer.RAM) 
                computer.RAM = ram;
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            if (!_byBrand.ContainsKey(brand))
                return Enumerable.Empty<Computer>();
            return _byBrand[brand].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            return computers.Where(c => c.ScreenSize == screenSize).OrderByDescending(c => c.Number);
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            if (!_byColor.ContainsKey(color))
            {
                return Enumerable.Empty<Computer>();
            }
            return _byColor[color].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            return computers.Where(c => minPrice <= minPrice && c.Price <= maxPrice)
                .OrderByDescending(c => c.Price);
        }


        //internal class ComputerComparerNumber : IComparer<Computer>
        //{

        //    public int Compare(Computer x, Computer y)
        //    {
        //        var cmp = y.Number.CompareTo(x.Number);
        //        return cmp == 0 ? y.Price.CompareTo(x.Price) : cmp;
        //    }
        //}

        //internal class ComputerComparerDouble : IComparer<double>
        //{

        //    public int Compare(double x, double y)
        //    {
        //        return y.CompareTo(x);
        //    }
        //}

        //internal class ComputerComparerPrice : IComparer<Computer>
        //{
        //    public int Compare(Computer x, Computer y)
        //    {
        //        var cmp = y.Price.CompareTo(x.Price);
        //        return cmp == 0 ? y.Number.CompareTo(x.Number) : cmp;
        //    }
        //}
    }

    //====================================================================================================================
    //public class Microsystems : IMicrosystem
    //{
    //    SortedDictionary<int, Computer> _allComputers = new SortedDictionary<int, Computer>();
    //    Dictionary<Brand, SortedSet<Computer>> _byBrand = new Dictionary<Brand, SortedSet<Computer>>();
    //    Dictionary<double, SortedSet<Computer>> _bySreanSize = new Dictionary<double, SortedSet<Computer>>();
    //    Dictionary<string, SortedSet<Computer>> _byColor = new Dictionary<string, SortedSet<Computer>>();
    //    Dictionary<double, HashSet<Computer>> _byPrice = new Dictionary<double, HashSet<Computer>>();

    //    public Microsystems()
    //    {
    //        _byBrand[Brand.ACER] = new SortedSet<Computer>(new ComputerComparerPrice());
    //        _byBrand[Brand.ASUS] = new SortedSet<Computer>(new ComputerComparerPrice());
    //        _byBrand[Brand.DELL] = new SortedSet<Computer>(new ComputerComparerPrice());
    //        _byBrand[Brand.HP] = new SortedSet<Computer>(new ComputerComparerPrice());
    //    }

    //    public void CreateComputer(Computer computer)
    //    {
    //        if (_allComputers.ContainsKey(computer.Number))
    //        {
    //            throw new ArgumentException();
    //        }
    //        _allComputers[computer.Number] = computer;

    //        if (!_byPrice.ContainsKey(computer.Price))
    //        {
    //            _byPrice[computer.Price] = new HashSet<Computer>();
    //        }
    //        _byPrice[computer.Price].Add(computer);


    //        if (!_byBrand.ContainsKey(computer.Brand))
    //        {
    //            _byBrand[computer.Brand] = new SortedSet<Computer>(new ComputerComparerPrice());
    //        }
    //        _byBrand[computer.Brand].Add(computer);

    //        if (!_bySreanSize.ContainsKey(computer.ScreenSize))
    //        {
    //            _bySreanSize[computer.ScreenSize] = new SortedSet<Computer>(new ComputerComparerNumber());
    //        }
    //        _bySreanSize[computer.ScreenSize].Add(computer);

    //        if (!_byColor.ContainsKey(computer.Color))
    //        {
    //            _byColor[computer.Color] = new SortedSet<Computer>(new ComputerComparerPrice());
    //        }
    //        _byColor[computer.Color].Add(computer);
    //    }

    //    public bool Contains(int number)
    //    {
    //        return _allComputers.ContainsKey(number);
    //    }

    //    public int Count()
    //    {
    //        return _allComputers.Count;
    //    }

    //    public Computer GetComputer(int number)
    //    {
    //        if (!Contains(number))
    //        {
    //            throw new ArgumentException();
    //        }
    //        return _allComputers[number];
    //    }

    //    public void Remove(int number)
    //    {
    //        var computer = GetComputer(number);
    //        if (computer == null)
    //            throw new ArgumentException();

    //        _allComputers.Remove(number);
    //        _bySreanSize[computer.ScreenSize].Remove(computer);
    //        _byBrand[computer.Brand].Remove(computer);
    //        _byColor[computer.Color].Remove(computer);
    //    }

    //    public void RemoveWithBrand(Brand brand)
    //    {
    //        if (_byBrand[brand].Count == 0)
    //        {
    //            throw new ArgumentException();
    //        }

    //        var brands = _byBrand[brand].Select(c => c.Number).ToList();

    //        foreach (var computer in brands)
    //        {
    //            Remove(computer);
    //        }

    //        // for (int i = 0; i < brands.Count(); i++)
    //        // {
    //        //     _allComputers.Remove(brands[i].Number);
    //        //
    //        //     _bySreanSize[brands[i].ScreenSize].Remove(brands[i]);
    //        //     if (_bySreanSize[brands[i].ScreenSize].Count == 0)
    //        //     {
    //        //         _bySreanSize.Remove(brands[i].ScreenSize);
    //        //     }
    //        //     
    //        //     _byColor[brands[i].Color].Remove(brands[i]);
    //        //     if (_byColor[brands[i].Color].Count == 0)
    //        //     {
    //        //         _byColor.Remove(brands[i].Color);
    //        //     }
    //        //
    //        //     _byPrice[brands[i].Price].Remove(brands[i]);
    //        //     if (_byPrice[brands[i].Price].Count == 0)
    //        //     {
    //        //         _byPrice.Remove(brands[i].Price);
    //        //     }
    //        // }
    //        // _byBrand.Clear();
    //    }

    //    public void UpgradeRam(int ram, int number)
    //    {
    //        var computer = GetComputer(number);
    //        if (computer == null)
    //            throw new ArgumentException();
    //        if (ram > computer.RAM)
    //        {
    //            computer.RAM = ram;
    //        }

    //    }

    //    public IEnumerable<Computer> GetAllFromBrand(Brand brand)
    //    {

    //        return _byBrand[brand];
    //    }

    //    public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
    //    {
    //        if (!_bySreanSize.ContainsKey(screenSize))
    //        {
    //            return new SortedSet<Computer>();
    //        }
    //        return _bySreanSize[screenSize];
    //    }

    //    public IEnumerable<Computer> GetAllWithColor(string color)
    //    {
    //        if (!_byColor.ContainsKey(color))
    //        {
    //            return new SortedSet<Computer>();
    //        }
    //        return _byColor[color];
    //    }

    //    public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
    //    {
    //        SortedSet<double> prices = new SortedSet<double>(_byPrice.Keys);
    //        var resultKeys = prices.GetViewBetween(minPrice, maxPrice).Reverse();
    //        var result = resultKeys.SelectMany(c => _byPrice[c]).OrderByDescending(c => c.Price).ToList();
    //        return result;
    //        // resultKeys.SelectMany(k => byAgeTown[k].GetValuesForKey(town).OrderBy(p => p.Email));
    //    }


    //    internal class ComputerComparerNumber : IComparer<Computer>
    //    {

    //        public int Compare(Computer x, Computer y)
    //        {
    //            var cmp = y.Number.CompareTo(x.Number);
    //            return cmp == 0 ? y.Price.CompareTo(x.Price) : cmp;
    //        }
    //    }

    //    internal class ComputerComparerDouble : IComparer<double>
    //    {

    //        public int Compare(double x, double y)
    //        {
    //            return y.CompareTo(x);
    //        }
    //    }

    //    internal class ComputerComparerPrice : IComparer<Computer>
    //    {
    //        public int Compare(Computer x, Computer y)
    //        {
    //            var cmp = y.Price.CompareTo(x.Price);
    //            return cmp == 0 ? y.Number.CompareTo(x.Number) : cmp;
    //        }
    //    }
    //}
}
