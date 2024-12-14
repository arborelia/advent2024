using Day10;
using AdventTests;
using AdventUtils;

namespace AdventTests
{
    [TestClass]
    public sealed class Day10Tests
    {
        public static readonly string testString = @"
0123
1234
8765
9876
        ".Trim();
        public static readonly CharGrid testGrid = CharGrid.FromString(testString);
        public static readonly string testString2 = @"
89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732
        ".Trim();
        public static readonly CharGrid testGrid2 = CharGrid.FromString(testString2);

        [TestMethod]
        public void TestSomething()
        {
        }
    }
}
