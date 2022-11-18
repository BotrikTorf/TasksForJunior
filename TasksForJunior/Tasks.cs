using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            int minSquadSize = 2;
            int maxSquadSize = 100;

            Commander commander = new Commander();
            Sniper sniper = new Sniper();
            MachineGunner machineGunner = new MachineGunner();
            Gunner gunner = new Gunner();
            StormTrooper stormTrooper = new StormTrooper();

            Detachment detachmentOne = null;
            Detachment detachmentTwo = null;

            Console.WriteLine("Добро пожаловать в программу МОДЕЛИРОВАНИЕ ВОЙНЫ.\n" +
                "Будут созданы два отряда с одинаковым количествой войнов.\n" +
                "В каждом отряде будет свой командир." +
                $"Размер отрядов: минимальный {minSquadSize}, а максимальный {maxSquadSize}.\n" +
                "В отряде могут быть:\n" +
                $"{commander.Name} - пока живой дает баф к урону и наносит урон в одну цель, здоровье среднее.\n" +
                $"{sniper.Name} - наносит большой урон в одну цель, но обладает малым здоровьем.\n" +
                $"{machineGunner.Name} - наносит урон до 5 целей, но может промахнуться, здоровье высокое.\n" +
                $"{gunner.Name} - средний урон в одну цель, здоровье среднее.\n" +
                $"{stormTrooper.Name} - наносит низкий урон цели и двум ближайшим противникам, здоровье высокое.");
            Console.Write("Введите количество бойцов в отряде: ");

            bool haveSquadSelected = true;

            while (haveSquadSelected)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= minSquadSize && result <= maxSquadSize)
                    {
                        detachmentOne = new Detachment(result);
                        detachmentTwo = new Detachment(result);
                        haveSquadSelected = false;
                    }
                    else
                    {
                        Console.Write("Вы не правильно ввели число. Повторите ввод: ");
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели число. Повторите ввод: ");
                }
            }

            Console.WriteLine("Первый отряд:");
            detachmentOne.ShowSoldiers();
            Console.WriteLine();
            Console.WriteLine("второй отряд:");
            detachmentTwo.ShowSoldiers();
        }

    }

    class Detachment
    {
        List<Soldier> _soldiers = new List<Soldier>();

        public Detachment(int size = 0)
        {
            _soldiers.Add(new Commander());
            Create(size);
        }

        private void Create(int size)
        {
            Random random = new Random();
            List<Soldier> allSoldiers = new List<Soldier>
            {
                { new Sniper() },
                { new MachineGunner() },
                { new Gunner() },
                { new StormTrooper() },
            };

            for (int i = 1; i < size; i++)
            {
                Soldier soldier = new Soldier();
                int index = random.Next(0, allSoldiers.Count);
                soldier = allSoldiers[index];

                if (soldier == allSoldiers[0])
                    soldier = new Sniper();

                if (soldier == allSoldiers[1])
                    soldier = new MachineGunner();

                if (soldier == allSoldiers[2])
                    soldier = new Gunner();

                if (soldier == allSoldiers[3])
                    soldier = new StormTrooper();

                _soldiers.Add(soldier);
            }
        }

        public void ShowSoldiers()
        {
            Console.WriteLine("В отряд завербовоны солдаты:");
            foreach (var soldier in _soldiers)
            {
                Console.WriteLine(soldier.Name);
            }
        }
    }

    class Soldier
    {
        public Soldier(string name = null)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }

    class Sniper : Soldier
    {
        public Sniper() : base("Sniper")
        {

        }
    }

    class MachineGunner : Soldier
    {
        public MachineGunner() : base("MachineGunner")
        {

        }
    }

    class Gunner : Soldier
    {

        public Gunner() : base("Gunner")
        {

        }
    }

    class StormTrooper : Soldier
    {
        public StormTrooper() : base("StormTrooper")
        {

        }
    }

    class Commander : Soldier
    {
        public Commander() : base("Commander")
        {

        }
    }

}
