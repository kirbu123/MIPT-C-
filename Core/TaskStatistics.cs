using TaskHub.Models;

namespace TaskHub.Core;

public sealed class TaskStatistics
{
    public int TotalCount { get; init; }
    public int CompletedCount { get; init; }
    public int OverdueCount { get; init; }
    public Dictionary<PriorityLevel, int> PriorityCounts { get; init; } = new();
}
