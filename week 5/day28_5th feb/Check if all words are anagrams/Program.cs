using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter comma separated words:");
        string[] words = Console.ReadLine().Split(',');

        string baseWord = String.Concat(words[0].Trim().OrderBy(c => c));

        bool allAnagrams = true;

        foreach (string word in words)
        {
            string sorted = String.Concat(word.Trim().OrderBy(c => c));

            if (sorted != baseWord)
            {
                allAnagrams = false;
                break;
            }
        }

        Console.WriteLine(allAnagrams);
    }
}
