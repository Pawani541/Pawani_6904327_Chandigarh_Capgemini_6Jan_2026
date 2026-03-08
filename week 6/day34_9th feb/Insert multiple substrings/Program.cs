using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string original = "ABCDEF";

        var inserts = new List<(int pos, string text)>
        {
            (0, "Hello"),
            (5, "World"),
            (6, "!")
        };

        inserts.Sort((a, b) => b.pos.CompareTo(a.pos));

        foreach (var item in inserts)
        {
            original = original.Substring(0, item.pos) + item.text + original.Substring(item.pos);
        }

        Console.WriteLine(original);
    }
}
