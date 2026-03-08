using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter number of records");
        int n = Convert.ToInt32(Console.ReadLine());

        string[] arr = new string[n];

        Console.WriteLine("Enter the records");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Console.ReadLine();
        }

        Console.WriteLine("Enter the location code");
        int locationCode = Convert.ToInt32(Console.ReadLine());

        int result = UserProgramCode.getDonation(arr, locationCode);
        Console.WriteLine(result);
    }
}
