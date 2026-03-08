using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter array size:");
        int input2 = Convert.ToInt32(Console.ReadLine());

        int[] input1 = new int[input2];

        Console.WriteLine("Enter array elements:");
        for (int i = 0; i < input2; i++)
        {
            Console.Write("Element " + (i + 1) + ": ");
            input1[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Enter element to search and remove:");
        int input3 = Convert.ToInt32(Console.ReadLine());

        int[] output = SearchRemoveAndSort(input1, input2, input3);

        Console.WriteLine("Output:");
        for (int i = 0; i < output.Length; i++)
        {
            Console.Write(output[i] + " ");
        }
    }

    static int[] SearchRemoveAndSort(int[] arr, int size, int search)
    {
        if (size < 0)
            return new int[] { -2 };

        for (int i = 0; i < size; i++)
        {
            if (arr[i] < 0)
                return new int[] { -1 };
        }

        int index = -1;
        for (int i = 0; i < size; i++)
        {
            if (arr[i] == search)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return new int[] { -3 };

        int[] result = new int[size - 1];
        int j = 0;

        for (int i = 0; i < size; i++)
        {
            if (i != index)
            {
                result[j++] = arr[i];
            }
        }

        // Sorting
        for (int i = 0; i < result.Length - 1; i++)
        {
            for (int k = i + 1; k < result.Length; k++)
            {
                if (result[i] > result[k])
                {
                    int temp = result[i];
                    result[i] = result[k];
                    result[k] = temp;
                }
            }
        }

        return result;
    }
}
