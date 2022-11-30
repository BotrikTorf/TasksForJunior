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

            List<Soldier> soldiers = new List<Soldier>
            {
                new Commander(),
                new Sniper(),
                new MachineGunner(),
                new Gunner(),
                new StormTrooper()
            };
            War war = new War();
            Squad squadFirst = new Squad("1");
            Squad squadSecond = new Squad("2");

            Console.WriteLine("Добро пожаловать в программу МОДЕЛИРОВАНИЕ ВОЙНЫ.\n" +
                "Будут созданы два отряда с одинаковым количеством войнов.\n" +
                "В каждом отряде будет свой командир." +
                $"Размер отрядов: минимальный {minSquadSize}, а максимальный {maxSquadSize}.\n" +
                "В отряде могут быть:");

            foreach (var soldier in soldiers)
            {
                soldier.ShowSoldierDescription();
            }

            Console.Write("Введите количество бойцов в отряде: ");

            bool haveSquadSelected = true;

            while (haveSquadSelected)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= minSquadSize && result <= maxSquadSize)
                    {
                        squadFirst.CreatSquad(result);
                        squadSecond.CreatSquad(result);
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

            squadFirst.ShowSolders();
            squadSecond.ShowSolders();
            Console.ReadKey();
            war.StartFight(squadFirst, squadSecond);
        }
    }

    class War
    {
        private Random _random = new Random();
        private Squad _squadFirst;
        private Squad _squadSecond;

        public void StartFight(Squad squadFirst, Squad squadSecond)
        {
            _squadFirst = squadFirst;
            _squadSecond = squadSecond;

            int firstSquadNumber = 1;
            int numberUnits = 2;
            bool isFight = true;

            Console.WriteLine("Сражение началось.");

            while (isFight)
            {
                if (_random.Next(0, numberUnits) == firstSquadNumber)
                {
                    Console.WriteLine("Бой начинает первый отряд:");
                    CalculateBattle(_squadFirst, _squadSecond);
                }
                else
                {
                    Console.WriteLine("Бой начинает второй отряд:");
                    CalculateBattle(_squadSecond, _squadFirst);
                }

                _squadFirst.RemovesDeadSoldiers();
                _squadSecond.RemovesDeadSoldiers();

                if (_squadFirst.NumberSoldiers == 0 || _squadSecond.NumberSoldiers == 0)
                {
                    isFight = false;
                }
            }

            ShowWinningSquad();
        }

        private void ShowWinningSquad()
        {
            if (_squadFirst.NumberSoldiers == 0 && _squadSecond.NumberSoldiers == 0)
            {
                Console.WriteLine("Ничья!");
            }
            else if (_squadFirst.NumberSoldiers == 0)
            {
                Console.WriteLine("Выиграл второй отряд!");
                _squadSecond.ShowSolders();
            }
            else
            {
                Console.WriteLine("Выиграл первый отряд!");
                _squadFirst.ShowSolders();
            }
        }

        private void CalculateBattle(Squad firstSquad, Squad secondSquad)
        {
            int firstDivisionIndex = 0;
            int secondDivisionIndex = 0;

            while (firstDivisionIndex < firstSquad.NumberSoldiers && secondDivisionIndex < secondSquad.NumberSoldiers)
            {
                if (firstDivisionIndex < firstSquad.NumberSoldiers)
                {
                    firstSquad.JoinFight(secondSquad.Soldiers, firstDivisionIndex);
                }

                if (secondDivisionIndex < secondSquad.NumberSoldiers)
                {
                    secondSquad.JoinFight(firstSquad.Soldiers, secondDivisionIndex);
                }

                firstDivisionIndex++;
                secondDivisionIndex++;
            }
        }
    }

    class Squad
    {
        private List<Soldier> _soldiers;
        private string _number;
        public Squad(string number)
        {
            _number = number;
            _soldiers = new List<Soldier> { new Commander() };
        }

        public int NumberSoldiers { get { return _soldiers.Count; } }

        public List<Soldier> Soldiers { get { return _soldiers; } }

        public void CreatSquad(int size)
        {
            Random random = new Random();

            for (int i = 1; i < size; i++)
            {
                List<Soldier> allSoldiers = new List<Soldier>
                {
                    { new Sniper() },
                    { new MachineGunner() },
                    { new Gunner() },
                    { new StormTrooper() },
                };

                int index = random.Next(0, allSoldiers.Count);
                _soldiers.Add(allSoldiers[index]);
            }
        }

        public void JoinFight(List<Soldier> soldiers, int index)
        {
            Console.Write($"{_number}. ");
            _soldiers[index].MakeDamage(soldiers);
        }

        public void RemovesDeadSoldiers()
        {
            int index = 0;

            while (index < _soldiers.Count)
            {
                if (_soldiers[index].Health == 0)
                {
                    Console.WriteLine($"Солдат {_number} отряда: {_soldiers[index].Name} - погиб");
                    _soldiers.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }

        public void ShowSolders()
        {
            Console.WriteLine($"В отряде номер {_number} служат солдаты:");
            ShowSquad();
            Console.WriteLine();
        }

        private void ShowSquad()
        {
            foreach (var soldier in _soldiers)
            {
                Console.Write(_number + ". ");
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

        public virtual void ShowSoldierDescription() { }

        public void TakeDamage(int damage)
        {
            Health -= (damage - Armor);
            Console.WriteLine($"Солдат {Name} получил урон {damage - Armor}. У него осталось здоровья : {Health}");
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

        public override void ShowSoldierDescription()
        {
            Console.WriteLine($"{Name} - наносит большой урон в одну цель, но обладает малым здоровьем.");
        }
    }

    class MachineGunner : Soldier
    {
        public MachineGunner() : base("MachineGunner", 50, 750, 100)
        {
            HitChance = 25;
        }

        public int HitChance { get; }

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

        public override void ShowSoldierDescription()
        {
            Console.WriteLine($"{Name} - наносит урон всему отряду, велика вероятность промахнуться, здоровье высокое.");
        }
    }

    class Gunner : Soldier
    {
        public Gunner() : base("Gunner", 50, 500, 100)
        {
            HitChance = 50;
        }

        public int HitChance { get; }

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

        public override void ShowSoldierDescription()
        {
            Console.WriteLine($"{Name} - средний урон в одну цель, но может промахнуться, здоровье среднее.");
        }
    }

    class StormTrooper : Soldier
    {
        public StormTrooper() : base("StormTrooper", 75, 750, 80) { }

        public override void MakeDamage(List<Soldier> soldiers)
        {
            int numberSoldiersTakingDamage = 3;
            Random random = new Random();

            if (Health > 0)
            {
                int[] index = new int[numberSoldiersTakingDamage];
                int enemyNumber = random.Next(0, soldiers.Count);

                base.MakeDamage(soldiers);

                if (soldiers.Count <= numberSoldiersTakingDamage)
                {
                    index = new int[soldiers.Count];

                    for (int i = 0; i < soldiers.Count; i++)
                    {
                        index[i] = i;
                    }
                }
                else if (enemyNumber > 0 && enemyNumber < soldiers.Count - 1)
                {
                    index[0] = enemyNumber;
                    index[1] = enemyNumber - 1;
                    index[2] = enemyNumber + 1;
                }
                else if (enemyNumber == 0)
                {
                    index[0] = enemyNumber;
                    index[1] = soldiers.Count - 1;
                    index[2] = enemyNumber + 1;
                }
                else
                {
                    index[0] = enemyNumber;
                    index[1] = enemyNumber - 1;
                    index[2] = 0;
                }

                for (int i = 0; i < index.Length; i++)
                {
                    soldiers[index[i]].TakeDamage(Damage);
                }
            }
        }

        public override void ShowSoldierDescription()
        {
            Console.WriteLine($"{Name} - наносит низкий урон цели и двум ближайшим противникам, здоровье высокое.");
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

        public override void ShowSoldierDescription()
        {
            Console.WriteLine($"{Name} - наносит урон в одну цель, здоровье среднее.");
        }
    }
}
