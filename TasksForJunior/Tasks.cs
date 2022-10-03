using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            string number = null;

            Console.Write("Введите число:");

            while (!ConvertsNunber(Console.ReadLine(), ref number))
            {
                Console.WriteLine("Вы набрали вместо числа слово!");
                Console.Write("Введите число:");
            }

            Console.WriteLine($"Вы набрали число {number}");
        }

        static bool ConvertsNunber(string text, ref string number)
        {
            if (int.TryParse(text, out int tempNumber))
            {
                number = tempNumber.ToString();
                return true;
            }
            else
            {
                number = text;
                return false;
            }
        }
    }
}
