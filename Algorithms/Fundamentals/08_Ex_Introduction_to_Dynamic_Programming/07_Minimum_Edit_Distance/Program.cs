using System;
using System.Collections.Generic;

namespace _07_Minimum_Edit_Distance
{
    class Program
    {
        static void Main(string[] args)
        {
            var replace = int.Parse(Console.ReadLine());
            var insert = int.Parse(Console.ReadLine());
            var delete = int.Parse(Console.ReadLine());

            var input = Console.ReadLine();
            var target = Console.ReadLine();

            var table = new int[target.Length + 1, input.Length + 1];
            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (target[r - 1] == input[c - 1])
                    {
                        table[r, c] = table[r - 1, c - 1] + 1;
                    }
                    else
                    {
                        table[r, c] = Math.Max(table[r - 1, c], table[r, c - 1]);
                    }
                }
            }

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    Console.Write(table[i, j] + " ");
                }
                Console.WriteLine();
            }

            var row = target.Length;
            var col = input.Length;
            var stack = new Stack<char>();

            while (row > 0 && col > 0)
            {
                if (target[row - 1] == input[col - 1])
                {
                    row -= 1;
                    col -= 1;
                    stack.Push(target[row]);
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

            var result = 0;
            var common = string.Join("", stack);
            if (replace < delete + insert)
            {
                if (input.Length > target.Length)
                {
                    result += (target.Length - common.Length) * replace;
                    result += (input.Length - target.Length) * delete;
                }
                else if (input.Length < target.Length)
                {
                    result += (input.Length - common.Length) * replace;
                    result += (target.Length - input.Length) * insert;
                }
                else
                {
                    result += (target.Length - common.Length) * replace;
                }
            }
            else
            {
                result += Math.Abs((common.Length - input.Length) * delete);
                result += Math.Abs((common.Length - target.Length) * insert);
            }
            Console.WriteLine($"Minimum edit distance: {result}");
        }
    }
}
