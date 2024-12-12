using Day5;

namespace AdventTests
{
    [TestClass]
    public sealed class Day5Tests
    {
        public static readonly string testPQ = @"
47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47
        ".Trim();
        public static readonly PrintQueueOrganizer pq = PrintQueueOrganizer.FromLines(testPQ.Split("\n").ToList());

        [TestMethod]
        public void TestDependencies()
        {
            var depsOf13 = pq.GetDepsBefore(13);
            Assert.IsTrue(depsOf13.Contains(47));
            Assert.IsFalse(depsOf13.Contains(46));

            var depsOf14 = pq.GetDepsBefore(14);
            Assert.AreEqual(0, depsOf14.Count);
        }

        [TestMethod]
        public void TestValidOrderings()
        {
            Assert.AreEqual(3, pq.NumValidOrderings());
            Assert.AreEqual(143, pq.SumValidOrderings());
        }

        [TestMethod]
        public void TestFixOrdering()
        {
            List<int> expected = [97, 75, 47, 61, 53];
            CollectionAssert.AreEqual(expected, pq.FixOrdering([75, 97, 47, 61, 53]));

            List<int> expected2 = [61, 29, 13];
            CollectionAssert.AreEqual(expected2, pq.FixOrdering([61, 13, 29]));
        }

        [TestMethod]
        public void TestFixOrderings()
        {
            Assert.AreEqual(123, pq.SumFixedOrderings());
        }
    }
}
