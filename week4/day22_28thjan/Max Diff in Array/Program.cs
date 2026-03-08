using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter number of elements");
        int n = Convert.ToInt32(Console.ReadLine());

        int[] arr = new int[n];

        Console.WriteLine("Enter the elements");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        int result = UserProgramCode.diffIntArray(arr);
        Console.WriteLine(result);
    }
}
