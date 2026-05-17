namespace TaskHub.Models;

public sealed class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public DateTime Deadline { get; set; }
    public TaskItemStatus Status { get; set; } = TaskItemStatus.New;

    public bool IsOverdue => Status != TaskItemStatus.Done && Deadline < DateTime.Now;
}
