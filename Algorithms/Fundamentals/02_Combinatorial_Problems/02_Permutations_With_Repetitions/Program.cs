using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _02_Permutations_With_Repetitions
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

            for (int i = index + 1; i < elements.Length; i++)
            {
                Swap(index, i);
                Permute(index + 1);
                Swap(index, i);
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
