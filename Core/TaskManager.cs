using TaskHub.Models;
using TaskHub.Storage;

namespace TaskHub.Core;

public sealed class TaskManager : IDisposable
{
    private readonly List<TaskItem> _tasks = new();
    private readonly HashSet<Guid> _notifiedOverdueTaskIds = new();
    private readonly IAsyncStorage<TaskItem> _storage;
    private readonly object _syncRoot = new();
    private readonly TimeSpan _checkInterval;
    private CancellationTokenSource? _monitoringCancellation;
    private Task? _monitoringTask;
    private bool _disposed;

    public event Action<TaskItem>? TaskOverdue;

    public TaskManager(IAsyncStorage<TaskItem> storage, TimeSpan? checkInterval = null)
    {
        _storage = storage;
        _checkInterval = checkInterval ?? TimeSpan.FromSeconds(5);
    }

    public IReadOnlyCollection<TaskItem> GetAllTasks()
    {
        lock (_syncRoot)
        {
            return _tasks.Select(task => CloneTask(task)).ToList();
        }
    }

    public IReadOnlyCollection<TaskItem> GetTasks(TaskFilter filter)
    {
        lock (_syncRoot)
        {
            return _tasks.Where(task => filter(task)).Select(task => CloneTask(task)).ToList();
        }
    }

    public TaskItem CreateTask(string title, string description, PriorityLevel priority, DateTime deadline, TaskItemStatus status)
    {
        ValidateText(title, nameof(title));
        ValidateText(description, nameof(description));

        var task = new TaskItem
        {
            Title = title.Trim(),
            Description = description.Trim(),
            Priority = priority,
            Deadline = deadline,
            Status = status
        };

        lock (_syncRoot)
        {
            _tasks.Add(task);
        }

        return CloneTask(task);
    }

    public bool DeleteTask(Guid id)
    {
        lock (_syncRoot)
        {
            int removed = _tasks.RemoveAll(task => task.Id == id);
            _notifiedOverdueTaskIds.Remove(id);
            return removed > 0;
        }
    }

    public bool EditTask(Guid id, string title, string description, PriorityLevel priority, TaskItemStatus status)
    {
        ValidateText(title, nameof(title));
        ValidateText(description, nameof(description));

        lock (_syncRoot)
        {
            TaskItem? task = _tasks.FirstOrDefault(item => item.Id == id);
            if (task is null)
            {
                return false;
            }

            task.Title = title.Trim();
            task.Description = description.Trim();
            task.Priority = priority;
            task.Status = status;
            return true;
        }
    }

    public IReadOnlyCollection<TaskItem> SearchByTitle(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Array.Empty<TaskItem>();
        }

        string search = value.Trim();
        return GetTasks(task => task.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
    }

    public IReadOnlyCollection<TaskItem> SearchByStatus(TaskItemStatus status) => GetTasks(task => task.Status == status);

    public IReadOnlyCollection<TaskItem> SearchByPriority(PriorityLevel priority) => GetTasks(task => task.Priority == priority);

    public TaskStatistics GetStatistics()
    {
        lock (_syncRoot)
        {
            var priorityCounts = Enum.GetValues<PriorityLevel>()
                .ToDictionary(priority => priority, priority => _tasks.Count(task => task.Priority == priority));

            return new TaskStatistics
            {
                TotalCount = _tasks.Count,
                CompletedCount = _tasks.Count(task => task.Status == TaskItemStatus.Done),
                OverdueCount = _tasks.Count(task => task.IsOverdue),
                PriorityCounts = priorityCounts
            };
        }
    }

    public async Task SaveAsync(string path, CancellationToken cancellationToken = default)
    {
        List<TaskItem> snapshot;
        lock (_syncRoot)
        {
            snapshot = _tasks.Select(task => CloneTask(task)).ToList();
        }

        await _storage.SaveAsync(path, snapshot, cancellationToken);
    }

    public async Task LoadAsync(string path, CancellationToken cancellationToken = default)
    {
        List<TaskItem> loaded = await _storage.LoadAsync(path, cancellationToken);
        lock (_syncRoot)
        {
            _tasks.Clear();
            _tasks.AddRange(loaded);
            _notifiedOverdueTaskIds.Clear();
        }
    }

    public void StartMonitoring()
    {
        ThrowIfDisposed();

        if (_monitoringTask is { IsCompleted: false })
        {
            return;
        }

        _monitoringCancellation = new CancellationTokenSource();
        _monitoringTask = Task.Run(() => MonitorLoopAsync(_monitoringCancellation.Token));
    }

    public async Task StopMonitoringAsync()
    {
        if (_monitoringCancellation is null || _monitoringTask is null)
        {
            return;
        }

        _monitoringCancellation.Cancel();

        try
        {
            await _monitoringTask;
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            _monitoringCancellation.Dispose();
            _monitoringCancellation = null;
            _monitoringTask = null;
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        StopMonitoringAsync().GetAwaiter().GetResult();
        _disposed = true;
    }

    private async Task MonitorLoopAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            List<TaskItem> newlyOverdue;

            lock (_syncRoot)
            {
                HashSet<Guid> currentOverdueIds = _tasks
                    .Where(task => task.IsOverdue)
                    .Select(task => task.Id)
                    .ToHashSet();

                _notifiedOverdueTaskIds.RemoveWhere(id => !currentOverdueIds.Contains(id));

                newlyOverdue = _tasks
                    .Where(task => task.IsOverdue && !_notifiedOverdueTaskIds.Contains(task.Id))
                    .Select(task => CloneTask(task))
                    .ToList();

                foreach (TaskItem task in newlyOverdue)
                {
                    _notifiedOverdueTaskIds.Add(task.Id);
                }
            }

            foreach (TaskItem task in newlyOverdue)
            {
                TaskOverdue?.Invoke(task);
            }

            await Task.Delay(_checkInterval, cancellationToken);
        }
    }

    private static TaskItem CloneTask(TaskItem task) =>
        new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Deadline = task.Deadline,
            Status = task.Status
        };

    private static void ValidateText(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be empty.", parameterName);
        }
    }

    private void ThrowIfDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(TaskManager));
        }
    }
}
