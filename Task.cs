using System;

class Task
{
    public string Title;
    public string Description;
    public string Status;
    public DateTime DueDate;

    // constuctor for task
    public Task(string title, string desc)
    {
        Title = title;
        Description = desc;
        Status = "Pending";
        DueDate = DateTime.Now.AddDays(7);
    }

    public void Display()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Description: {Description}");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine($"Due: {DueDate.ToShortDateString()}");
        Console.WriteLine("-------------------");
    }
}
