using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the string:");
        string s = Console.ReadLine().ToLower();

        int count = 0;
        foreach (char ch in s)
        {
            if (ch == 'a' || ch == 'e' || ch == 'i' || ch == 'o' || ch == 'u')
                count++;
        }

        Console.WriteLine("Number of vowels: " + count);
    }
}
