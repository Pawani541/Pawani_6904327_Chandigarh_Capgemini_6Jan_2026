using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter a number: ");
        int input1 = Convert.ToInt32(Console.ReadLine());

        if (input1 < 0)
        {
            Console.WriteLine(-1);
            return;
        }

        int sum = 0;
        while (input1 > 0)
        {
            int d = input1 % 10;
            if (d % 2 == 1)
                sum += d * d;

            input1 /= 10;
        }
        Console.WriteLine("Output = " + sum);
    }
}
