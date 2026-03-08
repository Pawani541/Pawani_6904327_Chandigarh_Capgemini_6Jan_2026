using System;

class NotDivisibleByOtherElementsInArray
{
    static void Main()
    {
        int[] arr = { 2, 3, 4, 9, 7 };

        for (int i = 0; i < arr.Length; i++)
        {
            bool divisible = false;

            for (int j = 0; j < arr.Length; j++)
            {
                if (i != j && arr[i] % arr[j] == 0)
                {
                    divisible = true;
                    break;
                }
            }

            if (!divisible)
                Console.WriteLine(arr[i]);
        }
    }
}