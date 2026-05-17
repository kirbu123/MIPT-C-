using System.Text.Json;

namespace TaskHub.Storage;

public sealed class JsonFileStorage<T> : IAsyncStorage<T>
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        WriteIndented = true
    };

    public async Task SaveAsync(string filePath, IReadOnlyCollection<T> items, CancellationToken cancellationToken = default)
    {
        try
        {
            await using FileStream stream = File.Create(filePath);
            await JsonSerializer.SerializeAsync(stream, items, SerializerOptions, cancellationToken);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            throw new InvalidOperationException("Failed to save data to file.", ex);
        }
    }

    public async Task<List<T>> LoadAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File was not found.", filePath);
        }

        try
        {
            await using FileStream stream = File.OpenRead(filePath);
            List<T>? items = await JsonSerializer.DeserializeAsync<List<T>>(stream, SerializerOptions, cancellationToken);
            return items ?? new List<T>();
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or JsonException)
        {
            throw new InvalidOperationException("Failed to load data from file.", ex);
        }
    }
}
