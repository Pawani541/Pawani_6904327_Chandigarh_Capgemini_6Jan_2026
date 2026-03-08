using System;
using System.Collections.Concurrent;
using System.Threading;

class Program
{
    static BlockingCollection<string> logs;
    static int processedCount = 0;

    static void Main()
    {
        
        Console.WriteLine("Enter total number of logs:");
        int totalLogs = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter buffer capacity:");
        int capacity = int.Parse(Console.ReadLine());

        logs = new BlockingCollection<string>(capacity);

        Thread producer = new Thread(() => Producer(totalLogs));
        Thread consumer1 = new Thread(Consumer1);
        Thread consumer2 = new Thread(Consumer2);

        producer.Start();
        consumer1.Start();
        consumer2.Start();

        producer.Join();
        consumer1.Join();
        consumer2.Join();

        // ===== OUTPUT =====
        Console.WriteLine("--------------------");
        Console.WriteLine("Total logs processed: " + processedCount);
    }

    static void Producer(int totalLogs)
    {
        for (int i = 1; i <= totalLogs; i++)
        {
            logs.Add("Log " + i);
            Console.WriteLine("Produced: Log " + i);
        }

        logs.CompleteAdding(); // important
    }

    static void Consumer1()
    {
        foreach (string log in logs.GetConsumingEnumerable())
        {
            try
            {
                Console.WriteLine("Consumer 1 processing " + log);
                Thread.Sleep(50);
                processedCount++;
            }
            catch
            {
                Console.WriteLine("Consumer 1 error");
            }
        }
    }

    static void Consumer2()
    {
        foreach (string log in logs.GetConsumingEnumerable())
        {
            try
            {
                Console.WriteLine("Consumer 2 processing " + log);
                Thread.Sleep(50);
                processedCount++;
            }
            catch
            {
                Console.WriteLine("Consumer 2 error");
            }
        }
    }
}
