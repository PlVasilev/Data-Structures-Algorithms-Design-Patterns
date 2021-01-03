using System;
using System.Collections.Generic;
using System.Linq;

namespace _03_Shortest_Path
{
    class Program
    {
        private static List<int>[] _graph;
        private static bool[] _visited;
        private static int[] _parents;

        static void Main(string[] args)
        {
            var num = int.Parse(Console.ReadLine());
            var edges = int.Parse(Console.ReadLine());

            _graph = ReadGraph(num,edges);
            _visited = new bool[_graph.Length];
            _parents = new int[_graph.Length];
            Array.Fill(_parents, -1);

            var startNode = int.Parse(Console.ReadLine());
            var endNode = int.Parse(Console.ReadLine());
           
            Bfs(startNode, endNode);
        }

        private static void Bfs(in int startNode, int destination)
        {
            if (_visited[startNode] == true) return;
            
            var queue = new Queue<int>();
            queue.Enqueue(startNode);

            _visited[startNode] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destination)
                {
                    var path = ReconstructPath(destination);
                    Console.WriteLine($"Shortest path length is: {path.Count - 1}");
                    Console.WriteLine(string.Join(" ", path));
                }

                foreach (var child in _graph[node])
                {
                    if (!_visited[child])
                    {
                        _parents[child] = node;
                        queue.Enqueue(child);
                        _visited[child] = true;
                    }
                }
            }
        }

        private static Stack<int> ReconstructPath(int destination)
        {
            var path = new Stack<int>();
            var index = destination;
            while (index != -1)
            {
                path.Push(index);
                index = _parents[index];
            }
            return path;
        }

        private static List<int>[] ReadGraph(in int num, int edges)
        {
            var result = new List<int>[num + 1];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new List<int>();
            }

            for (int i = 0; i < edges; i++)
            {
                var edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var from = edge[0];
                var to = edge[1];

                result[from] ??= new List<int>();
                result[from].Add(to);
            }
            return result;
        }
    }
}
