namespace MIPT_course;

public class Repository<T> where T : IEntity
{
    private readonly Dictionary<int, T> _items = new();

    public int Count => _items.Count;

    public void Add(T item)
    {
        if (_items.ContainsKey(item.Id))
        {
            throw new InvalidOperationException($"Entity with Id {item.Id} already exists.");
        }

        _items[item.Id] = item;
    }

    public bool Remove(int id)
    {
        return _items.Remove(id);
    }

    public T? GetById(int id)
    {
        return _items.TryGetValue(id, out var item) ? item : default;
    }

    public IReadOnlyList<T> GetAll()
    {
        return _items.Values.ToList();
    }

    public IReadOnlyList<T> Find(Predicate<T> predicate)
    {
        var result = new List<T>();

        foreach (var item in _items.Values)
        {
            if (predicate(item))
            {
                result.Add(item);
            }
        }

        return result;
    }
}
