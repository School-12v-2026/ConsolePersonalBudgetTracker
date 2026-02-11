using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Точка 5 – зареждане от файл при стартиране
        List<string> expenses = LoadFromFile();

        bool running = true;

        while (running)
        {
            // Точка 6 – цветно меню
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.ResetColor();

            Console.WriteLine("1. Добавяне на разход");
            Console.WriteLine("2. Показване на разходи");
            Console.WriteLine("3. Сортиране на разходи");
            Console.WriteLine("4. Изход");

            Console.Write("Избери опция: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Въведи разход: ");
                    string expense = Console.ReadLine();
                    expenses.Add(expense);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Разходът е добавен.");
                    Console.ResetColor();
                    break;

                case "2":
                    Console.WriteLine("\n--- РАЗХОДИ ---");

                    if (expenses.Count == 0)
                    {
                        Console.WriteLine("Няма въведени разходи.");
                    }
                    else
                    {
                        foreach (string e in expenses)
                        {
                            // Точка 6 – оцветяване на разходите
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine(e);
                            Console.ResetColor();
                        }
                    }
                    break;

                case "3":
                    // Точка 8 – сортиране
                    SortExpenses(expenses);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Разходите са сортирани.");
                    Console.ResetColor();
                    break;

                case "4":
                    // Точка 4 + 5 – изход и запис във файл
                    SaveToFile(expenses);
                    running = false;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Довиждане!");
                    Console.ResetColor();
                    break;

                default:
                    Console.WriteLine("Невалиден избор!");
                    break;
            }
        }
    }

    // ---------- ТОЧКА 5 – ЗАРЕЖДАНЕ ----------
    static List<string> LoadFromFile()
    {
        List<string> expenses = new List<string>();

        if (File.Exists("expenses.txt"))
        {
            string[] lines = File.ReadAllLines("expenses.txt");
            foreach (string line in lines)
            {
                expenses.Add(line);
            }
        }

        return expenses;
    }

    // ---------- ТОЧКА 5 – ЗАПИС ----------
    static void SaveToFile(List<string> expenses)
    {
        using (StreamWriter writer = new StreamWriter("expenses.txt"))
        {
            foreach (string expense in expenses)
            {
                writer.WriteLine(expense);
            }
        }
    }

    // ---------- ТОЧКА 8 – СОРТИРАНЕ ----------
    static void SortExpenses(List<string> expenses)
    {
        expenses.Sort();
    }
}