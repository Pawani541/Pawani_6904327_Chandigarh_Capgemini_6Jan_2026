using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter number of elements");
        int n = Convert.ToInt32(Console.ReadLine());

        List<int> list = new List<int>();

        Console.WriteLine("Enter the elements");
        for (int i = 0; i < n; i++)
        {
            list.Add(Convert.ToInt32(Console.ReadLine()));
        }

        Console.WriteLine("Enter the value");
        int value = Convert.ToInt32(Console.ReadLine());

        List<int> result = UserProgramCode.GetElements(list, value);

        if (result.Count == 1 && result[0] == -1)
        {
            Console.WriteLine("No element found");
        }
        else
        {
            foreach (int num in result)
                Console.Write(num + " ");
        }
    }
}
