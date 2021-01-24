using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;
using Edge = _05_Cable_Network_AS.Edge;


namespace _05_Cable_Network_AS
{
    public class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
        public override string ToString() => $"{First} {Second} {Weight}";
    }

    class Program
    {
        private static List<Edge>[] _graph;
        private static HashSet<int> _spanningTree = new HashSet<int>();


        static void Main(string[] args)
        {
            var budget = int.Parse(Console.ReadLine());
            var nodesCount = int.Parse(Console.ReadLine());
            var edgesCount = int.Parse(Console.ReadLine());

            _graph = new List<Edge>[nodesCount];
            for (int i = 0; i < _graph.Length; i++)
                _graph[i] = new List<Edge>();

            for (int i = 0; i < edgesCount; i++)
            {
                var edgeData = Console.ReadLine().Split().ToArray();
                var first = int.Parse(edgeData[0]);
                var second = int.Parse(edgeData[1]);
                var weight = int.Parse(edgeData[2]);
                if (edgeData.Length == 4)
                {
                    _spanningTree.Add(first);
                    _spanningTree.Add(second);
                }
                var edge = new Edge();
                edge.First = first;
                edge.Second = second;
                edge.Weight = weight;
                _graph[first].Add(edge);
                _graph[second].Add(edge);
            }
            Console.WriteLine($"Budget used: {Prim(budget)}");
        }

        private static int Prim(int budget)
        {
            var used = 0;
            var queue = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            foreach (var node in _spanningTree)
                queue.AddMany(_graph[node]);
            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = -1;
                if (_spanningTree.Contains(edge.First) && !_spanningTree.Contains(edge.Second))
                    nonTreeNode = edge.Second;
                if (!_spanningTree.Contains(edge.First) && _spanningTree.Contains(edge.Second))
                    nonTreeNode = edge.First;

                if (nonTreeNode == -1) continue;
                if (edge.Weight > budget) break;

                used += edge.Weight;
                budget -= edge.Weight;
                _spanningTree.Add(nonTreeNode);
                queue.AddMany(_graph[nonTreeNode]);
            }
            return used;
        }
    }
}
