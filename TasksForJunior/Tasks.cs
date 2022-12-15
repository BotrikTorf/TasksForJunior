using System;
using System.Collections.Generic;
using System.Linq;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            List<Criminal> criminals = CreateListCriminals();

            Show(criminals);

            var criminalAfterAmnesty = from criminal in criminals
                where criminal.Crime != "Антиправительственное"
                select criminal;

            criminals = criminalAfterAmnesty.ToList();
            Show(criminals);
        }

        static List<Criminal> CreateListCriminals()
        {
            List<string> surnames = new List<string>
                { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов" };
            List<string> names = new List<string> 
                { "Александр", "Алексей", "Андрей", "Артем", "Виктор", "Даниил", "Дмитрий", "Егор" };
            List<string> middleNames = new List<string>
            { "Александрович", "Алексеевич", "Андреевич", "Анатольевич", "Викторович", "Данилович", "Дмитриевич",
                "Егорович" };
            List<string> crimes = new List<string>
                { "Кража", "Мошенничество", "Наркотики", "Убийство", "Налоги", "Антиправительственное" };
            List<Criminal> criminals = new List<Criminal>();
            Random random = new Random();
            int count = 50;

            for (int i = 0; i < count; i++)
            {
                criminals.Add(new Criminal(
                    surnames[random.Next(0, surnames.Count)], 
                    names[random.Next(0, names.Count)],
                    middleNames[random.Next(0, middleNames.Count)], 
                    crimes[random.Next(0,crimes.Count)]));
            }

            return criminals;
        }

        static void Show(List<Criminal> criminals)
        {
            int number = 0;

            foreach (var criminal in criminals)
            {
                Console.Write($"{number}. ");
                criminal.Show();
                number++;
            }

            Console.WriteLine();
        }

        class Criminal
        {
            public Criminal(string surname, string name, string middleName, string crime)
            {
                Surname = surname;
                Name = name;
                MiddleName = middleName;
                Crime = crime;
            }

            public string Surname { get; }

            public string Name { get; }

            public string MiddleName { get; }

            public string Crime { get; }

            public void Show()
            {
                Console.WriteLine($"{Surname} {Name} {MiddleName} - {Crime}");
            }
        }
    }
}
