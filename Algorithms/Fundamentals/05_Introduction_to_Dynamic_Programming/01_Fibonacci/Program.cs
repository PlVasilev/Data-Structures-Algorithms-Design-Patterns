using System;
using System.Collections.Generic;

namespace _01_Fibonacci
{
    class Program
    {
        private static Dictionary<int, long> _memo = new Dictionary<int, long>();

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            Console.WriteLine(GetFibonacci(n));
        }

        private static long GetFibonacci(in int n)
        {
            if (_memo.ContainsKey(n))
                return _memo[n];
            
            if (n <= 2)
                return 1;
            
            var result = GetFibonacci(n - 1) + GetFibonacci(n - 2);
            _memo[n] = result;

            return result;
        }
    }
}
