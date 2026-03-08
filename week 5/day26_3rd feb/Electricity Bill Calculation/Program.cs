using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter first meter reading:");
        string input1 = Console.ReadLine();

        Console.WriteLine("Enter second meter reading:");
        string input2 = Console.ReadLine();

        Console.WriteLine("Enter rate per unit:");
        int rate = int.Parse(Console.ReadLine());

        int reading1 = int.Parse(Regex.Match(input1, @"\d+").Value);
        int reading2 = int.Parse(Regex.Match(input2, @"\d+").Value);

        int units = Math.Abs(reading2 - reading1);
        int bill = units * rate;

        Console.WriteLine("Bill Amount: " + bill);
    }
}
