using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter first number: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Enter second number: ");
        int b = int.Parse(Console.ReadLine());

        Console.Write("Enter third number: ");
        int c = int.Parse(Console.ReadLine());

        int greatest = a;

        if (b > greatest)
            greatest = b;
        if (c > greatest)
            greatest = c;

        Console.WriteLine("Greatest number is: " + greatest);
    }
}
