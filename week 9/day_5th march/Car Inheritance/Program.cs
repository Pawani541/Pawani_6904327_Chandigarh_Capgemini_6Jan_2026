using System;

class Car
{
    public string Brand;

    public Car(string b)
    {
        Brand = b;
    }

    public virtual void Start()
    {
        Console.WriteLine("Car Started");
    }
}

class ElectricCar : Car
{
    public ElectricCar(string b) : base(b) { }

    public override void Start()
    {
        Console.WriteLine("Electric Car Started: " + Brand);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter Car Brand:");

        string brand = Console.ReadLine();

        ElectricCar e = new ElectricCar(brand);

        e.Start();
    }
}