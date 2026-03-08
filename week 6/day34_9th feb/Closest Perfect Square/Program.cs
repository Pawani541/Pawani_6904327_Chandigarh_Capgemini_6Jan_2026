using System;
class ClosestPerfectSquare
{
    static void Main()
    {
        Console.Write("Enter a positive integer: ");
        int num = int.Parse(Console.ReadLine());

        int root = (int)Math.Sqrt(num);

        int lowerSquare = root * root;
        int higherSquare = (root + 1) * (root + 1);

        int result;
        if (num - lowerSquare <= higherSquare - num)
            result = lowerSquare;
        else
            result = higherSquare;

        Console.WriteLine("Closest Perfect Square:  " + result);
    }
}