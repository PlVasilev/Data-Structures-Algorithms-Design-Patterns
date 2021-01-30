using System;
using System.Collections.Generic;

namespace _02_Maximum_Tasks_Assignment
{
    class Program
    {
        private static int[,] _graph;
        private static int[] _parents;

        static void Main(string[] args)
        {
            // 0, 1, 2, 3, 4, 5, 6, 7
            // S, A, B, C, 1, 2, 3, F
            var people = int.Parse(Console.ReadLine());
            var tasks = int.Parse(Console.ReadLine());

            _graph = ReadGraph(people, tasks);
            var nodes = _graph.GetLength(0);

            _parents = new int[nodes];
            Array.Fill(_parents, -1);
            // PrintMatrix();
            var start = 0;
            var target = nodes - 1;

            while (Bfs(start, target))
            {
                var node = target;
                while (node != start)
                {
                    var parent = _parents[node];
                    _graph[parent, node] = 0;
                    _graph[node, parent] = 1;
                    node = parent;
                }
            }
            // PrintMatrix();
            for (int person = 1; person <= people; person++)
                for (int task = people + 1; task <= people + tasks; task++)
                    if (_graph[task, person] > 0)
                    {
                        Console.WriteLine($"{(char) (64 + person)}-{task - people}");
                        break;
                    }
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < _graph.GetLength(0); row++)
            {
                for (int col = 0; col < _graph.GetLength(1); col++)
                    Console.Write($"{_graph[row, col]} ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private static bool Bfs(int start, int target)
        {
            var visited = new bool[_graph.GetLength(0)];
            var queue = new Queue<int>();
            visited[start] = true;
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                if (node == target) return true;

                for (int child = 0; child < _graph.GetLength(1); child++)
                {
                    if (!visited[child] && _graph[node, child] > 0)
                    {
                        _parents[child] = node;
                        visited[child] = true; // child
                        queue.Enqueue(child);
                    }
                }
            }
            return false;
        }
        private static int[,] ReadGraph(int people, int tasks)
        {
            var nodes = people + tasks + 2;
            var result = new int[nodes, nodes];

            var start = 0;
            var target = nodes - 1;

            for (int person = 1; person <= people; person++)
                result[start, person] = 1;
            for (int task = people + 1; task <= people + tasks; task++)
                result[task, target] = 1;

            for (int person = 1; person <= people; person++)
            {
                var personTasks = Console.ReadLine();
                for (int task = 0; task < personTasks.Length; task++)
                {
                    if (personTasks[task] == 'Y')
                        result[person, people + 1 + task] = 1;

                }
            }
            return result;
        }
    }
}
