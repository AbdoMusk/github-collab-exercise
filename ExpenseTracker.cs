using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class ExpenseTracker
{
    static List<Transaction> transactions = new List<Transaction>();
    static int nextId = 1;
    static string dataFile = "transactions.csv";

    static void Main(string[] args)
    {
        LoadTransactions();
        bool running = true;
        
        while(running)
        {
            Console.Clear();
            Console.WriteLine("Welcome to Your Expense Tracker!");
            Console.WriteLine("1. Add Transaction");
            Console.WriteLine("2. View Transactions");
            Console.WriteLine("3. Update Transaction");
            Console.WriteLine("4. Delete Transaction");
            Console.WriteLine("5. View Summary");
            Console.WriteLine("6. Save & Exit");
            Console.Write("\nChoose an option: ");
            
            string choice = Console.ReadLine();
            
            switch(choice)
            {
                case "1":
                    AddTransaction();
                    break;
                case "2":
                    ViewTransactions();
                    break;
                case "3":
                    UpdateTransaction();
                    break;
                case "4":
                    DeleteTransaction();
                    break;
                case "5":
                    ViewSummary();
                    break;
                case "6":
                    SaveTransactions();
                    running = false;
                    Console.WriteLine("Data saved. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // adds transaction to the list
    static void AddTransaction()
    {
        Console.Clear();
        Console.WriteLine("--- Add New Transaction ---");
        
        Console.Write("Enter title: ");
        string title = Console.ReadLine();
        
        Console.Write("Enter amount (negative for expense, positive for income): ");
        string amountStr = Console.ReadLine();
        decimal amount;
        
        if(!decimal.TryParse(amountStr, out amount))
        {
            Console.WriteLine("Invalid amount!");
            Console.ReadKey();
            return;
        }
        
        Console.Write("Enter category (Food, Transport, Salary, Entertainment, etc): ");
        string category = Console.ReadLine();
        
        Console.Write("Enter date (MM/DD/YYYY) or press Enter for today: ");
        string dateStr = Console.ReadLine();
        DateTime date;
        
        if(string.IsNullOrEmpty(dateStr))
        {
            date = DateTime.Now;
        }
        else if(!DateTime.TryParse(dateStr, out date))
        {
            Console.WriteLine("Invalid date format!");
            Console.ReadKey();
            return;
        }
        
        Transaction newTrans = new Transaction(nextId, title, amount, category, date);
        transactions.Add(newTrans);
        nextId++;
        
        Console.WriteLine("\nTransaction added successfully!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void ViewTransactions()
    {
        Console.Clear();
        Console.WriteLine("--- All Transactions ---\n");
        
        if(transactions.Count == 0)
        {
            Console.WriteLine("No transactions yet!");
        }
        else
        {
            // show in table format
            Console.WriteLine($"{"ID",-5} {"Title",-20} {"Amount",-12} {"Category",-15} {"Date",-12}");
            Console.WriteLine(new string('-', 65));
            
            foreach(var trans in transactions)
            {
                // color based on income or expense
                if(trans.Amount >= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                
                Console.WriteLine($"{trans.Id,-5} {trans.Title,-20} {trans.Amount,-12:C} {trans.Category,-15} {trans.Date.ToShortDateString(),-12}");
                Console.ResetColor();
            }
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    // update any field of a transaction
    static void UpdateTransaction()
    {
        Console.Clear();
        Console.WriteLine("--- Update Transaction ---\n");
        
        if(transactions.Count == 0)
        {
            Console.WriteLine("No transactions available!");
            Console.ReadKey();
            return;
        }
        
        foreach(var trans in transactions)
        {
            Console.WriteLine($"{trans.Id}. {trans.Title} - {trans.Amount:C}");
        }
        
        Console.Write("\nEnter transaction ID to update: ");
        string input = Console.ReadLine();
        int transId;
        
        if(!int.TryParse(input, out transId))
        {
            Console.WriteLine("Invalid ID!");
            Console.ReadKey();
            return;
        }
        
        Transaction found = transactions.FirstOrDefault(t => t.Id == transId);
        
        if(found == null)
        {
            Console.WriteLine("Transaction not found!");
            Console.ReadKey();
            return;
        }
        
        Console.WriteLine("\nLeave blank to keep current value:");
        
        Console.Write($"Title [{found.Title}]: ");
        string newTitle = Console.ReadLine();
        if(!string.IsNullOrEmpty(newTitle))
        {
            found.Title = newTitle;
        }
        
        Console.Write($"Amount [{found.Amount}]: ");
        string newAmount = Console.ReadLine();
        if(!string.IsNullOrEmpty(newAmount))
        {
            decimal amt;
            if(decimal.TryParse(newAmount, out amt))
            {
                found.Amount = amt;
            }
        }
        
        Console.Write($"Category [{found.Category}]: ");
        string newCat = Console.ReadLine();
        if(!string.IsNullOrEmpty(newCat))
        {
            found.Category = newCat;
        }
        
        Console.WriteLine("\nTransaction updated!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // deletes a transaction from list
    static void DeleteTransaction()
    {
        Console.Clear();
        Console.WriteLine("--- Delete Transaction ---\n");
        
        if(transactions.Count == 0)
        {
            Console.WriteLine("No transactions to delete!");
            Console.ReadKey();
            return;
        }
        
        foreach(var trans in transactions)
        {
            Console.WriteLine($"{trans.Id}. {trans.Title} - {trans.Amount:C}");
        }
        
        Console.Write("\nEnter transaction ID to delete: ");
        string input = Console.ReadLine();
        int transId;
        
        if(!int.TryParse(input, out transId))
        {
            Console.WriteLine("Invalid ID!");
            Console.ReadKey();
            return;
        }
        
        Transaction found = transactions.FirstOrDefault(t => t.Id == transId);
        
        if(found == null)
        {
            Console.WriteLine("Transaction not found!");
            Console.ReadKey();
            return;
        }
        
        Console.Write($"Are you sure you want to delete '{found.Title}'? (y/n): ");
        string confirm = Console.ReadLine();
        
        if(confirm.ToLower() == "y")
        {
            transactions.Remove(found);
            Console.WriteLine("\nTransaction deleted!");
        }
        else
        {
            Console.WriteLine("\nDeletion cancelled.");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // shows summary with totals and category breakdown
    static void ViewSummary()
    {
        Console.Clear();
        Console.WriteLine("--- Summary / Analysis ---\n");
        
        if(transactions.Count == 0)
        {
            Console.WriteLine("No transactions to analyze!");
            Console.ReadKey();
            return;
        }
        
        // calculate totals using linq
        decimal totalIncome = transactions.Where(t => t.Amount > 0).Sum(t => t.Amount);
        decimal totalExpenses = transactions.Where(t => t.Amount < 0).Sum(t => t.Amount);
        decimal balance = totalIncome + totalExpenses;
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Total Income:   {totalIncome:C}");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Total Expenses: {totalExpenses:C}");
        Console.ResetColor();
        Console.WriteLine($"Balance:        {balance:C}");
        
        Console.WriteLine("\n--- Expenses by Category ---");
        
        // group expenses by category
        var expensesByCategory = transactions
            .Where(t => t.Amount < 0)
            .GroupBy(t => t.Category)
            .Select(g => new { Category = g.Key, Total = g.Sum(t => t.Amount) });
        
        foreach(var cat in expensesByCategory)
        {
            Console.WriteLine($"{cat.Category}: {cat.Total:C}");
        }
        
        Console.WriteLine("\n--- Recent Transactions ---");
        
        // show last 5 transactions sorted by date
        var recent = transactions.OrderByDescending(t => t.Date).Take(5);
        
        foreach(var trans in recent)
        {
            Console.WriteLine($"{trans.Date.ToShortDateString()} - {trans.Title}: {trans.Amount:C}");
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    // saves transactions to csv file
    static void SaveTransactions()
    {
        try
        {
            using(StreamWriter writer = new StreamWriter(dataFile))
            {
                writer.WriteLine("Id,Title,Amount,Category,Date");
                
                foreach(var trans in transactions)
                {
                    // escape commas in title
                    string title = trans.Title.Replace(",", ";");
                    writer.WriteLine($"{trans.Id},{title},{trans.Amount},{trans.Category},{trans.Date.ToShortDateString()}");
                }
            }
            Console.WriteLine("Transactions saved to file!");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error saving: {ex.Message}");
        }
    }

    // loads transactions from csv file at startup
    static void LoadTransactions()
    {
        if(!File.Exists(dataFile))
        {
            return;
        }
        
        try
        {
            string[] lines = File.ReadAllLines(dataFile);
            
            // skip header line
            for(int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');
                
                if(parts.Length >= 5)
                {
                    int id = int.Parse(parts[0]);
                    string title = parts[1].Replace(";", ",");
                    decimal amount = decimal.Parse(parts[2]);
                    string category = parts[3];
                    DateTime date = DateTime.Parse(parts[4]);
                    
                    transactions.Add(new Transaction(id, title, amount, category, date));
                    
                    // keep track of highest id
                    if(id >= nextId)
                    {
                        nextId = id + 1;
                    }
                }
            }
            
            Console.WriteLine($"Loaded {transactions.Count} transactions from file.");
            Console.ReadKey();
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Error loading: {ex.Message}");
            Console.ReadKey();
        }
    }
}
