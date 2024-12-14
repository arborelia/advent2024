var lines = AdventUtils.IO.GetLines("input5.txt");
var pq = Day5.PrintQueueOrganizer.FromLines(lines);
int sum = pq.SumValidOrderings();
int sumFixed = pq.SumFixedOrderings();
Console.WriteLine($"Sum of valid middle pages: {sum}");
Console.WriteLine($"Sum of corrected middle pages: {sumFixed}");
