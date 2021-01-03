using System;
using System.Collections.Generic;

namespace _01_Binomial_Coefficients
{
    class Program
    {
        private static Dictionary<string, long> _rows = new Dictionary<string, long>();


        static void Main(string[] args)
        {
            int row = int.Parse(Console.ReadLine());
            int col = int.Parse(Console.ReadLine());

            Console.WriteLine(GetBinomialCoef(row,col));
        }

        private static long GetBinomialCoef(int row, int col)
        {
            var key = $"{row}{col}";
            if (_rows.ContainsKey(key))
                return _rows[key];

            if (col == 0 || col == row)
                return 1;

            var result = GetBinomialCoef(row - 1, col) + GetBinomialCoef(row - 1, col - 1);
            _rows[key] = result;


            return result;
        }
    }
}
