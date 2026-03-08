using System;

class FToC
{
    static void Main()
    {
        Console.Write("Enter Fahrenheit: ");
        double f = double.Parse(Console.ReadLine());
        double output;

        if (f < 0) output = -1;
        else output = (f - 32) * 5.0 / 9.0;

        Console.WriteLine("Output: " + output);
    }
}
s