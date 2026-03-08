using System;

class Program
{
    static void Main()
    {
        string input = "A1B2C3";
        string digits = "";

        foreach (char c in input)
        {
            if (char.IsDigit(c))
                digits += c;
        }

        Console.WriteLine(digits);
        Console.WriteLine(input);
    }
}
