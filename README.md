The CLI Task Tracker is a simple command-line application designed to help users manage their tasks efficiently. It allows users to add, update, delete, and track the status of tasks (To-Do, In-Progress, and Done). Tasks are stored in a JSON file, making the data persistent across sessions. This project was built using C# .NET and focuses on file handling, user input processing, and CLI interactions.

# Features
- Add a task with a unique ID and description

- Update a task's status (To-Do, In-Progress, Done)

- Delete tasks when no longer needed

- List all tasks or filter by status

- Persistent storage using a JSON file

- Error handling for invalid inputs

# Usage

# Add a New Task
dotnet run add "Complete my project"

# Update a Task's Status
dotnet run update <task_id> in-progress
# Example:
dotnet run update 1 done

# Delete a Task
dotnet run delete <task_id>
# Example:
dotnet run delete 1

# List All Tasks
dotnet run list

# List Completed Tasks
dotnet run list done

# List In-Progress Tasks
dotnet run list in-progress

# List To-Do Tasks
dotnet run list todo

# File structure
cli-task-tracker/
│── tasks.json  # Stores task data
│── Program.cs  # Main application logic
│── cli-task-tracker.csproj  # Project file

Developed by Akhona Mbatha.
