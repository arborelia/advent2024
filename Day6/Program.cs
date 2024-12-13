using Day6;

var lines = AdventUtils.AdventIO.GetLines("input6.txt");
var grid = new AdventUtils.CharGrid(lines);
var tracker = new GuardTracker(grid);
var numVisited = tracker.SimulateGuard();
var numSuggestedObstructions = tracker.SuggestObstructions();
Console.WriteLine($"positions visited: {numVisited}");
Console.WriteLine($"suggested obstructions: {numSuggestedObstructions}");