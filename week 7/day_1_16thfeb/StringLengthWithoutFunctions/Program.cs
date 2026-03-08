using System;

class StringLengthWithoutFunctions
{
    static void Main()
    {
        string str = "Programming";
        int count = 0;

        foreach (char c in str)
            count++;

        Console.WriteLine("Length: " + count);
    }
}