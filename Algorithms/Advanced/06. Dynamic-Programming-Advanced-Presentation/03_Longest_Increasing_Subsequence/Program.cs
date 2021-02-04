using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_Longest_Increasing_Subsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var length = new int[numbers.Length];
            var prev = new int[numbers.Length];

            var bestLength = 0;
            var lastIndex = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                prev[i] = -1;
                var currentNumber = numbers[i];
                var currentBestSq = 1;

                for (int j = i - 1; j >= 0; j--)
                {
                    var prevNumber = numbers[j];
                    if (prevNumber < currentNumber && length[j] + 1 >= currentBestSq) // > right most >= left most
                    {
                        currentBestSq = length[j] + 1;
                        prev[i] = j;
                    }
                }
                if (currentBestSq > bestLength)
                {
                    bestLength = currentBestSq;
                    lastIndex = i;
                }
                length[i] = currentBestSq;
            }
            var lis = new Stack<int>();
            while (lastIndex != -1)
            {
                lis.Push(numbers[lastIndex]);
                lastIndex = prev[lastIndex];
            }
            Console.WriteLine(string.Join(" ",lis));
        }
    }
}
