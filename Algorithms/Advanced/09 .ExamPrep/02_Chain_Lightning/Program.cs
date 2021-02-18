using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Wintellect.PowerCollections;

namespace _02_Chain_Lightning
{
    class Edge
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{First} {Second} {Weight}";
        }
    }

    class Program
    {
        private static Dictionary<int, List<Edge>> _edgesByNode = new Dictionary<int, List<Edge>>();

        private static int[] _damageTaken;

        static void Main(string[] args)
        {
            // var queue = new OrderedBag<int>(Comparer<int>.Create((f, s) => s - f));
            var v = int.Parse(Console.ReadLine() ?? string.Empty);
            var e = int.Parse(Console.ReadLine() ?? string.Empty);
            var lightnings = int.Parse(Console.ReadLine() ?? string.Empty);
            for (int i = 0; i < v; i++)
                _edgesByNode[i] = new List<Edge>();
            ReadGraph(e);
            _damageTaken = new int[v];

            for (int i = 0; i < v; i++)
                _edgesByNode[i] = _edgesByNode[i].OrderBy(x => x.Weight).ToList();

            for (int i = 0; i < lightnings; i++)
            {
                var lightningStrike = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var target = lightningStrike[0];
                var force = lightningStrike[1];

                var isHit = new bool[v];
                DFS(target, isHit, force);
                
            }
            Console.WriteLine(_damageTaken.Max());
        }

        private static void DFS(int currentTarget, bool[] isHit, int force)
        {
            if (isHit[currentTarget]) return;
            isHit[currentTarget] = true;
            _damageTaken[currentTarget] += force;
            var halfForce = force /= 2;
  
            foreach (var currentTargetEdge in _edgesByNode[currentTarget])
            {
                var currentTargetChild = currentTargetEdge.First == currentTarget ? currentTargetEdge.Second : currentTargetEdge.First;
                if (isHit[currentTargetChild]) continue;
                DFS(currentTargetChild, isHit, halfForce);
            }
        }

        private static void ReadGraph(int e)
        {
            for (int i = 0; i < e; i++)
            {
                var edgeData = Console.ReadLine()?
                    .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)// Framework C# code
                    .Select(int.Parse)
                    .ToArray();

                if (edgeData == null) continue;
                var firstNode = edgeData[0];
                var secondNode = edgeData[1];
                var weight = edgeData[2];

                var edge = new Edge
                {
                    First = firstNode,
                    Second = secondNode,
                    Weight = weight
                };
                _edgesByNode[firstNode].Add(edge);
                _edgesByNode[secondNode].Add(edge);
            }
        }
    }
}
