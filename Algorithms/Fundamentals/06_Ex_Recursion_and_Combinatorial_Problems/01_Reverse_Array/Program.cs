using System;
using System.Linq;

namespace _01_Reverse_Array
{
    class Program
    {
        private static int[] input;

        static void Main(string[] args)
        {
            input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            
            for (int left = 0; left < input.Length /2; left++)
            {
                var right = input.Length - 1 - left;
                Swap(input, left, right);
            }

            Console.WriteLine(string.Join(" ", input));
           // PrintRecursion(input.Length);
        }

        private static void Swap(int[] input, int left, int right)
        {
            var temp = input[left];
            input[left] = input[right];
            input[right] = temp;
        }

        private static void PrintRecursion(int index)
        {
            index--;
            if (index < 0) return;
            Console.Write($"{input[index]} ");
            PrintRecursion(index);
            
        }
    }
}

