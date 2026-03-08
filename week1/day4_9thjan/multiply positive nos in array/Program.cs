using System;

class MultiplyPositives
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = int.Parse(Console.ReadLine());
        int output1 = 1;

        if (size < 0)
        {
            output1 = -2;
        }
        else
        {
            int[] arr = new int[size];
            Console.WriteLine("Enter array elements:");

            for (int i = 0; i < size; i++)
            {
                Console.Write("arr[" + i + "] = ");
                arr[i] = int.Parse(Console.ReadLine());
                if (arr[i] > 0)
                    output1 *= arr[i];
            }
        }

        Console.WriteLine("\nOutput: " + output1);
    }
}
