using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter salary per month: ");
        int salary = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter working days: ");
        int days = Convert.ToInt32(Console.ReadLine());

        int result;

        if (salary > 9000) result = -1;
        else if (salary < 0) result = -2;
        else if (days < 0) result = -4;
        else
        {
            double daily = salary / 30.0;
            double earned = daily * days;
            if (days == 31) earned += 500;

            double food = 0.5 * earned;
            double travel = 0.2 * earned;

            result = (int)(earned - (food + travel));
        }

        Console.WriteLine(result);
    }
}
