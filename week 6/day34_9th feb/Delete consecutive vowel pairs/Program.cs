using System;

class Program
{
    static void Main()
    {
        string s = "beautiful";
        string vowels = "aeiouAEIOU";
        int deletions = 0;

        for (int i = 0; i < s.Length - 1; i++)
        {
            if (vowels.Contains(s[i]) && vowels.Contains(s[i + 1]))
            {
                deletions++;
                i++;
            }
        }

        Console.WriteLine(deletions);
    }
}
