using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter product records (Press ENTER on empty line to stop):");

        Dictionary<string, int> maxSales = new Dictionary<string, int>();

        while (true)
        {
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
                break;

            string[] parts = input.Split('-');

            if (parts.Length != 2)
                continue; // invalid line skip

            string productId = parts[0].Trim();
            int sales = int.Parse(parts[1].Trim());

            if (!maxSales.ContainsKey(productId))
                maxSales[productId] = sales;
            else
                maxSales[productId] = Math.Max(maxSales[productId], sales);
        }

        var sorted = maxSales.OrderByDescending(x => x.Value);

        Console.WriteLine("Output:");

        foreach (var item in sorted)
            Console.WriteLine(item.Key + "-" + item.Value);
    }
}
