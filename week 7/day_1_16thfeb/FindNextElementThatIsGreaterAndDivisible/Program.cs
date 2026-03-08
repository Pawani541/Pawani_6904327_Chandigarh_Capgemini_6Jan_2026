using System;

class FindNextElementThatIsGreaterAndDivisible
{
    static void Main()
    {
        int[] arr = { 2, 3, 5, 8, 10 };

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] > arr[i] && arr[j] % arr[i] == 0)
                {
                    Console.WriteLine(arr[i] + " -> " + arr[j]);
                    break;
                }
            }
        }
    }
}