using System;
using System.Collections.Generic;
using System.Linq;


namespace _05_Cable_Network
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }

        public bool Connected { get; set; }

        public override string ToString() => $"{First} {Second} {Weight} {Connected}";
    }

    class Program
    {
        private static List<Edge> _edges;

        static void Main(string[] args)
        {
            var budget = int.Parse(Console.ReadLine());
            var nodesCount = int.Parse(Console.ReadLine());
            var num = int.Parse(Console.ReadLine());
            _edges = ReadEdges(num);

            var sortedNotConnectedEdges = _edges.OrderBy(e => e.Weight).Where(x => x.Connected == false).ToList();
            var sortedConnectedEdges = _edges.OrderBy(e => e.Weight).Where(x => x.Connected).ToList();
            var connectedNodes = sortedConnectedEdges.Select(e => e.First).Union(sortedConnectedEdges.Select(e => e.Second)).ToHashSet();

            var nodes = _edges.Select(e => e.First).Union(_edges.Select(e => e.Second)).ToHashSet();

            var parents = Enumerable.Repeat(-1, nodesCount + 1).ToArray(); // fill array with -1

            foreach (var node in nodes)
                parents[node] = node;

            foreach (var edge in sortedConnectedEdges)
            {
                var firstNodeRoot = GetRoot(parents, edge.First);
                var secondNodeRoot = GetRoot(parents, edge.Second);
                if (firstNodeRoot == secondNodeRoot) continue;

                parents[firstNodeRoot] = secondNodeRoot;
            }

            var used = 0;

            for (int i = 0; i < sortedNotConnectedEdges.Count; i++)
            {
                var edge = sortedNotConnectedEdges[i];

                var firstNodeRoot = GetRoot(parents, edge.First);
                var secondNodeRoot = GetRoot(parents, edge.Second);

                if (firstNodeRoot == secondNodeRoot) continue;
                if (!connectedNodes.Contains(edge.First) && !connectedNodes.Contains(edge.Second))
                {
                    if (i < sortedNotConnectedEdges.Count)
                    {
                        var next = sortedNotConnectedEdges[i + 1];
                        sortedNotConnectedEdges[i] = next;
                        sortedNotConnectedEdges[i + 1] = edge;
                        i--;
                    }
                    continue;
                }
                connectedNodes.Add(edge.First);
                connectedNodes.Add(edge.Second);
                used += edge.Weight;
                if (used > budget)
                {
                    used -= edge.Weight;
                    break;
                }
                parents[firstNodeRoot] = secondNodeRoot;
            }

            //foreach (var edge in sortedNotConnectedEdges)
            //{
            //    var firstNodeRoot = GetRoot(parents, edge.First);
            //    var secondNodeRoot = GetRoot(parents, edge.Second);

            //    if (firstNodeRoot == secondNodeRoot) continue;
            //    if (!connectedNodes.Contains(edge.First) && !connectedNodes.Contains(edge.Second))
            //    {
            //        continue;
            //    }
            //    connectedNodes.Add(edge.First);
            //    connectedNodes.Add(edge.Second);
            //    used += edge.Weight;
            //    if (used > budget)
            //    {
            //        used -= edge.Weight;
            //        break;
            //    }
            //    parents[firstNodeRoot] = secondNodeRoot;
            //}

            Console.WriteLine($"Budget used: {used}");
        }

        private static int GetRoot(int[] parents, int node)
        {
            while (node != parents[node])
                node = parents[node];

            return node;
        }

        private static List<Edge> ReadEdges(in int num)
        {
            var result = new List<Edge>();
            for (var i = 0; i < num; i++)
            {
                var edgeData = Console.ReadLine().Split(" ").ToArray();
                var first = int.Parse(edgeData[0]);
                var second = int.Parse(edgeData[1]);
                var weight = int.Parse(edgeData[2]);
                bool connected = edgeData.Length > 3;

                result.Add(new Edge
                {
                    First = first,
                    Second = second,
                    Weight = weight,
                    Connected = connected
                });
            }
            return result;
        }
    }
}
