using System;

class CountOccurrencesInSecondList
{
    static void Main()
    {
        int[] arr1 = { 1, 2, 3, 4 };
        int[] arr2 = { 2, 3, 2, 4, 5, 2 };

        foreach (int num in arr1)
        {
            int count = 0;

            foreach (int n in arr2)
            {
                if (num == n)
                    count++;
            }

            Console.WriteLine(num + " occurs " + count + " times");
        }
    }
}