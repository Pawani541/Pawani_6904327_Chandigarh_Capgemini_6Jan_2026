using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        string color = Console.ReadLine();

        // Regex: starts with # and 6 hex characters
        if (Regex.IsMatch(color, "^#[0-9A-Fa-f]{6}$"))
            Console.WriteLine("Valid");
        else
            Console.WriteLine("Invalid");
    }
}
