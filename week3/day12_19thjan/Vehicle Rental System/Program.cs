using System;

class Vehicle
{
    public string Number { get; set; }
    public double RatePerDay { get; set; }

    public virtual double CalculateRent(int days)
    {
        return RatePerDay * days;
    }
}

class Car : Vehicle { }
class Bike : Vehicle { }
class Truck : Vehicle { }

class Customer
{
    public string Name 
    {
        get;
        set;
    }
}

class RentalTransaction
{
    public Vehicle Vehicle;
    public Customer Customer;
    public int Days;

    public void ShowBill()
    {
        Console.WriteLine("Customer Name: " + Customer.Name);
        Console.WriteLine("Vehicle No: " + Vehicle.Number);
        Console.WriteLine("Total Rent: " + Vehicle.CalculateRent(Days));
    }
}

class Program
{
    static void Main()
    {
        Car car = new Car();
        car.Number = "MH12AB1234";
        car.RatePerDay = 1500;

        Customer c = new Customer();
        c.Name = "Aarav";

        RentalTransaction rt = new RentalTransaction();
        rt.Vehicle = car;
        rt.Customer = c;
        rt.Days = 3;

        rt.ShowBill();
    }
}

