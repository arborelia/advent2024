using Day1;

namespace Day1Tests
{
    [TestClass]
    public sealed class Day1Tests
    {
        [TestMethod]
        public void TestExample()
        {
            NumberPair[] pairs =
            {
                new NumberPair(3, 4),
                new NumberPair(4, 3),
                new NumberPair(2, 5),
                new NumberPair(1, 3),
                new NumberPair(3, 9),
                new NumberPair(3, 3)
            };
            int diff = Day1.SortedDifferences.CalculateSortedDifference(pairs.ToList());
            Assert.AreEqual(11, diff);
        }
    }
}
