using System;
using System.Linq;

namespace _05_Quicksort
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            QuickSort(numbers,0,numbers.Length - 1);

            Console.WriteLine(string.Join(" ", numbers));
        }

        private static void QuickSort(int[] array, int startIdx, in int endIdx)
        {
            if (startIdx >= endIdx)
                return;
            var pivotIdx = startIdx;
            var leftIdx = startIdx + 1;
            var rightIdx = endIdx;
            while (leftIdx <= rightIdx)
            {
                if (array[leftIdx] > array[pivotIdx] &&
                    array[rightIdx] < array[pivotIdx])
                {
                    Swap(array, leftIdx, rightIdx);
                }

                if (array[leftIdx] <= array[pivotIdx])
                {
                    leftIdx += 1;
                }

                if (array[rightIdx] >= array[pivotIdx])
                {
                    rightIdx -= 1;
                }

            }

            Swap(array, pivotIdx, rightIdx);

            var isLeftSubArraysSmaller =
                rightIdx - 1 - startIdx < endIdx - (rightIdx + 1);
            if (isLeftSubArraysSmaller)
            {
                QuickSort(array, startIdx, rightIdx - 1);
                QuickSort(array, rightIdx + 1, endIdx);
            }
            else
            {
                QuickSort(array, rightIdx + 1, endIdx);
                QuickSort(array, startIdx, rightIdx - 1);
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
