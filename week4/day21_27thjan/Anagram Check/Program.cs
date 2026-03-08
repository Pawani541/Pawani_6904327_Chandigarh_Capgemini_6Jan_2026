using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter first string:");
        string s1 = Console.ReadLine();

        Console.WriteLine("Enter second string:");
        string s2 = Console.ReadLine();

        if (s1.Length != s2.Length)
        {
            Console.WriteLine("Not Anagram");
            return;
        }

        Dictionary<char, int> count = new Dictionary<char, int>();

        foreach (char ch in s1)
        {
            if (count.ContainsKey(ch))
                count[ch]++;
            else
                count[ch] = 1;
        }

        foreach (char ch in s2)
        {
            if (!count.ContainsKey(ch) || count[ch] == 0)
            {
                Console.WriteLine("Not Anagram");
                return;
            }
            count[ch]--;
        }

        Console.WriteLine("Anagram");
    }
}
