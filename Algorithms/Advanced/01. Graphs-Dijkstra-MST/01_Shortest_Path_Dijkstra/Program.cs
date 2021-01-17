using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01_Shortest_Path_Dijkstra
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        private static Dictionary<int, List<Edge>> edgesByNode;

        static void Main(string[] args)
        {
            // var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => s - f));
            var e = int.Parse(Console.ReadLine());

            var edgesByNode = ReadGraph(e);
            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());

            var maxNode = edgesByNode.Keys.Max();

            var distances = new int[maxNode + 1];
            Array.Fill(distances, int.MaxValue);
            
            distances[start] = 0;

            var queue = new OrderedBag<int>(
                Comparer<int>.Create((f, s) => distances[f] - distances[s])) { start };

            while (queue.Count > 0)
            {
                var minNode = queue.RemoveFirst();
                var children = edgesByNode[minNode];

                if (minNode == end) 
                    break;
                
                foreach (var child in children)
                {
                    var childNode = child.First == minNode ? child.Second : child.First;

                    if (distances[childNode] == int.MaxValue) 
                        queue.Add(childNode);

                    var newDistance = child.Weight + distances[minNode];

                    if (newDistance < distances[childNode]) 
                        distances[childNode] = newDistance;

                    queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                }
            }

            Console.WriteLine(distances[end]);
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
                var firstNode = edgeData[0];
                var secondNode = edgeData[1];
                var weight = edgeData[2];

                if (!result.ContainsKey(firstNode))
                    result.Add(firstNode, new List<Edge>()); 
                
                if (!result.ContainsKey(secondNode))
                    result.Add(secondNode, new List<Edge>());

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight
                };

                result[firstNode].Add(edge);
                result[secondNode].Add(edge);
            }
            return result;
        }
    }
}
