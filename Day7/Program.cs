using Day7;

var lines = AdventUtils.IO.GetLines("input7.txt");
long sum = Calculation.SumOfSatisfiableLines(lines, ConcatOption.Disallow);
long sumWithConcat = Calculation.SumOfSatisfiableLines(lines, ConcatOption.Allow);
Console.WriteLine($"sum of satisfiable: {sum}");
Console.WriteLine($"with concatenation: {sumWithConcat}");
