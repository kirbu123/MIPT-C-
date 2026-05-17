namespace TaskHub.Storage;

public interface IAsyncStorage<T>
{
    Task SaveAsync(string filePath, IReadOnlyCollection<T> items, CancellationToken cancellationToken = default);
    Task<List<T>> LoadAsync(string filePath, CancellationToken cancellationToken = default);
}
