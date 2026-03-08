using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

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
    Dictionary<IBook, int> books = new Dictionary<IBook, int>();
    public void AddBook(IBook book, int quantity)
    {
        books.Add(book, quantity);
    }
    public void RemoveBook(IBook book, int quantity)
    {
        if (books.ContainsKey(book))
        {
            books[book] -= quantity;
            if (books[book] <= 0) 
            books.Remove(Book);
        }
    }
    public int CalculateTotal()
    {
        int total = 0;
        foreach (var b in books)
        {
            total += b.Key.Price * b.Value;
        }
        return total;
    }
    List<(string, int)> CategoryTotalPrice()
    {
    var result = books
        .GroupBy(x => x.Key.Category)
        .Select(g => (g.Key, g.Sum(x => x.Key.Price * x.Value)))
        .ToList();
    return result;
}

List<(string, int, int)> BooksInfo();
{
    return books
       .Select(x => (x.Key.Title, x.Value, x.Key.Price))
       .ToList();
}
List<(string, string, int)> CategoryAndAuthorWithCount();
{
    var result = Books
        .GroupBy(x => new { x.Key.Category, x.Key.Author })
        .Select(g => (g.Key.Category, g.Key.Author, g.Sum(x => Value)))
        .ToList();
    return result;
}



}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new
StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        ILibrarySystem librarySystem = new LibrarySystem();
        int bCount = Convert.ToInt32(Console.ReadLine().Trim());
        for (int i = 1; i <= bCount; i++)
        {
            var a = Console.ReadLine().Trim().Split(" ");

            IBook e = new Book();
            e.Id = Convert.ToInt32(a[0]);
            e.Title = a[1];
            e.Author = a[2];
            e.Category = a[3];
            e.Price = Convert.ToInt32(a[4]);


            librarySystem.AddBook(e, Convert.ToInt32(a[5]));
        }

        textWriter.WriteLine("Book Info:");

        var booksInfo = librarySystem.BooksInfo();
        foreach (var (title, quantity, price) in booksInfo.OrderBy(a => a.Item1))
        {
            textWriter.WriteLine($"Book Name:{title}, Quantity:{quantity}, 
Price:{ price}
            "); 
        }

        textWriter.WriteLine("Category Total Price:");
        var categoryTotalPrice = librarySystem.CategoryTotalPrice();
        foreach (var (category, totalPrice) in categoryTotalPrice.OrderBy(a =>
a.Item1))
        {
            textWriter.WriteLine($"Category:{category}, Total Price:{totalPrice}");
        }

        List<(string, string, int)> categoryAndAuthorWithCount =
librarySystem.CategoryAndAuthorWithCount();
        textWriter.WriteLine("Category And Author With Count:");
        foreach (var (category, author, count) in
categoryAndAuthorWithCount.OrderBy(a => a.Item1))
        {
            
            textWriter.WriteLine($"Category:{category}, Author:{author}, 
Count:{ count}
            "); 
        }

        int total = librarySystem.CalculateTotal();
        textWriter.WriteLine($"Total Price: {total}");


        textWriter.Flush();
        textWriter.Close();
    }
}

