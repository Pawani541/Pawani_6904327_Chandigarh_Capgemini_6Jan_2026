using System;

public delegate int MyDelegate(int a, int b);

class Program
{
    static int Add(int x, int y)
    {
        return x + y;
    }

    static int Multiply(int x, int y)
    {
        return x * y;
    }

    static void Main()
    {
        MyDelegate d = Add;
        Console.WriteLine(d(5, 3));   // 8

        d = Multiply;
        Console.WriteLine(d(5, 3));   // 15
    }
}
