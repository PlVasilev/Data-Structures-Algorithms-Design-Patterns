using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Areas_in_Matrix
{
    class Program
    {
        private static Dictionary<char, int> areas = new Dictionary<char, int>();
        private static char[,] matrix;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            matrix = new char[rows, cols];


            for (int i = 0; i < rows; i++)
            {
                var row = Console.ReadLine();
                for (int j = 0; j < row.Length; j++)
                {
                    matrix[i, j] = row[j];
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] != '*')
                    {
                        var key = matrix[i, j];
                        var count = GetArea(i, j, 0, matrix[i,j]);
                        if (!areas.ContainsKey(key))
                            areas.Add(key, 1);
                        else
                            areas[key]++;
                        
                    }
                }
            }

            Console.WriteLine($"Areas: {areas.Values.Sum()}");
            foreach (var area in areas.OrderBy(x => x.Key))
                Console.WriteLine($"Letter '{area.Key}' -> {area.Value}");
            
        }

        private static int GetArea(int row, int col, int result, char lookingFor)
        {
            result++;
            matrix[row, col] = '*';
            if (row - 1 >= 0 && matrix[row - 1, col] == lookingFor)
            {
                result = GetArea(row - 1, col, result, lookingFor);
            }
            if (row + 1 < matrix.GetLength(0) && matrix[row + 1, col] == lookingFor)
            {
                result = GetArea(row + 1, col, result, lookingFor);
            }
            if (col - 1 >= 0 && matrix[row, col - 1] == lookingFor)
            {
                result = GetArea(row, col - 1, result, lookingFor);
            }
            if (col + 1 < matrix.GetLength(1) && matrix[row, col + 1] == lookingFor)
            {
                result = GetArea(row, col + 1, result, lookingFor);
            }

            return result;
        }
    }
}

