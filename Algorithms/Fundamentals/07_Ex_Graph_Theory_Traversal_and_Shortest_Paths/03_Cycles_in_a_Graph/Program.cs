using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Xml;

namespace _03_Cycles_in_a_Graph
{
    class Program
    {
        private static Dictionary<string, List<string>> _graph;
        private static HashSet<string> _visited;
        private static HashSet<string> _cycles;

        static void Main(string[] args)
        {
            _graph = ReadGraph();
            _visited = new HashSet<string>();
            _cycles = new HashSet<string>();

            foreach (var node in _graph.Keys)
            {
                try
                {
                    Dfs(node);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine($"Acyclic: No");
                    return;
                }
            }
            Console.WriteLine("Acyclic: Yes");
        }

        private static void Dfs(string node)
        {
            if (_cycles.Contains(node))
            {
                throw new InvalidOperationException();
            }

            //  if (_visited.Contains(node)) return;

            _cycles.Add(node);
            _visited.Add(node);

            foreach (var child in _graph[node])
                Dfs(child);

            _cycles.Remove(node);
        }


        private static Dictionary<string, List<string>> ReadGraph()
        {
            var result = new Dictionary<string, List<string>>();
            var input = Console.ReadLine();

            while (input != "End")
            {
                var parts = input.Split("-", StringSplitOptions.RemoveEmptyEntries);
                var node = parts[0];
                var child = parts[1];

                if (!result.ContainsKey(node))
                    result[node] = new List<string>();

                if (!result.ContainsKey(child))
                    result[child] = new List<string>();

                result[node].Add(child);
                input = Console.ReadLine();
            }
            return result;
        }
    }
}
