using System;

class ShortestWordInAString
{
    static void Main()
    {
        string str = "C Sharp Programming Language";

        string[] words = str.Split(' ');
        string shortest = words[0];

        foreach (string w in words)
        {
            if (w.Length < shortest.Length)
                shortest = w;
        }

        Console.WriteLine("Shortest word: " + shortest);
    }
}