using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_Distance_Between_Vertices_AS
{
    class Program
    {
        private static Dictionary<int, List<int>> _graph;

        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var p = int.Parse(Console.ReadLine());

            _graph = ReadGraph(n);

            for (int i = 0; i < p; i++)
            {
                var pair = Console.ReadLine().Split('-', StringSplitOptions.RemoveEmptyEntries).ToArray();
                var start = int.Parse(pair[0]) ;
                var end = int.Parse(pair[1]);

                var steps = GetShortestPath(start, end);
                Console.WriteLine($"{{{start}, {end}}} -> {steps}");
            }
        }

        private static int GetShortestPath(int start, int end)
        {
            var queue = new Queue<int>();
            queue.Enqueue(start);

            var steps = new Dictionary<int, int>{{start, 0}};

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == end)
                    return steps[node];
                
                foreach (var child in _graph[node])
                {
                    if (steps.ContainsKey(child)) 
                        continue;
                    queue.Enqueue(child);
                    steps[child] = steps[node] + 1;
                }
            }
            return -1;
        }

        private static Dictionary<int, List<int>> ReadGraph(in int n)
        {
            var result = new Dictionary<int, List<int>>();

            for (int i = 0; i < n; i++)
            {
                var parts = Console.ReadLine().Split(':',StringSplitOptions.RemoveEmptyEntries);
                var node = int.Parse(parts[0]);
                if (parts.Length == 1)
                    result[node] = new List<int>();
                else
                    result[node]= parts[1].Split().Select(int.Parse).ToList();
            }
            return result;
        }
    }
}
