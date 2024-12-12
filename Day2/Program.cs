var lines = AdventUtils.AdventIO.GetLines("input2.txt");
var numSafe = Day2.RedNosedReports.CountSafe(lines);
var numSafeIsh = Day2.RedNosedReports.CountSafeWithDampening(lines);
Console.WriteLine($"Number of safe reports: {numSafe}");
Console.WriteLine($"With problems dampener: {numSafeIsh}");
