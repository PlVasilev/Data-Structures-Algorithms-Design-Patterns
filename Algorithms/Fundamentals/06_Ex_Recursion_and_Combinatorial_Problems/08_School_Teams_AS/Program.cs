using System;
using System.Collections.Generic;
using System.Linq;

namespace _08_School_Teams_AS
{
    class Program
    {


        static void Main(string[] args)
        {
            var girls = Console.ReadLine().Split(", ");
            var boys = Console.ReadLine().Split(", ");

            var girlsComb = new string[3];
            var girlsList = new List<string[]>();
            Comb(0, 0, girlsComb, girls, girlsList);

            var buysComb = new string[2];
            var boysList = new List<string[]>();
            Comb(0,0, buysComb,boys, boysList);

            foreach (var girlsResult in girlsList)
            {
                foreach (var boysResult in boysList)
                {
                    Console.WriteLine(string.Join(", ", girlsResult) +", "+string.Join(", ", boysResult));
                }
            }
        }

        private static void Comb(int cellInx, int elemInx, string[] comb, string[] elements, List<string[]> result)
        {
            if (cellInx >= comb.Length)
            {
                result.Add(comb.ToArray());
                return;
            }

            for (int i = elemInx; i < elements.Length; i++)
            {
                comb[cellInx] = elements[i];
                Comb(cellInx + 1, i+ 1, comb, elements, result);
            }
        }
    }
}
