using System;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var priorityQueque = new OrderedBag<int>(
                // CompairElelemts
                );
            
            foreach (var cookie in cookies)
            {
                priorityQueque.Add(cookie);
                
            }

            int currentMinSweetness = priorityQueque.GetFirst();
            int steps = 0;
            while (currentMinSweetness < k && priorityQueque.Count > 1)
            {
                int leastSweetCookie = priorityQueque.RemoveFirst();
                int secondLeastSweetCookie = priorityQueque.RemoveFirst();

                int combined = leastSweetCookie + (2 * secondLeastSweetCookie);
                priorityQueque.Add(combined);

                currentMinSweetness = priorityQueque.GetFirst();
                steps++;
            }

            return currentMinSweetness < k ? -1 : steps;
            // Descending
            int CompairElelemts(int first, int second)
            {
                return second - first;
            }
            //if Object implement
        }
    }
}
