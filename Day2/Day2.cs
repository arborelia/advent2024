using System.Runtime.CompilerServices;
using System.Diagnostics;

// solution to: https://adventofcode.com/2024/day/2
namespace Day2
{
    // A Report is a list of integers (levels).
    public record struct Report(List<int> levels)
    {
        // We judge a report to be "safe" if all differences between adjacent level are within a particular range.
        // The optional "Problem Dampener" can turn an unsafe report into a safe one by removing one level from the sequence.
        public bool DifferencesInRange(int min, int max, bool dampenProblems)
        {
            bool allowDamp = dampenProblems;
            for (int i = 1; i < levels.Count; i++)
            {
                int diff = levels[i] - levels[i-1];
                if (diff < min || diff > max)
                {
                    // This difference is out of range.
                    if (allowDamp)
                    {
                        // Try to resolve the problem with a "damped" versions of this report.
                        // We try removing the previous element and the current one, then test those without dampening.
                        List<int> dampedListA = levels.Take(i - 1).Concat(levels.Skip(i)).ToList();
                        List<int> dampedListB = levels.Take(i).Concat(levels.Skip(i + 1)).ToList();
                        Report dampedReportA = new Report(dampedListA);
                        Report dampedReportB = new Report(dampedListB);
                        return dampedReportA.DifferencesInRange(min, max, false) || dampedReportB.DifferencesInRange(min, max, false);
                    }
                    else return false;
                }
            }
            return true;
        }

        // Construct a Report from a space-separated input line.
        public static Report FromLine(string line)
        {
            var parts = line.Split(' ');
            var nums = from part in parts select int.Parse(part);
            return new Report(nums.ToList());
        }

        // A report is specified to be safe if all its differences are in [-3 .. -1] or all its differences are in [1 .. 3].
        public bool IsSafe(bool dampenProblems)
        {
            return DifferencesInRange(-3, -1, dampenProblems) || DifferencesInRange(1, 3, dampenProblems);
        }
    }

    public static class RedNosedReports
    {
        // Count the number of safe reports, either with or without problem dampening.
        public static int CountSafeEnough(List<string> lines, bool dampenProblems)
        {
            int count = 0;
            foreach (string line in lines)
            {
                Report report = Report.FromLine(line);
                if (report.IsSafe(dampenProblems)) count += 1;
            }
            return count;
        }

        // Count the number of already safe reports.
        public static int CountSafe(List<string> lines)
        {
            return CountSafeEnough(lines, false);
        }

        // Count the number of safe reports with problem dampening.
        public static int CountSafeWithDampening(List<string> lines)
        {
            return CountSafeEnough(lines, true);
        }

        public static void Main()
        {
            var lines = AdventUtils.AdventIO.GetLines("input2.txt");
            var numSafe = CountSafe(lines);
            var numSafeIsh = CountSafeWithDampening(lines);
            Console.WriteLine($"Number of safe reports: {numSafe}");
            Console.WriteLine($"With problems dampener: {numSafeIsh}");
        }
    }
}