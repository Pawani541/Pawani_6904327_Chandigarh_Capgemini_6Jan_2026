using System;

class CompareArrays
{
    static void Main()
    {
        Console.Write("Enter size of both arrays: ");
        int size = int.Parse(Console.ReadLine());

        int[] a = new int[size];
        int[] b = new int[size];
        int[] output = new int[size];

        if (size < 0)
        {
            output[0] = -2;
        }
        else
        {
            bool neg = false;

            Console.WriteLine("Enter first array values:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("a[" + i + "] = ");
                a[i] = int.Parse(Console.ReadLine());
                if (a[i] < 0) neg = true;
            }

            Console.WriteLine("\nEnter second array values:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("b[" + i + "] = ");
                b[i] = int.Parse(Console.ReadLine());
                if (b[i] < 0) neg = true;
            }

            if (neg)
            {
                output[0] = -1;
            }
            else
            {
                for (int i = 0; i < size; i++)
                {
                    output[i] = Math.Max(a[i], b[i]);
                }
            }
        }

        Console.WriteLine("\nOutput array:");
        foreach (int v in output)
            Console.Write(v + " ");
    }
}
