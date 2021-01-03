using System;
using System.Linq;

namespace _03_Bubble_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            BubleSort(numbers);

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void BubleSort(int[] numbers)
        {
            var isSorted = false;
            var i = 0;
            while (!isSorted)
            {
                isSorted = true;
                for (int j = 1; j < numbers.Length - i; j++)
                {
                    if (numbers[j - 1] > numbers[j])
                    {
                        isSorted = false;
                        Swap(numbers, j - 1, j);
                    }
                }
                i += 1;
            }


        }

        private static void Swap(int[] numbers, in int first, in int second)
        {
            var temp = numbers[first];
            numbers[first] = numbers[second];
            numbers[second] = temp;
        }
    }
}
