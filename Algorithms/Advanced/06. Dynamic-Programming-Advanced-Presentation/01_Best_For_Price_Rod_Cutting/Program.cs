using System;
using System.Linq;

namespace _01_Best_For_Price_Rod_Cutting
{
    class Program
    {
        private static int[] _bestPrices;
        private static int[] _bestCombo;

        static void Main(string[] args)
        {
            var prices = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var length = int.Parse(Console.ReadLine());
            _bestPrices = new int[length + 1];
            _bestCombo = new int[length + 1];



            for (int i = 1; i <= length; i++)
            {
                var bestPrice = prices[i];
                var bestCombo = i;
                for (int j = 1; j < i; j++)
                {
                    if (_bestPrices[j] + _bestPrices[i - j] > bestPrice)
                    {
                        bestPrice = _bestPrices[j] + _bestPrices[i - j];
                        bestCombo = j;
                    }
                }

                _bestPrices[i] = bestPrice;
                _bestCombo[i] = bestCombo;
            }

            Console.WriteLine(_bestPrices[length]);

            while (length != 0)
            {
                Console.Write($"{_bestCombo[length]} ");
                length -= _bestCombo[length];
            }


            // CutRod(length, prices);
            // Console.WriteLine(_bestPrices[length]);
            //
            // while (length != 0)
            // {
            //     Console.Write($"{_bestCombo[length]} ");
            //     length -= _bestCombo[length];
            // }
        }

        private static int CutRod(int length, int[] prices)
        {
            if (length == 0) return 0;
            if (_bestPrices[length] != 0)
                return _bestPrices[length];

            var bestPrice = prices[length];
            var bestCombo = length;
            for (int i = 1; i <= length; i++)
            {
                var currentPrice = prices[i] + CutRod(length - i, prices);
                if (currentPrice > bestPrice)
                {
                    bestPrice = currentPrice;
                    bestCombo = i;
                }
            }
            _bestPrices[length] = bestPrice;
            _bestCombo[length] = bestCombo;
            return bestPrice;
        }
    }
}
