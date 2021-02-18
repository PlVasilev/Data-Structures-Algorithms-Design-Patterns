using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Battle_Points
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
            
            Console.WriteLine(dp[items.Count, maxCapacity]);

            var currentEnergy = maxCapacity;
            for (int i = items.Count - 1; i >= 0; i--)
            {
                if (included[i, currentEnergy])
                {
                    var enemyIndex = i - 1;
                    Console.WriteLine(items[enemyIndex].Name);
                    currentEnergy -= items[enemyIndex].Weight;

                    if (currentEnergy <= 0)
                    {
                        break;
                    }
                }
            }

            // var totalValue = dp[items.Count, maxCapacity];
            // var totalWeight = 0;

            //var includedItems = new SortedSet<Item>(
            //    Comparer<Item>.Create((f, s) => string.Compare(f.Name, s.Name, StringComparison.Ordinal)));
            //for (int row = included.GetLength(0) - 1; row >= 0; row--)
            //{
            //    if (!included[row, maxCapacity]) continue;
            //    var includedItem = items[row - 1];
            //    maxCapacity -= includedItem.Weight;
            //    totalWeight += includedItem.Weight;
            //    includedItems.Add(includedItem);
            //}
            //Console.WriteLine($"Total Weight: {totalWeight}");
            //Console.WriteLine($"Total Value: {totalValue}");
            //foreach (var includedItem in includedItems)
            //    Console.WriteLine(includedItem.Name);
        }

        private static List<Item> ReadItems()
        {
            var energies = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var battlePoints = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var result = new List<Item>();

            for (int i = 0; i < energies.Length; i++)
            {
                result.Add(new Item
                {
                    Name = i.ToString(),
                    Weight = energies[i],
                    Value = battlePoints[i],
                });
            }
            
            return result;
        }
    }
}
