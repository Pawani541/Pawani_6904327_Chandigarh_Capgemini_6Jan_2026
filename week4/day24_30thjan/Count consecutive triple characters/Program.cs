using System;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        int count = 0;

        // Loop till length-2 for checking 3 chars
        for (int i = 0; i < input.Length - 2; i++)
        {
            if (input[i] == input[i + 1] && input[i] == input[i + 2])
            {
                count++;
                i += 2; // Skip counted characters
            }
        }

        Console.WriteLine(count);
    }
}
