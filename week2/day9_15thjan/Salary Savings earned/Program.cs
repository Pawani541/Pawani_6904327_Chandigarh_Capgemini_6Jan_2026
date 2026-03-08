using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter salary: ");
        int salary = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter working days: ");
        int days = Convert.ToInt32(Console.ReadLine());

        if (salary > 9000) 
        {
            Console.WriteLine(-1);
            return;
        }
        if (salary < 0)
        {
            Console.WriteLine(-3);
            return;
        }
        if (days < 0)
        {
            Console.WriteLine(-4); 
            return;
        }

        double daily = salary / 30.0;
        double earned = daily * days;
        if (days == 31) earned += 500;

        double food = earned * 0.5;
        double travel = earned * 0.2;
        double saved = earned - food - travel;

        Console.WriteLine((int)saved);
    }
}
