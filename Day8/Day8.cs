using AdventUtils;

// solution to: https://adventofcode.com/2024/day/6
namespace Day8
{
    public readonly record struct Position(int Row, int Col)
    {
        // Find the antinode of this node on the other side of node 'other'.
        public readonly Position FindAntinode(Position other)
        {
            int rowDiff = other.Row - Row;
            int colDiff = other.Col - Col;
            return new Position(other.Row + rowDiff, other.Col + colDiff);
        }
    }

    public class AntinodeLocator(CharGrid grid)
    {
        public CharGrid Grid { get; init; } = grid;

        // Build a dictionary from node symbols to all their positions.
        public Dictionary<char, List<Position>> NodePositions()
        {
            Dictionary<char, List<Position>> result = [];
            for (int row = 0; row < Grid.Height(); row++)
            {
                for (int col = 0; col < Grid.Width(); col++)
                {
                    char ch = Grid.CharAt(row, col)!.Value;
                    if (ch != '.')
                    {
                        result.SetDefault(ch).Add(new Position(row, col));
                    }
                }
            }
            return result;
        }

        // Find the number of distinct antinodes, from all pairs of nodes with
        // matching symbols.
        public int NumAntinodes()
        {
            HashSet<Position> antinodePositions = [];
            foreach (List<Position> positions in NodePositions().Values)
            {
                foreach (Position pos1 in positions)
                {
                    foreach (Position pos2 in positions)
                    {
                        if (pos1 != pos2)
                        {
                            Position antinode = pos1.FindAntinode(pos2);
                            // check if position is in bounds
                            if (Grid.CharAt(antinode.Row, antinode.Col) != null)
                            {
                                antinodePositions.Add(antinode);
                            }
                        }
                    }
                }
            }
            return antinodePositions.Count;
        }
    }
}
