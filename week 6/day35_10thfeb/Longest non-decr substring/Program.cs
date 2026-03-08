using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter binary string:");
        string s = Console.ReadLine();

        int zeros = 0, ones = 0;

        foreach (char c in s)
        {
            if (c == '0') zeros++;
            else ones++;
        }

        Console.WriteLine("Longest non-decreasing substring length: " + (zeros + ones));
    }
}
