using System;

class ToggleCaseOfEachCharacter
{
    static void Main()
    {
        string str = "ProGraMMinG";
        string result = "";

        foreach (char c in str)
        {
            if (char.IsUpper(c))
                result += char.ToLower(c);
            else
                result += char.ToUpper(c);
        }

        Console.WriteLine(result);
    }
}