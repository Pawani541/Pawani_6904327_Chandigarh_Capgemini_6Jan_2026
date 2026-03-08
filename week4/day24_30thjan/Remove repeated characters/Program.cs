using System;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        string result = "";

        // Add character only if not already present
        foreach (char c in input)
            if (!result.Contains(c.ToString()))
                result += c;

        Console.WriteLine(result);
    }
}

