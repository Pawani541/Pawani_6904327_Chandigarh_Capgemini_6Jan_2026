using System;

class ReverseWithoutFunctions
{
    static void Main()
    {
        string str = "Programming";
        string rev = "";

        for (int i = str.Length - 1; i >= 0; i--)
            rev += str[i];

        Console.WriteLine(rev);
    }
}