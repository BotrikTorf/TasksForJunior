using System;
using System.Collections.Generic;
using System.Linq;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            List<Criminal> criminals = new List<Criminal>
            {
                new Criminal
                    ("Хоакин","Гусман","Лоэра",true, 160, 80, "Мексиканец"),
                new Criminal
                    ("Петров","Максим","Владимирович",true, 172, 80, "Русский"),
                new Criminal
                    ("Пичу́шкин","Александр","Ю́рьевич",true, 180, 78, "Русский"),
                new Criminal
                    ("Никулин","Евгений","Сергеевич",true, 165, 81, "Русский"),
                new Criminal
                    ("Поулсен","Кевин","", false, 168, 83, "Американец"),
                new Criminal
                    ("Калс","Майкл","",false, 173, 75, "Американец"),
                new Criminal
                    ("Иванов","Иван","Иванович",false, 181, 80, "Украинец"),
                new Criminal
                    ("Бульбашов","Бульбаш","Бульбашович",false, 160, 80, "Белорус"),
                new Criminal
                    ("Андрей","Сорокин","Владимирович",false, 178, 90, "Белорус"),
            };

            Console.Write("Какой рост преступника: ");
            int growth = int.Parse(Console.ReadLine());
            Console.Write("Какой вес преступника: ");
            int weight = int.Parse(Console.ReadLine());
            Console.WriteLine("Национальность преступника: ");
            string nationality = Console.ReadLine();

            var showCriminals = from criminal in criminals
                where criminal.Growth == growth && criminal.Weight == weight && 
                      criminal.Nationality == nationality && criminal.HaveImprisoned == false
                select criminal;

            foreach (var criminal in showCriminals)
            {
                criminal.Show();
            }
        }
    }

    class Criminal
    {
        public Criminal
            (string surname, string name, string middleName, bool haveImprisoned, 
                int growth, int weight, string nationality)
        {
            Surname = surname;
            Name = name;
            MiddleName = middleName;
            HaveImprisoned = haveImprisoned;
            Growth = growth;
            Weight = weight;
            Nationality = nationality;
        }

        public string Surname { get; }

        public string Name { get; }

        public string MiddleName { get; }

        public bool HaveImprisoned { get; }

        public int Growth { get; }

        public int Weight { get; }

        public string Nationality { get; }

        public void Show()
        {
            Console.WriteLine($"{Surname} {Name} {MiddleName}");
        }
    }
}
