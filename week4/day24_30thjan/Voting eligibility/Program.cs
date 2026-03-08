using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter age:");
        int age = int.Parse(Console.ReadLine());

        if (age >= 18)
            Console.WriteLine("Eligible to vote");
        else
            Console.WriteLine("Not eligible to vote");
    }
}

