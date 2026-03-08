using System;

class CheckIfSubstring
{
    static void Main()
    {
        string str = "hello world";
        string sub = "world";

        if (str.Contains(sub))
            Console.WriteLine("Substring found");
        else
            Console.WriteLine("Substring not found");
    }
}