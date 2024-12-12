// solution to: https://adventofcode.com/2024/day/4
namespace Day5
{
    public record struct PrintQueueOrganizer(Dictionary<int, SortedSet<int>> Deps, List<List<int>> Orderings)
    {
        public static PrintQueueOrganizer Empty()
        {
            return new PrintQueueOrganizer([], []);
        }

        public static PrintQueueOrganizer FromLines(List<string> lines)
        {
            var pq = PrintQueueOrganizer.Empty();
            pq.AddFromLines(lines);
            return pq;
        }

        public void AddDependency(int before, int after)
        {
            if (!Deps.ContainsKey(after))
            {
                Deps[after] = new SortedSet<int>();
            }
            Deps[after].Add(before);
        }

        public readonly SortedSet<int> GetDepsBefore(int after)
        {
            if (Deps.TryGetValue(after, out SortedSet<int>? value))
            {
                return value;
            }
            else return [];
        }

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

        public static void Main()
        {
        }
    }
}
