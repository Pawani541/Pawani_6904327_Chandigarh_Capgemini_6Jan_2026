using System;
using System.Collections.Generic;
using System.Linq;

class User
{
    public int Id;
    public string IdentityNumber;
}

class UserManager
{
    public static void CompareUsers(List<User> db, List<User> newUsers)
    {
        int updated = 0;
        int inserted = 0;

        foreach (var n in newUsers)
        {
            var exist = db.FirstOrDefault(x => x.IdentityNumber == n.IdentityNumber);

            if (exist != null)
                updated++;
            else
                inserted++;
        }

        Console.WriteLine("Updated Users: " + updated);
        Console.WriteLine("Inserted Users: " + inserted);
    }
}

class Program
{
    static void Main()
    {
        try
        {
            List<User> db = new List<User>();
            List<User> newUsers = new List<User>();

            Console.WriteLine("Enter number of users in DB:");
            int n = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter users in format: Id IdentityNumber");

            for (int i = 0; i < n; i++)
            {
                var a = Console.ReadLine().Split(' ');

                db.Add(new User
                {
                    Id = int.Parse(a[0]),
                    IdentityNumber = a[1]
                });
            }

            Console.WriteLine("Enter number of new users:");
            int m = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter new users in format: Id IdentityNumber");

            for (int i = 0; i < m; i++)
            {
                var a = Console.ReadLine().Split(' ');

                newUsers.Add(new User
                {
                    Id = int.Parse(a[0]),
                    IdentityNumber = a[1]
                });
            }

            UserManager.CompareUsers(db, newUsers);
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input format. Please enter correct numbers.");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Input missing values. Please follow the correct format.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
    }
}