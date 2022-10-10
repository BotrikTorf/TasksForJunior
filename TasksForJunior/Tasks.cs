using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandSum = "sum";
            const string CommandExit = "exit";
            bool isWork = true;
            List<int> array = new List<int>();

            Console.WriteLine($"Для добавления числа в массив введите число.\n" +
                $"Для вычисления ссумы всех элементов массива введите команду {CommandSum}.\n" +
                $"Для выхода из программы введите команду {CommandExit}.");

            while (isWork)
            {
                string inputString = Console.ReadLine().ToLower();

                if (int.TryParse(inputString, out int number))
                {
                    array.Add(number);
                }
                else
                {
                    if (inputString == CommandExit)
                    {
                        isWork = false;
                    }
                    else
                    {
                        if (inputString == CommandSum)
                        {
                            SumArray(array);
                        }
                        else
                        {
                            Console.WriteLine("Вы не правельно ввели команду");
                        }
                    }
                }
            }
        }

        static void SumArray(List<int> array)
        {
            int sum = 0;

            foreach (var number in array)
            {
                sum += number;
            }

            Console.WriteLine($"Сумма всех элементов массива равна {sum}");
        }
    }
}
