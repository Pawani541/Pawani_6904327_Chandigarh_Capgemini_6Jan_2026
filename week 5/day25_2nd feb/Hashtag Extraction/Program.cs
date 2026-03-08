using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter text:");
        string input = Console.ReadLine();

        MatchCollection matches = Regex.Matches(input, @"#\w+");

        foreach (Match m in matches)
            Console.WriteLine(m.Value);
    }
}
