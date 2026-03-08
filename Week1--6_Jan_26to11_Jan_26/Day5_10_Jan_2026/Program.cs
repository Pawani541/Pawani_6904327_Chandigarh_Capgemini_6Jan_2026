using System;

class AddArrays
{
    static void Main()
    {
        Console.Write("Enter size of arrays: ");
        int size = int.Parse(Console.ReadLine());
        int[] output = new int[size];

        if (size < 0)
        {
            output[0] = -2;
        }
        else
        {
            int[] a = new int[size];
            int[] b = new int[size];
            bool neg = false;

            Console.WriteLine("Enter arr1:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("a[" + i + "] = ");
                a[i] = int.Parse(Console.ReadLine());
                if (a[i] < 0) neg = true;
            }

            Console.WriteLine("Enter arr2:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("b[" + i + "] = ");
                b[i] = int.Parse(Console.ReadLine());
                if (b[i] < 0) neg = true;
            }

            if (neg) output[0] = -1;
            else
                for (int i = 0; i < size; i++)
                    output[i] = a[i] + b[size - 1 - i];
        }

        Console.Write("Output: ");
        foreach (int x in output) Console.Write(x + " ");
    }
}
