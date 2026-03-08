using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] arr = { 7, 3, 9, 1, 8, 6 };
        int size = arr.Length;

        if (size < 0)
        {
            Console.WriteLine("-1");
            return;
        }

        int mid = size / 2;
        int[] firstHalf = arr.Take(mid).OrderBy(x => x).ToArray();
        int[] secondHalf = arr.Skip(mid).OrderByDescending(x => x).ToArray();

        int[] output = firstHalf.Concat(secondHalf).ToArray();
        Console.WriteLine("Output: " + string.Join(",", output));
    }
}
