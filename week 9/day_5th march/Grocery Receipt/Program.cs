using System;
using System.Collections.Generic;
using System.Globalization;

abstract class GroceryReceiptBase2
{
    public Dictionary<string, decimal> Prices { get; set; }
    public Dictionary<string, int> Discounts { get; set; }

    public GroceryReceiptBase2(Dictionary<string, decimal> prices,
                               Dictionary<string, int> discounts)
    {
        Prices = prices;
        Discounts = discounts;
    }

    public abstract IEnumerable<(string fruit, decimal price, decimal total)>
        Calculate(List<Tuple<string, int>> shoppingList);
}

class GroceryReceipt2 : GroceryReceiptBase2
{
    public GroceryReceipt2(Dictionary<string, decimal> prices,
                           Dictionary<string, int> discounts)
        : base(prices, discounts)
    {
    }

    public override IEnumerable<(string fruit, decimal price, decimal total)>
        Calculate(List<Tuple<string, int>> shoppingList)
    {
        List<(string, decimal, decimal)> result = new List<(string, decimal, decimal)>();

        foreach (var item in shoppingList)
        {
            string fruit = item.Item1;
            int quantity = item.Item2;

            decimal price = Prices[fruit];
            decimal total = price * quantity;

            if (Discounts.ContainsKey(fruit))
            {
                int discount = Discounts[fruit];
                total = total - (total * discount / 100);
            }

            result.Add((fruit, price, total));
        }

        return result;
    }
}

class Program
{
    static void Main()
    {
        List<Tuple<string, int>> boughtItems = new List<Tuple<string, int>>();
        Dictionary<string, decimal> prices = new Dictionary<string, decimal>();
        Dictionary<string, int> discounts = new Dictionary<string, int>();

        Console.WriteLine("Enter number of fruits:");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter fruit name and price (Example: Apple 10)");

        for (int i = 0; i < n; i++)
        {
            var a = Console.ReadLine().Split(' ');
            prices.Add(a[0], Convert.ToDecimal(a[1]));
        }

        Console.WriteLine("Enter number of discount items:");
        int m = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter fruit name and discount percentage (Example: Apple 10)");

        for (int i = 0; i < m; i++)
        {
            var a = Console.ReadLine().Split(' ');
            discounts.Add(a[0], Convert.ToInt32(a[1]));
        }

        Console.WriteLine("Enter number of items bought:");
        int b = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter fruit name and quantity (Example: Apple 2)");

        for (int i = 0; i < b; i++)
        {
            var a = Console.ReadLine().Split(' ');
            boughtItems.Add(new Tuple<string, int>(a[0], Convert.ToInt32(a[1])));
        }

        GroceryReceipt2 g = new GroceryReceipt2(prices, discounts);

        var result = g.Calculate(boughtItems);

        Console.WriteLine("\nReceipt:");

        foreach (var x in result)
        {
            Console.WriteLine($"{x.fruit} {x.price.ToString("0.0", CultureInfo.InvariantCulture)} {x.total.ToString("0.0", CultureInfo.InvariantCulture)}");
        }
    }
}