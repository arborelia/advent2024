using Day3;


namespace AdventTests
{
    [TestClass]
    public sealed class Day3Tests
    {
        public static readonly string testDataA = @"xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        public static readonly string testDataB = @"xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

        [TestMethod]
        public void TestExample()
        {
            Assert.AreEqual(161, Day3.MullItOver.SumOfMultiplications(testDataA));
            Assert.AreEqual(48, Day3.MullItOver.SumOfOptionalMultiplications(testDataB));
        }
    }
}
