using System;
class SumAvgMult5
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = int.Parse(Console.ReadLine());
        int output = 0;

        if (size < 0) output = -2;
        else
        {
            int[] arr = new int[size];
            bool neg = false;
            int sum = 0, count = 0;

            Console.WriteLine("Enter elements:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("arr[" + i + "] = ");
                arr[i] = int.Parse(Console.ReadLine());
                if (arr[i] < 0) neg = true;
            }

            if (neg) output = -1;
            else
            {
                foreach (int v in arr)
                {
                    if (v % 5 == 0)
                    {
                        sum += v;
                        count++;
                    }
                }
                output = (count > 0) ? sum / count : 0;
            }
        }

        Console.WriteLine("Output: " + output);
    }
}
