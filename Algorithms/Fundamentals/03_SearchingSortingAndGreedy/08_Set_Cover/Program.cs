using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace _08_Set_Cover
{
    class Program
    {
        static void Main(string[] args)
        {
            var universe = Console.ReadLine().Split(", ").Select(int.Parse).ToList();
            var sets = new List<int[]>();
            var num = int.Parse(Console.ReadLine());
            for (int i = 0; i < num; i++)
            {
                sets.Add(Console.ReadLine().Split(", ").Select(int.Parse).ToArray());
            }

            var selectedSets = new List<int[]>();
            while (universe.Count > 0)
            {
                var currentSet = sets
                    .OrderByDescending(s => s.Count(e => universe.Contains(e)))
                    .FirstOrDefault();
                foreach (var number in currentSet)
                { universe.Remove(number); }

                sets.Remove(currentSet);
                selectedSets.Add(currentSet);
            }

            Console.WriteLine($"Sets to take ({selectedSets.Count}):");
            for (int i = 0; i < selectedSets.Count; i++)
            {
                Console.WriteLine(string.Join(", ", selectedSets[i]));
            }

        }
    }
}
