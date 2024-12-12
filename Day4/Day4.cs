using AdventUtils;

// solution to: https://adventofcode.com/2024/day/4
namespace Day4
{
    public class WordSearchGrid(List<string> lines) : AdventUtils.CharGrid(lines)
    {
        // Extract a fixed-length string by reading the grid starting from a particular row and column,
        // and proceeding in a particular direction given by deltaRow and deltaCol.
        public string ExtractWordSearch(int row, int col, int deltaRow, int deltaCol, int length)
        {
            List<char> chars = new List<char>();
            for (int i = 0; i < length; i++)
            {
                char found = CharAt(row + i * deltaRow, col + i * deltaCol) ?? '.';
                chars.Add(found);
            }
            return string.Join("", chars);
        }

        // Find whether the word "XMAS" can be read in a particular direction starting from
        // a particular row and column.
        public bool FindXmas(int row, int col, int deltaRow, int deltaCol)
        {
            return ExtractWordSearch(row, col, deltaRow, deltaCol, 4) == "XMAS";
        }

        // Find whether this location is the center of an "X" made of the word "MAS" in either direction.
        public bool FindCrossMas(int row, int col)
        {
            string slash = ExtractWordSearch(row - 1, col - 1, 1, 1, 3);
            string backslash = ExtractWordSearch(row + 1, col - 1, -1, 1, 3);
            return (
                (slash == "SAM" || slash == "MAS")
                && (backslash == "SAM" || backslash == "MAS")
            );
        }

        // Count the number of instances of "XMAS" in the word search.
        public int CountXmas()
        {
            int count = 0;
            for (int row = 0; row < Height(); row++)
            {
                for (int col = 0; col < Width(); col++)
                {
                    for (int deltaRow = -1; deltaRow <= 1; deltaRow++)
                    {
                        for (int deltaCol = -1; deltaCol <= 1; deltaCol++)
                        {
                            if (deltaRow == 0 && deltaCol == 0) continue;
                            if (FindXmas(row, col, deltaRow, deltaCol)) count++;
                        }
                    }
                }
            }
            return count;
        }

        // Count the number of Xes made of "MAS" in the word search.
        public int CountCrossMas()
        {
            int count = 0;
            for (int row = 0; row < Height(); row++)
            {
                for (int col = 0; col < Width(); col++)
                {
                    if (FindCrossMas(row, col)) count++;
                }
            }
            return count;
        }

        public static void Main()
        {
            var lines = AdventUtils.AdventIO.GetLines("input4.txt");
            var grid = new WordSearchGrid(lines);
            int xmasCount = grid.CountXmas();
            int crossmasCount = grid.CountCrossMas();
            Console.WriteLine($"XMASes found: {xmasCount}");
            Console.WriteLine($"Crossed MASes found: {crossmasCount}");
        }
    }
}
