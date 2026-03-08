using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter base text:");
        string input1 = Console.ReadLine();

        Console.WriteLine("Enter name:");
        string name = Console.ReadLine();

        string output = input1 + " " + name;

        string pattern = @"^hi-how-are-you-Dear\s[A-Za-z]{16,}$";

        if (Regex.IsMatch(output, pattern, RegexOptions.IgnoreCase))
            Console.WriteLine("Valid");
        else
            Console.WriteLine(output);
    }
}
