using System;

class QuestionPaperMarks
{
    static void Main()
    {
        int X = int.Parse(Console.ReadLine());
        int Y = int.Parse(Console.ReadLine());
        int N1 = int.Parse(Console.ReadLine());
        int N2 = int.Parse(Console.ReadLine());
        int M = int.Parse(Console.ReadLine());

        bool isValid = false;
        int bestA = 0, bestB = 0;

        // Try maximum Type 1 questions first
        for (int a = N1; a >= 0; a--)
        {
            int remaining = M - (a * X);

            if (remaining < 0)
                continue;

            if (remaining % Y == 0)
            {
                int b = remaining / Y;
                if (b <= N2)
                {
                    isValid = true;
                    bestA = a;
                    bestB = b;
                    break;
                }
            }
        }

        if (isValid)
        {
            Console.WriteLine("Valid");
            Console.WriteLine(bestA);
            Console.WriteLine(bestB);
        }
        else
        {
            Console.WriteLine("Invalid");
        }
    }
}
