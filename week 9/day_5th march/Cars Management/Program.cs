using System;
using System.Collections.Generic;

class Car
{
    public string Brand;
    public int Year;

    public Car(string b, int y)
    {
        Brand = b;
        Year = y;
    }
}

class CarManager
{
    List<Car> cars = new List<Car>();

    public void AddCar(Car c)
    {
        cars.Add(c);
    }

    public void ShowCars()
    {
        foreach (var c in cars)
        {
            Console.WriteLine(c.Brand + " " + c.Year);
        }
    }
}

class Program
{
    static void Main()
    {
        CarManager cm = new CarManager();

        Console.WriteLine("Enter number of cars:");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter Brand Year:");
            var a = Console.ReadLine().Split(' ');

            cm.AddCar(new Car(a[0], int.Parse(a[1])));
        }

        Console.WriteLine("Car List:");

        cm.ShowCars();
    }
}