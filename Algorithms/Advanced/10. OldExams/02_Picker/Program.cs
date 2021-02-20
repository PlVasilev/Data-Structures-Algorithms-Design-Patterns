using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Picker
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        private static List<Edge> _edges;

        static void Main(string[] args)
        {
            var num1 = int.Parse(Console.ReadLine());
            var num = int.Parse(Console.ReadLine());
            _edges = ReadEdges(num);

            var sortedEdges = _edges.OrderBy(e => e.Weight).ToList();

            var nodes = _edges.Select(e => e.First).Union(_edges.Select(e => e.Second)).ToHashSet();

            var parents = Enumerable.Repeat(-1, nodes.Max() + 1).ToArray(); // fill array with -1

            foreach (var node in nodes)
                parents[node] = node;

            var result = 0;

            foreach (var edge in sortedEdges)
            {
                var firstNodeRoot = GetRoot(parents, edge.First);
                var secondNodeRoot = GetRoot(parents, edge.Second);

                if (firstNodeRoot == secondNodeRoot) continue;

                Console.WriteLine($"{edge.First} {edge.Second}");
                result += edge.Weight;
                parents[firstNodeRoot] = secondNodeRoot;
            }

            Console.WriteLine(result);
        }

        private static int GetRoot(int[] parents, int node)
        {
            while (node != parents[node])
                node = parents[node];

            return node;
        }


        private static List<Edge> ReadEdges(int num)
        {
            var result = new List<Edge>();
            for (var i = 0; i < num; i++)
            {
                var edgeData = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                result.Add(new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight
                });
            }
            return result;
        }
    }
}
