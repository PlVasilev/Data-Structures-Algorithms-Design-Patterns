using System;
using System.Collections.Generic;
using System.Linq;

namespace _04_Longest_Path_Big_Trip
{
    class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }
        public override string ToString() => $"{From} {To} {Weight}";
    }

    class Program
    {
        private static List<Edge>[] _graph;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            _graph = new List<Edge>[nodes];
            for (int i = 0; i < _graph.Length; i++)
                _graph[i] = new List<Edge>();


            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                var from = edgeData[0];
                var to = edgeData[1];
                var weight = edgeData[2];

                _graph[from].Add(new Edge { From = from, To = to, Weight = weight });
            }

            var source = int.Parse(Console.ReadLine());
            var destination = int.Parse(Console.ReadLine());

            var sortedNodes = TopologicalSorting();
            var distances = new double[_graph.Length];
            Array.Fill(distances, double.NegativeInfinity); // double.PositiveInfinity
            distances[source] = 0;

            var prev = new int[_graph.Length];
            Array.Fill(prev, -1);

            while (sortedNodes.Count > 0)
            {
                var node = sortedNodes.Pop();

                foreach (var edge in _graph[node])
                {
                    var newDistance = distances[node] + edge.Weight;
                    if (newDistance > distances[edge.To]) // <
                    {
                        distances[edge.To] = newDistance;
                        prev[edge.To] = node;
                    }
                }
            }
            Console.WriteLine(distances[destination]);
            Console.WriteLine(string.Join(" ", GetPath(prev, destination)));
        }

        private static Stack<int> GetPath(int[] prev, int destination)
        {
            var path = new Stack<int>();
            while (destination != -1)
            {
                path.Push(destination);
                destination = prev[destination];
            }
            return path;
        }

        private static Stack<int> TopologicalSorting()
        {
            var visited = new bool[_graph.Length];
            var sorted = new Stack<int>();
            for (int node = 1; node < _graph.Length; node++)
                DFS(node, visited, sorted);
            return sorted;
        }

        private static void DFS(int node, bool[] visited, Stack<int> sorted)
        {
            if (visited[node]) return;
            visited[node] = true;
            foreach (var edge in _graph[node])
                DFS(edge.To, visited, sorted);
            sorted.Push(node);
        }
    }
}
