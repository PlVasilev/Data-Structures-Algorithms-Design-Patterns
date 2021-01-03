using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace _04_AS_Sum_with_Limited_Amount_of_Coins
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var target = int.Parse(Console.ReadLine());
            var sums = CalcSums(numbers);

            Console.WriteLine(sums[target]);
        }

        private static Dictionary<int,int> CalcSums(int[] numbers)
        {
            var result = new Dictionary<int,int>{{0, 1}};

            foreach (var number in numbers)
            {
                var sums = result.Keys.ToArray();
                foreach (var sum in sums)
                {
                    var newSum = sum + number;
                    if (!result.ContainsKey(newSum))
                        result.Add(newSum, 1);
                    else
                        result[newSum] += 1;
                }
            }
            return result;
        }
    }
}
