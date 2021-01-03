using System;
using System.Linq;

namespace _06_Merge_Sort
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

            var sorted = MergeSort(numbers);

            Console.WriteLine(string.Join(" ", sorted));
        }

        public static int[] MergeSort(int[] array)
        {
            if (array.Length == 1)
                return array;

            var middleIdx = array.Length / 2;
            var leftHalf = array.Take(middleIdx).ToArray();
            var rightHalf = array.Skip(middleIdx).ToArray();

            return MergeArrays(MergeSort(leftHalf), MergeSort(rightHalf));
        }

        public static int[] MergeArrays(int[] left, int[] right)
        {
            var sorted = new int[left.Length + right.Length];
            var sortedIdx = 0; var leftIdx = 0; var rightIdx = 0;
            while (leftIdx < left.Length && rightIdx < right.Length)
            {
                if (left[leftIdx] < right[rightIdx])
                {
                    sorted[sortedIdx++] = left[leftIdx++];
                }
                else
                {
                    sorted[sortedIdx++] = right[rightIdx++];
                }
            }

            while (leftIdx < left.Length)
            {
                sorted[sortedIdx] = left[leftIdx];
                sortedIdx += 1;
                leftIdx += 1;
            }

            while (rightIdx < right.Length)
            {
                sorted[sortedIdx] = right[rightIdx];
                sortedIdx += 1;
                rightIdx += 1;
            }

            return sorted;
        }
    }
}
