using System;

namespace _05_AS_Word_Differences
{
    class Program
    {
        static void Main(string[] args)
        {
            var input1 = Console.ReadLine();
            var input2 = Console.ReadLine();

            var table = new int[input1.Length + 1, input2.Length + 1];
            for (int r = 1; r < table.GetLength(0); r++)
                table[r, 0] = r;
            
            for (int c = 1; c < table.GetLength(1); c++)
                table[0,c] = c;

            for (int r = 1; r < table.GetLength(0); r++)
            {
                for (int c = 1; c < table.GetLength(1); c++)
                {
                    if (input1[r - 1] == input2[c - 1])
                        table[r, c] = table[r - 1, c - 1];
                    
                    else
                        table[r, c] = Math.Min(table[r, c - 1], table[r - 1, c]) + 1;
                }
            }
            Console.WriteLine($"Deletions and Insertions: {table[input1.Length, input2.Length]}");
        }
    }
}
