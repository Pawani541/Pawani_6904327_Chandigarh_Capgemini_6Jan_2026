using System;

class CountConsonantsAndVowels
{
    static void Main()
    {
        string str = "hello world";
        int vowels = 0, consonants = 0;

        foreach (char c in str)
        {
            if ("aeiou".Contains(c))
                vowels++;
            else if (char.IsLetter(c))
                consonants++;
        }

        Console.WriteLine("Vowels: " + vowels);
        Console.WriteLine("Consonants: " + consonants);
    }
}