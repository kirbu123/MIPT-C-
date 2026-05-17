# TaskHub Console App

TaskHub is a console task manager written in C#.

## Features

- Create tasks with title, description, priority, deadline and status.
- View all tasks, completed tasks, not completed tasks and high-priority tasks.
- Edit task title, description, priority and status.
- Delete tasks by id.
- Search tasks by title, status and priority.
- Show statistics: total, completed, overdue and count by priority.
- Save tasks to JSON file.
- Load tasks from JSON file.
- Use async/await for save and load operations.
- Run background deadline monitoring and show overdue notifications.

## Tech Requirements Covered

- OOP with classes and enums.
- Collections: `List<TaskItem>`, `Dictionary<PriorityLevel, int>`, `HashSet<Guid>`.
- Generic types: `IAsyncStorage<T>`, `JsonFileStorage<T>`.
- Delegate: `TaskFilter`.
- Exception handling for validation, input, and file operations.
- `IDisposable` in `TaskManager`.
- Static classes/methods for UI and input helpers.
- Async operations with `Task` and `await`.
- Multithreading with background monitoring task.

## Run

1. Ensure .NET 8 SDK is installed.
2. From repository root run:

```bash
dotnet run --project TaskHub.csproj
```

## Data Format

Tasks are saved as JSON array with fields:

- `Id`
- `Title`
- `Description`
- `Priority`
- `Deadline`
- `Status`