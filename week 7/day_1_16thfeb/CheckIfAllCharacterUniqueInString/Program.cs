using System;

class CheckIfAllCharacterUniqueInString
{
    static void Main()
    {
        string str = "hello";
        bool unique = true;

        for (int i = 0; i < str.Length; i++)
        {
            for (int j = i + 1; j < str.Length; j++)
            {
                if (str[i] == str[j])
                {
                    unique = false;
                    break;
                }
            }
        }

        Console.WriteLine(unique ? "All characters unique" : "Not unique");
    }
}