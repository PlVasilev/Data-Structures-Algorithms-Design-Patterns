using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace _05_Connected_Areas_in_a_Matrix
{
    class Program
    {
        private static List<int[]> areas;
        private static char[,] matrix;

        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            matrix = new char[rows,cols];

            areas = new List<int[]>();

            for (int i = 0; i < rows; i++)
            {
                var row = Console.ReadLine();
                for (int j = 0; j < row.Length; j++)
                {
                    matrix[i,j] = row[j];
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i,j] == '-')
                    {
                        var area = new int[3];
                        area[0] = i;
                        area[1] = j;
                        area[2] = GetArea(i, j, 0);
                        areas.Add(area);
                    }
                }
            }

            Console.WriteLine($"Total areas found: {areas.Count}");
            int counter = 1;
            foreach (var area in areas.OrderByDescending(x => x[2]))
            {
                Console.WriteLine($"Area #{counter} at ({area[0]}, {area[1]}), size: {area[2]}");
                counter++;
            }
        }

        private static int GetArea(int row,int col,int result)
        {
            result++;
            matrix[row, col] = '*';
            if (row - 1 >= 0 && matrix[row - 1, col] == '-')
            {
               result = GetArea(row - 1, col, result);
            }
            if (row + 1 < matrix.GetLength(0) && matrix[row + 1, col] == '-')
            {
                result = GetArea(row + 1, col, result);
            }
            if (col - 1 >= 0 && matrix[row, col - 1] == '-')
            {
                result = GetArea(row, col - 1, result);
            }
            if (col + 1 < matrix.GetLength(1) && matrix[row, col + 1] == '-')
            {
                result = GetArea(row, col + 1, result);
            }

            return result;
        }
    }
}
