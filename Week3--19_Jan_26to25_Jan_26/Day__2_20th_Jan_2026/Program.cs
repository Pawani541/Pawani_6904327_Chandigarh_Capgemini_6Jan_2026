using System;
using System.Collections.Generic;
using System.Text;

class VowelAssignment
{
    static bool IsVowel(char c)
    {
        c = char.ToLower(c);
        return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u';
    }

    static void Main()
    {
        string first = Console.ReadLine();
        string second = Console.ReadLine();

        // Store consonants of second word (case-insensitive)
        HashSet<char> secondConsonants = new HashSet<char>();
        foreach (char c in second)
        {
            char lower = char.ToLower(c);
            if (!IsVowel(lower))
            {
                secondConsonants.Add(lower);
            }
        }

        // Step 1: Remove common consonants
        StringBuilder filtered = new StringBuilder();
        foreach (char c in first)
        {
            char lower = char.ToLower(c);

            if (IsVowel(lower) || !secondConsonants.Contains(lower))
            {
                filtered.Append(c);
            }
        }

        // Step 2: Remove consecutive duplicates
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < filtered.Length; i++)
        {
            if (i == 0 || filtered[i] != filtered[i - 1])
            {
                result.Append(filtered[i]);
            }
        }

        Console.WriteLine(result.ToString());
    }
}
