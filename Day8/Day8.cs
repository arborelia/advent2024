using AdventUtils;

// solution to: https://adventofcode.com/2024/day/6
namespace Day8
{
    public readonly record struct Position(int Row, int Col)
    {
        // Euclidean algorithm for greatest common divisor, adapted for
        // signed ints by taking the absolute value.
        private static int GCD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a | b;
        }

        // Find the antinode of this node on the other side of node 'other'.
        public readonly Position FindAntinode(Position other)
        {
            int rowDiff = other.Row - Row;
            int colDiff = other.Col - Col;
            return new Position(other.Row + rowDiff, other.Col + colDiff);
        }
        // Find all nodes collinear with this and other, in the direction of
        // other. (We'll find the rest when the arguments are swapped.)
        // 'size' is a number at least as large as the grid's dimensions,
        // which tells us when to stop.
        public readonly List<Position> FindCollinear(Position other, int size)
        {
            int rowDiff = other.Row - Row;
            int colDiff = other.Col - Col;

            // A collinear point can be between the nodes. These happen when
            // the GCD of the row and column differences is greater than 1.
            int gcd = GCD(rowDiff, colDiff);
            rowDiff /= gcd;
            colDiff /= gcd;

            List<Position> positions = [];
            for (int mul = 0; ; mul++)
            {
                int row = Row + rowDiff * mul;
                int col = Col + colDiff * mul;
                if (row < 0 || col < 0 || row >= size || col >= size)
                {
                    return positions;
                }
                positions.Add(new(row, col));
            }
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

        // Find the number of positions for collinear nodes.
        public int NumCollinear()
        {
            HashSet<Position> collinearPositions = [];
            int size = Math.Max(Grid.Height(), Grid.Width());
            foreach (List<Position> positions in NodePositions().Values)
            {
                foreach (Position pos1 in positions)
                {
                    foreach (Position pos2 in positions)
                    {
                        if (pos1 != pos2)
                        {
                            foreach (Position collinear in pos1.FindCollinear(pos2, size))
                            {
                                // check if position is in bounds
                                if (Grid.CharAt(collinear.Row, collinear.Col) != null)
                                {
                                    collinearPositions.Add(collinear);
                                }
                            }
                        }
                    }
                }
            }
            return collinearPositions.Count;
        }

    }
}
