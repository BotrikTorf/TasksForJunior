using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            /*// First Task Variables
            const ushort MinLife = 0;

            bool bulletHit = false;
            byte amountOfLife = byte.MaxValue;
            sbyte maxMana = sbyte.MaxValue;
            short minMana = 0;
            int manaAvailable = 100;
            uint thingsInBackpack;
            float cardLength;
            char potionIndicator = 'H';
            string unitNames = "Drunk Tree";*/

            Console.WriteLine("Добрый день!");
            Console.Write("Как к вам можно обращатся (укажите свае имя): ");
            string userName = Console.ReadLine();
            Console.WriteLine("Очень приятно! Меня завут БОТ.");
            Console.WriteLine("Для составления анкеты мне нужны еще некоторые данные о вас:");
            Console.Write("Укажите ваш возраст(указать сколько вам полных лет): ");
            sbyte userAge = sbyte.Parse(Console.ReadLine());
            Console.Write("Укажите последнее место работы: ");
            string userPlaceOfWork = Console.ReadLine();
            Console.Write("Укажите ваш пол (мужчина или женщина): ");
            string userGender = Console.ReadLine();
            Console.WriteLine("Давайте проверим ваши данные:");
            Console.WriteLine($"Вас зовут {userName}, Ваш возраст {userAge} лет," +
                $" последнее место работы {userPlaceOfWork} и ваш пол {userGender}");
            Console.ReadLine();
            //но мне больше нравиться так
            Console.WriteLine("Вас зовут {0}, Ваш возраст {1} лет, " +
                "последнее место работы {2} и ваш пол {3}", 
                userName, userAge, userPlaceOfWork, userGender);
            Console.ReadLine();
        }
    }
}
