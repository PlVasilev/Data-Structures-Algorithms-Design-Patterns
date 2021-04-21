using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _01_Le_Tour_de_Sofia_AS
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        private static List<Edge>[] _graph;
        private static Dictionary<int, Dictionary<int, int>> _treeByNode = new Dictionary<int, Dictionary<int, int>>();
        private static Dictionary<int, int> _damageByNode = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());
            var lightnings = int.Parse(Console.ReadLine());

            _graph = ReadGraph(nodes, edges);

            for (int i = 0; i < lightnings; i++)
            {
                var lightningData = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var node = lightningData[0];
                var damage = lightningData[1];

                if (!_treeByNode.ContainsKey(node))
                    _treeByNode[node] = Prim(node);

                var tree = _treeByNode[node];

                foreach (var kvp in tree)
                {
                    // kvp.key = node kvp.value = depth
                    var depth = kvp.Value;
                    var currentDmg = CalculateDMG(damage, depth);
                    if (!_damageByNode.ContainsKey(kvp.Key))
                        _damageByNode.Add(kvp.Key,0);

                    _damageByNode[kvp.Key] += currentDmg;
                }
            }
            Console.WriteLine(_damageByNode.Max(kvp => kvp.Value));
        }

        private static int CalculateDMG( int damage,  int depth)
        {
            for (int i = 0; i < depth-1; i++) 
                damage /= 2;
            return damage;
        }

        private static Dictionary<int, int> Prim(int startNode)
        {
            var tree = new Dictionary<int, int> { { startNode, 1 } };
            var queue = new OrderedBag<Edge>(Comparer<Edge>.Create((f, s) => f.Weight - s.Weight));
            queue.AddMany(_graph[startNode]);

            while (queue.Count > 0)
            {
                var edge = queue.RemoveFirst();
                var nonTreeNode = GetNonTreeNode(tree, edge);
                if (nonTreeNode == -1) continue;

                var treeNode = GetTreeNode(tree, edge);
                tree.Add(nonTreeNode, tree[treeNode] + 1);
                queue.AddMany(_graph[nonTreeNode]);

            }
            return tree;
        }

        private static int GetTreeNode(Dictionary<int, int> tree, Edge edge) => 
            tree.ContainsKey(edge.First) ? edge.First : edge.Second;

        private static int GetNonTreeNode(Dictionary<int, int> tree, Edge edge)
        {
            if (tree.ContainsKey(edge.First) && !tree.ContainsKey(edge.Second)) return edge.Second;
            if (!tree.ContainsKey(edge.First) && tree.ContainsKey(edge.Second)) return edge.First;
            return -1;
        }

        private static List<Edge>[] ReadGraph(int nodes, int edges)
        {
            var result = new List<Edge>[nodes];

            for (int node = 0; node < result.Length; node++)
                result[node] = new List<Edge>();

            for (int i = 0; i < edges; i++)
            {
                var edgeData = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var edge = new Edge
                {
                    First = edgeData[0],
                    Second = edgeData[1],
                    Weight = edgeData[2]
                };
                result[edgeData[0]].Add(edge);
                result[edgeData[1]].Add(edge);
            }
            return result;
        }
    }
}
