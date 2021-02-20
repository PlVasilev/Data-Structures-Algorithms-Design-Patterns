using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace _03_Problem
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        private static Dictionary<int, List<Edge>> _edgesByNode;

        static void Main(string[] args)
        {
            // var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => s - f));
            var v = int.Parse(Console.ReadLine() ?? string.Empty);
            var exitRooms = Console.ReadLine()
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var e = int.Parse(Console.ReadLine() ?? string.Empty);


            _edgesByNode = ReadGraph(e, v);
            var exitTimeData = Console.ReadLine()
                .Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            var exitTime = exitTimeData[0] * 60 + exitTimeData[1];



            for (int i = 0; i < v; i++)
            {
                if (exitRooms.Contains(i)) continue;

                var distances = new int[v];
                for (int k = 0; k < distances.Length; k++) // // Framework C# code
                    distances[k] = int.MaxValue;

                var start = i;
                distances[start] = 0;

                foreach (var end in exitRooms)
                {
                    var queue = new OrderedBag<int>(
                        Comparer<int>.Create((f, s) => distances[f] - distances[s])) { start };
                    while (queue.Count > 0)
                    {
                        var minNode = queue.RemoveFirst();
                        var children = _edgesByNode[minNode];

                        if (minNode == end || distances[minNode] == int.MaxValue) break;

                        foreach (var child in children)
                        {
                            var childNode = child.First == minNode ? child.Second : child.First;

                            if (distances[childNode] == int.MaxValue)
                                queue.Add(childNode);

                            var newDistance = child.Weight + distances[minNode];

                            if (newDistance >= distances[childNode]) continue;
                            distances[childNode] = newDistance;
                            queue = new OrderedBag<int>(queue,
                                Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                        }
                    }
                }

                var quickestWay = exitRooms.Select(t1 => distances[t1]).Concat(new[] {int.MaxValue}).Min();

                if (quickestWay == int.MaxValue)
                {
                    Console.WriteLine($"Unreachable {i} (N/A)");
                    continue;
                }
                var t = TimeSpan.FromSeconds(quickestWay);
                var sec = string.Format("{0:D2}:{1:D2}:{2:D2}", ((int)t.TotalHours), t.Minutes, t.Seconds);
                Console.WriteLine(quickestWay > exitTime ? $"Unsafe {i} ({sec})" : $"Safe {i} ({sec})");
            }
        }

        private static Dictionary<int, List<Edge>> ReadGraph(int e, int v)
        {
            var result = new Dictionary<int, List<Edge>>();

            for (int i = 0; i < v; i++) result[i] = new List<Edge>();
            
            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine()?
                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)// Framework C# code
                    .ToArray();

                if (edgeData == null) continue;
                var firstNode = int.Parse(edgeData[0]);
                var secondNode = int.Parse(edgeData[1]);
                var minuntes_seconds = edgeData[2]
                    .Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                var weight = minuntes_seconds[0] * 60 + minuntes_seconds[1];

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight
                };

                result[firstNode].Add(edge);
                result[secondNode].Add(edge);
            }
            return result;
        }
    }
}
