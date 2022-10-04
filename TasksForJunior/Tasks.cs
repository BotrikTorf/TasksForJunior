using System;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Console.Write("Введите число: ");
            string textNumber = Console.ReadLine();
            Console.WriteLine($"Вы набрали число {ReadNumber(textNumber)}");
        }

        private static int ReadNumber(string textNumber)
        {
            int numberTemp = 0;
            bool isRead = true;

            while (isRead)
            {
                if (int.TryParse(textNumber, out int number))
                {
                    numberTemp = number;
                    isRead = false;
                }
                else
                {
                    Console.WriteLine("Вы ввели не правельно число!");
                    Console.Write("Введите число: ");
                    textNumber = Console.ReadLine();
                }
            }

            return numberTemp;
        }
    }
}
