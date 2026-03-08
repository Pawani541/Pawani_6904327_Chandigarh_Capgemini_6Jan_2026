using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter password:");
        string password = Console.ReadLine();

        string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$";

        if (Regex.IsMatch(password, pattern))
            Console.WriteLine("Strong");
        else
            Console.WriteLine("Weak");
    }
}
