using System;
using System.Collections.Generic;

namespace _05_Word_Differences
{
    class Program
    {
        static void Main(string[] args)
        {
            var input1 = Console.ReadLine();
            var input2 = Console.ReadLine();

            var table = new int[input1.Length + 1, input2.Length + 1];
            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (input1[r - 1] == input2[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max(table[r - 1, c], table[r, c - 1]);
                    }
                }
            }
            // Console.WriteLine(table[input1.Length, input2.Length]);

            var row = input1.Length;
            var col = input2.Length;
            var stack = new Stack<char>();

            while (row > 0 && col > 0)
            {
                if (input1[row - 1] == input2[col - 1])
                {
                    row -= 1;
                    col -= 1;
                    stack.Push(input1[row]);
                }
                else if (table[row - 1, col] > table[row, col - 1])
                {
                    row -= 1;
                }
                else
                {
                    col -= 1;
                }
            }

            Console.WriteLine($"Deletions and Insertions: {(input2.Length - stack.Count) * 2}");
        }
    }
}
