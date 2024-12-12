using Medallion.Collections;

// solution to: https://adventofcode.com/2024/day/5
namespace Day5
{
    // A PrintQueueOrganizer encapsulates the data provided in this task: a structure of dependency
    // pairs, and then a list of orderings to check against the dependenices.
    public record struct PrintQueueOrganizer(Dictionary<int, SortedSet<int>> Deps, List<List<int>> Orderings)
    {
        public static PrintQueueOrganizer Empty()
        {
            return new PrintQueueOrganizer([], []);
        }

        // Construct an empty PrintQueueOrganizer, then fill its data from the given lines of input.
        public static PrintQueueOrganizer FromLines(List<string> lines)
        {
            var pq = PrintQueueOrganizer.Empty();
            pq.AddFromLines(lines);
            return pq;
        }

        // Add a dependency that page 'before' must be printed before page 'after'.
        public void AddDependency(int before, int after)
        {
            if (!Deps.ContainsKey(after))
            {
                Deps[after] = new SortedSet<int>();
            }
            Deps[after].Add(before);
        }

        // Get the set of page numbers that must be printed before 'after'.
        public readonly SortedSet<int> GetDepsBefore(int after)
        {
            if (Deps.TryGetValue(after, out SortedSet<int>? value))
            {
                return value;
            }
            else return [];
        }

        // Parse the lines of input and fill the data structures in the PrintQueueOrganizer.
        public void AddFromLines(List<string> lines)
        {
            foreach (var line in lines)
            {
                if (line.Contains('|'))
                {
                    var parts = line.Split('|');
                    int before = int.Parse(parts[0]);
                    int after = int.Parse(parts[1]);
                    AddDependency(before, after);
                }
                else if (line.Contains(','))
                {
                    var orderList = from part in line.Split(',')
                                    select int.Parse(part);
                    Orderings.Add(orderList.ToList());
                }
            }
        }

        public readonly bool CheckOrdering(List<int> ordering)
        {
            for (int i = 0; i < ordering.Count; i++)
            {
                int currentPage = ordering[i];
                SortedSet<int> pagesBefore = GetDepsBefore(currentPage);
                SortedSet<int> pagesAfter = new(ordering[(i + 1)..]);
                List<int> clashes = new(pagesBefore.Intersect(pagesAfter));
                if (clashes.Count > 0) return false;
            }
            return true;
        }

        public readonly List<int> FixOrdering(List<int> ordering)
        {
            var pq = this;
            List<int> GetRelevantDeps(int page)
            {
                return pq.GetDepsBefore(page).Intersect(ordering).ToList();
            }
            return new(ordering.OrderTopologicallyBy(GetRelevantDeps));
        }

        // Number of valid orderings. Not strictly needed to solve the problem, but used in tests.
        public readonly int NumValidOrderings()
        {
            return Orderings.Count(CheckOrdering);
        }

        // Sum of the middle numbers of all valid orderings, which is what the task asks for.
        public readonly int SumValidOrderings()
        {
            int sum = 0;
            foreach (var ordering in Orderings)
            {
                if (CheckOrdering(ordering))
                {
                    int midpoint = (ordering.Count - 1) / 2;
                    sum += ordering[midpoint];
                }
            }
            return sum;
        }

        // Sum of the middle numbers of all invalid orderings, after they have been corrected.
        public readonly int SumFixedOrderings()
        {
            int sum = 0;
            foreach (var ordering in Orderings)
            {
                if (!CheckOrdering(ordering))
                {
                    var reordered = FixOrdering(ordering);
                    int midpoint = (reordered.Count - 1) / 2;
                    sum += reordered[midpoint];
                }
            }
            return sum;
        }

        public static void Main()
        {
            var lines = AdventUtils.AdventIO.GetLines("input5.txt");
            var pq = PrintQueueOrganizer.FromLines(lines);
            int sum = pq.SumValidOrderings();
            int sumFixed = pq.SumFixedOrderings();
            Console.WriteLine($"Sum of valid middle pages: {sum}");
            Console.WriteLine($"Sum of corrected middle pages: {sumFixed}");
        }
    }
}
