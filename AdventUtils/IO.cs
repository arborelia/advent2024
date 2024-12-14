namespace AdventUtils
{
    public static class IO
    {
        public static readonly string AppName = "AdventOfCode";

        // We'll look for all the data files in %AppData%/AdventOfCode.
        public static string GetAppPath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName, filename);
        }

        public static StreamReader GetStream(string filename)
        {
            var path = GetAppPath(filename);
            return new StreamReader(path);
        }

        // Get the lines of an input data file as a List.
        public static List<string> GetLines(string filename)
        {
            var path = GetAppPath(filename);
            return [.. File.ReadAllLines(path)];
        }

        // Get an input data file as a single string.
        public static string GetString(string filename)
        {
            var path = GetAppPath(filename);
            return File.ReadAllText(path);
        }

        // Split a string into exactly two parts at a separator, returning
        // the result as a tuple.
        public static (string, string) Partition(string s, string separator)
        {
            var parts = s.Split(separator, 2);
            if (parts.Length == 2)
            {
                return (parts[0], parts[1]);
            }
            else
            {
                throw new Exception($"String '{s}' does not contain the separator '{separator}'.");
            }
        }

        public static (string, string) Partition(string s, char separator)
        {
            return Partition(s, separator.ToString());
        }
    }
}
