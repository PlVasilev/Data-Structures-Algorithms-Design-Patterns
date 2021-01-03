using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_The_Story_Telling
{
    class Program
    {
        private static Dictionary<string, List<string>> _graph;
        private static HashSet<string> _visited;
        private static Stack<Stack<string>> _results = new Stack<Stack<string>>();

        static void Main(string[] args)
        {
            _graph = ReadGraph();

            _visited = new HashSet<string>();

            foreach (var node in _graph.Keys)
            {
                if (_visited.Contains(node))
                    continue;
                var current = new Stack<string>();
                Dfs(node, current);
                _results.Push(current);
            }

            foreach (var result in _results)
            {
                Console.Write(string.Join(" ", result));
                Console.Write(" ");
            }
        }

        private static void Dfs(in string node, Stack<string> current)
        {
            if (_visited.Contains(node))
                return;
            _visited.Add(node);

            foreach (var child in _graph[node])
                Dfs(child, current);
            current.Push(node);
        }

        private static Dictionary<string, List<string>> ReadGraph()
        {
            var result = new Dictionary<string, List<string>>();
            var input = Console.ReadLine();

            while (input != "End")
            {
                var parts = input.Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                var node = parts[0];
                if (node.Contains("->"))
                {
                    node = node.Substring(0, node.Length - 3);
                }
                if (!result.ContainsKey(node))
                    result[node] = new List<string>();
                if (parts.Length == 1)
                {
                    input = Console.ReadLine();
                    continue;
                }

                var children = parts[1].Split().ToArray();
                foreach (var child in children)
                {
                    if (!result.ContainsKey(child))
                        result[child] = new List<string>();
                    result[node].Add(child);
                }
                input = Console.ReadLine();
            }
            return result;
        }
    }
}
