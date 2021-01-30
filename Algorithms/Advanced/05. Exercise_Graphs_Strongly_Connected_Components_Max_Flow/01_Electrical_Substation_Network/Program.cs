using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_Electrical_Substation_Network
{
    class Program
    {
        private static List<int>[] _graph;
        private static List<int>[] _reversedGraph;
        private static Stack<int> _sorted;

        static void Main(string[] args)
        {
            var nodesCount = int.Parse(Console.ReadLine());
            var rowsCount = int.Parse(Console.ReadLine());

            (_graph, _reversedGraph) = ReadGraph(nodesCount, rowsCount);
            _sorted = TopologicalSorting();
            var visited = new bool[nodesCount];
            // Console.WriteLine("Strongly Connected Components:");
            while (_sorted.Count > 0)
            {
                var node = _sorted.Pop();
                if (visited[node])
                    continue;
                var component = new Stack<int>();
                ReverseDFS(node, visited, component);

                Console.WriteLine($"{string.Join(", ", component)}");
            }
        }

        private static void ReverseDFS(int node, bool[] visited, Stack<int> component)
        {
            if (visited[node])
                return;
            visited[node] = true;
            foreach (var child in _reversedGraph[node])
                ReverseDFS(child, visited, component);
            component.Push(node);
        }

        private static Stack<int> TopologicalSorting()
        {
            var result = new Stack<int>();
            var visited = new bool[_graph.Length];

            for (int node = 0; node < _graph.Length; node++)
                DFS(node, visited, result);
            return result;
        }

        private static void DFS(int node, bool[] visited, Stack<int> stack)
        {
            if (visited[node]) return;
            visited[node] = true;

            foreach (var child in _graph[node])
                DFS(child, visited, stack);
            stack.Push(node);
        }

        private static (List<int>[] original, List<int>[] reversed) ReadGraph(int nodesCount, int rowsCount)
        {
            var result = new List<int>[nodesCount];
            var reversed = new List<int>[nodesCount];

            for (int i = 0; i < nodesCount; i++)
            {
                result[i] = new List<int>();
                reversed[i] = new List<int>();
            }
            for (int i = 0; i < rowsCount; i++)
            {
                var data = Console.ReadLine().Split(", ").Select(int.Parse).ToList();
                var node = data[0];
                for (int j = 1; j < data.Count; j++)
                {
                    var child = data[j];
                    result[node].Add(child);
                    reversed[child].Add(node);
                }
            }
            return (result, reversed);
        }
    }
}
