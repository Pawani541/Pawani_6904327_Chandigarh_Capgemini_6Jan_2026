using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("What is the correct way to declare an integer?");
        Console.WriteLine("a. int 1x=10;");
        Console.WriteLine("b. int x=10;");
        Console.WriteLine("c. float x=10.0f;");
        Console.WriteLine("d. string x=\"10\";");

        Console.Write("Choose your answer letter: ");
        char choice = char.Parse(Console.ReadLine());

        if (choice == 'b' || choice == 'B')
            Console.WriteLine("Correct choice!");
        else
            Console.WriteLine("Incorrect choice!");
    }
}
