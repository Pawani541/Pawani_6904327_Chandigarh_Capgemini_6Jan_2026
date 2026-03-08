using System;

class ReplaceSpacesWithHyphenInString
{
    static void Main()
    {
        string str = "C Sharp Programming Language";

        string result = str.Replace(" ", "-");

        Console.WriteLine(result);
    }
}