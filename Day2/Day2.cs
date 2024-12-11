using System.Runtime.CompilerServices;
using System.Diagnostics;

namespace Day2
{
    public record struct Report(List<int> levels)
    {
        public bool DifferencesInRange(int min, int max, bool dampenProblems)
        {
            bool allowDamp = dampenProblems;
            for (int i = 1; i < levels.Count; i++)
            {
                int diff = levels[i] - levels[i-1];
                if (diff < min || diff > max)
                {
                    if (allowDamp)
                    {
                        List<int> dampedListA = levels.Take(i - 1).Concat(levels.Skip(i)).ToList();
                        List<int> dampedListB = levels.Take(i).Concat(levels.Skip(i + 1)).ToList();
                        Report dampedReportA = new Report(dampedListA);
                        Report dampedReportB = new Report(dampedListB);
                        return dampedReportA.DifferencesInRange(min, max, false) || dampedReportB.DifferencesInRange(min, max, false);
                    }
                    else return false;
                }
            }
            int current = levels[0];
            foreach (int level in levels.Skip(1))
            {
                int diff = level - current;
                if (diff < min || diff > max) return false;
                current = level;
            }
            return true;
        }

        public static Report FromLine(string line)
        {
            var parts = line.Split(' ');
            var nums = from part in parts select int.Parse(part);
            return new Report(nums.ToList());
        }

        public bool IsSafe(bool dampenProblems)
        {
            return DifferencesInRange(-3, -1, dampenProblems) || DifferencesInRange(1, 3, dampenProblems);
        }
    }

    public static class RedNosedReports
    {
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

        public static int CountSafe(List<string> lines)
        {
            return CountSafeEnough(lines, false);
        }

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