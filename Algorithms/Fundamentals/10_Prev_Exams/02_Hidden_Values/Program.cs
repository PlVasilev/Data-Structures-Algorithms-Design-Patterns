using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Hidden_Values
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int[] result = maxSubArraySum(a);
            Console.WriteLine(result[0]);
            var sequence = new List<int>();
            var target = result[0];
            for (int i = result[1]; i >= 0; i--)
            {
                sequence.Add(a[i]);
                target -= a[i];
                if (target == 0 )
                {
                    break;
                }
            }
            Console.WriteLine(string.Join(" ", sequence));
        }

        static int[] maxSubArraySum(int[] a)
        {
            int size = a.Length;
            int max_so_far = int.MinValue;
            int max_ending_here = 0;
            int max_element = 0;
            var result = new int[2];

            for (int i = 0; i < size; i++)
            {
                max_ending_here = max_ending_here + a[i];

                if (max_so_far < max_ending_here)
                {
                    max_so_far = max_ending_here;
                    max_element = i;
                }

                if (max_ending_here < 0)
                    max_ending_here = 0;
            }

            result[0] = max_so_far;
            result[1] = max_element;
            return result;
        }
    }
}
