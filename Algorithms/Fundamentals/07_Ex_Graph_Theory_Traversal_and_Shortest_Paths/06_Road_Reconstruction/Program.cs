using System;
using System.Collections.Generic;
using System.Linq;

namespace _06_Road_Reconstruction
{
    public class Edge
    {
        public Edge(string first, string second)
        {
            First = first;
            Second = second;
        }

        public string First { get; set; }
        public string Second { get; set; }
    }

    class Program
    {
        private static Dictionary<string, List<string>> _graph = new Dictionary<string, List<string>>();
        private static List<Edge> _edges = new List<Edge>();
        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            var num2 = int.Parse(Console.ReadLine());
            ProcessInput(num2);
            var result = new HashSet<string>();
            foreach (var edge in _edges)
            {
                var first = edge.First;
                var second = edge.Second;
                _graph[first].Remove(second);
                _graph[second].Remove(first);

                if (!HasPath(first, second))
                {
                    var edgeToAdd = int.Parse(first) < int.Parse(second) ? $"{first} {second}" : $"{second} {first}";
                    result.Add(edgeToAdd);
                }
                _graph[first].Add(second);
                _graph[second].Add(first);
            }

            Console.WriteLine($"Important streets:");
            foreach (var removedEdge in result)
            {
                Console.WriteLine(removedEdge);
            }
        }

        private static bool HasPath(string source, string destination)
        {
            var queue = new Queue<string>();
            queue.Enqueue(source);
            var visited = new HashSet<string>();

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (var child in _graph[node])
                {
                    if (visited.Contains(child))
                    {
                        continue;
                    }

                    visited.Add(child);
                    queue.Enqueue(child);
                }
            }
            return visited.Count == _graph.Count;
        }

        private static void ProcessInput(in int num)
        {
            for (int i = 0; i < num; i++)
            {
                var input = Console.ReadLine();
                var inputArr = input.Split(" - ", StringSplitOptions.RemoveEmptyEntries).ToArray();
                var key = inputArr[0];
                var child = inputArr[1];
                if (!_graph.ContainsKey(key))
                    _graph.Add(key, new List<string>());
                _graph[key].Add(child);
                if (!_graph.ContainsKey(child))
                    _graph.Add(child, new List<string>());
                if (!_graph[child].Contains(child))
                {
                    _graph[child].Add(key);
                }

                _edges.Add(new Edge(key, child));
            }
        }
    }
}
