using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the string:");
        string s = Console.ReadLine();

        Dictionary<char, int> freq = new Dictionary<char, int>();

        foreach (char ch in s)
        {
            if (freq.ContainsKey(ch))
                freq[ch]++;
            else
                freq[ch] = 1;
        }

        char result = '\0';
        foreach (char ch in s)
        {
            if (freq[ch] == 1)
            {
                result = ch;
                break;
            }
        }

        if (result == '\0')
            Console.WriteLine("No non-repeating character");
        else
            Console.WriteLine("First non-repeating character: " + result);
    }
}
