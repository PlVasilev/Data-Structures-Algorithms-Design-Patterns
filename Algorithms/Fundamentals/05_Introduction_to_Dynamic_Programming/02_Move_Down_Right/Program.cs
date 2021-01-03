using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _02_Move_Down_Right
{
    class Program
    {
        static void Main(string[] args)
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());

            var numbers = new int[rows, cols];

            for (var i = 0; i < rows; i++)
            {
                var elements = Console.ReadLine().Split().Select(int.Parse).ToArray();

                for (var j = 0; j < elements.Length; j++)
                {
                    numbers[i, j] = elements[j];
                }
            }

            var sums = new int[rows, cols];
            sums[0, 0] = numbers[0, 0];

            for (int c = 1; c < cols; c++)
            {
                sums[0, c] = sums[0, c - 1] + numbers[0, c];
            }

            for (int r = 1; r < rows; r++)
            {
                sums[r, 0] = sums[r-1, 0] + numbers[r, 0];
            }

            for (int r = 1; r < sums.GetLength(0); r++)
            {
                for (int c = 1; c < sums.GetLength(1); c++)
                {
                    var upperCell = sums[r - 1, c];
                    var leftCell = sums[r, c - 1];
                    sums[r, c] = Math.Max(upperCell, leftCell) + numbers[r, c];
                }
            }

            // Console.WriteLine(sums[rows-1,cols-1]);
            var rowInd = rows - 1;
            var colInd = cols - 1;
            var path = new Stack<string>();
            path.Push( $"[{rowInd}, {colInd}]");

            while (rowInd > 0 && colInd > 0)
            {
                var upperCell = sums[rowInd - 1, colInd];
                var leftCell = sums[rowInd, colInd - 1];
                if (upperCell > leftCell)
                {
                    rowInd -= 1;
                }
                else
                {
                    colInd -= 1;
                }
                path.Push($"[{rowInd}, {colInd}]");
            }

            while (rowInd > 0)
            {
                rowInd -= 1;
                path.Push($"[{rowInd}, {colInd}]");
            }

            while (colInd > 0)
            {
                colInd -= 1;
                path.Push($"[{rowInd}, {colInd}]");
            }
            Console.WriteLine(string.Join(" ", path));
        }
    }
}
