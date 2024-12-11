using System;
using AdventIO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace Day1
{
    public record struct NumberPair(int First, int Second);

    public static class SortedDifferences
    {
        public static List<NumberPair> FromInput(string filename)
        {
            Regex regex = new Regex(@"(\d+)\s+(\d+)");
            List<NumberPair> result = new List<NumberPair> ();
            foreach (string line in AdventIO.AdventIO.GetLines(filename))
            {
                Match match = regex.Match(line);
                if (match.Success)
                {
                    result.Add(new NumberPair(int.Parse(match.Groups[1].Value), int.Parse(match.Groups[2].Value)));
                }

            }
            return result;
        }

        public static int CalculateSortedDifference(List<NumberPair> pairs)
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

        public static void Main()
        {
            var pairs = Day1.SortedDifferences.FromInput("input1a.txt");
            Console.WriteLine(CalculateSortedDifference(pairs));
        }
    }

    [JsonSerializable(typeof(NumberPair))]
    internal partial class SourceGenerationContext : JsonSerializerContext { }
}
