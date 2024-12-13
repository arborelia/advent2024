using Day6;
using AdventTests;
using AdventUtils;

namespace AdventTests
{
    [TestClass]
    public sealed class Day6Tests
    {
        public static readonly string testGrid = @"
....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...
        ".Trim();
        private static readonly CharGrid grid = new(testGrid.Split(Environment.NewLine).ToList());
        public static readonly GuardTracker tracker = new(grid);

        [TestMethod]
        public void TestGuardTracker()
        {
            Assert.AreEqual(41, tracker.SimulateGuard());
        }

        [TestMethod]
        public void TestObstructions()
        {
            Assert.AreEqual(6, tracker.SuggestObstructions());
        }
    }
}
