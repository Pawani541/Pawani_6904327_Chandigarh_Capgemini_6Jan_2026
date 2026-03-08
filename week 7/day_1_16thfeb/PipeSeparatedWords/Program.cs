using System;

class PipeSeparatedWords
{
    static void Main()
    {
        string str = "C Sharp Programming Language";
        string[] words = str.Split(' ');

        Console.WriteLine(string.Join("|", words));
    }
}