using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Press any key...");

        ConsoleKeyInfo key = Console.ReadKey();
        Console.WriteLine();

        if (key.KeyChar >= '0' && key.KeyChar <= '9')
            Console.WriteLine("You pressed number: " + key.KeyChar);
        else
            Console.WriteLine("Not allowed");
    }
}

