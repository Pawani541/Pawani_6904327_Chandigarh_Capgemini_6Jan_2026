using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter first list (comma separated):");
        int[] list1 = Console.ReadLine().Split(',').Select(x => int.Parse(x.Trim())).ToArray();

        Console.WriteLine("Enter second list (comma separated):");
        int[] list2 = Console.ReadLine().Split(',').Select(x => int.Parse(x.Trim())).ToArray();

        foreach (int n in list1)
        {
            int sum = list2.Where(x => x == n).Sum();
            Console.WriteLine($"{n}-{sum}");
        }
    }
}
