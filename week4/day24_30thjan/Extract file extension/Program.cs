using System;

class Program
{
    static void Main()
    {
        string file = "File.dat";

        // Finding last dot
        int index = file.LastIndexOf('.');

        // Extract extension
        Console.WriteLine(file.Substring(index + 1));
    }
}

