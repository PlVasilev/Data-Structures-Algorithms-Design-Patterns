using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_Prob
{
    class Program
    {
        private static List<int>[] _graph;

        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            _graph = ReadGraph(num);

            var paths = int.Parse(Console.ReadLine());
            for (var i = 0; i < paths; i++)
            {
                var hasPath = true;
                var path = Console.ReadLine().Split().Select(int.Parse).ToArray();
                for (var j = 0; j < path.Length - 1; j++)
                {
                    if (_graph[path[j]].Contains(path[j + 1])) continue;
                    hasPath = false;
                    break;
                }
                Console.WriteLine(hasPath ? "yes" : "no");
            }
        }

        private static List<int>[] ReadGraph(in int num)
        {
            var result = new List<int>[num];

            for (var i = 0; i < result.Length; i++)
            {
                var input = Console.ReadLine();
                if (input == string.Empty)
                    result[i] = new List<int>();
                
                else
                    result[i] = input.Split().Select(int.Parse).ToList();
            }
            return result;
        }
    }
}
