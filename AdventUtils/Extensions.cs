namespace AdventUtils
{
    // implements an equivalent of Python's setdefault.
    // Adapted from https://stackoverflow.com/a/3427050/773754 by Shaun McCarthy
    public static class DictExtensions
    {
        public static TValue SetDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (!dictionary.TryGetValue(key, out TValue? result))
            {
                return dictionary[key] = Activator.CreateInstance<TValue>()!;
            }
            return result;
        }
    }
}