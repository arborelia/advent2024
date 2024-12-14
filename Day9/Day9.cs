using System.Diagnostics;

namespace Day9
{
    public class DiskSpan(int index, int start, int length, bool isFile)
    {
        public int Index { get; set; } = index;
        public int Start { get; set; } = start;
        public int Length { get; set; } = length;
        public bool IsFile { get; set; } = isFile;
        public int End
        {
            get
            {
                return Start + Length;
            }
        }
        public long Checksum()
        {
            // trapezoidal formula for sum of consecutive numbers
            long indexSum = (Start + End - 1) * Length / 2;
            return indexSum * Index;
        }
    }

    public class DiskCompactor
    {
        public static int DigitCharToNumber(char ch)
        {
            return ch - '0';
        }

        public static char NumberToDigitChar(int digit)
        {
            return (char)('0' + digit);
        }

        // Make a list of DiskSpans, representing either files or free space,
        // from the input string format that alternates files and free spaces.
        //
        // For convenience, free spaces have an 'index' that doesn't matter.
        public static List<DiskSpan> BuildSpans(string input)
        {
            bool isFile = true;
            int index = 0;
            int pos = 0;
            List<DiskSpan> spans = [];
            foreach (char ch in input)
            {
                int length = DigitCharToNumber(ch);
                spans.Add(new DiskSpan(index, pos, length, isFile));
                pos += length;
                if (isFile)
                {
                    isFile = false;
                }
                else
                {
                    isFile = true;
                    index++;
                }
            }
            return spans;
        }

        // compact the files described by these spans, and return their checksum
        public static long CompactChecksum(string input)
        {
            var spans = BuildSpans(input);
            int frontIndex = 0;
            long checksum = 0;
            int backIndex = spans.Count - 1;
            int writeIndex = 0;
            int remainingBack = spans[backIndex].Length;
            while (true)
            {
                // add the indices in a front file to the checksum
                if (!spans[frontIndex].IsFile) throw new Exception("expected file in front");
                var fileLength = spans[frontIndex].Length;
                // if we reach a file that we've already been using to fill
                // gaps, then the length is the number of blocks left in the file
                if (frontIndex == backIndex) fileLength = remainingBack;
                for (int i = 0; i < fileLength; i++)
                {
                    checksum += writeIndex * spans[frontIndex].Index;
                    writeIndex++;
                }

                // advance to the gap between front files
                frontIndex++;
                if (frontIndex > backIndex) return checksum;
                if (spans[frontIndex].IsFile) throw new Exception("expected free space");


                // fill the gap with blocks from a back file, adding them to the checksum
                for (int i = 0; i < spans[frontIndex].Length; i++)
                {
                    if (remainingBack == 0)
                    {
                        // skip over gaps between back files
                        backIndex -= 2;
                        if (frontIndex > backIndex) return checksum;
                        if (!spans[backIndex].IsFile) throw new Exception("expected file in back");
                        remainingBack = spans[backIndex].Length;
                    }
                    remainingBack -= 1;
                    checksum += writeIndex * spans[backIndex].Index;
                    writeIndex++;
                }

                // advance to the next front file
                frontIndex++;
                if (frontIndex > backIndex) return checksum;
            }
        }

        public static long DefragmentedChecksum(string input)
        {
            // We don't need to account for gaps anymore, only keep track of
            // where each file is
            List<DiskSpan> spans = BuildSpans(input).Where(span => span.IsFile).ToList();
            List<DiskSpan> spansInOrder = [.. spans];
            for (int spanIndex = spans.Count - 1; spanIndex >= 0; spanIndex--)
            {
                var currentSpan = spans[spanIndex];
                if (spanIndex != currentSpan.Index) throw new Exception("got spans mixed up");

                // Using the new order of the file spans, find a gap that's big enough, if possible
                for (int i = 0; i < spansInOrder.Count - 1; i++)
                {
                    int spanEnd = spansInOrder[i].Start + spansInOrder[i].Length;
                    int gapLength = spansInOrder[i + 1].Start - spanEnd;

                    // If the span fits in the gap, and the gap is to the left of
                    // currentSpan, then move currentSpan.
                    if (currentSpan.Length <= gapLength && spanEnd < currentSpan.Start)
                    {
                        currentSpan.Start = spanEnd;
                        if (!spansInOrder.Remove(currentSpan))
                        {
                            throw new Exception("couldn't remove span");
                        }
                        spansInOrder.Insert(i + 1, currentSpan);
                        break;
                    }
                }
            }
            return spansInOrder.Select(span => span.Checksum()).Sum();
        }
    }
}