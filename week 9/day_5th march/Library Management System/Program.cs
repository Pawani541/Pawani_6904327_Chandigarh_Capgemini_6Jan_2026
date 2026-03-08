using System;
using System.Collections.Generic;
using System.Linq;

public interface IBook
{
    int Id { get; set; }
    string Title { get; set; }
    string Author { get; set; }
    string Category { get; set; }
    int Price { get; set; }
}

public interface ILibrarySystem
{
    void AddBook(IBook book, int quantity);
    void RemoveBook(IBook book, int quantity);
    int CalculateTotal();
    List<(string, int)> CategoryTotalPrice();
    List<(string, int, int)> BooksInfo();
    List<(string, string, int)> CategoryAndAuthorWithCount();
}

public class Book : IBook
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Category { get; set; }
    public int Price { get; set; }
}

public class LibrarySystem : ILibrarySystem
{
    private Dictionary<IBook, int> books = new Dictionary<IBook, int>();

    public void AddBook(IBook book, int quantity)
    {
        var existing = books.Keys.FirstOrDefault(x => x.Title == book.Title);

        if (existing != null)
        {
            books[existing] += quantity;
        }
        else
        {
            books.Add(book, quantity);
        }
    }

    public void RemoveBook(IBook book, int quantity)
    {
        var existing = books.Keys.FirstOrDefault(x => x.Title == book.Title);

        if (existing != null)
        {
            books[existing] -= quantity;

            if (books[existing] <= 0)
                books.Remove(existing);
        }
    }

    public int CalculateTotal()
    {
        return books.Sum(x => x.Key.Price * x.Value);
    }

    public List<(string, int)> CategoryTotalPrice()
    {
        return books
            .GroupBy(x => x.Key.Category)
            .Select(g => (g.Key, g.Sum(x => x.Key.Price * x.Value)))
            .ToList();
    }

    public List<(string, int, int)> BooksInfo()
    {
        return books
            .Select(x => (x.Key.Title, x.Value, x.Key.Price))
            .ToList();
    }

    public List<(string, string, int)> CategoryAndAuthorWithCount()
    {
        return books
            .GroupBy(x => new { x.Key.Category, x.Key.Author })
            .Select(g => (g.Key.Category, g.Key.Author, g.Sum(x => x.Value)))
            .ToList();
    }
}

class Program
{
    static void Main()
    {
        ILibrarySystem library = new LibrarySystem();

        IBook b1 = new Book { Id = 1, Title = "PeterPan", Author = "JamesMathewBarrie", Category = "KidsClassics", Price = 193 };
        IBook b2 = new Book { Id = 2, Title = "TheWizardOfOz", Author = "FrankBaum", Category = "KidsClassics", Price = 394 };

        library.AddBook(b1, 11);
        library.AddBook(b2, 3);

        Console.WriteLine("Book Info:");
        foreach (var item in library.BooksInfo())
        {
            Console.WriteLine($"Book Name:{item.Item1}, Quantity:{item.Item2}, Price:{item.Item3}");
        }

        Console.WriteLine("Category Total Price:");
        foreach (var item in library.CategoryTotalPrice())
        {
            Console.WriteLine($"Category:{item.Item1}, Total Price:{item.Item2}");
        }

        Console.WriteLine("Category And Author With Count:");
        foreach (var item in library.CategoryAndAuthorWithCount())
        {
            Console.WriteLine($"Category:{item.Item1}, Author:{item.Item2}, Count:{item.Item3}");
        }

        Console.WriteLine($"Total Price: {library.CalculateTotal()}");
    }
}