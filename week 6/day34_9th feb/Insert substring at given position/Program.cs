using System;

class Program
{
    static void Main()
    {
        string result = InsertAtPosition("C programming", "ABC", 3);
        Console.WriteLine(result);
    }

    static string InsertAtPosition(string original, string toInsert, int position)
    {
        return original.Substring(0, position) + toInsert + original.Substring(position);
    }
}
