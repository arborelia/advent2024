using System;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

// solution to: https://adventofcode.com/2024/day/1
namespace Day1
{
    public record struct NumberPair(int Left, int Right);

    public static class NumberListOperations
    {
        // Make a list of NumberPairs from whitespace-separated lines of input.
        public static List<NumberPair> PairsFromInput(string filename)
        {
            Regex regex = new Regex(@"(\d+)\s+(\d+)");
            List<NumberPair> result = new List<NumberPair>();
            foreach (string line in AdventUtils.IO.GetLines(filename))
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    result.Add(new NumberPair(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
                }

            }
            return result;
        }

        // Find the sum of absolute differences between the sorted sequences of left and right items.
        public static int SortedDifference(List<NumberPair> pairs)
        {
            int sum = 0;
            List<int> leftNumbers = (from pair in pairs
                                     orderby pair.Left
                                     select pair.Left).ToList();
            List<int> rightNumbers = (from pair in pairs
                                      orderby pair.Right
                                      select pair.Right).ToList();
            for (int i = 0; i < leftNumbers.Count; i++)
            {
                int diff = leftNumbers[i] - rightNumbers[i];
                sum += Math.Abs(diff);
            }
            return sum;
        }

        // Find the sum of all pairwise matches between a left item and a right item.
        public static int SimilarityScore(List<NumberPair> pairs)
        {
            var similarities = from left in pairs
                               from right in pairs
                               where left.Left == right.Right
                               select left.Left;
            return similarities.Sum();
        }
    }

    // For debugging purposes, make NumberPairs JSON-serializable.
    [JsonSerializable(typeof(NumberPair))]
    internal partial class SourceGenerationContext : JsonSerializerContext { }
}
