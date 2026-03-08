using System;

class RemoveAllVowelsFromAString
{
    static void Main()
    {
        string str = "Programming";
        string result = "";

        foreach (char c in str)
        {
            if (!"aeiouAEIOU".Contains(c))
                result += c;
        }

        Console.WriteLine(result);
    }
}