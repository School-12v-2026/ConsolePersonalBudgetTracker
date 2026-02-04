using System;

public class Expense
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"{Id}. {Amount} лв | {Category} | {Date.ToShortDateString()}";
    }
}
