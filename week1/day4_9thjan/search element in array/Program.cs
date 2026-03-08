using System;

class SearchElement
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int size = int.Parse(Console.ReadLine());
        int output1 = 1;

        if (size < 0) output1 = -2;
        else
        {
            int[] arr = new int[size];
            bool neg = false;

            Console.WriteLine("Enter array elements:");
            for (int i = 0; i < size; i++)
            {
                Console.Write("arr[" + i + "] = ");
                arr[i] = int.Parse(Console.ReadLine());
                if (arr[i] < 0) neg = true;
            }

            Console.Write("\nEnter element to search: ");
            int search = int.Parse(Console.ReadLine());

            if (neg) output1 = -1;
            else
            {
                for (int i = 0; i < size; i++)
                {
                    if (arr[i] == search)
                    {
                        output1 = i; // location
                        break;
                    }
                }
            }
        }

        Console.WriteLine("\nOutput: " + output1);
    }
}
