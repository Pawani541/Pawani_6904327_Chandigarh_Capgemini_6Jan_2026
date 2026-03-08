using System;
using System.Collections.Generic;
using System.Linq;

class User
{
    public string Email;
    public string Password;
    public string Location;

    public User(string e, string p, string l)
    {
        Email = e;
        Password = p;
        Location = l;
    }
}

class AuthSystem
{
    List<User> users = new List<User>();

    public void Register(User u)
    {
        if (users.Any(x => x.Email == u.Email))
            Console.WriteLine("User already registered");
        else
        {
            users.Add(u);
            Console.WriteLine("User registered");
        }
    }

    public void Login(User u)
    {
        var user = users.FirstOrDefault(x => x.Email == u.Email);

        if (user == null)
            Console.WriteLine("User not found");
        else if (user.Password != u.Password)
            Console.WriteLine("Wrong password");
        else
            Console.WriteLine("Login successful");
    }
}

class Program
{
    static void Main()
    {
        AuthSystem auth = new AuthSystem();

        Console.WriteLine("Register user (email password location)");
        var a = Console.ReadLine().Split(' ');

        User u = new User(a[0], a[1], a[2]);

        auth.Register(u);

        Console.WriteLine("Login user (email password location)");
        var b = Console.ReadLine().Split(' ');

        User u2 = new User(b[0], b[1], b[2]);

        auth.Login(u2);
    }
} 