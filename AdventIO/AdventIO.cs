namespace AdventUtils
{
    public static class AdventIO
    {
        public static readonly string AppName = "AdventOfCode";

        public static string GetAppPath(string filename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), AppName, filename);
        }

        public static StreamReader GetStream(string filename) {
            var path = GetAppPath(filename);
            return new StreamReader(path);
        }

        public static List<string> GetLines(string filename)
        {
            var path = GetAppPath(filename);
            return [.. File.ReadAllLines(path)];
        }
    }
}
