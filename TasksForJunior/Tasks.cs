using System;
using System.Collections.Generic;
using System.Linq;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            List<Soldier> soldiersFirst = CreateSolders();
            List<Soldier> soldiersSecond = CreateSolders();

            Show(soldiersFirst.OrderBy(soldier => soldier.Surname).ToList());
            Show(soldiersSecond.OrderBy(soldier => soldier.Surname).ToList());

            var temp = soldiersFirst.Where(soldier => soldier.Surname.StartsWith("Б")).
                OrderBy(soldier => soldier.Surname);
            soldiersFirst= soldiersFirst.Where(solder => solder.Surname[0] != 'Б').ToList();
            soldiersSecond = soldiersSecond.Union(temp).ToList();
            Show(soldiersFirst.OrderBy(soldier => soldier.Surname).ToList());
            Show(soldiersSecond.OrderBy(soldier => soldier.Surname).ToList());
        }

        private static List<Soldier> CreateSolders()
        {
            List<Soldier> soldiers = new List<Soldier>();
            Random random = new Random();
            List<string> surnames = new List<string>
                { "Аксаков", "Бажов", "Гоголь", "Есенин", "Жуковский", "Лермонтов", "Маршак", "Некрасов" };
            List<string> names = new List<string>
                { "Александр", "Борис", "Виктор", "Георгий", "Даниил", "Евгений", "Жак", "Иван" };
            List<string> middleNames = new List<string>
            { "Александрович", "Борисович", "Викторович", "Георгиевич", "Данилович", "Евгениевич",
                "Иванович" };
            List<string> armaments = new List<string>
                { "СПС Гюрза", "Пулемет Печенег", "ВСС Винторез", "АН-94 Абакан", "Автомат АК-74М",
                    "Пулемет НСВ", "Винтовка СВД", "Пулемет РПК" };
            List<string> rank = new List<string>
                { "Рядовой", "Ефрейтор", "Младший сержант", "Сержант", "Старший сержант", "Старшина" };
            int minMilitaryService = 0;
            int maxMilitaryService = 18;
            int count = 50;

            for (int i = 0; i < count; i++)
            {
                soldiers.Add(new Soldier(
                    surnames[random.Next(0, surnames.Count)],
                    names[random.Next(0, names.Count)],
                    middleNames[random.Next(0, middleNames.Count)],
                    armaments[random.Next(0, armaments.Count)],
                    rank[random.Next(0, rank.Count)],
                    random.Next(minMilitaryService, maxMilitaryService)));
            }

            return soldiers;
        }

        private static void Show(List<Soldier> soldiers)
        {
            for (int i = 0; i < soldiers.Count; i++)
            {
                Console.WriteLine($"{i:00}. {soldiers[i].Surname} {soldiers[i].Name} {soldiers[i].MiddleName} - {soldiers[i].Rank}");
            }

            Console.WriteLine();
        }
    }

    class Soldier
    {
        public Soldier(string surname, string name, string middleName, string armament, string rank, int militaryService)
        {
            Surname = surname;
            Name = name;
            MiddleName = middleName;
            Armament = armament;
            Rank = rank;
            MilitaryService = militaryService;
        }


        public string Surname { get; }

        public string Name { get; }

        public string MiddleName { get; }

        public string Armament { get; }

        public string Rank { get; }

        public int MilitaryService { get; }
    }
}