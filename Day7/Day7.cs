using System.Diagnostics;
using System.Text.Json;
using AdventUtils;

// solution to: https://adventofcode.com/2024/day/7
namespace Day7
{
    // The second variant of the task allows concatenation as an operator.
    // This enum enables specifying whether concatenation is allowed as a
    // self-explanatory parameter, defaulting to disallowed.
    public enum ConcatOption
    {
        Disallow,
        Allow
    }

    public readonly record struct Calculation(long Target, List<long> Values)
    {
        public static Calculation FromLine(string line)
        {
            (string targetStr, string valuesStr) = IO.Partition(line, ": ");
            long target = long.Parse(targetStr);
            List<long> values = new(valuesStr.Split(" ").Select(long.Parse));
            return new Calculation(target, values);
        }

        public bool CanSatisfy()
        {
            return CanSatisfy(ConcatOption.Disallow);
        }

        public bool CanSatisfy(ConcatOption allowConcat)
        {
            // Dynamically step backward through the list.
            // 
            // We have a set of target values we want to attain, starting with
            // the actual Target. We can attain a target value if we can make
            // it as A + B or A * B, where B is the last value in the list.
            // We drop B from the end of the list, use these possibilities for
            // A as new targets, and try to satisfy these targets the same way.
            //
            // When the list is down to a single item, it needs to match one of
            // the target values.
            SortedSet<long> targets = [Target];
            for (int pos = Values.Count - 1; pos > 0; pos--)
            {
                long value = Values[pos];
                string targetsDebug = JsonSerializer.Serialize(targets);
                SortedSet<long> newTargets = [];
                foreach (long aTarget in targets)
                {
                    // Check if we can add this value to a natural number to
                    // reach the target.
                    if (aTarget >= value)
                    {
                        newTargets.Add(aTarget - value);
                    }

                    // Check if we can multiply this value by a natural number
                    // to reach the target.
                    if (value > 0 && aTarget % value == 0)
                    {
                        newTargets.Add(aTarget / value);
                    }
                    targets = newTargets;

                    // Check if we can concatenate digits to reach the target.
                    if (allowConcat == ConcatOption.Allow)
                    {
                        string targetString = aTarget.ToString();
                        string valString = value.ToString();
                        if (targetString.EndsWith(valString))
                        {
                            string rest = targetString[0..^valString.Length];
                            if (rest.Length > 0)
                            {
                                newTargets.Add(long.Parse(rest));
                            }
                        }
                    }
                }
            }

            // If we get to the start of the list and we can start the equation
            // with the first value, then the equation can be satisfied.
            return targets.Contains(Values[0]);
        }

        public static long SumOfSatisfiableLines(List<string> lines)
        {
            return SumOfSatisfiableLines(lines, ConcatOption.Disallow);
        }

        public static long SumOfSatisfiableLines(List<string> lines, ConcatOption allowConcat)
        {
            var satisfiableTargets = from line in lines
                                     let calc = Calculation.FromLine(line)
                                     where calc.CanSatisfy(allowConcat)
                                     select calc.Target;
            return satisfiableTargets.Sum();
        }
    }
}