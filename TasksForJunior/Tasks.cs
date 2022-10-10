using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            int storeAccount = 0;

            Queue<int> shoppingQueue = CreatesQueue();
            int totalClients = shoppingQueue.Count;

            while (shoppingQueue.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"Клиент {totalClients - shoppingQueue.Count + 1} потратил {shoppingQueue.Peek()} руб.");
                storeAccount += shoppingQueue.Dequeue();
                Console.WriteLine($"Счет магазина: {storeAccount}");
                Console.WriteLine("Для продолжения нажмите любую клавишу.");
                Console.ReadKey();
            }
        }

        static Queue<int> CreatesQueue()
        {
            Random random = new Random();
            int queueLength = random.Next(5, 51);
            Queue<int> shoppingQueue = new Queue<int>();

            for (int i = 0; i < queueLength; i++)
            {
                shoppingQueue.Enqueue(random.Next(1, 101));
            }

            return shoppingQueue;
        }
    }
}
