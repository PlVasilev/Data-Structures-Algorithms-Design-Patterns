using System;
using System.Linq;

namespace _02_Nested_Loops_To_Recursion
{
    class Program
    {
        private static int _length;

        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            _length = num;
            var result = new int[num];
            PrintRecursion(0, result);
        }

        private static void PrintRecursion(int index, int[] result) 
        {
            if (index >= result.Length)
                Console.WriteLine(string.Join(" ", result));
            else
            {
                for (int i = 1; i <= _length; i++)
                {
                    result[index] = i;
                    PrintRecursion(index + 1, result);
                }
            }
        }
    }
}
