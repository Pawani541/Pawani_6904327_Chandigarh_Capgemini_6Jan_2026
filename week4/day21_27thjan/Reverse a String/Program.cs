using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the string:");
        string s = Console.ReadLine();

        string rev = "";
        for (int i = s.Length - 1; i >= 0; i--)
        {
            rev += s[i];
        }

        Console.WriteLine("Reversed string: " + rev);
    }
}

