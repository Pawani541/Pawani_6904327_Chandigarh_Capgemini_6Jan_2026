using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a binary number:");
        int input1 = Convert.ToInt32(Console.ReadLine());

        int output = BinaryToDecimal(input1);

        Console.WriteLine("Output: " + output);
    }

    static int BinaryToDecimal(int binary)
    {
        if (binary > 11111)
            return -2;

        int decimalValue = 0;
        int baseValue = 1;

        while (binary > 0)
        {
            int digit = binary % 10;

            if (digit != 0 && digit != 1)
                return -1;

            decimalValue += digit * baseValue;
            baseValue *= 2;
            binary /= 10;
        }

        return decimalValue;
    }
}
