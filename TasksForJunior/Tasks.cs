using System;
using System.Collections.Generic;
using System.Linq;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            List<Player> players = CreateListPlayers();


        }

        private static List<Player> CreateListPlayers()
        {
            List<string> names = new List<string>
                { "Александр", "Борис", "Виктор", "Георгий", "Даниил", "Евгений", "Жак", "Иван" };
            List<Player> players = new List<Player>();
            Random random = new Random();
            int count = 100;
            int minLevel = 1;
            int maxLevel = 100;
            int minStrength = 0;
            int maxStrength = 10000;

            for (int i = 0; i < count; i++)
            {
                players.Add(new Player(
                    names[random.Next(0, names.Count)],
                    random.Next(minLevel, maxLevel + 1),
                    random.Next(minStrength, maxStrength + 1)));
            }

            return players;
        }

        static void Show(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                Console.Write($"{i + 1:000}. ");
            }

            Console.WriteLine();
        }
    }

    class Player
    {
        public Player(string name, int level, int strength)
        {
            Name = name;
            Level = level;
            Strength = strength;
        }

        public string Name { get; }

        public int Level { get; }

        public int Strength { get; }
    }
}
