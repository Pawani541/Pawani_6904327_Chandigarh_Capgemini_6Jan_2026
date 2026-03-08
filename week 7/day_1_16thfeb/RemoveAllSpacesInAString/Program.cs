using System;

class RemoveAllSpacesInAString
{
    static void Main()
    {
        string str = "C Sharp Programming";
        string result = "";

        foreach (char c in str)
        {
            if (c != ' ')
                result += c;
        }

        Console.WriteLine(result);
    }
}