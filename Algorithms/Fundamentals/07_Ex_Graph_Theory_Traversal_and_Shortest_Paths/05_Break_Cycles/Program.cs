using System;
using System.Collections.Generic;
using System.Linq;

namespace _05_Break_Cycles
{
    public class Edge
    {
        public Edge(string first,string second)
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
            ProcessInput(num);

            _edges = _edges.OrderBy(e => e.First).ThenBy(e => e.Second).ToList();

            var removedEdges = new List<string>();

            foreach (var edge in _edges)
            {
                var first = edge.First;
                var second = edge.Second;
                _graph[first].Remove(second);
                _graph[second].Remove(first);

                if (HasPath(first,second))
                {
                    var edgeToAdd = $"{first} - {second}";
                    var reversed = $"{second} - {first}";
                    if (removedEdges.Contains(reversed)) continue;
                    removedEdges.Add(edgeToAdd);
                }
                else
                {
                    _graph[first].Add(second);
                    _graph[second].Add(first);
                }
            }

            Console.WriteLine($"Edges to remove: {removedEdges.Count}");
            foreach (var removedEdge in removedEdges)
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
                if (node == destination)
                    return true;
                
                foreach (var child in _graph[node])
                {
                    if (visited.Contains(child)) continue;
                    visited.Add(child);
                    queue.Enqueue(child);
                }
            }
            return false;
        }


        private static void ProcessInput(in int num)
        {
            for (int i = 0; i < num; i++)
            {
                var input = Console.ReadLine();
                var inputArr = input.Split(" -> ",StringSplitOptions.RemoveEmptyEntries).ToArray();
                var key = inputArr[0];
                var children = inputArr[1].Split();
                if (!_graph.ContainsKey(key))
                    _graph.Add(key, new List<string>());

                foreach (var child in children)
                {
                    _graph[key].Add(child);
                    _edges.Add(new Edge(key, child));
                }
            }
        }
    }
}
