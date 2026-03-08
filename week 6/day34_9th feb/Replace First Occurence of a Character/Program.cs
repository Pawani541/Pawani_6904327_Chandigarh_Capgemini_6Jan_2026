using System;

class ReplaceFirstOccurrence
{
    static void Main()
    {
        string input = "I love programming.";
        char oldChar = '.';
        char newChar = '!';

        int index = input.IndexOf(oldChar);

        if (index != -1)
        {
            input = input.Remove(index, 1)
                         .Insert(index, newChar.ToString());
        }

        Console.WriteLine("String after replacing '" + oldChar +
                           "' with '" + newChar + "':");
        Console.WriteLine(input);
    }
}
