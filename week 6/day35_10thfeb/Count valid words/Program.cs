using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a sentence:");
        string input = Console.ReadLine();

        string[] words = input.Split(' ');
        int validCount = 0;

        foreach (string word in words)
        {
            if (word.Length > 2 &&
                Regex.IsMatch(word, "^[a-zA-Z0-9]+$") &&
                Regex.IsMatch(word, "[aeiouAEIOU]") &&
                Regex.IsMatch(word, "[bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ]"))
            {
                validCount++;
            }
        }

        Console.WriteLine("Valid words count: " + validCount);
    }
}
