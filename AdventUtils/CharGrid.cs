namespace AdventUtils
{
    // A grid of characters, where getting an out-of-bounds character returns null instead
    // of an error.
    public class CharGrid(List<string> lines)
    {
        public List<string> Lines { get; init; } = lines;

        // Construct a CharGrid from a newline-delimited string
        public static CharGrid FromString(string s)
        {
            var lines = s.Split(["\n", "\r", "\r\n"], StringSplitOptions.None);
            return new CharGrid([.. lines]);
        }

        // Get the character at a particular grid location, or null if it's off the grid.
        public char? CharAt(int row, int col)
        {
            if (!IsInBounds(row, col)) return null;
            return Lines[row][col];
        }

        public bool IsInBounds(int row, int col)
        {
            if (row < 0 || row >= Lines.Count) return false;
            string line = Lines[row];
            if (col < 0 || col >= line.Length) return false;
            return true;
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

        public CharGrid AlterCharAt(int row, int col, char newChar)
        {
            // Make a copy
            CharGrid newGrid = new([.. Lines]);
            string rowToAlter = newGrid.Lines[row];
            newGrid.Lines[row] = rowToAlter[..col] + newChar.ToString() + rowToAlter[(col + 1)..];
            return newGrid;
        }
    }
}