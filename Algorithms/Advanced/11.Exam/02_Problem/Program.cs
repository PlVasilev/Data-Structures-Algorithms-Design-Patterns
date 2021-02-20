using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Problem
{
    class Program
    {
        class Box
        {
            public int Width { get; set; }
            public int Depth { get; set; }

            public int Height { get; set; }

            public override string ToString()
            {
                return $"{Width} {Depth} {Height}";
            }
        }

        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            var numbers = new List<Box>();
            for (int i = 0; i < num; i++)
            {
                var boxData = Console.ReadLine().Split().Select(int.Parse).ToArray();
                numbers.Add(new Box
                {
                    Width = boxData[0],
                    Depth = boxData[1],
                    Height = boxData[2]
                });
            }
            var length = new int[numbers.Count];
            var prev = new int[numbers.Count];

            var bestLength = 0;
            var lastIndex = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                prev[i] = -1;
                var currentNumber = numbers[i];
                var currentBestSq = 1;

                for (int j = i - 1; j >= 0; j--)
                {
                    var prevNumber = numbers[j];
                    if (prevNumber.Depth < currentNumber.Depth
                        && prevNumber.Width < currentNumber.Width
                        && prevNumber.Height < currentNumber.Height
                        && length[j] + 1 >= currentBestSq) // > right most >= left most
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
            var lis = new Stack<Box>();
            while (lastIndex != -1)
            {
                lis.Push(numbers[lastIndex]);
                lastIndex = prev[lastIndex];
            }
            Console.WriteLine(string.Join("\n", lis));
        }
    }
}
