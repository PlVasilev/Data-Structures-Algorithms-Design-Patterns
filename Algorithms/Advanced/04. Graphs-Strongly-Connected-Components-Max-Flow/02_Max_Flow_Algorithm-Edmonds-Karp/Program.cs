using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Max_Flow_Algorithm_Edmonds_Karp
{
    class Program
    {
        private static int[,] _graph;
        private static int[] _parents;

        static void Main(string[] args)
        {
            var nodes = int.Parse(Console.ReadLine());
            _graph = ReadGraph(nodes);
            _parents = new int[nodes];
            Array.Fill(_parents, -1);

            var source = int.Parse(Console.ReadLine());
            var target = int.Parse(Console.ReadLine());
            var maxFlow = 0;

            while (BFS(source,target))
            {
                //Find min flow
                var currentFlow = GetCurrentFlow(source, target);

                // max flow += min flow
                maxFlow += currentFlow;

                // Modify capacities
                ApplyCurrentFlow(source, target, currentFlow);
            }

            Console.WriteLine($"Max flow = {maxFlow}");
        }

        private static void ApplyCurrentFlow(int source,int target,int currentFlow)
        {
            var node = target;
            while (node != source)
            {
                var parent = _parents[node];
                _graph[parent, node] -= currentFlow;
                node = parent;
            }
        }

        private static int GetCurrentFlow(int source,int target)
        {
            var node = target;
            var minFlow = int.MaxValue;
            while (node != source)
            {
                var parent = _parents[node];
                var flow = _graph[parent, node];
                if (minFlow > flow)
                    minFlow = flow;
                node = parent;
            }
            return minFlow;
        }

        private static bool BFS(int source,int target)
        {
            var queue = new Queue<int>();
            var visited = new bool[_graph.GetLength(0)];
            queue.Enqueue(source);
            visited[source] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                for (int child = 0; child < _graph.GetLength(1); child++)
                {
                    if (!visited[child] && _graph[node, child] > 0)
                    {
                        visited[child] = true;
                        queue.Enqueue(child);
                        _parents[child] = node;
                    }
                }
            }
            return visited[target];
        }

        private static int[,] ReadGraph(int nodes)
        {
            var result = new int[nodes,nodes];
            for (int i = 0; i < nodes; i++)
            {
                var capacities = Console.ReadLine().Split(", ").Select(int.Parse).ToArray();
                for (int j = 0; j < capacities.Length; j++)
                {
                    var capacity = capacities[j];
                    result[i, j] = capacity;
                }
            }
            return result;
        }
    }
}
