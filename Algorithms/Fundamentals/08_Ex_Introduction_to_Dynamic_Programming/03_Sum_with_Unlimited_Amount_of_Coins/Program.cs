using System;
using System.Linq;

namespace _03_Sum_with_Unlimited_Amount_of_Coins
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var target = int.Parse(Console.ReadLine());
            var count = CalcSums(numbers, target);

            Console.WriteLine(count);
        }

        private static int CalcSums(int[] numbers, int target)
        {
            var sums = new int[target + 1];
            sums[0] = 1;

            foreach (var number in numbers)
            {
                for (int sum = number; sum < sums.Length; sum++)
                {
                    sums[sum] += sums[sum - number];
                }
            }
            return sums[target];
        }
    }
}
