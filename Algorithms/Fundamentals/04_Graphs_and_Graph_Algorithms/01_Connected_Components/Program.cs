using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_Connected_Components
{
    class Program
    {
        private static Dictionary<int, List<int>> _graph;
        private static HashSet<int> _visited;
        private static List<List<int>> _results = new List<List<int>>();

        static void Main(string[] args)
        {
            _graph = new Dictionary<int, List<int>>();
            var num = int.Parse(Console.ReadLine());
            for (int i = 0; i < num; i++)
            {
                var toAdd = new List<int>();
                var input = Console.ReadLine();
                if (input != string.Empty)
                {
                    toAdd = input.Split().Select(int.Parse).ToList();
                }

                _graph[i] = new List<int>(toAdd);
            }
            _visited = new HashSet<int>();

            foreach (var node in _graph.Keys)
            {
                if (_visited.Contains(node))
                    continue;
                // var current = new List<int>();
                // Dfs(node, current);
                // _results.Add(current);

                Bfs(node);

            }

            foreach (var result in _results)
            {
                Console.WriteLine("Connected component: " + string.Join(" ", result));
            }
        }

        private static void Bfs(in int startNode)
        {
            var queue = new Queue<int>();
            queue.Enqueue(startNode);
            _visited.Add(startNode);
            var result = new List<int>();
            result.Add(startNode);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                foreach (var child in _graph[node])
                {
                    if (!_visited.Contains(child))
                    {
                        queue.Enqueue(child);
                        _visited.Add(child);
                        result.Add(child);
                    }
                }
            }
            _results.Add(result);
        }

        private static void Dfs(in int node, List<int> current)
        {
            if (_visited.Contains(node))
                return;
            _visited.Add(node);

            foreach (var child in _graph[node])
                Dfs(child, current);
            current.Add(node);
        }
    }
}
