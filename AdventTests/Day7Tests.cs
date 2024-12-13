using Day7;
using AdventTests;
using AdventUtils;

namespace AdventTests
{
    [TestClass]
    public sealed class Day7Tests
    {
        public static readonly List<string> testLines = @"
190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20
        ".Trim().Split('\n').ToList();

        [TestMethod]
        public void TestSatisfy()
        {
            Assert.IsTrue(Calculation.FromLine("190: 10 19").CanSatisfy());
            Assert.IsTrue(Calculation.FromLine("3267: 81 40 27").CanSatisfy());
            Assert.IsFalse(Calculation.FromLine("156: 15 6").CanSatisfy());
            Assert.IsTrue(Calculation.FromLine("156: 15 6").CanSatisfy(ConcatOption.Allow));
        }

        [TestMethod]
        public void TestSumOfSatisfiable()
        {
            Assert.AreEqual(3749, Calculation.SumOfSatisfiableLines(testLines));
            Assert.AreEqual(11387, Calculation.SumOfSatisfiableLines(testLines, ConcatOption.Allow));
        }
    }
}
