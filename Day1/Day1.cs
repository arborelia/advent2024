using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Day1
{
    public record struct NumberPair(int First, int Second);

    public static class NumberListOperations
    {
        public static List<NumberPair> FromInput(string filename)
        {
            Regex regex = new Regex(@"(\d+)\s+(\d+)");
            List<NumberPair> result = new List<NumberPair> ();
            foreach (string line in AdventUtils.AdventIO.GetLines(filename))
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    result.Add(new NumberPair(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
                }

            }
            return result;
        }

        public static int SortedDifference(List<NumberPair> pairs)
        {
            int sum = 0;
            List<int> firstNumbers = (from pair in pairs
                                       orderby pair.First
                                       select pair.First).ToList();
            List<int> secondNumbers = (from pair in pairs
                                       orderby pair.Second
                                       select pair.Second).ToList();
            for (int i = 0; i < firstNumbers.Count; i++)
            {
                int diff = firstNumbers[i] - secondNumbers[i];
                sum += Math.Abs(diff);
            }
            return sum;
        }

        public static int SimilarityScore(List<NumberPair> pairs)
        {
            var similarities = from left in pairs
                               from right in pairs
                               where left.First == right.Second
                               select left.First;
            return similarities.Sum();
        }

        public static void Main()
        {
            var pairs = Day1.NumberListOperations.FromInput("input1a.txt");
            int diff = SortedDifference(pairs);
            int sim = SimilarityScore(pairs);
            Console.WriteLine($"Sorted difference: {diff}");
            Console.WriteLine($"Similarity score: {sim}");
        }
    }

    [JsonSerializable(typeof(NumberPair))]
    internal partial class SourceGenerationContext : JsonSerializerContext { }
}
