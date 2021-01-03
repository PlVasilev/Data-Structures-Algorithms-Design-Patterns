using System;
using System.Collections.Generic;
using System.Linq;

namespace _06_Cinema
{
    class Program
    {
        private static List<string> elements;
        private static Dictionary<int, string> selectedSeats = new Dictionary<int, string>();

        static void Main(string[] args)
        {
            elements = Console.ReadLine().Split(", ").ToList();
            var input = Console.ReadLine();
            while (input != "generate")
            {
                var inputArr = input.Split(" - ").ToArray();
                var name = inputArr[0];
                var seat = int.Parse(inputArr[1]);
                selectedSeats[seat] = name;
                input = Console.ReadLine();
                elements.Remove(name);
            }
            Permute(0);
        }

        private static void Permute(int index)
        {
            if (index >= elements.Count)
            {
                var result = new string[elements.Count + selectedSeats.Count];
                foreach (var selectedSeat in selectedSeats)
                {
                    result[selectedSeat.Key - 1] = selectedSeat.Value;
                }

                for (int i = 0; i < elements.Count; i++)
                {
                    for (int j = 0; j < result.Length; j++)
                    {
                        if (result[j] == null)
                        {
                            result[j] = elements[i];
                            break;
                        }
                        
                    }
                }
                Console.WriteLine(string.Join(" ", result));
                return;
            }
            Permute(index + 1);

            for (int i = index + 1; i < elements.Count; i++)
            {
                Swap(index, i);
                Permute(index + 1);
                Swap(index, i);
            }
        }

        private static void Swap(in int first, in int second)
        {
            var temp = elements[first];
            elements[first] = elements[second];
            elements[second] = temp;
        }
    }
}
