namespace AdventUtils
{
    // A grid of characters, where getting an out-of-bounds character returns null instead
    // of an error.
    public class CharGrid(List<string> lines)
    {
        public List<string> Lines { get; init; } = lines;

        // Get the character at a particular grid location, or null if it's off the grid.
        public char? CharAt(int row, int col)
        {
            if (row < 0 || row >= Lines.Count) return null;
            string line = Lines[row];
            if (col < 0 || col >= line.Length) return null;
            return line[col];
        }

        // Get the height of the grid
        public int Height()
        {
            return Lines.Count;
        }

        // Get the width of the grid, assuming all lines are equal length
        public int Width()
        {
            if (Height() == 0) return 0;
            return Lines[0].Length;
        }
    }
}