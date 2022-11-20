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
            commander.ShowOption();
            Sniper sniper = new Sniper();
            sniper.ShowOption();
            MachineGunner machineGunner = new MachineGunner();
            machineGunner.ShowOption();
            Gunner gunner = new Gunner();
            gunner.ShowOption();
            StormTrooper stormTrooper = new StormTrooper();
            stormTrooper.ShowOption();
            War war = null;

            Console.WriteLine("Добро пожаловать в программу МОДЕЛИРОВАНИЕ ВОЙНЫ.\n" +
                "Будут созданы два отряда с одинаковым количеством войнов.\n" +
                "В каждом отряде будет свой командир." +
                $"Размер отрядов: минимальный {minSquadSize}, а максимальный {maxSquadSize}.\n" +
                "В отряде могут быть:\n" +
                $"{commander.Name} - наносит урон в одну цель, здоровье среднее.\n" +
                $"{sniper.Name} - наносит большой урон в одну цель, но обладает малым здоровьем.\n" +
                $"{machineGunner.Name} - наносит урон до 5 целей, велика вероятность промахнуться, здоровье высокое.\n" +
                $"{gunner.Name} - средний урон в одну цель, но может промахнуться, здоровье среднее.\n" +
                $"{stormTrooper.Name} - наносит низкий урон цели и двум ближайшим противникам, здоровье высокое.");
            Console.Write("Введите количество бойцов в отряде: ");

            bool haveSquadSelected = true;

            while (haveSquadSelected)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= minSquadSize && result <= maxSquadSize)
                    {
                        war = new War(result);
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

            war.ShowSquads();
            //war.StartFight();
        }

    }

    class War
    {
        private List<Soldier> _squadFirst = new List<Soldier>();
        private List<Soldier> _squadSecond = new List<Soldier>();
        private Random _random = new Random();

        public War(int size)
        {
            _squadFirst = CreatSquad(size);
            _squadSecond = CreatSquad(size);
        }

        public void StartFight()
        {
            int firstSquadNumber = 1;
            int numberUnits = 2;
            bool isFight = true;

            Console.WriteLine("Сражение началось.");

            while (isFight)
            {
                if (_random.Next(0, numberUnits) == firstSquadNumber)
                {
                    if (_squadFirst[0] == new Commander())
                    {
                        for (int i = 0; i < _squadFirst.Count; i++)
                        {

                        }
                    }

                    for (int i = 0; i < _squadFirst.Count; i++)
                    {
                        //_squadFirst[i];
                    }
                }
            }

        }

        public void ShowSquads()
        {
            Console.WriteLine();
            Console.WriteLine("В первый отряд завербованы солдаты:");
            foreach (var soldier in _squadFirst)
            {
                soldier.ShowOption();
            }

            Console.WriteLine();
            Console.WriteLine("Во второй отряд завербованы солдаты:");
            foreach (var soldier in _squadSecond)
            {
                soldier.ShowOption();
            }
        }

        private List<Soldier> CreatSquad(int size)
        {
            List<Soldier> squad = new List<Soldier>();
            List<Soldier> allSoldiers = new List<Soldier>
            {
                { new Sniper() },
                { new MachineGunner() },
                { new Gunner() },
                { new StormTrooper() },
            };
            Commander commander = new Commander();
            squad.Add(commander);

            for (int i = 1; i < size; i++)
            {
                Soldier soldier;
                int index = _random.Next(0, allSoldiers.Count);
                soldier = allSoldiers[index];

                if (soldier == allSoldiers[0])
                    soldier = new Sniper();

                if (soldier == allSoldiers[1])
                    soldier = new MachineGunner();

                if (soldier == allSoldiers[2])
                    soldier = new Gunner();

                if (soldier == allSoldiers[3])
                    soldier = new StormTrooper();

                squad.Add(soldier);
            }

            return squad;
        }
    }

    class Soldier
    {
        private int _health;

        public Soldier(string name = null, int armor = 0, int health = 0, int damage = 0)
        {
            Name = name;
            Armor = armor;
            _health = health;
            Damage = damage;
        }

        public string Name { get; }

        public int Damage { get; }

        public int Armor { get; }

        public int Health
        {
            get
            {
                return _health;
            }
            private protected set
            {
                if (value < 0)
                    _health = 0;
                else
                    _health = value;
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= (damage - Armor);
        }

        public virtual void MakeDamage(List<Soldier> soldiers) { }

        public void ShowOption()
        {
            Console.WriteLine($"{Name}: здоровье - {Health}, броня - {Armor}, урон - {Damage}");
        }
    }

    class Sniper : Soldier
    {
        public Sniper() : base("Sniper", 50, 250, 170)
        {

        }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            Random random = new Random();
            soldiers[random.Next(0, soldiers.Count)].TakeDamage(Damage);
        }
    }

    class MachineGunner : Soldier
    {
        public MachineGunner() : base("MachineGunner", 50, 750, 100)
        {

        }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            Random random = new Random();
            soldiers[random.Next(0, soldiers.Count)].TakeDamage(Damage);
        }
    }

    class Gunner : Soldier
    {

        public Gunner() : base("Gunner", 50, 500, 100)
        {

        }



        public override void MakeDamage(List<Soldier> soldiers)
        {
            Random random = new Random();
            soldiers[random.Next(0, soldiers.Count)].TakeDamage(Damage);
        }
    }

    class StormTrooper : Soldier
    {
        public StormTrooper() : base("StormTrooper", 75, 750, 80)
        {

        }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            Random random = new Random();
            int enemyNumber = random.Next(0, soldiers.Count);

            if (soldiers.Count > 3)
            {
                soldiers[enemyNumber].TakeDamage(Damage);

                if (enemyNumber == 0)
                {
                    soldiers[soldiers.Count].TakeDamage(Damage);
                    soldiers[enemyNumber + 1].TakeDamage(Damage);
                }

                if (enemyNumber == soldiers.Count - 1)
                {
                    soldiers[0].TakeDamage(Damage);
                    soldiers[enemyNumber - 1].TakeDamage(Damage);
                }

                if (enemyNumber > 0 && enemyNumber < soldiers.Count - 1)
                {
                    soldiers[enemyNumber + 1].TakeDamage(Damage);
                    soldiers[enemyNumber - 1].TakeDamage(Damage);
                }
            }
        }
    }

    class Commander : Soldier
    {
        public Commander() : base("Commander", 50, 500, 100)
        {

        }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            Random random = new Random();
            soldiers[random.Next(0, soldiers.Count)].TakeDamage(Damage);
        }
    }

}
