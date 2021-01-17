using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _03_MST_Prim
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }
    class Program
    {
        private static Dictionary<int, List<Edge>> _edges;
        private static HashSet<int> _forest = new HashSet<int>();
        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            _edges = ReadEdges(num);
            foreach (var node in _edges.Keys.Where(node => !_forest.Contains(node)))
                Prim(node);
        }

        private static void Prim( int node)
        {
            _forest.Add(node);

            var queue = new OrderedBag<Edge>(
                _edges[node],
                Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(edge.First, edge.Second);
                if (nonTreeNode == -1) continue;

                Console.WriteLine($"{edge.First} - {edge.Second}");
                _forest.Add(nonTreeNode);
                queue.AddMany(_edges[nonTreeNode]);
            }
        }

        private static int GetNonTreeNode(int first, int second)
        {
            var nonTreeNode = -1;

            if (_forest.Contains(first) && !_forest.Contains(second))
                nonTreeNode = second;
            else if (_forest.Contains(second) && !_forest.Contains(first))
                nonTreeNode = first;

            return nonTreeNode;
        }

        private static Dictionary<int, List<Edge>> ReadEdges(int num)
        {
            var result = new Dictionary<int, List<Edge>>();
            for (var i = 0; i < num; i++)
            {
                var edgeData = Console.ReadLine().Split(new []{", "}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                var first = edgeData[0];
                var second = edgeData[1];
                var weight = edgeData[2];

                if (!result.ContainsKey(first))
                    result.Add(first, new List<Edge>());

                if (!result.ContainsKey(second))
                    result.Add(second, new List<Edge>());

                var edge = new Edge { First = first, Second = second, Weight = weight };
                result[first].Add(edge);
                result[second].Add(edge);

            }
            return result;
        }
    }
}
