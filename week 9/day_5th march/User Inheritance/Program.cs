using System;

class User
{
    public string Name;

    public User(string n)
    {
        Name = n;
    }

    public virtual void Show()
    {
        Console.WriteLine("User: " + Name);
    }
}

class Admin : User
{
    public Admin(string n) : base(n) { }

    public override void Show()
    {
        Console.WriteLine("Admin User: " + Name);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter user name:");

        string name = Console.ReadLine();

        Admin a = new Admin(name);

        a.Show();
    }
}