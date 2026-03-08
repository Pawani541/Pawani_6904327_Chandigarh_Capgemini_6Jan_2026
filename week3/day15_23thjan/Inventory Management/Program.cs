using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Book> books = new List<Book>();

        // INPUT
        Console.WriteLine("How many books to add?");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter book name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter price:");
            double price = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter stock:");
            int stock = int.Parse(Console.ReadLine());

            books.Add(new Book { Name = name, Price = price, Stock = stock });
        }

        // Find cheap books
        Console.WriteLine("Enter target price:");
        double target = double.Parse(Console.ReadLine());

        var cheapBooks = books.Where(b => b.Price < target);

        Console.WriteLine("Books cheaper than target price:");
        foreach (var b in cheapBooks)
            Console.WriteLine(b.Name);

        // Increase price by 10%
        books.ForEach(b => b.Price += b.Price * 0.10);

        // Remove out-of-stock
        books.RemoveAll(b => b.Stock == 0);

        Console.WriteLine("Final Inventory:");
        foreach (var b in books)
            Console.WriteLine(b.Name + " - " + b.Price);
    }
}

class Book
{
    public string Name;
    public double Price;
    public int Stock;
}
