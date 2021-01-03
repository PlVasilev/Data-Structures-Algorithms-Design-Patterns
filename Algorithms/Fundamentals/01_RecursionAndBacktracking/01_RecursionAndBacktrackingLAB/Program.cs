using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _01_RecursionAndBacktrackingLAB
{
    class Program
    {
        static void Main(string[] args)
        {
            // GetSum(int.Parse(Console.ReadLine() ?? string.Empty));
            // Console.WriteLine(Factorial(int.Parse(Console.ReadLine() ?? string.Empty)));
            // int num = int.Parse(Console.ReadLine() ?? string.Empty);
            // Gen01(0, new int[num]);
             LabirinthPaths();
            //Console.WriteLine(GetFibunacci(int.Parse(Console.ReadLine())));


        }

        private static int GetFibunacci(int n)
        {
            if (n == 0)
            {
                return 0;
            }
            if (n == 1)
            {
                return 1;
            }

            int prev = 0;
            int current = 1;
            var result = 0;

            for (int i = 0; i < n; i++)
            {
                result = prev + current;
                prev = current;
                current = result;
            }

            return result;
        }

        private static void LabirinthPaths()
        {
            var rows = int.Parse(Console.ReadLine());
            var cols = int.Parse(Console.ReadLine());
            var lab = new char[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                var line = Console.ReadLine();
                for (int j = 0; j < line.Length; j++)
                {
                    lab[i, j] = line[j];
                }
            }
            var directions = new List<char>();
            FindAllPaths(lab, 0, 0, directions, '\0');
        }

        private static void FindAllPaths(char[,] lab, int row, int col,List<char> directions, char direction)
        {
            if (IsOutside(lab, row, col) || IsWall(lab,row,col) || IsVisited(lab,row,col)) return;

            directions.Add(direction);

            if ( lab[row, col] == 'e')
            {
                Console.WriteLine(string.Join("", directions));
                directions.RemoveAt(directions.Count - 1);
                return;
            }

            lab[row, col] = 'v';

            FindAllPaths(lab, row, col - 1, directions, 'L');
            FindAllPaths(lab, row, col + 1, directions, 'R');
            FindAllPaths(lab, row - 1, col, directions, 'U');
            FindAllPaths(lab, row + 1, col, directions, 'D');
            
            directions.RemoveAt(directions.Count - 1);
            lab[row, col] = '-';
        }

        private static bool IsVisited(char[,] lab, in int row, in int col) => lab[row, col] == 'v';
        
        private static bool IsWall(char[,] lab, in int row, in int col) => lab[row, col] == '*';
        
        private static bool IsOutside(char[,] lab, in int row, in int col) =>
         row < 0 || row >= lab.GetLength(0) || col < 0 || col >= lab.GetLength(1);
        


        static void Gen01(int index, int[] vector)
        {
            if (index >= vector.Length)
                Console.WriteLine(string.Join("", vector));
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    vector[index] = i;
                    Gen01(index + 1, vector);
                }
            }
        }


        private static void GetSum(int i)
        {
            if (i <= 0) return;

            Console.WriteLine(new string('*', i));
            GetSum(i - 1);
            Console.WriteLine(new string('#', i));
        }

        private static int Factorial(int n)
        {
            if (n == 0) return 1;
            return n * Factorial(n - 1);
        }
    }
}
