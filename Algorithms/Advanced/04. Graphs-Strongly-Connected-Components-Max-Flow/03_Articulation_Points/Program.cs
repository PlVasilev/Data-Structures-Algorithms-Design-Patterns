using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_Articulation_Points
{
    class Program
    {
        private static List<int>[] _graph;
        private static int[] _depths;
        private static int[] _lowPoint;
        private static int[] _parents;
        private static bool[] _visited;
        private static readonly List<int> ArticulationPoints = new List<int>();

        static void Main()
        {
            var nodesCount = int.Parse(Console.ReadLine() ?? string.Empty);
            var linesCount = int.Parse(Console.ReadLine() ?? string.Empty);

            _graph = ReadGraph(nodesCount, linesCount);
            _depths = new int[nodesCount];
            _lowPoint = new int[nodesCount];
            _parents = new int[nodesCount];
            _visited = new bool[nodesCount];
            Array.Fill(_parents, -1);

            for (var node = 0; node < _graph.Length; node++)
                if (!_visited[node]) 
                    FindArticulationPoint(node, 1);
            Console.WriteLine($"Articulation points: {string.Join(", ", ArticulationPoints)}");
        }

        private static void FindArticulationPoint( int node, int depth)
        {
            _visited[node] = true;
            _lowPoint[node] = depth;
            _depths[node] = depth;
            var childCount = 0;
            var isArticulationPint = false;

            foreach (var child in _graph[node])
            {
                if (!_visited[child])
                {
                    _parents[child] = node;
                    FindArticulationPoint(child, depth + 1);
                    childCount += 1;
                    if (_lowPoint[child] >= depth)
                        isArticulationPint = true;
                    _lowPoint[node] = Math.Min(_lowPoint[node], _lowPoint[child]);
                }
                else if (_parents[node] != child)
                    _lowPoint[node] = Math.Min(_lowPoint[node], _depths[child]);
            }
            if ((_parents[node] == -1 && childCount > 1) || (_parents[node] != -1 && isArticulationPint))
                ArticulationPoints.Add(node);
        }

        private static List<int>[] ReadGraph(in int nodesCount, in int linesCount)
        {
            var result = new List<int>[nodesCount];
            for (int node = 0; node < result.Length; node++)
                result[node] = new List<int>();
            for (int i = 0; i < linesCount; i++)
            {
                var data = Console.ReadLine()?.Split(", ").Select(int.Parse).ToArray();
                if (data != null)
                {
                    var first = data[0];
                    for (int j = 1; j < data.Length; j++)
                    {
                        var second = data[j];
                        result[first].Add(second);
                        result[second].Add(first);
                    }
                }
            }
            return result;
        }
    }
}
