using System;
using System.Globalization;

namespace _05_Subset_Sum_WithRepetitions
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = new[] { 3, 5 };
            var target = 14;

            var sums = new bool[target + 1];
            sums[0] = true;

            for (var sum = 0; sum < sums.Length; sum++)
            {
                if (!sums[sum]) continue;
                foreach (var num in nums)
                {
                    var newSum = sum + num;
                    if (newSum <= target)
                        sums[newSum] = true;
                }
            }
            Console.WriteLine(sums[target]);

            while (target > 0)
            {
                foreach (var num in nums)
                {
                    var prev = target - num;
                    if (prev < 0 || !sums[prev]) continue;
                    Console.Write(num + " ");
                    target = prev;
                }
            }
        }
    }
}
