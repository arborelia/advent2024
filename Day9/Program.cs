using Day9;

var data = AdventUtils.IO.GetString("input9.txt").Trim();
long checksum = DiskCompactor.CompactChecksum(data);
long defrag = DiskCompactor.DefragmentedChecksum(data);
Console.WriteLine($"compacted checksum: {checksum}");
Console.WriteLine($"defragmented checksum: {defrag}");
