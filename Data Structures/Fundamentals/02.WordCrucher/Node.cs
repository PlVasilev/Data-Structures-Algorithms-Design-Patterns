using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _02.WordCruncher
{
    public class WordCruncher
    {
        private List<WordCrucher.Node> permutation;

        public WordCruncher(string[] input, string target)
        {
            permutation = CreatePermutations(input.OrderBy(s => s), target);
        }

        private List<WordCrucher.Node> CreatePermutations(IEnumerable<string> input, string target)
        {
            if (string.IsNullOrEmpty(target) || !input.Any())
                return null;

            List<WordCrucher.Node> returnValues = null;
            foreach (var item in input)
            {
                if (target.StartsWith(item))
                {
                    if (returnValues == null)
                    {
                        returnValues = new List<WordCrucher.Node>();
                    }
                    var node = new WordCrucher.Node()
                    {
                        Key = item,
                        Value = CreatePermutations(input.Except(new string[] { item }),
                            target.Substring(item.Length))
                    };

                    if (node.Value == null && node.Key != target)
                        continue;
                    returnValues.Add(node);
                }
            }

            return returnValues;
        }

        public IEnumerable<IEnumerable<string>> GetPaths()
        {
            List<string> way = new List<string>();
            foreach (var key in VisitPath(permutation, new List<string>()))
            {
                if (key == null)
                {
                    yield return way;
                    way.Clear();
                }
                else
                {
                    way.Add(key);
                }
            }
        }

        private IEnumerable<string> VisitPath(List<WordCrucher.Node> nodes, List<string> path)
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

        public List<WordCrucher.Node> Value { get; set; }
    }
}
