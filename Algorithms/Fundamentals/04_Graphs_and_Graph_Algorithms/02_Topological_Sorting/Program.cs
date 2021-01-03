using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Topological_Sorting
{
    class Program
    {
        private static Dictionary<string, List<string>> _graph;
        private static Dictionary<string, int> _dependencies;


        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            _graph = ReadGraph(num); //.OrderBy(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value);
            _dependencies = ExtractDependencies();
            var sorted = TopologicalSorting();

            Console.WriteLine(sorted == null
                ? "Invalid topological sorting"
                : $"Topological sorting: {string.Join(", ", sorted)}");
        }

        private static List<string> TopologicalSorting()
        {
            var result = new List<string>();
            while (_dependencies.Count > 0)
            {
                var nodeToRemove = _dependencies.FirstOrDefault(x => x.Value == 0);
                if (string.IsNullOrEmpty(nodeToRemove.Key))
                    break;

                var node = nodeToRemove.Key;
                var children = _graph[node];

                result.Add(node);

                foreach (var child in children)
                    _dependencies[child] -= 1;
                
                _dependencies.Remove(nodeToRemove.Key);
            }
            return _dependencies.Count > 0 ? null : result;
        }

        private static Dictionary<string, int> ExtractDependencies()
        {
            var dependencies = new Dictionary<string, int>();
            foreach (var kvp in _graph)
            {
                var node = kvp.Key;
                var children = kvp.Value;

                if (!dependencies.ContainsKey(node))
                    dependencies.Add(node,0);

                foreach (var child in children)
                {
                    if (!dependencies.ContainsKey(child))
                        dependencies.Add(child, 1);
                    
                    else
                        dependencies[child] += 1;
                }
            }

            return dependencies;
        }

        private static Dictionary<string, List<string>> ReadGraph(in int num)
        {
            var graphResult = new Dictionary<string, List<string>>();
            for (int i = 0; i < num; i++)
            {
                var input = Console.ReadLine().Split(" -> ", StringSplitOptions.RemoveEmptyEntries);
                var key = input[0];
                if (input.Length > 1)
                {
                    var children = input[1].Split(", ").ToList();
                    graphResult[key] = children;
                }
                else
                {
                    if (key.Contains("->"))
                    {
                        graphResult[key.Substring(0, key.Length - 3)] = new List<string>();
                    }
                    else
                    {
                        graphResult[key] =  new List<string>();
                    }
                   
                }
            }
            return graphResult;
        }
    }
}
