using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_School_Teams
{
    class Program
    {
        private static string[] elementsGirls;
        private static string[] elementsBoys;
        private static string[] variationsGirls;
        private static string[] variationsBoys;
        private static bool[] usedGirls;
        private static bool[] usedboys;
        private static List<string[]> resultsGirls = new List<string[]>();
        private static List<string[]> resultsBoys = new List<string[]>();

        static void Main(string[] args)
        {
            elementsGirls = Console.ReadLine().Split(", ").ToArray();
            elementsBoys = Console.ReadLine().Split(", ").ToArray();

            variationsGirls = new string[3];
            usedGirls = new bool[elementsGirls.Length];

            variationsBoys = new string[2];
            usedboys = new bool[elementsBoys.Length];

            VariationsBoys(0);
            VariationsGirls(0);
        }

        private static void VariationsGirls(int index)
        {
            if (index >= variationsGirls.Length)
            {
                foreach (var result in resultsGirls)
                {
                    if (result.Contains(variationsGirls[0]) && result.Contains(variationsGirls[1]) && result.Contains(variationsGirls[2]))
                    {
                       return;
                    }
                }

                resultsGirls.Add(new string[]{ variationsGirls[0] , variationsGirls[1], variationsGirls[2] });
                foreach (var boys in resultsBoys)
                {
                    Console.WriteLine(string.Join(", ", variationsGirls) + ", " + string.Join(", ", boys));
                }
                return;
            }

            for (int i = 0; i < elementsGirls.Length; i++)
            {
                if (!usedGirls[i])
                {
                    usedGirls[i] = true;
                    variationsGirls[index] = elementsGirls[i];
                    VariationsGirls(index + 1);
                    usedGirls[i] = false;
                }
            }
        }

        private static void VariationsBoys(int index)
        {
            if (index >= variationsBoys.Length)
            {
                foreach (var result in resultsBoys)
                {
                    if (result.Contains(variationsBoys[0]) && result.Contains(variationsBoys[1]))
                    {
                        return;
                    }
                }

                resultsBoys.Add(new string[] { variationsBoys[0], variationsBoys[1]});
                return;
            }

            for (int i = 0; i < elementsBoys.Length; i++)
            {
                if (!usedboys[i])
                {
                    usedboys[i] = true;
                    variationsBoys[index] = elementsBoys[i];
                    VariationsBoys(index + 1);
                    usedboys[i] = false;
                }
            }
        }
    }
}
