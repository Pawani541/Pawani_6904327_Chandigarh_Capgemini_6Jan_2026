using System;
class EvenOddAvg
{
    static void Main()
    {
        int size = int.Parse(Console.ReadLine());
        int output1 = 0;

        if (size < 0) output1 = -2;
        else
        {
            int[] arr = new int[size];
            bool neg = false;
            for (int i = 0; i < size; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
                if (arr[i] < 0) neg = true;
            }

            if (neg) output1 = -1;
            else
            {
                int sumEven = 0, sumOdd = 0;
                foreach (int v in arr)
                {
                    if (v % 2 == 0) sumEven += v;
                    else sumOdd += v;
                }
                output1 = (sumEven + sumOdd) / 2;
            }
        }
        Console.WriteLine(output1);
    }
}
