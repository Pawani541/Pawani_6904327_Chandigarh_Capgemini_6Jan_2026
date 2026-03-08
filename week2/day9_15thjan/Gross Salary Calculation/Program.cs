using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter basic salary: ");
        int basic = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter working days: ");
        int days = Convert.ToInt32(Console.ReadLine());

        if (basic < 0) 
        {
            Console.WriteLine(-1); return;
        }
        if (basic > 10000) 
        {
            Console.WriteLine(-2); return;
        }
        if (days > 31) 
        {
            Console.WriteLine(-3); return;
        }

        double da = 0.75 * basic;
        double hra = 0.50 * basic;
        double gross = basic + da + hra;

        Console.WriteLine(gross);
    }
}
