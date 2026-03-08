using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter current invoice number:");
        string invoice = Console.ReadLine();

        Console.WriteLine("Enter increment value:");
        int inc = int.Parse(Console.ReadLine());

        Match match = Regex.Match(invoice, @"\d+");

        int number = int.Parse(match.Value);
        int newNumber = number + inc;

        string updatedInvoice = Regex.Replace(invoice, @"\d+", newNumber.ToString());

        Console.WriteLine("Updated Invoice: " + updatedInvoice);
    }
}
