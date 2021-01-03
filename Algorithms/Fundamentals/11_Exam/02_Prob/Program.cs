using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Prob
{
    class Program
    {
        static void Main(string[] args)
        {
            var input1 = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var input2 = Console.ReadLine().Split().Select(int.Parse).ToArray();

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
            Console.WriteLine(table[input1.Length, input2.Length]);
        }
    }
}
