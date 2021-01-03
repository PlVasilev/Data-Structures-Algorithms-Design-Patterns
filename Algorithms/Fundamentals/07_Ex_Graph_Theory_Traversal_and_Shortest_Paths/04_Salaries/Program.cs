using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Salaries
{
    class Program
    {
        private static Dictionary<int, List<int>> _empolees = new Dictionary<int, List<int>>();
        private static Dictionary<int, int> _empoleeSalary = new Dictionary<int, int>();
        private static int _result;
        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());

            for (int i = 0; i < num; i++)
            {
                var toAdd = new List<int>();
                var input = Console.ReadLine();
                for (int j = 0; j < num; j++)
                {
                    if (input[j] == 'Y')
                    {
                        toAdd.Add(j);
                    }
                }
                _empolees[i] = toAdd;
            }

            for (int i = 0; i < _empolees.Count; i++)
            {
                _empoleeSalary[i] = GetEmployeeSalary(i);
            }

            Console.WriteLine(_empoleeSalary.Values.Sum());

            //for (int i = 0; i < _empolees.Count; i++)
            //{
            //    if (_empolees[i].Count == 0)
            //    {
            //        _result++;
            //        continue;
            //    }
            //    foreach (var empoleesKey in _empolees[i])
            //        GetSlalary(empoleesKey);
            //}
            //Console.WriteLine(_result);
        }

        private static int GetEmployeeSalary(in int i)
        {
            if (_empoleeSalary.ContainsKey(i))
            {
                return _empoleeSalary[i];
            }
            var children = _empolees[i];
            if (children.Count == 0)
            {
                return 1;
            }

            var salary = 0;
            foreach (var child in children)
            {
                salary += GetEmployeeSalary(child);
            }
            return salary;
        }

        private static void GetSlalary(int key)
        {
            if (_empolees[key].Count == 0)
            {
                _result++;
                return;
            }
            foreach (var i in _empolees[key])
                GetSlalary(i);
        }
    }
}
