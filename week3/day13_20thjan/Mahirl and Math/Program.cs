using System;
using System.Collections.Generic;

class MinimalOperations
{
    static void Main()
    {
        int N = int.Parse(Console.ReadLine());

        // BFS Queue: (current value, steps)
        Queue<(int value, int steps)> queue = new Queue<(int, int)>();
        HashSet<int> visited = new HashSet<int>();

        queue.Enqueue((10, 0));
        visited.Add(10);

        while (queue.Count > 0)
        {
            var (current, steps) = queue.Dequeue();

            // If target reached
            if (current == N)
            {
                Console.WriteLine(steps);
                return;
            }

            // Possible operations
            int[] nextValues = {
                current + 2,
                current - 1,
                current * 3
            };

            foreach (int next in nextValues)
            {
                // Limit range to avoid infinite growth
                if (next >= 0 && next <= 3 * N && !visited.Contains(next))
                {
                    visited.Add(next);
                    queue.Enqueue((next, steps + 1));
                }
            }
        }
    }
}
