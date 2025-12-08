using System;

class Transaction
{
    public int Id;
    public string Title;
    public decimal Amount;
    public string Category;
    public DateTime Date;

    // constuctor for transaction
    public Transaction(int id, string title, decimal amount, string category, DateTime date)
    {
        Id = id;
        Title = title;
        Amount = amount;
        Category = category;
        Date = date;
    }

    public void Display()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Amount: {Amount:C}");
        Console.WriteLine($"Category: {Category}");
        Console.WriteLine($"Date: {Date.ToShortDateString()}");
        Console.WriteLine("-------------------");
    }
}
