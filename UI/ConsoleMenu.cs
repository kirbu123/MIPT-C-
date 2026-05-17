using TaskHub.Models;

namespace TaskHub.UI;

public static class ConsoleMenu
{
    public static void PrintMainMenu()
    {
        Console.WriteLine();
        Console.WriteLine("=== TaskHub ===");
        Console.WriteLine("1. Create task");
        Console.WriteLine("2. View tasks");
        Console.WriteLine("3. Edit task");
        Console.WriteLine("4. Delete task");
        Console.WriteLine("5. Search tasks");
        Console.WriteLine("6. Show statistics");
        Console.WriteLine("7. Save tasks");
        Console.WriteLine("8. Load tasks");
        Console.WriteLine("0. Exit");
        Console.Write("Select option: ");
    }

    public static void PrintViewMenu()
    {
        Console.WriteLine();
        Console.WriteLine("View tasks:");
        Console.WriteLine("1. All");
        Console.WriteLine("2. Completed");
        Console.WriteLine("3. Not completed");
        Console.WriteLine("4. High priority");
        Console.Write("Select option: ");
    }

    public static void PrintSearchMenu()
    {
        Console.WriteLine();
        Console.WriteLine("Search tasks:");
        Console.WriteLine("1. By title");
        Console.WriteLine("2. By status");
        Console.WriteLine("3. By priority");
        Console.Write("Select option: ");
    }

    public static void PrintTasks(IReadOnlyCollection<TaskItem> tasks)
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (TaskItem task in tasks.OrderBy(task => task.Deadline))
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"Id: {task.Id}");
            Console.WriteLine($"Title: {task.Title}");
            Console.WriteLine($"Description: {task.Description}");
            Console.WriteLine($"Priority: {task.Priority}");
            Console.WriteLine($"Deadline: {task.Deadline:g}");
            Console.WriteLine($"Status: {task.Status}");
            Console.WriteLine($"Overdue: {(task.IsOverdue ? "Yes" : "No")}");
        }
        Console.WriteLine("----------------------------------------");
    }
}
