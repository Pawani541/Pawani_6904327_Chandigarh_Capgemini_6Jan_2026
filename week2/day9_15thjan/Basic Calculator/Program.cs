using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter first number: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        int b = Convert.ToInt32(Console.ReadLine());

        if (a < 0 || b < 0) { Console.WriteLine(-1); return; }

        Console.Write("Enter operation (1=+,2=-,3=*,4=/): ");
        int op = Convert.ToInt32(Console.ReadLine());

        int result = 0;

        if (op == 1) result = a + b;
        else if (op == 2) result = a - b;
        else if (op == 3) result = a * b;
        else if (op == 4) result = b != 0 ? a / b : 0;

        Console.WriteLine(result);
    }
}
