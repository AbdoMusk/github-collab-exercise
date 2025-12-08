using System;
using System.Collections.Generic;

class Program
{
    static List<Task> tasks = new List<Task>();

    static void Main(string[] args)
    {
        bool running = true;
        
        while(running)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Your Task Manager!");
            Console.WriteLine("1. Add a Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Update Task Status");
            Console.WriteLine("4. Delete a Task");
            Console.WriteLine("5. Exit");
            Console.Write("\nChoose an option: ");
            
            string choice = Console.ReadLine();
            
            switch(choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    UpdateStatus();
                    break;
                case "4":
                    DeleteTask();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // adds task to the list
    static void AddTask()
    {
        Console.Clear();
        Console.WriteLine("--- Add New Task ---");
        
        Console.Write("Enter task title: ");
        string title = Console.ReadLine();
        
        Console.Write("Enter task description: ");
        string desc = Console.ReadLine();
        
        Task newTask = new Task(title, desc);
        tasks.Add(newTask);
        
        Console.WriteLine("\nTask added successfully!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ViewTasks()
    {
        Console.Clear();
        Console.WriteLine("--- All Tasks ---\n");
        
        if(tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet!");
        }
        else
        {
            for(int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"Task #{i + 1}:");
                tasks[i].Display();
            }
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    // change status to completed or pending
    static void UpdateStatus()
    {
        Console.Clear();
        Console.WriteLine("--- Update Task Status ---\n");
        
        if(tasks.Count == 0)
        {
            Console.WriteLine("No tasks available!");
            Console.ReadKey();
            return;
        }
        
        for(int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i].Title} - {tasks[i].Status}");
        }
        
        Console.Write("\nEnter task number to update: ");
        string input = Console.ReadLine();
        int taskNum;
        
        if(int.TryParse(input, out taskNum) && taskNum > 0 && taskNum <= tasks.Count)
        {
            Task selectedTask = tasks[taskNum - 1];
            
            // toggles the status
            if(selectedTask.Status == "Pending")
            {
                selectedTask.Status = "Completed";
            }
            else
            {
                selectedTask.Status = "Pending";
            }
            
            Console.WriteLine($"\nTask status updated to: {selectedTask.Status}");
        }
        else
        {
            Console.WriteLine("Invalid task number!");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // deletes a task from list
    static void DeleteTask()
    {
        Console.Clear();
        Console.WriteLine("--- Delete Task ---\n");
        
        if(tasks.Count == 0)
        {
            Console.WriteLine("No tasks to delete!");
            Console.ReadKey();
            return;
        }
        
        for(int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i].Title}");
        }
        
        Console.Write("\nEnter task number to delete: ");
        string input = Console.ReadLine();
        int taskNum;
        
        if(int.TryParse(input, out taskNum) && taskNum > 0 && taskNum <= tasks.Count)
        {
            Console.Write($"Are you sure you want to delete '{tasks[taskNum - 1].Title}'? (y/n): ");
            string confirm = Console.ReadLine();
            
            if(confirm.ToLower() == "y")
            {
                tasks.RemoveAt(taskNum - 1);
                Console.WriteLine("\nTask deleted!");
            }
            else
            {
                Console.WriteLine("\nDeletion cancelled.");
            }
        }
        else
        {
            Console.WriteLine("Invalid task number!");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
