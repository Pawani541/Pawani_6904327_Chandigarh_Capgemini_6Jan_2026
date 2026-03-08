using System;

class Computer
{
    public string Brand;
    public int Ram;

    public Computer(string b, int r)
    {
        Brand = b;
        Ram = r;
    }

    public virtual void Display()
    {
        Console.WriteLine("Brand: " + Brand);
        Console.WriteLine("RAM: " + Ram);
    }
}

class Laptop : Computer
{
    public int Battery;

    public Laptop(string b, int r, int bat) : base(b, r)
    {
        Battery = bat;
    }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("Battery Life: " + Battery);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Brand RAM Battery:");

        var a = Console.ReadLine().Split(' ');

        Laptop l = new Laptop(a[0], int.Parse(a[1]), int.Parse(a[2]));

        l.Display();
    }
}