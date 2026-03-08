using System;
class CoupleDivisibleByN
{
    static void Main()
    {
        int N = 4;
        int[] arr = { 2, 2, 4, 0 };
        int count = 0;
        for (int i = 0; i < N - 1; i++)
        {
            int sum = arr[i] + arr[i + 1];
            if (sum % N == 0)
            {
                count++;
            }
        }
        Console.WriteLine("Output: " + count);
    }
}