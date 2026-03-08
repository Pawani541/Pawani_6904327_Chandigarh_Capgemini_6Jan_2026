using System;

class FindAllSubstrings
{
    static void Main()
    {
        string str = "abc";

        for (int i = 0; i < str.Length; i++)
        {
            for (int j = i + 1; j <= str.Length; j++)
            {
                Console.WriteLine(str.Substring(i, j - i));
            }
        }
    }
}