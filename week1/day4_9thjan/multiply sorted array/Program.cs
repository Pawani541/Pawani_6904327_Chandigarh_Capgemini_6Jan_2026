using System;
class MultiplyArrays
{
    static void Main()
    {
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

            for (int i = 0; i < size; i++)
            {
                a[i] = int.Parse(Console.ReadLine());
                if (a[i] < 0) neg = true;
            }
            for (int i = 0; i < size; i++)
            {
                b[i] = int.Parse(Console.ReadLine());
                if (b[i] < 0) neg = true;
            }

            if (neg) output[0] = -1;
            else
            {
                Array.Sort(a);
                Array.Sort(b);
                Array.Reverse(b);
                for (int i = 0; i < size; i++)
                {
                    output[i] = a[i] * b[i];
                }
            }
        }
        foreach (int v in output) Console.Write(v + " ");
    }
}
