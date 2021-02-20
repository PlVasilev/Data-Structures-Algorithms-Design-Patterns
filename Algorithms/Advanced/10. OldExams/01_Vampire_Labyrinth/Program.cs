using System;

namespace _01_Vampire_Labyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Wintellect.PowerCollections;

    namespace _01_Food_Programme
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
                var v = int.Parse(Console.ReadLine());
                var e = int.Parse(Console.ReadLine() ?? string.Empty);

                var startEnd = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var start = startEnd[0];
                var end = startEnd[1];

                _edgesByNode = ReadGraph(e);

                var maxNode = _edgesByNode.Keys.Max();

                var distances = new int[maxNode + 1];
                for (int i = 0; i < distances.Length; i++) // // Framework C# code
                    distances[i] = int.MaxValue;


                distances[start] = 0;

                var prev = new int[maxNode + 1];
                prev[start] = -1;

                var queue = new OrderedBag<int>(
                    Comparer<int>.Create((f, s) => distances[f] - distances[s])) {start};

                while (queue.Count > 0)
                {
                    var minNode = queue.RemoveFirst();
                    var children = _edgesByNode[minNode];

                    if (minNode == end || distances[minNode] == int.MaxValue)
                        break;

                    foreach (var child in children)
                    {
                        var childNode = child.First == minNode ? child.Second : child.First;

                        if (distances[childNode] == int.MaxValue)
                            queue.Add(childNode);

                        var newDistance = child.Weight + distances[minNode];

                        if (newDistance >= distances[childNode]) continue;
                        distances[childNode] = newDistance;
                        prev[childNode] = minNode;
                        queue = new OrderedBag<int>(queue, Comparer<int>.Create((f, s) => distances[f] - distances[s]));
                    }
                }

                if (distances[end] == int.MaxValue)
                {
                    Console.WriteLine("There is no such path.");
                    return;
                }



                var path = new Stack<int>();

                var node = end;
                while (node != -1)
                {
                    path.Push(node);
                    node = prev[node];
                }

                Console.WriteLine(string.Join(" ", path));
                Console.WriteLine(distances[end]);
            }

            private static Dictionary<int, List<Edge>> ReadGraph(int e)
            {
                var result = new Dictionary<int, List<Edge>>();

                for (int i = 0; i < e; i++)
                {
                    var edgeData = Console.ReadLine()?
                        .Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries) // Framework C# code
                        .Select(int.Parse)
                        .ToArray();

                    if (edgeData == null) continue;
                    var firstNode = edgeData[0];
                    var secondNode = edgeData[1];
                    var weight = edgeData[2];

                    if (!result.ContainsKey(firstNode))
                        result.Add(firstNode, new List<Edge>());

                    if (!result.ContainsKey(secondNode))
                        result.Add(secondNode, new List<Edge>());

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
}

