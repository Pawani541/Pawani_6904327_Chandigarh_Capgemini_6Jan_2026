using System;

class RemoveConsecutiveVowels
{
    static void Main()
    {
        string str = "beautiful";
        string result = "";
        string vowels = "aeiouAEIOU";

        for (int i = 0; i < str.Length; i++)
        {
            if (i > 0 && vowels.Contains(str[i]) && vowels.Contains(str[i - 1]))
                continue;

            result += str[i];
        }

        Console.WriteLine(result);
    }
}