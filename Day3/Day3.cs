using System.Diagnostics;
using System.Text.RegularExpressions;

// solution to: https://adventofcode.com/2024/day/3
namespace Day3
{
    public static class MullItOver
    {
        private static readonly Regex MultExpression = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
        private static readonly Regex DoOrDontExpression = new Regex(@"(do|don't)\(\)");

        // Find subsequences like mul(X,Y), where X and Y are 1-3 digit non-negative numbers.
        public static IEnumerable<Match> FindInstructions(string garble)
        {
            return MultExpression.Matches(garble);
        }

        // Get the value of a mul(X,Y) expression.
        public static int MultValue(Match mult)
        {
            return int.Parse(mult.Groups[1].Value) * int.Parse(mult.Groups[2].Value);
        }

        // Get the sum of all multiplications found in the string.
        public static int SumOfMultiplications(string garble)
        {
            var multiplications = from mult in FindInstructions(garble)
                                  select MultValue(mult);
            return multiplications.Sum();
        }

        // Toggle multiplications on or off with the do() and don't() instructions.
        // Sum only the multiplications in parts of the string where multiplications are toggled on.
        public static int SumOfOptionalMultiplications(string garble)
        {
            int pos = 0;
            int sum = 0;
            bool multipliersOn = true;
            while (true)
            {
                var multMatch = MultExpression.Match(garble, pos);
                var doMatch = DoOrDontExpression.Match(garble, pos);
                if (!multMatch.Success && !doMatch.Success)
                {
                    return sum;
                }
                var multMatchPos = multMatch.Success ? multMatch.Index : int.MaxValue;
                var doMatchPos = doMatch.Success ? doMatch.Index : int.MaxValue;
                if (multMatchPos < doMatchPos)
                {
                    // Debug.WriteLine($"found a mult: {multMatch.Value}")
                    if (multipliersOn)
                    {
                        sum += MultValue(multMatch);
                    }
                    pos = multMatch.Index + multMatch.Length;
                }
                else
                {
                    string instruction = doMatch.Groups[1].Value;
                    multipliersOn = (instruction == "do");
                    pos = doMatch.Index + doMatch.Length;
                }
            }
        }

        public static void Main()
        {
            string garble = AdventUtils.AdventIO.GetString("input3.txt");
            int sumOfMults = SumOfMultiplications(garble);
            int sumOfOptionalMults = SumOfOptionalMultiplications(garble);
            Console.WriteLine($"Sum of all multiplications: {sumOfMults}");
            Console.WriteLine($"Sum of optional multiplications: {sumOfOptionalMults}");
        }
    }
}