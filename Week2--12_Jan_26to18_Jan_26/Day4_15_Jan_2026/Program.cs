using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int[] output;

        if (n < 0)
        {
            output = new int[] { -2 };
        }
        else
        {
            int[] input1 = new int[n];
            int[] input2 = new int[n];

            Console.WriteLine("Enter input1 elements:");
            for (int i = 0; i < n; i++) input1[i] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter input2 elements:");
            for (int i = 0; i < n; i++) input2[i] = Convert.ToInt32(Console.ReadLine());

            if (Array.Exists(input1, x => x < 0) || Array.Exists(input2, x => x < 0))
            {
                output = new int[] { -1 };
            }
            else
            {
                output = new int[n];
                for (int i = 0; i < n; i++)
                {
                    output[i] = input1[i] + input2[n - i - 1];
                }
            }
        }

        Console.WriteLine(string.Join(", ", output));
    }
}
