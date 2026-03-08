using System;
using System.Collections.Generic;

class Property
{
    public string Location;
    public int Price;

    public Property(string l, int p)
    {
        Location = l;
        Price = p;
    }
}

class RealEstate
{
    List<Property> properties = new List<Property>();

    public void AddProperty(Property p)
    {
        properties.Add(p);
    }

    public void ShowProperties()
    {
        foreach (var p in properties)
        {
            Console.WriteLine(p.Location + " " + p.Price);
        }
    }
}

class Program
{
    static void Main()
    {
        RealEstate re = new RealEstate();

        Console.WriteLine("Enter number of properties:");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter Location Price:");
            var a = Console.ReadLine().Split(' ');

            re.AddProperty(new Property(a[0], int.Parse(a[1])));
        }

        Console.WriteLine("Property List:");
        re.ShowProperties();
    }
}