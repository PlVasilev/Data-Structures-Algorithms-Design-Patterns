using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Subset_Sum
{
    class Program
    {
        private static bool _isEmpty = true;
        static void Main(string[] args)
        {
            var nums = new[] { 3, 5, 1, 4, 2 };
            var target = 7;
            // var sums = GetAllSums(nums);
            // Console.WriteLine(sums.Contains(target));
            // Console.WriteLine(string.Join(" ", sums));
            //0 3 5 8 1 4 6 9 7 12 10 13 2 11 14 15
            //3 8 4 9 7 12 13 5 10 6 11 14 15 - 2 0 1
            var sums = GetAllSumsDict(nums);
            if (!sums.ContainsKey(target))
            {
                Console.WriteLine("Sum not Existing");
                return;
                
            }
            while (target != 0)
            {
                var number = sums[target];
                target -= number;
                Console.WriteLine(number);
            }

        }

        private static Dictionary<int,int> GetAllSumsDict(int[] nums)
        {
            var sums = new Dictionary<int, int> {{0,0}};

            foreach (var num in nums)
            {
                var currentSums = sums.Keys.ToArray();
                foreach (var sum in currentSums)
                {
                    var newSum = sum + num;
                    if (!sums.ContainsKey(newSum))
                    {
                        sums.Add(newSum, num);
                    }
                }
            }
            return sums;
        }

        private static HashSet<int> GetAllSums(int[] nums)
        {
            var sums = new HashSet<int>{0};

            foreach (var num in nums)
            {
                var newSums = new HashSet<int>();
                foreach (var sum in sums)
                {
                    var newSum = sum + num;
                    newSums.Add(newSum);
                }
                //if (_isEmpty == true && newSums.Count > 0)
                //{
                //    sums.Remove(0);
                //    _isEmpty = false;
                //}
                sums.UnionWith(newSums);
             
            }
            return sums;
        }
    }
}
