using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_Find_Bi_Connected_Components
{
    class Program
    {
        private static List<int>[] _graph;
        private static int[] _depths;
        private static int[] _lowPoint;
        private static int[] _parents;
        private static bool[] _visited;
        private static readonly List<int> ArticulationPoints = new List<int>();
        private static Stack<int> _stack = new Stack<int>();
        private static List<HashSet<int>> _components = new List<HashSet<int>>();

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
            {
                if (!_visited[node])
                {
                    FindArticulationPoint(node, 1);
                    var component = new HashSet<int>();
                    while (_stack.Count > 1)
                    {
                        var stackChild = _stack.Pop();
                        var stackNode = _stack.Pop();
                        component.Add(stackNode);
                        component.Add(stackChild);
                    }
                    _components.Add(component);
                }
            }
            Console.WriteLine($"Articulation points: {string.Join(", ", ArticulationPoints)}");
            Console.WriteLine($"Number of bi-connected components: {_components.Count}");
           foreach (var component in _components)
           {
               Console.WriteLine(string.Join(" ", component));
           }
        }


        private static void FindArticulationPoint(int node, int depth)
        {
            _visited[node] = true;
            _lowPoint[node] = depth;
            _depths[node] = depth;
            var childCount = 0;

            foreach (var child in _graph[node])
            {
                if (!_visited[child])
                {
                    _stack.Push(node);
                    _stack.Push(child);

                    _parents[child] = node;
                    FindArticulationPoint(child, depth + 1);
                    childCount += 1;

                    if ((_parents[node] == -1 && childCount > 1) || 
                        (_parents[node] != -1 && _lowPoint[child] >= depth))
                    {
                        ArticulationPoints.Add(node);
                        var component = new HashSet<int>();
                        while (true)
                        {
                            var stackChild = _stack.Pop();
                            var stackNode = _stack.Pop();
                            component.Add(stackNode);
                            component.Add(stackChild);
                            if (stackNode == node && stackChild == child) break;
                        }
                        _components.Add(component);
                    }
                    _lowPoint[node] = Math.Min(_lowPoint[node], _lowPoint[child]);
                }
                else if (_parents[node] != child && _depths[child] < _lowPoint[node])
                {
                    _stack.Push(node);
                    _stack.Push(child);

                    _lowPoint[node] = _depths[child];
                }
            }
        }

        private static List<int>[] ReadGraph(in int nodesCount, in int linesCount)
        {
            var result = new List<int>[nodesCount];
            for (int node = 0; node < result.Length; node++)
                result[node] = new List<int>();
            for (int i = 0; i < linesCount; i++)
            {
                var data = Console.ReadLine()?.Split(" ").Select(int.Parse).ToArray();
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
