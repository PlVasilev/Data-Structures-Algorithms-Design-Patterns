using System;
using System.Collections.Generic;

namespace _07_Word_Cruncher_AS
{
    class Program
    {
        private static string target;
        private static string current;
        private static Dictionary<int, List<string>> wordsByLen;
        private static Dictionary<string, int> occurences = new Dictionary<string, int>();
        private static List<string> selectedWords = new List<string>();
        private static HashSet<string> results = new HashSet<string>();

        static void Main(string[] args)
        {
            var words = Console.ReadLine().Split(", ");
            target = Console.ReadLine();

            wordsByLen = new Dictionary<int, List<string>>();

            foreach (var word in words)
            {
                if (!target.Contains(word)) continue;
                
                var len = word.Length;
                if (!wordsByLen.ContainsKey(len))
                    wordsByLen.Add(len , new List<string>());

                if (occurences.ContainsKey(word))
                    occurences[word] += 1;
                else
                    occurences.Add(word,1);
                
                wordsByLen[len].Add(word);
            }

            current = string.Empty;
            GenSolution(target.Length);

            Console.WriteLine(string.Join(Environment.NewLine, results));
        }

        private static void GenSolution(int len)
        {
            if (len == 0)
            {
                if (current == target)
                    results.Add(string.Join(" ", selectedWords));
                return;
            }

            foreach (var kvp in wordsByLen)
            {
                if (kvp.Key > len) continue;
                
                foreach (var word in kvp.Value)
                {
                    if (occurences[word] <= 0) continue;
                    current += word;

                    if (IsMatching(target, current))
                    {
                        occurences[word] -= 1;
                        selectedWords.Add(word);
                        GenSolution(len - word.Length);
                        selectedWords.RemoveAt(selectedWords.Count - 1);
                        occurences[word] += 1;
                    }
                    current = current.Remove(current.Length - word.Length, word.Length);
                }
            }
        }

        private static bool IsMatching(string expected, string actual)
        {
            for (int i = 0; i < actual.Length; i++)
            {
                if (expected[i] != actual[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
