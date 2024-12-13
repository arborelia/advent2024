using Day4;

namespace AdventTests
{
    [TestClass]
    public sealed class Day4Tests
    {
        public static readonly string testGrid = @"
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
        ".Trim();

        [TestMethod]
        public void TestCharAt()
        {
            WordSearchGrid grid = new WordSearchGrid(testGrid.Split(Environment.NewLine).ToList());
            Assert.AreEqual('M', grid.CharAt(0, 0)!);
            Assert.IsNull(grid.CharAt(-1, 0));
            Assert.IsNull(grid.CharAt(3, 20));
            Assert.IsNull(grid.CharAt(3, -1));
            Assert.IsNull(grid.CharAt(20, 20));
        }

        [TestMethod]
        public void TestFindXmas()
        {
            WordSearchGrid grid = new WordSearchGrid(testGrid.Split(Environment.NewLine).ToList());
            Assert.IsTrue(grid.FindXmas(0, 5, 0, 1));
            Assert.IsTrue(grid.FindXmas(0, 4, 1, 1));
            Assert.IsFalse(grid.FindXmas(0, 5, -1, 1));
            Assert.IsFalse(grid.FindXmas(0, 5, 1, 0));
        }

        [TestMethod]
        public void TestCountXmas()
        {
            WordSearchGrid grid = new WordSearchGrid(testGrid.Split(Environment.NewLine).ToList());
            Assert.AreEqual(18, grid.CountXmas());
        }

        [TestMethod]
        public void TestCountCrossMas()
        {
            WordSearchGrid grid = new WordSearchGrid(testGrid.Split("\n").ToList());
            Assert.AreEqual(9, grid.CountCrossMas());
        }
    }
}
