using Day1;

namespace AdventTests
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
            int diff = Day1.NumberListOperations.SortedDifference(pairs.ToList());
            Assert.AreEqual(11, diff);
            int sim = Day1.NumberListOperations.SimilarityScore(pairs.ToList());
            Assert.AreEqual(31, sim);
        }
    }
}
