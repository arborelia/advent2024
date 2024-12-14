using Day9;
using AdventTests;
using AdventUtils;

namespace AdventTests
{
    [TestClass]
    public sealed class Day9Tests
    {
        public static readonly string testString = "2333133121414131402";

        [TestMethod]
        public void TestDigits()
        {
            Assert.AreEqual(0, DiskCompactor.DigitCharToNumber('0'));
            Assert.AreEqual(9, DiskCompactor.DigitCharToNumber('9'));
            Assert.AreEqual('0', DiskCompactor.NumberToDigitChar(0));
            Assert.AreEqual('9', DiskCompactor.NumberToDigitChar(9));
        }

        [TestMethod]
        public void TestCompaction()
        {
            Assert.AreEqual(60, DiskCompactor.CompactChecksum("12345"));
            Assert.AreEqual(1928, DiskCompactor.CompactChecksum(testString));
        }

        [TestMethod]
        public void TestSpanChecksum()
        {
            var span = new DiskSpan(2, 10, 3, true);
            Assert.AreEqual(66, span.Checksum());
        }

        [TestMethod]
        public void TestDefragment()
        {
            Assert.AreEqual(92, DiskCompactor.DefragmentedChecksum("12355"));
            Assert.AreEqual(2858, DiskCompactor.DefragmentedChecksum(testString));
        }
    }
}
