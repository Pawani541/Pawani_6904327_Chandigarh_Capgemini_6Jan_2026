using System;

class ReversePipeSeparatedWords
{
    static void Main()
    {
        Console.Write("Enter pipe separated words: ");
        string input = Console.ReadLine();

        // Split by '|'
        string[] words = input.Split('|');

        // Reverse the array
        Array.Reverse(words);

        // Join back with '|'
        string result = string.Join("|", words);

        Console.WriteLine("Reversed order:");
        Console.WriteLine(result);
    }
}
