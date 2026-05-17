namespace MIPT_course;

public static class CollectionUtils
{
    public static List<T> Distinct<T>(List<T> source)
    {
        var seen = new HashSet<T>();
        var result = new List<T>();

        foreach (var item in source)
        {
            if (seen.Add(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public static Dictionary<TKey, List<TValue>> GroupBy<TValue, TKey>(
        List<TValue> source,
        Func<TValue, TKey> keySelector) where TKey : notnull
    {
        var result = new Dictionary<TKey, List<TValue>>();

        foreach (var item in source)
        {
            var key = keySelector(item);
            if (!result.TryGetValue(key, out var group))
            {
                group = new List<TValue>();
                result[key] = group;
            }

            group.Add(item);
        }

        return result;
    }

    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(
        Dictionary<TKey, TValue> first,
        Dictionary<TKey, TValue> second,
        Func<TValue, TValue, TValue> conflictResolver) where TKey : notnull
    {
        var result = new Dictionary<TKey, TValue>();

        foreach (var pair in first)
        {
            result[pair.Key] = pair.Value;
        }

        foreach (var pair in second)
        {
            if (result.TryGetValue(pair.Key, out var existingValue))
            {
                result[pair.Key] = conflictResolver(existingValue, pair.Value);
            }
            else
            {
                result[pair.Key] = pair.Value;
            }
        }

        return result;
    }

    public static T MaxBy<T, TKey>(List<T> source, Func<T, TKey> selector)
        where TKey : IComparable<TKey>
    {
        if (source.Count == 0)
        {
            throw new InvalidOperationException("Sequence contains no elements.");
        }

        var maxItem = source[0];
        var maxKey = selector(maxItem);

        for (var i = 1; i < source.Count; i++)
        {
            var currentItem = source[i];
            var currentKey = selector(currentItem);
            if (Comparer<TKey>.Default.Compare(currentKey, maxKey) > 0)
            {
                maxItem = currentItem;
                maxKey = currentKey;
            }
        }

        return maxItem;
    }
}
