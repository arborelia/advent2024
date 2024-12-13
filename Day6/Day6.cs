using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using AdventUtils;

// solution to: https://adventofcode.com/2024/day/6
namespace Day6
{
    public readonly record struct GuardState(int Row, int Col, int DeltaRow, int DeltaCol)
    {
        // The next GuardState when the guard steps forward.
        public readonly GuardState Advance()
        {
            return new GuardState(Row + DeltaRow, Col + DeltaCol, DeltaRow, DeltaCol);
        }

        // The next GuardState when the guard turns.
        public readonly GuardState Turn()
        {
            return new GuardState(Row, Col, DeltaCol, -DeltaRow);
        }
    }

    public class GuardTracker(CharGrid grid)
    {
        public CharGrid Grid { get; init; } = grid;

        // Find the guard's starting position, indicated by the ^ character.
        public GuardState GetGuardPosition()
        {
            for (int row = 0; row < Grid.Height(); row++)
            {
                for (int col = 0; col < Grid.Width(); col++)
                {
                    if (Grid.CharAt(row, col) == '^')
                    {
                        return new(row, col, -1, 0);
                    }
                }
            }
            throw new Exception("Grid does not contain ^");
        }

        // What is the character in the next cell the guard is facing?
        // Return a space if the guard is facing the edge of the grid.
        public char FacingCharacter(GuardState state)
        {
            GuardState nextState = state.Advance();
            return Grid.CharAt(nextState.Row, nextState.Col) ?? ' ';
        }

        // Find the number of positions the guard visits, assuming the guard's
        // simulation does not loop.
        public int SimulateGuard()
        {
            GuardState state = GetGuardPosition();
            HashSet<(int, int)> positionsVisited = [];

            while (true)
            {
                positionsVisited.Add((state.Row, state.Col));
                // Find the character in the position the next step will lead to. If it's
                // a wall, turn right. Keep turning right if that faces another wall.
                while (FacingCharacter(state) == '#')
                {
                    state = state.Turn();
                }
                if (FacingCharacter(state) == ' ') return positionsVisited.Count;
                state = state.Advance();
            }
        }

        // Check if the guard gets stuck in a loop on this grid.
        public bool IsGuardLooping()
        {
            GuardState state = GetGuardPosition();
            HashSet<GuardState> statesVisited = [];
            while (true)
            {
                if (statesVisited.Contains(state)) return true;
                statesVisited.Add(state);
                // Find the character in the position the next step will lead to. If it's
                // a wall, turn right. Keep turning right if that faces another wall.
                while (FacingCharacter(state) == '#')
                {
                    state = state.Turn();
                    if (statesVisited.Contains(state)) return true;
                    statesVisited.Add(state);
                }
                if (FacingCharacter(state) == ' ') return false;
                state = state.Advance();
            }
        }

        // Search for places where we can add an obstruction and cause the guard to loop.
        // The only relevant places are ones that are along the guard's original path.
        public int SuggestObstructions()
        {
            HashSet<(int, int)> suggestedObstructions = [];
            GuardState state = GetGuardPosition();

            while (true)
            {
                int row = state.Row;
                int col = state.Col;
                // We can only put obstructions in open locations, marked with
                // a dot
                if (Grid.CharAt(row, col) == '.')
                {
                    GuardTracker newTracker = new(Grid.AlterCharAt(row, col, '#'));
                    if (newTracker.IsGuardLooping())
                    {
                        suggestedObstructions.Add((row, col));
                    }
                }

                // Find the character in the position the next step will lead to. If it's
                // a wall, turn right. Keep turning right if that faces another wall.
                while (FacingCharacter(state) == '#')
                {
                    state = state.Turn();
                }
                if (FacingCharacter(state) == ' ') return suggestedObstructions.Count;
                state = state.Advance();
            }
        }
    }
}