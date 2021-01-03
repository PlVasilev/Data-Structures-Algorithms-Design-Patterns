using System;

namespace _07_NChooseKCount
{
    // Pascal's Triangle
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var k = int.Parse(Console.ReadLine());

            Console.WriteLine(Binom(n,k));
        }
        static long Binom(int n, int k)
        {
            if (n <= 1)
                return 1;
            if (k == 0 || k == n)
                return 1;
            return Binom(n - 1, k) + Binom(n - 1, k - 1);
        }

    }
}
