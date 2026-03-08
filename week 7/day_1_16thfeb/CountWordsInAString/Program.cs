using System;

class CountWordsInAString
{
    static void Main()
    {
        string str = "C sharp programming language";

        string[] words = str.Split(' ');

        Console.WriteLine("Word count: " + words.Length);
    }
}