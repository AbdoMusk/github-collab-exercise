# Personal Expense Tracker Console App

simple expense tracker i made for my class project to help manage money and track spending

## what it does

- add income and expense transactions
- view all transactions in a nice table (expenses in red, income in green)
- update transaction details like title, amount, category
- delete transactions (asks for confirmation)
- shows summary with total income, expenses and balance
- groups expenses by category so you can see where your money goes
- saves everything to a csv file so you dont lose your data

## how to run

you need C# compiler installed then:

```
csc ExpenseTracker.cs Transaction.cs
```

then run it:
```
ExpenseTracker.exe
```

## how to use

theres a menu when you start it with these options:
1. Add Transaction - enter title, amount (negative for expense), category, date
2. View Transactions - see all your transactions in a table
3. Update Transaction - change any detail of a transaction
4. Delete Transaction - remove a transaction
5. View Summary - see totals, balance, expenses by category
6. Save & Exit - saves your data and closes the app

## categories i use

- Food
- Transport
- Salary
- Entertainment
- Bills
- Shopping
- Other

you can use whatever categories you want tho

## example

```
--- All Transactions ---

ID    Title                Amount       Category        Date        
-----------------------------------------------------------------
1     Paycheck             $2,500.00    Salary          12/01/2024
2     Groceries            -$85.50      Food            12/02/2024
3     Bus Pass             -$50.00      Transport       12/03/2024
4     Netflix              -$15.99      Entertainment   12/05/2024
```

## files

- `Transaction.cs` - the transaction class with all the properties
- `ExpenseTracker.cs` - main program with all the menu and functions
- `transactions.csv` - where your data gets saved (created automatically)
