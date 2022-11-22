using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            int minSquadSize = 4;
            int maxSquadSize = 20;

            Commander commander = new Commander();
            Sniper sniper = new Sniper();
            MachineGunner machineGunner = new MachineGunner();
            Gunner gunner = new Gunner();
            StormTrooper stormTrooper = new StormTrooper();
            War war = null;

            Console.WriteLine("Добро пожаловать в программу МОДЕЛИРОВАНИЕ ВОЙНЫ.\n" +
                "Будут созданы два отряда с одинаковым количеством войнов.\n" +
                "В каждом отряде будет свой командир." +
                $"Размер отрядов: минимальный {minSquadSize}, а максимальный {maxSquadSize}.\n" +
                "В отряде могут быть:\n" +
                $"{commander.Name} - наносит урон в одну цель, здоровье среднее.\n" +
                $"{sniper.Name} - наносит большой урон в одну цель, но обладает малым здоровьем.\n" +
                $"{machineGunner.Name} - наносит урон всему отряду, велика вероятность промахнуться, здоровье высокое.\n" +
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

            war.ShowStartingUnits();
            war.StartFight();
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
                    Console.WriteLine("Бой начинает первый отряд:");
                    CalculateBattle(ref _squadFirst, ref _squadSecond);
                }
                else
                {
                    Console.WriteLine("Бой начинает второй отряд:");
                    CalculateBattle(ref _squadSecond, ref _squadFirst);
                }

                _squadFirst = RemovesDeadSoldiers(_squadFirst);
                _squadSecond = RemovesDeadSoldiers(_squadSecond);

                if (_squadFirst.Count == 0 || _squadSecond.Count == 0)
                {
                    isFight = false;
                }
            }

            ShowWinningSquad();
        }

        private void ShowWinningSquad()
        {
            if (_squadFirst.Count == 0 && _squadSecond.Count == 0)
            {
                Console.WriteLine("Ничья!");
            }
            else if (_squadFirst.Count == 0)
            {
                Console.WriteLine("Выиграл второй отряд!");
                Console.WriteLine("В отряде остались бойцы:");
                ShowSquad(_squadSecond);
            }
            else
            {
                Console.WriteLine("Выиграл первый отряд!");
                Console.WriteLine("В отряде остались бойцы:");
                ShowSquad(_squadFirst);
            }
        }

        private void CalculateBattle(ref List<Soldier> firstSquad, ref List<Soldier> secondSquad)
        {
            int firstDivisionIndex = 0;
            int secondDivisionIndex = 0;

            while (firstDivisionIndex < firstSquad.Count && secondDivisionIndex < secondSquad.Count)
            {
                if (firstDivisionIndex < firstSquad.Count)
                {
                    firstSquad[firstDivisionIndex].MakeDamage(secondSquad);
                }

                if (secondDivisionIndex < secondSquad.Count)
                {
                    secondSquad[secondDivisionIndex].MakeDamage(firstSquad);
                }

                firstDivisionIndex++;
                secondDivisionIndex++;
            }
        }

        public void ShowStartingUnits()
        {
            Console.WriteLine();
            Console.WriteLine("В первый отряд завербованы солдаты:");
            ShowSquad(_squadFirst);
            Console.WriteLine();
            Console.WriteLine("Во второй отряд завербованы солдаты:");
            ShowSquad(_squadSecond);
        }

        private List<Soldier> RemovesDeadSoldiers(List<Soldier> listSolders)
        {
            List<Soldier> tempListSolders = new List<Soldier>();

            for (int i = 0; i < listSolders.Count; i++)
            {
                if (listSolders[i].Health > 0)
                {
                    tempListSolders.Add(listSolders[i]);
                }

                if (listSolders[i].Health == 0)
                {
                    Console.WriteLine($"{listSolders[i].Name} - погиб");
                }
            }

            return tempListSolders;
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

        private void ShowSquad(List<Soldier> soldiers)
        {
            foreach (var soldier in soldiers)
            {
                soldier.ShowOption();
            }
        }
    }

    class Soldier
    {
        private int _health;
        private Random _random = new Random();

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
            private set
            {
                if (value < 0)
                    _health = 0;
                else
                    _health = value;
            }
        }

        public virtual void MakeDamage(List<Soldier> soldiers)
        {
            Console.WriteLine($"Солдат {Name} наносит урон");
        }

        public void TakeDamage(int damage)
        {
            Console.WriteLine($"Солдат {Name} получил урон {damage - Armor}");
            Health -= (damage - Armor);
        }

        public bool CanHitBullet(int hitChance)
        {
            int minChance = 1;
            int maxChance = 101;

            return hitChance <= _random.Next(minChance, maxChance);
        }

        public void ShowOption()
        {
            Console.WriteLine($"{Name}: здоровье - {Health}, броня - {Armor}, урон - {Damage}");
        }
    }

    class Sniper : Soldier
    {
        public Sniper() : base("Sniper", 50, 250, 170) { }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            if (Health > 0)
            {
                Random random = new Random();

                base.MakeDamage(soldiers);
                soldiers[random.Next(0, soldiers.Count)].TakeDamage(Damage);
            }
        }
    }

    class MachineGunner : Soldier
    {
        public MachineGunner() : base("MachineGunner", 50, 750, 100)
        {
            HitChance = 25;
        }

        int HitChance { get; }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            if (Health > 0)
            {
                base.MakeDamage(soldiers);

                for (int i = 0; i < soldiers.Count; i++)
                {
                    if (CanHitBullet(HitChance))
                    {
                        soldiers[i].TakeDamage(Damage);
                    }
                }
            }
        }
    }

    class Gunner : Soldier
    {
        public Gunner() : base("Gunner", 50, 500, 100)
        {
            HitChance = 50;
        }

        int HitChance { get; }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            if (Health > 0)
            {
                Random random = new Random();

                base.MakeDamage(soldiers);

                if (CanHitBullet(HitChance))
                {
                    soldiers[random.Next(0, soldiers.Count)].TakeDamage(Damage);
                }
            }
        }
    }

    class StormTrooper : Soldier
    {
        public StormTrooper() : base("StormTrooper", 75, 750, 80) { }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            if (Health > 0)
            {
                Random random = new Random();

                base.MakeDamage(soldiers);
                int enemyNumber = random.Next(0, soldiers.Count);

                if (soldiers.Count > 3)
                {
                    soldiers[enemyNumber].TakeDamage(Damage);

                    if (enemyNumber == 0)
                    {
                        soldiers[soldiers.Count - 1].TakeDamage(Damage);
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
                else
                {
                    for (int i = 0; i < soldiers.Count; i++)
                    {
                        soldiers[i].TakeDamage(Damage);
                    }
                }
            }
        }
    }

    class Commander : Soldier
    {
        public Commander() : base("Commander", 50, 500, 100) { }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            if (Health > 0)
            {
                Random random = new Random();

                base.MakeDamage(soldiers);
                soldiers[random.Next(0, soldiers.Count)].TakeDamage(Damage);
            }
        }
    }
}
