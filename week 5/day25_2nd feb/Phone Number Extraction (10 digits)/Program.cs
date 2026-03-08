using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter text:");
        string input = Console.ReadLine();

        MatchCollection matches = Regex.Matches(input, @"\b\d{10}\b");

        foreach (Match m in matches)
            Console.WriteLine(m.Value);
    }
}
