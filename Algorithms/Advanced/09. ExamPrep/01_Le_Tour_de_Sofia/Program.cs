using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01_Le_Tour_de_Sofia
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        private static Dictionary<int, List<Edge>> _edgesByNode;

        static void Main(string[] args)
        {
            // var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => s - f));
            var v = int.Parse(Console.ReadLine() ?? string.Empty);
            var e = int.Parse(Console.ReadLine() ?? string.Empty);
            var start = int.Parse(Console.ReadLine() ?? string.Empty);
            var end = start;

            _edgesByNode = ReadGraph(e, v);
            var maxNode = _edgesByNode.Keys.Max();

            var distances = new double[v];
            for (int i = 0; i < distances.Length; i++) // // Framework C# code
                distances[i] = double.PositiveInfinity;


            distances[start] = 0;

            var prev = new int[v];
            prev[start] = -1;

           // var next = new int[maxNode + 1];
           // for (int i = 0; i < next.Length; i++)
           // {
           //     next[i] = -1;
           // }

            var queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s]))) { start };
            var begin = true;

            while (queue.Count > 0)
            {
                var minNode = queue.RemoveFirst();
                var children = _edgesByNode[minNode];

                if ((minNode == end && !begin) || double.IsPositiveInfinity(distances[minNode]))
                    break;

                begin = false;
                foreach (var child in children)
                {
                    var childNode = child.First == minNode ? child.Second : child.First;

                    if (double.IsPositiveInfinity(distances[childNode]))
                        queue.Add(childNode);

                    var newDistance = child.Weight + distances[minNode];
                    if (childNode == end && Math.Abs(distances[end]) < 0.01)
                    {
                        distances[end] = double.PositiveInfinity;
                    }

                    if (newDistance >= distances[childNode]) continue;
                    distances[childNode] = newDistance;
                    prev[childNode] = minNode;
                  //  next[minNode] = childNode;
                    queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f].CompareTo(distances[s])));
                }
            }

            Console.WriteLine(Math.Abs(distances[end]) < 0.01
                ? distances.Where(x => !double.IsPositiveInfinity(x)).ToList().Count
                : distances[end]);
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e, int v)
        {
            var result = new Dictionary<int, List<Edge>>();
            for (int i = 0; i < v; i++)
            {
                result[i] =  new List<Edge>();
            }

            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine()?
                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)// Framework C# code
                    .Select(int.Parse)
                    .ToArray();

                if (edgeData == null) continue;
                var firstNode = edgeData[0];
                var secondNode = edgeData[1];
                var weight = edgeData[2];

                if (!result.ContainsKey(firstNode))
                    result.Add(firstNode, new List<Edge>());

                // if (!result.ContainsKey(secondNode))
                //     result.Add(secondNode, new List<Edge>());

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight
                };

                result[firstNode].Add(edge);
                // result[secondNode].Add(edge);
            }
            return result;
        }
    }
}
