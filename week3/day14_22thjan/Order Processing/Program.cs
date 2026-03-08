using System;
using System.Collections.Generic;
using System.Threading;

class OrderProcessing
{
    static int ProcessedCount = 0;
    static int Revenue = 0;
    static object locker = new object();
    static Queue<int> orders = new Queue<int>();

    static bool useLock = true;

    static void Worker()
    {
        while (true)
        {
            int price;
            lock (orders)
            {
                if (orders.Count == 0)
                    return;
                price = orders.Dequeue();
            }

            if (useLock)
            {
                lock (locker)
                {
                    ProcessedCount++;
                    Revenue += price;
                }
            }
            else
            {
                ProcessedCount++;
                Revenue += price;
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("Enter number of workers:");
        int workers = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter total number of orders:");
        int totalOrders = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter price per order:");
        int pricePerOrder = int.Parse(Console.ReadLine());

        Console.WriteLine("Use lock? (yes/no):");
        string choice = Console.ReadLine();
        useLock = choice.ToLower() == "yes";

        for (int i = 0; i < totalOrders; i++)
            orders.Enqueue(pricePerOrder);

        Thread[] threads = new Thread[workers];

        for (int i = 0; i < workers; i++)
        {
            threads[i] = new Thread(Worker);
            threads[i].Start();
        }

        foreach (Thread t in threads)
            t.Join();

        Console.WriteLine("\nFinal Output:");
        Console.WriteLine("ProcessedCount = " + ProcessedCount);
        Console.WriteLine("Revenue = " + Revenue);
    }
}
