using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCruncher
{
    public class WordCruncher
    {
        private SortedSet<string> results = new SortedSet<string>();
        private List<Node> permutation;

        public WordCruncher(string[] input, string target)
        {
            permutation = CreatePermutations(input.OrderBy(s => s).ToList(), target);
            foreach (var path in GetAllPaths())
            {
                var result = string.Join(' ', path);
                if (!results.Contains(result))
                {
                    results.Add(result);
                }
            }
        }


        private List<Node> CreatePermutations(List<string> input, string target)
        {
            if (string.IsNullOrEmpty(target) || input.Count() == 0)
                return null;


            List<Node> returnValues = null;
            for (int i = 0; i < input.Count(); i++)
            {
                var key = input[i];

                if (target.StartsWith(key))
                {
                    var node = new Node()
                    {
                        Key = key,
                        Value = CreatePermutations(input.Where((s ,Index) => Index != i).ToList(),
                        target.Substring(key.Length))
                    };

                    if (node.Value == null && node.Key != target)
                        continue;

                    if (returnValues == null)
                    {
                        returnValues = new List<Node>();
                    }
                    returnValues.Add(node);
                }
            }

            return returnValues;
        }

        public IEnumerable<string> GetPaths()
        {
            foreach (var result in results)
            {
                yield return result;
            }
        }

        public IEnumerable<IEnumerable<string>> GetAllPaths()
        {
            List<string> way = new List<string>();
            foreach (var key in VisitPath(permutation, new List<string>()))
            {
                if (key == null)
                {
                    yield return way;
                    way = new List<string>();
                }
                else
                {
                    way.Add(key);
                }
            }
        }

        private IEnumerable<string> VisitPath(List<Node> permutation, List<string> path)
        {
            if (permutation == null)
            {
                foreach (var item in path)
                {
                    yield return item;
                }
                yield return null;
            }
            else
            {
                foreach (var node in permutation)
                {
                    path.Add(node.Key);
                    foreach (var item in VisitPath(node.Value, path))
                    {
                        yield return item;
                    }
                    path.RemoveAt(path.Count - 1);
                }
            }
        }
    }

    public class Node
    {
        public string Key { get; set; }

        public List<Node> Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine()?.Split(", ");
            var target = Console.ReadLine();

            WordCruncher wc = new WordCruncher(input, target);

            foreach (var path in wc.GetPaths())
            {
                Console.WriteLine(path);
            }
        }
    }
}
