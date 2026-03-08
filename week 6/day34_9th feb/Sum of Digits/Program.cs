using System;
class SumOfDigits
{
    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        string input = Console.ReadLine();
        int sum = 0;

        foreach (char c in input)
        {
            sum += c - '0';
        }
        Console.WriteLine("Sum of digits " + sum);
    }
}