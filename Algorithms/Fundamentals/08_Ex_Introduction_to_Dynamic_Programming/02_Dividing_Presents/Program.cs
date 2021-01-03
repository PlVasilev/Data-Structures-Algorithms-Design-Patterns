using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Dividing_Presents
{
    class Program
    {
        static void Main(string[] args)
        {
            var presents = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var sums = CalkSums(presents);
            var presentSum = presents.Sum();
            var bobScore = (int)Math.Ceiling(presentSum / 2.0) ;

            while (!sums.ContainsKey(bobScore))
                bobScore += 1;

            var alanScore = presentSum - bobScore;

            var alenPresent = GetPresents(sums, alanScore);
            Console.WriteLine($"Difference: {bobScore-alanScore}");
            Console.WriteLine($"Alan:{alanScore} Bob:{bobScore}");
            Console.WriteLine($"Alan takes: {string.Join(" ", alenPresent)}");
            Console.WriteLine($"Bob takes the rest.");
        }

        private static List<int> GetPresents(Dictionary<int, int> sums, int target)
        {
            var presents = new List<int>();
            while (target != 0)
            {
                var present = sums[target];
                presents.Add(present);
                target -= present;
            }
            return presents;
        }

        private static Dictionary<int, int> CalkSums(int[] numbers)
        {
            var result = new Dictionary<int, int> { { 0, 0 } };
            foreach (var number in numbers)
            {
                var sums = result.Keys.ToArray();
                foreach (var sum in sums)
                {
                    var newSum = sum + number;
                    if (!result.ContainsKey(newSum))
                        result.Add(newSum, number);
                }
            }
            return result;
        }
    }
}
