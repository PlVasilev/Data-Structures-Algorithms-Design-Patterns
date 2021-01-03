using System;
using System.Linq;
using System.Text;

namespace _02_Nuclear_Waste
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] costs = Console.ReadLine().Split().Select(int.Parse).ToArray();

            int flasks = int.Parse(Console.ReadLine());


            int[] bestCosts = new int[flasks + 1];
            int[] bestCounts = new int[flasks + 1];

            for (int i = 1; i <= flasks; i++)
            {
                bestCosts[i] = int.MaxValue;
                for (int j = 1; j <= i; j++)
                {
                    if (j > costs.Length)
                    {
                        break;
                    }

                    int newBestCost = bestCosts[i - j];
                    int currentCost = costs[j - 1];
                    int newValue = newBestCost + currentCost;
                    int currentBestCost = bestCosts[i];
                    if (newValue < currentBestCost)
                    {
                        bestCosts[i] = newValue;
                        bestCounts[i] = j;
                    }
                }
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("Cost: ")
                .Append(bestCosts[flasks])
                .Append(Environment.NewLine);

            while (flasks > 0)
            {
                sb.Append(bestCounts[flasks])
                    .Append(" => ")
                    .Append(costs[bestCounts[flasks] - 1])
                    .Append(Environment.NewLine);
                flasks -= bestCounts[flasks];
            }

            Console.WriteLine(sb.ToString());
        }
    }
}
