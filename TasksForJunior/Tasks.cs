﻿using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string Health = "жизни";
            const string Mana = "манны";
            int lengthHealthBar = 10;
            ConsoleColor colorHealth = ConsoleColor.Red;
            int xMaxHealthBar = 10;
            int xPositionHealth = 10;
            int yPositionHealth = 10;
            int lengthFillingHealth = 50;
            int lengthManaBar = 10;
            ConsoleColor colorMana = ConsoleColor.Blue;
            int xMaxManaBar = 10;
            int xPositionMana = 11;
            int yPositionMana = 10;
            int lengthFillingMana = 50;

            Console.SetBufferSize(150, 30);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.WindowHeight = 29;
            Console.WindowWidth = 150;

            SetOptionsBar(ref lengthHealthBar, ref xMaxHealthBar, ref lengthFillingHealth, ref xPositionHealth,
            ref yPositionHealth, Health);

            SetOptionsBar(ref lengthManaBar, ref xMaxManaBar, ref lengthFillingMana, ref xPositionMana,
            ref yPositionMana, Mana);

            ShowBar(xPositionHealth, yPositionHealth, lengthHealthBar, lengthFillingHealth, colorHealth);

            ShowBar(xPositionMana, yPositionMana, lengthManaBar, lengthFillingMana, colorMana);
        }

        static void ShowBar(int xPosition, int yPosition, int length, int filling, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(xPosition, yPosition);
            int lengthFilled = (int)Math.Round(length*(filling/100.0));

            for (int i = 0; i < lengthFilled; i++)
                Console.Write("#");

            Console.BackgroundColor = ConsoleColor.Gray;

            for (int i = 0; i < length - lengthFilled; i++)
                Console.Write("_");

            Console.WriteLine();
        }

        static void SetOptionsBar(ref int lengthBar, ref int xMaxBar, ref int lengthFilling, ref int xPosition, 
            ref int yPosition, string name)
        {
            Console.Write($"Введите длину полоски {name} (она должна быть не больше 150 знаков): ");
            lengthBar = Convert.ToInt32(Console.ReadLine());
            xMaxBar = Console.WindowWidth - lengthBar;
            Console.Write($"Введите на сколько процентов (от 0 до 100) она будет полная (отобразится " +
                $"полная часть '#' пустая часть '_')");
            lengthFilling = Convert.ToInt32(Console.ReadLine());
            Console.Write($"Координаты начального положения зависит от длины полоски {name}.\n" +
                $"Вы можете расположить начало полоски {name} от 0 до {xMaxBar} по горизонтали, " +
                $"введите значение: ");
            xPosition = SetStartPosition(xMaxBar);

            Console.Write($"Введите значение по вертикали (не может быть больше высоты окна {Console.WindowHeight}): ");
            yPosition = SetStartPosition(Console.WindowHeight);
        }

        private static int SetStartPosition(int maxBar)
        {
            bool isRepeatInput = true;
            int position = 0;

            while (isRepeatInput)
            {
                position = Convert.ToInt32(Console.ReadLine());

                if (position > 0 && position < maxBar)
                    isRepeatInput = false;
                else
                    Console.Write($"Вы неправельно ввели значение. Повторите еще раз: ");
            }

            return position;
        }
    }
}
