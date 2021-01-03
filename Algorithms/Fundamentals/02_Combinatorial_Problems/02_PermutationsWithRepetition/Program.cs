using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_PermutationsWithRepetition
{
    class Program
    {
        private static string[] elements;

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split().ToArray();

            Permute(0);

        }

        private static void Permute(int index)
        {
            if (index >= elements.Length)
            {
                Console.WriteLine(string.Join(" ", elements));
                return;
            }
            Permute(index + 1);
            var swapped = new HashSet<string>{elements[index]};

            for (int i = index + 1; i < elements.Length; i++)
            {
                if (!swapped.Contains(elements[i]))
                {
                    Swap(index, i);
                    Permute(index + 1);
                    Swap(index, i);
                    swapped.Add(elements[i]);
                }
            }
        }

        private static void Swap(in int first, in int second)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }
    }
}
