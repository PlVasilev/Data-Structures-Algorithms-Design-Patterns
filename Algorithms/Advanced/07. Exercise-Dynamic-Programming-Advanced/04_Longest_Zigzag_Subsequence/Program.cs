using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Longest_Zigzag_Subsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var list = new List<int>();


            int lastSign = 0;
            var length = 1;
            list.Add(0);
            for (int i = 1; i < numbers.Length; ++i)
            {
                int Sign = signum(numbers[i] - numbers[i - 1]);

                if (Sign != 0 && Sign != lastSign) // it qualifies 
                {
                    lastSign = Sign;// updating lastSign 
                    length++;
                    list.Add(i);
                    list[list.Count - 2] = i - 1;
                }
            }

            // Console.WriteLine(length);
            foreach (var i in list)
            {
                Console.Write($"{numbers[i]} ");
            }
        }

        static int signum(int n)
        {
            if (n != 0) return n > 0 ? 1 : -1;
            return 0;
        }
    }
}
