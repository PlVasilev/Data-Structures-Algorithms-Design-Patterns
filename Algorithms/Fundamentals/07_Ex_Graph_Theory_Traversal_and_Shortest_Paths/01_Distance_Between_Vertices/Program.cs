using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_Distance_Between_Vertices
{
    class Program
    {
        private static Dictionary<int, List<int>> _graph;
        private static Dictionary<int, bool> _visited = new Dictionary<int, bool>();
        private static Dictionary<int, int> _parents = new Dictionary<int, int>();

        static void Main(string[] args)
        {
            var edges = int.Parse(Console.ReadLine());
            var pathsCount = int.Parse(Console.ReadLine());

            _graph = ReadGraph(edges);

            foreach (var b in _graph)
                _visited.Add(b.Key, false);
            foreach (var b in _graph)
                _parents.Add(b.Key, -1);

            GetPaths(pathsCount);
        }

        private static void GetPaths(in int pathsCount)
        {
            for (int i = 0; i < pathsCount; i++)
            {
                var path = Console.ReadLine().Split("-").Select(int.Parse).ToArray();
                var startNode = path[0];
                var endNode = path[1];
                Bfs(startNode, endNode);

                foreach (var b in _graph)
                    _visited[b.Key] = false;
                foreach (var b in _graph)
                    _parents[b.Key] = -1;
            }
        }

        private static void Bfs(in int startNode, int destination)
        {
            if (_visited[startNode] == true) return;
            bool isPath = false;
            var queue = new Queue<int>();
            queue.Enqueue(startNode);


            _visited[startNode] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node == destination)
                {
                    isPath = true;
                    var path = ReconstructPath(destination);
                    Console.WriteLine($"{{{startNode}, {destination}}} -> {path.Count - 1}");
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

            if (isPath == false)
            {
                Console.WriteLine($"{{{startNode}, {destination}}} -> -1");
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

        private static Dictionary<int, List<int>> ReadGraph(in int num)
        {
            var result = new Dictionary<int, List<int>>();

            for (int i = 0; i < num; i++)
            {
                var edge = Console.ReadLine().Split(":").ToArray();
                var from = int.Parse(edge[0]);
                if (!result.ContainsKey(from))
                {
                   result.Add(from, new List<int>()); 
                }

                if (edge[1].Equals(string.Empty)) continue;
                var to =  edge[1].Split(" ").Select(int.Parse).ToList();
                result[@from] = to;
            }
            return result;
        }
    }
}

