using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Task
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set; } // "todo", "in-progress", "done"
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

class TaskTracker
{
    private const string FilePath = "tasks.json";
    private List<Task> tasks;

    public TaskTracker()
    {
        tasks = LoadTasks();
    }

    private List<Task> LoadTasks()
    {
        if (!File.Exists(FilePath)) return new List<Task>();
        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Task>>(json) ?? new List<Task>();
    }

    private void SaveTasks()
    {
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public void AddTask(string description)
    {
        int newId = tasks.Count > 0 ? tasks[^1].Id + 1 : 1;
        var task = new Task { Id = newId, Description = description, Status = "todo", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now };
        tasks.Add(task);
        SaveTasks();
        Console.WriteLine("Task added successfully.");
    }

    public void UpdateTask(int id, string description)
    {
        var task = tasks.Find(t => t.Id == id);
        if (task == null) { Console.WriteLine("Task not found."); return; }
        task.Description = description;
        task.UpdatedAt = DateTime.Now;
        SaveTasks();
        Console.WriteLine("Task updated successfully.");
    }

    public void DeleteTask(int id)
    {
        tasks.RemoveAll(t => t.Id == id);
        SaveTasks();
        Console.WriteLine("Task deleted successfully.");
    }

    public void UpdateStatus(int id, string status)
    {
        if (status != "todo" && status != "in-progress" && status != "done")
        {
            Console.WriteLine("Invalid status. Use 'todo', 'in-progress', or 'done'.");
            return;
        }
        var task = tasks.Find(t => t.Id == id);
        if (task == null) { Console.WriteLine("Task not found."); return; }
        task.Status = status;
        task.UpdatedAt = DateTime.Now;
        SaveTasks();
        Console.WriteLine("Task status updated successfully.");
    }

    public void ListTasks(string status = "all")
    {
        var filteredTasks = status == "all" ? tasks : tasks.FindAll(t => t.Status == status);
        if (filteredTasks.Count == 0) { Console.WriteLine("No tasks found."); return; }
        foreach (var task in filteredTasks)
        {
            Console.WriteLine($"[{task.Id}] {task.Description} - {task.Status} (Created: {task.CreatedAt}, Updated: {task.UpdatedAt})");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: dotnet run <command> [arguments]");
            return;
        }

        TaskTracker tracker = new TaskTracker();
        switch (args[0].ToLower())
        {
            case "add":
                if (args.Length < 2) { Console.WriteLine("Usage: dotnet run add <description>"); return; }
                tracker.AddTask(string.Join(" ", args[1..]));
                break;
            case "update":
                if (args.Length < 3 || !int.TryParse(args[1], out int updateId)) { Console.WriteLine("Usage: dotnet run update <id> <new description>"); return; }
                tracker.UpdateTask(updateId, string.Join(" ", args[2..]));
                break;
            case "delete":
                if (args.Length < 2 || !int.TryParse(args[1], out int deleteId)) { Console.WriteLine("Usage: dotnet run delete <id>"); return; }
                tracker.DeleteTask(deleteId);
                break;
            case "status":
                if (args.Length < 3 || !int.TryParse(args[1], out int statusId)) { Console.WriteLine("Usage: dotnet run status <id> <status>"); return; }
                tracker.UpdateStatus(statusId, args[2]);
                break;
            case "list":
                string statusFilter = args.Length > 1 ? args[1] : "all";
                tracker.ListTasks(statusFilter);
                break;
            default:
                Console.WriteLine("Invalid command. Available commands: add, update, delete, status, list");
                break;
        }
    }
}
