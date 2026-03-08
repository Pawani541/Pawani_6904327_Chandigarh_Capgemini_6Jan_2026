using System;

class Count3Array
{
    static void Main()
    {
        Console.Write("Enter size: ");
        int size = int.Parse(Console.ReadLine());
        int output = 0;

        if (size < 0) output = -1;
        else
        {
            int[] arr = new int[size];
            bool neg = false;

            Console.WriteLine("Enter elements:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("arr[" + i + "] = ");
                arr[i] = int.Parse(Console.ReadLine());
                if (arr[i] < 0) neg = true;
            }

            if (neg) output = -1;
            else
                foreach (int v in arr)
                    if (v % 3 == 0) output++;
        }
        Console.WriteLine("Output: " + output);
    }
}
