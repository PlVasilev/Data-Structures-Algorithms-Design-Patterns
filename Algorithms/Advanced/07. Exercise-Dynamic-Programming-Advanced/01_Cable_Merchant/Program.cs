using System;
using System.Linq;

namespace _01_Cable_Merchant
{
    class Program
    {
        private static int[] _bestPrices;
        private static int[] _bestCombo;

        static void Main(string[] args)
        {
            var inputPrices = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var connectorPrice = int.Parse(Console.ReadLine());
            var prices = new int[inputPrices.Length + 1];
            for (int i = 1; i < prices.Length; i++)
            {
                prices[i] = inputPrices[i - 1];
            }
            _bestPrices = new int[prices.Length];
            _bestCombo = new int[prices.Length];

            for (int i = 1; i < prices.Length; i++)
            {
                var bestPrice = prices[i];
                var bestCombo = i;
                for (int j = 1; j < i; j++)
                {
                    var availableBestPrice = _bestPrices[j] - connectorPrice + _bestPrices[i - j]- connectorPrice;
                    if (availableBestPrice <= bestPrice) continue;
                    bestPrice = availableBestPrice;
                    bestCombo = j;
                }
                _bestPrices[i] = bestPrice;
                _bestCombo[i] = bestCombo;
            }

            for (int i = 1; i < prices.Length; i++)
                Console.Write($"{_bestPrices[i]} ");
        }
    }
}
