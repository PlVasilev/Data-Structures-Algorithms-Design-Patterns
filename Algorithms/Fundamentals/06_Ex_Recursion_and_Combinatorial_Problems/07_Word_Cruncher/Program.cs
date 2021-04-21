using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _07_Word_Cruncher
{
    class Program
    {
        private static string[] elements;
        private static int k;
        private static string target;
        private static string[] variations;
        private static bool[] used;
        private static List<List<string>> results = new List<List<string>>();

        static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(", ").ToHashSet();
            elements = input.ToArray();
            target = Console.ReadLine();

            for (int i = 1; i <= elements.Length; i++)
            {
                variations = new string[i];
                used = new bool[elements.Length];

                Variations(0);
            }

            for (int i = results.Count - 1; i >= 0; i--)
            {
                Console.WriteLine(string.Join(" ", results[i]));
            }

        }

        private static void Variations(int index)
        {
            if (index >= variations.Length)
            {
                if (string.Join("", variations) == target)
                {
                  //  results.Add(variations.ToList());
                    Console.WriteLine(string.Join(" ", variations));
                }
                return;
            }

            for (int i = 0; i < elements.Length; i++)
            {
                if (!used[i])
                {
                    used[i] = true;
                    variations[index] = elements[i];
                    Variations(index + 1);
                    used[i] = false;
                }
            }
        }
    }
}
