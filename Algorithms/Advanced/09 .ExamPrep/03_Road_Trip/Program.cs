using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_Road_Trip
{
    public class Item
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var items = ReadItems();

            var maxCapacity = int.Parse(Console.ReadLine());
            var included = new bool[items.Count + 1, maxCapacity + 1];

            var dp = new int[items.Count + 1, maxCapacity + 1];
            for (int row = 1; row < dp.GetLength(0); row++)
            {
                var currentItem = items[row - 1];
                for (int capacity = 1; capacity < dp.GetLength(1); capacity++)
                {
                    var skip = dp[row - 1, capacity];
                    if (currentItem.Weight > capacity)
                    {
                        dp[row, capacity] = skip;
                        continue;
                    }
                    var take = currentItem.Value + dp[row - 1, capacity - currentItem.Weight];
                    if (take > skip)
                    {
                        dp[row, capacity] = take;
                        included[row, capacity] = true;
                    }
                    else dp[row, capacity] = skip;
                }
            }

            Console.WriteLine($"Maximum value: {dp[items.Count, maxCapacity]}");
        }

        private static List<Item> ReadItems()
        {
            var value = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
            var space = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();

            var result = new List<Item>();

            for (int i = 0; i < value.Length; i++)
            {
                result.Add(new Item
                {
                    Name = i.ToString(),
                    Weight = space[i],
                    Value = value[i],
                });
            }

            return result;
        }
    }
}
