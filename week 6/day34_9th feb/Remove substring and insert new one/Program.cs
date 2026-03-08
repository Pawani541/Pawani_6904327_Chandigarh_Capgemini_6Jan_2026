using System;

class Program
{
    static void Main()
    {
        string original = "HelloWorld";
        string result = original.Remove(5, 5).Insert(5, "Universe");
        Console.WriteLine(result);
    }
}
