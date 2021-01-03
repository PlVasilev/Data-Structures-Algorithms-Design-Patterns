using System;
using System.Linq;

namespace _01_Binary_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var searched = int.Parse(Console.ReadLine());

            Console.WriteLine(BinarySearch(numbers, searched));
        }

        private static int BinarySearch(int[] numbers, in int searched)
        {
            var startInx = 0;
            var endInx = numbers.Length - 1;

            while (startInx <= endInx)
            {
                var midInx = (startInx + endInx) / 2;

                if (numbers[midInx] == searched) 
                    return midInx;

                if (searched > numbers[midInx])
                    startInx = midInx + 1;
                
                if (searched < numbers[midInx])
                    endInx = midInx - 1;
            }

            return -1;
        }
    }
}
