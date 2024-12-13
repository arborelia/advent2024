using Day2;


namespace AdventTests
{
    [TestClass]
    public sealed class Day2Tests
    {
        public static readonly string testData = @"
7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
".Trim();

        [TestMethod]
        public void TestExample()
        {
            List<string> testLines = testData.Split(Environment.NewLine).ToList();
            int numSafe = Day2.RedNosedReports.CountSafe(testLines);
            Assert.AreEqual(2, numSafe);

            int numSafeIsh = Day2.RedNosedReports.CountSafeWithDampening(testLines);
            Assert.AreEqual(4, numSafeIsh);
        }
    }
}
