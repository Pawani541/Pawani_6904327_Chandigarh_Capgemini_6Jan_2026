using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Predicate<int> isEven = n => n % 2 == 0;

        Console.WriteLine(isEven(4)); // True
        Console.WriteLine(isEven(7)); // False

        List<int> nums = new List<int> { 1, 3, 4, 6, 7 };
        int evenNum = nums.Find(isEven);
        Console.WriteLine(evenNum); // 4
    }
}
