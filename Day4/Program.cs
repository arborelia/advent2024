var lines = AdventUtils.IO.GetLines("input4.txt");
var grid = new Day4.WordSearchGrid(lines);
int xmasCount = grid.CountXmas();
int crossmasCount = grid.CountCrossMas();
Console.WriteLine($"XMASes found: {xmasCount}");
Console.WriteLine($"Crossed MASes found: {crossmasCount}");
