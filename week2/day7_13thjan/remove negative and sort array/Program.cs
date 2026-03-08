using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] arr = { 20, -10, 4, 78, -3, 12 };
        int size = arr.Length;

        if (size < 0)
        {
            Console.WriteLine("-1");
            return;
        }

        var result = arr.Where(x => x >= 0).OrderBy(x => x).ToArray();

        Console.WriteLine("Output: " + string.Join(",", result));
    }
}
