using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01_Most_Reliable_Path
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }

        public override string ToString() => $"{First} {Second} {Weight}";
    }

    class Program
    {
        private static List<Edge>[] _graph;

        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            _graph = ReadGraph(nodesCount, edges);

            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            var distances = new double[nodesCount];
            var prev = new int[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                distances[i] = double.NegativeInfinity;
                prev[i] = -1;
            }
            distances[start] = 100;
            var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f]))) {start};

            while (queue.Count > 0)
            {
                var node = queue.RemoveFirst();
                if (node == end) break;

                var children = _graph[node];
                foreach (var edge in children)
                {
                    var childNode = edge.First == node ? edge.Second : edge.First;
                    if (double.IsNegativeInfinity(distances[childNode]))
                        queue.Add(childNode);

                    var newDistance = distances[node] * edge.Weight / 100.0;
                    if (newDistance <= distances[childNode]) continue;

                    distances[childNode] = newDistance;
                    prev[childNode] = node;
                    queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[s].CompareTo(distances[f])));
                }
            }
            var path = GetPath(prev, end);
            Console.WriteLine($"Most reliable path reliability: {distances[end]:F}%" );
            Console.WriteLine(string.Join(" -> ", path));
        }

        private static Stack<int> GetPath(int[] prev, int node)
        {
            var result = new Stack<int>();
            while (node != -1)
            {
                result.Push(node);
                node = prev[node];
            }
            return result;
        }

        private static List<Edge>[] ReadGraph(int nodesCount, int edges)
        {
            var result = new List<Edge>[nodesCount];
            for (int i = 0; i < nodesCount; i++)
                result[i] = new List<Edge>();

            for (int i = 0; i < edges; i++)
            {
                var edgeData = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];
                var edge = new Edge { First = first, Second = second, Weight = weight };
                result[first].Add(edge);
                result[second].Add(edge);
            }
            return result;
        }
    }
}
