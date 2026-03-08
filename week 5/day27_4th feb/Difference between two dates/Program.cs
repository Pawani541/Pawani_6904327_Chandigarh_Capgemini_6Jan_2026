using System;
using System.Globalization;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter first date (dd/MM/yyyy):");
        string d1 = Console.ReadLine();

        Console.WriteLine("Enter second date (dd/MM/yyyy):");
        string d2 = Console.ReadLine();

        DateTime date1 = DateTime.ParseExact(d1, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        DateTime date2 = DateTime.ParseExact(d2, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        int diff = Math.Abs((date2 - date1).Days);

        Console.WriteLine(diff + " days");
    }
}
