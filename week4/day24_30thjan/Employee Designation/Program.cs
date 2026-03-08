using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter total number of elements:");
        int n = int.Parse(Console.ReadLine());

        string[] arr = new string[n];

        Console.WriteLine("Enter employee names and designations:");
        for (int i = 0; i < n; i++)
            arr[i] = Console.ReadLine();

        Console.WriteLine("Enter designation to search:");
        string designation = Console.ReadLine();

        string[] result = UserProgramCode.getEmployee(arr, designation);

        Console.WriteLine("Output:");
        foreach (string s in result)
            Console.WriteLine(s);
    }
}

