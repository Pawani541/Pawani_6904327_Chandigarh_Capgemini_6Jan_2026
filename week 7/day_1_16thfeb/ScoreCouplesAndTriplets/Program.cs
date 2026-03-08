using System;

class ScoreCouplesAndTriplets
{
    static void Main()
    {
        int[] arr = { 1, 1, 1, 2, 2, 3, 3, 3 };

        int couples = 0;
        int triplets = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            int count = 1;

            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] == arr[j])
                    count++;
            }

            if (count == 2)
                couples++;
            else if (count == 3)
                triplets++;
        }

        Console.WriteLine("Couples: " + couples);
        Console.WriteLine("Triplets: " + triplets);
    }
}