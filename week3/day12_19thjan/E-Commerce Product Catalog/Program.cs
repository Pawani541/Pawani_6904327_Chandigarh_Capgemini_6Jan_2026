using System;

class EProduct   
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public double Price { get; protected set; }

    public EProduct(int id, string name, double price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public virtual void Display()
    {
        Console.WriteLine($"{Id} - {Name} - ₹{Price}");
    }
}

class Electronics : EProduct
{
    public Electronics(int id, string name, double price)
        : base(id, name, price) { }
}

class Clothing : EProduct
{
    public Clothing(int id, string name, double price)
        : base(id, name, price) { }
}

class Books : EProduct
{
    public Books(int id, string name, double price)
        : base(id, name, price) { }
}

class Customer
{
    public string Name { get; set; }
}

class Order
{
    public EProduct Product;
    public Customer Customer;

    public void ShowOrder()
    {
        Console.WriteLine("Customer: " + Customer.Name);
        Product.Display();
    }
}

class Program
{
    static void Main()
    {
        Electronics e = new Electronics(1, "Laptop", 55000);
        Clothing c = new Clothing(2, "T-Shirt", 1200);
        Books b = new Books(3, "C# Guide", 500);

        Customer cust = new Customer { Name = "Aarav" };

        Order o1 = new Order { Customer = cust, Product = e };
        Order o2 = new Order { Customer = cust, Product = c };
        Order o3 = new Order { Customer = cust, Product = b };

        o1.ShowOrder();
        o2.ShowOrder();
        o3.ShowOrder();
    }
}
