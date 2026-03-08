using System;

class Program
{
    static void Main()
    {
        string input = "banana|apple|mango";
        string[] words = input.Split('|');

        Array.Sort(words);

        Console.WriteLine(string.Join("|", words));
    }
}
