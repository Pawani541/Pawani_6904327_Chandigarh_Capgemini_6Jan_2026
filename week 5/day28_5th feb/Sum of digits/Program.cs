using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a positive integer:");
        string num = Console.ReadLine();

        int sum = 0;

        foreach (char c in num)
            sum += c - '0';

        Console.WriteLine("Sum of digits: " + sum);
    }
}
