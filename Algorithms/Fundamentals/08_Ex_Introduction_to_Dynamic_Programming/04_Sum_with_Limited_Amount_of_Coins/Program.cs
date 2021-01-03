using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Sum_with_Limited_Amount_of_Coins
{
    class Program
    {
        static void Main(string[] args)
        {
            var nums = Console.ReadLine().Split().Select(int.Parse).OrderBy(x => x).ToArray();
            var target = int.Parse(Console.ReadLine());
            Console.WriteLine(GetAllSumsDict(nums, target));
        }

        private static int GetAllSumsDict(int[] nums, int target)
        {
            var sums = 0;
            List<List<int>> list =new List<List<int>>();

            for (int i = nums.Length -1; i >= 0; i--)
            {
                var currentSums = 0;
                var currentList = new List<int>();
                for (int j = nums.Length - i - 1; j >= 0; j--)
                {
                    
                    if (currentSums + nums[j] > target)
                    {
                        continue;
                    }
                    currentList.Add(nums[j]);
                    currentSums += nums[j];
                    if (currentSums == target)
                    {
                        list.Add(currentList);
                        sums++;
                        break;
                    }
                }
            }

            foreach (var el in list)
            {
                Console.WriteLine($"{target} = " + string.Join(" + ", el));
            }
            return sums;
        }
    }
}

