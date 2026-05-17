using TaskHub.Core;
using TaskHub.Models;
using TaskHub.Storage;
using TaskHub.UI;
using TaskHub.Utils;

IAsyncStorage<TaskItem> storage = new JsonFileStorage<TaskItem>();
using var manager = new TaskManager(storage);

manager.TaskOverdue += task =>
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine();
    Console.WriteLine($"[Notification] Task is overdue: {task.Title} ({task.Id})");
    Console.ResetColor();
};

manager.StartMonitoring();

bool isRunning = true;

while (isRunning)
{
    ConsoleMenu.PrintMainMenu();
    string? input = Console.ReadLine();

    try
    {
        switch (input)
        {
            case "1":
                CreateTask(manager);
                break;
            case "2":
                ViewTasks(manager);
                break;
            case "3":
                EditTask(manager);
                break;
            case "4":
                DeleteTask(manager);
                break;
            case "5":
                SearchTasks(manager);
                break;
            case "6":
                ShowStatistics(manager);
                break;
            case "7":
                await SaveTasksAsync(manager);
                break;
            case "8":
                await LoadTasksAsync(manager);
                break;
            case "0":
                isRunning = false;
                break;
            default:
                Console.WriteLine("Unknown menu option.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
}

await manager.StopMonitoringAsync();

static void CreateTask(TaskManager manager)
{
    Console.WriteLine();
    string title = InputReader.ReadRequiredText("Title: ");
    string description = InputReader.ReadRequiredText("Description: ");
    PriorityLevel priority = InputReader.ReadEnum<PriorityLevel>("Priority (Low/Medium/High): ");
    DateTime deadline = InputReader.ReadDateTime("Deadline (e.g. 2026-12-31 18:00): ");
    TaskItemStatus status = InputReader.ReadEnum<TaskItemStatus>("Status (New/InProgress/Done): ");

    TaskItem created = manager.CreateTask(title, description, priority, deadline, status);
    Console.WriteLine($"Task created with Id: {created.Id}");
}

static void ViewTasks(TaskManager manager)
{
    ConsoleMenu.PrintViewMenu();
    string? option = Console.ReadLine();
    IReadOnlyCollection<TaskItem> tasks = option switch
    {
        "1" => manager.GetAllTasks(),
        "2" => manager.GetTasks(task => task.Status == TaskItemStatus.Done),
        "3" => manager.GetTasks(task => task.Status != TaskItemStatus.Done),
        "4" => manager.GetTasks(task => task.Priority == PriorityLevel.High),
        _ => Array.Empty<TaskItem>()
    };

    if (option is not ("1" or "2" or "3" or "4"))
    {
        Console.WriteLine("Unknown menu option.");
        return;
    }

    ConsoleMenu.PrintTasks(tasks);
}

static void EditTask(TaskManager manager)
{
    Console.WriteLine();
    Guid id = InputReader.ReadGuid("Task Id: ");
    string title = InputReader.ReadRequiredText("New title: ");
    string description = InputReader.ReadRequiredText("New description: ");
    PriorityLevel priority = InputReader.ReadEnum<PriorityLevel>("New priority (Low/Medium/High): ");
    TaskItemStatus status = InputReader.ReadEnum<TaskItemStatus>("New status (New/InProgress/Done): ");

    bool updated = manager.EditTask(id, title, description, priority, status);
    Console.WriteLine(updated ? "Task updated." : "Task not found.");
}

static void DeleteTask(TaskManager manager)
{
    Console.WriteLine();
    Guid id = InputReader.ReadGuid("Task Id: ");
    bool deleted = manager.DeleteTask(id);
    Console.WriteLine(deleted ? "Task deleted." : "Task not found.");
}

static void SearchTasks(TaskManager manager)
{
    ConsoleMenu.PrintSearchMenu();
    string? option = Console.ReadLine();

    IReadOnlyCollection<TaskItem> result;
    switch (option)
    {
        case "1":
            string value = InputReader.ReadRequiredText("Title contains: ");
            result = manager.SearchByTitle(value);
            break;
        case "2":
            TaskItemStatus status = InputReader.ReadEnum<TaskItemStatus>("Status (New/InProgress/Done): ");
            result = manager.SearchByStatus(status);
            break;
        case "3":
            PriorityLevel priority = InputReader.ReadEnum<PriorityLevel>("Priority (Low/Medium/High): ");
            result = manager.SearchByPriority(priority);
            break;
        default:
            Console.WriteLine("Unknown menu option.");
            return;
    }

    ConsoleMenu.PrintTasks(result);
}

static void ShowStatistics(TaskManager manager)
{
    TaskStatistics stats = manager.GetStatistics();

    Console.WriteLine();
    Console.WriteLine($"Total tasks: {stats.TotalCount}");
    Console.WriteLine($"Completed tasks: {stats.CompletedCount}");
    Console.WriteLine($"Overdue tasks: {stats.OverdueCount}");
    Console.WriteLine("By priority:");
    foreach (var item in stats.PriorityCounts.OrderBy(pair => pair.Key))
    {
        Console.WriteLine($"- {item.Key}: {item.Value}");
    }
}

static async Task SaveTasksAsync(TaskManager manager)
{
    Console.WriteLine();
    string path = InputReader.ReadRequiredText("File path to save: ");
    await manager.SaveAsync(path);
    Console.WriteLine("Tasks saved.");
}

static async Task LoadTasksAsync(TaskManager manager)
{
    Console.WriteLine();
    string path = InputReader.ReadRequiredText("File path to load: ");
    await manager.LoadAsync(path);
    Console.WriteLine("Tasks loaded.");
}
