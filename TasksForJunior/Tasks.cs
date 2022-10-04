using System;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            ReadNumber();
        }

        private static int ReadNumber()
        {
            int numberTemp = 0;
            bool isRead = true;

            while (isRead)
            {
                Console.Write("Введите число: ");
                string textNumber = Console.ReadLine();

                if (int.TryParse(textNumber, out int number))
                {
                    numberTemp = number;
                    isRead = false;
                }
            }

            return numberTemp;
        }
    }
}
