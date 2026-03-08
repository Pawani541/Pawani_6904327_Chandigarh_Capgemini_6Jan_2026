using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter size: ");
        int n = Convert.ToInt32(Console.ReadLine());

        if (n < 0)
        {
            Console.WriteLine(-2); return; 
        }

        int[] arr = new int[n];
        Console.WriteLine("Enter elements:");
        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
            if (arr[i] < 0) { Console.WriteLine(-1); return; }
        }

        Console.Write("Enter number to insert: ");
        int insert = Convert.ToInt32(Console.ReadLine());

        // Sort (Bubble Sort)
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (arr[i] > arr[j])
                {
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
        }

        // insert
        int[] output = new int[n + 1];
        for (int i = 0; i < n; i++) output[i] = arr[i];
        output[n] = insert;

        // sort again
        for (int i = 0; i < output.Length; i++)
        {
            for (int j = i + 1; j < output.Length; j++)
            {
                if (output[i] > output[j])
                {
                    int temp = output[i];
                    output[i] = output[j];
                    output[j] = temp;
                }
            }
        }

        Console.WriteLine(string.Join(", ", output));
    }
}
