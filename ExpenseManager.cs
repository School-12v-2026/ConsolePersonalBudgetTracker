using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ExpenseManager
{
    private List<Expense> expenses = new List<Expense>();
    private string filePath = "expenses.txt";

    public ExpenseManager()
    {
        LoadFromFile();
    }

    public void AddExpense(decimal amount, string category, DateTime date)
    {
        int id = expenses.Count == 0 ? 1 : expenses.Max(e => e.Id) + 1;

        Expense exp = new Expense
        {
            Id = id,
            Amount = amount,
            Category = category,
            Date = date
        };

        expenses.Add(exp);
        SaveToFile();
    }

    public void ShowAllExpenses()
    {
        foreach (var e in expenses)
        {
            if (e.Amount > 100)
                Console.ForegroundColor = ConsoleColor.Red;
            else if (e.Amount < 20)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ResetColor();

            Console.WriteLine(e);
        }

        Console.ResetColor();
    }

    public void DeleteExpense(int id)
    {
        var exp = expenses.FirstOrDefault(e => e.Id == id);
        if (exp != null)
        {
            expenses.Remove(exp);
            SaveToFile();
            Console.WriteLine("Разходът е изтрит.");
        }
        else
        {
            Console.WriteLine("Няма такъв разход.");
        }
    }

    public void ShowStatistics()
    {
        Console.WriteLine("----- Статистика -----");

        decimal total = expenses.Sum(e => e.Amount);
        Console.WriteLine($"Общо похарчени пари: {total} лв");

        var max = expenses.OrderByDescending(e => e.Amount).FirstOrDefault();
        Console.WriteLine($"Най-голям разход: {max?.Amount} лв ({max?.Category})");

        Console.WriteLine("Разходи по категории:");
        var byCategory = expenses.GroupBy(e => e.Category);
        foreach (var group in byCategory)
        {
            Console.WriteLine($"{group.Key}: {group.Sum(e => e.Amount)} лв");
        }

        decimal avg = expenses.Count > 0 ? expenses.Average(e => e.Amount) : 0;
        Console.WriteLine($"Среден разход: {avg:F2} лв");
    }

    private void SaveToFile()
    {
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            foreach (var e in expenses)
            {
                sw.WriteLine($"{e.Id}|{e.Amount}|{e.Category}|{e.Date}");
            }
        }
    }

    private void LoadFromFile()
    {
        if (!File.Exists(filePath)) return;

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines)
        {
            var parts = line.Split('|');
            expenses.Add(new Expense
            {
                Id = int.Parse(parts[0]),
                Amount = decimal.Parse(parts[1]),
                Category = parts[2],
                Date = DateTime.Parse(parts[3])
            });
        }
    }
}
