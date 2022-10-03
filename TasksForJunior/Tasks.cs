using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            bool isRight;

            Console.Write("Введите число:");
            isRight = int.TryParse(Console.ReadLine(), out int number);

            while (!isRight)
            {
                Console.WriteLine("Вы набрали вместа числа стова!");
                Console.Write("Введите число:");
                string text = Console.ReadLine();
                isRight = int.TryParse(text, out int numberRun);

                if (isRight)
                {
                    number = numberRun;
                }
            }

            Console.WriteLine($"Вы набрали число {number}");
        }
    }
}
