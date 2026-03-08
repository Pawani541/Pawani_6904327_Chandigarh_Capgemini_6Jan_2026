using System;

class LongestWordInAString
{
    static void Main()
    {
        string str = "C sharp programming language";

        string[] words = str.Split(' ');
        string longest = "";

        foreach (string w in words)
        {
            if (w.Length > longest.Length)
                longest = w;
        }

        Console.WriteLine("Longest word: " + longest);
    }
}