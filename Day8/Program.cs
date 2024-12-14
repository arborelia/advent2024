using Day8;

var lines = AdventUtils.IO.GetLines("input8.txt");
var grid = new AdventUtils.CharGrid(lines);
var locator = new AntinodeLocator(grid);
int numAntinodes = locator.NumAntinodes();
int numCollinear = locator.NumCollinear();
Console.WriteLine($"number of antinodes: {numAntinodes}");
Console.WriteLine($"number of collinear nodes: {numCollinear}");