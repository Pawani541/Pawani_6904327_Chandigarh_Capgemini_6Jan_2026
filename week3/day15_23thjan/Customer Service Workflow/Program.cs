using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Queue<string> tickets = new Queue<string>();
        Stack<string> actions = new Stack<string>();

        // Add tickets
        tickets.Enqueue("Ticket 1");
        tickets.Enqueue("Ticket 2");
        tickets.Enqueue("Ticket 3");

        Console.WriteLine("Processing Tickets:");

        // Process tickets
        for (int i = 0; i < 3; i++)
        {
            string ticket = tickets.Dequeue();
            Console.WriteLine("Processing " + ticket);

            actions.Push("Action done on " + ticket);
        }

        // Undo last action
        Console.WriteLine("Undoing last action:");
        Console.WriteLine(actions.Pop());

        // Remaining tickets
        Console.WriteLine("Remaining Tickets:");
        foreach (var t in tickets)
            Console.WriteLine(t);
    }
}

