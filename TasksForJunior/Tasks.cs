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
            Squad squadFirst = new Squad();
            Squad squadSecond = new Squad();

            Console.WriteLine("Добро пожаловать в программу МОДЕЛИРОВАНИЕ ВОЙНЫ.\n" +
                "Будут созданы два отряда с одинаковым количеством войнов.\n" +
                "В каждом отряде будет свой командир." +
                $"Размер отрядов: минимальный {minSquadSize}, а максимальный {maxSquadSize}.\n" +
                "В отряде могут быть:");

            foreach (var soldier in soldiers)
            {
                soldier.ShowDescription();
            }

            Console.Write("Введите количество бойцов в отряде: ");

            bool haveSquadSelected = true;

            while (haveSquadSelected)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result >= minSquadSize && result <= maxSquadSize)
                    {
                        squadFirst = new Squad("1", result);
                        squadSecond = new Squad("2", result);
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
                    Soldier soldier = secondSquad.GetSoldier(secondDivisionIndex);

                    firstSquad.TakeDamege(soldier.MakeDamage(firstSquad.NumberSoldiers), soldier.Damage);
                }

                if (secondDivisionIndex < secondSquad.NumberSoldiers)
                {
                    Soldier soldier = firstSquad.GetSoldier(firstDivisionIndex);

                    secondSquad.TakeDamege(soldier.MakeDamage(secondSquad.NumberSoldiers), soldier.Damage);
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

        public Squad() { }

        public Squad(string number, int size)
        {
            _number = number;
            _soldiers = new List<Soldier> { new Commander() };
            Creat(size);
        }

        public int NumberSoldiers => _soldiers.Count;

        public void TakeDamege(List<int> index, int damage)
        {
            if (index.Count == 0)
            {
                Console.WriteLine("Солдат промахнулся.");
            }
            else
            {
                for (int i = 0; i < index.Count; i++)
                {
                    _soldiers[index[i]].TakeDamage(damage);
                }
            }
        }

        public Soldier GetSoldier(int index)
        {
            return _soldiers[index];
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
            Show();
            Console.WriteLine();
        }

        private void Creat(int size)
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

        private void Show()
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

        public virtual List<int> MakeDamage(int numberSolder)
        {
            List<int> index = new List<int>();
            return index;
        }

        public int SelectTargetDamage(int squadSize) => _random.Next(0, squadSize);

        public virtual void ShowDescription() { }

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

        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - наносит большой урон в одну цель, но обладает малым здоровьем.");
        }

        public override List<int> MakeDamage(int numberSolder)
        {
            List<int> index = new List<int>();

            if (Health > 0)
            {
                Random random = new Random();

                Console.WriteLine($"Солдат {Name} наносит урон");
                index.Add(random.Next(0, numberSolder));
            }

            return index;
        }
    }

    class MachineGunner : Soldier
    {
        public MachineGunner() : base("MachineGunner", 50, 750, 100)
        {
            HitChance = 25;
        }

        public int HitChance { get; }

        public override List<int> MakeDamage(int numberSolder)
        {
            List<int> index = new List<int>();

            if (Health > 0)
            {
                Console.WriteLine($"Солдат {Name} наносит урон");

                for (int i = 0; i < numberSolder; i++)
                {
                    if (CanHitBullet(HitChance))
                    {
                        index.Add(i);
                    }
                }
            }

            return index;
        }

        public override void ShowDescription()
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

        public override List<int> MakeDamage(int numberSolder)
        {
            List<int> index = new List<int>();

            if (Health > 0)
            {
                Random random = new Random();

                Console.WriteLine($"Солдат {Name} наносит урон");

                if (CanHitBullet(HitChance))
                {
                    index.Add(random.Next(0, numberSolder));
                }
            }

            return index;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - средний урон в одну цель, но может промахнуться, здоровье среднее.");
        }
    }

    class StormTrooper : Soldier
    {
        public StormTrooper() : base("StormTrooper", 75, 750, 80) { }

        public override List<int> MakeDamage(int numberSolder)
        {
            List<int> index = new List<int>();
            int numberSoldiersTakingDamage = 3;
            Random random = new Random();

            if (Health > 0)
            {
                int enemyNumber = random.Next(0, numberSolder);

                Console.WriteLine($"Солдат {Name} наносит урон");

                if (numberSolder <= numberSoldiersTakingDamage)
                {
                    for (int i = 0; i < numberSolder; i++)
                    {
                        index.Add(i);
                    }
                }
                else if (enemyNumber > 0 && enemyNumber < numberSolder - 1)
                {
                    index.Add(enemyNumber);
                    index.Add(enemyNumber - 1);
                    index.Add(enemyNumber + 1);
                }
                else if (enemyNumber == 0)
                {
                    index.Add(enemyNumber);
                    index.Add(numberSolder - 1);
                    index.Add(enemyNumber + 1);
                }
                else
                {
                    index.Add(enemyNumber);
                    index.Add(enemyNumber - 1);
                    index.Add(0);
                }
            }

            return index;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - наносит низкий урон цели и двум ближайшим противникам, здоровье высокое.");
        }
    }

    class Commander : Soldier
    {
        public Commander() : base("Commander", 50, 500, 100) { }

        public override List<int> MakeDamage(int numberSolder)
        {
            List<int> index = new List<int>();

            if (Health > 0)
            {
                Random random = new Random();

                Console.WriteLine($"Солдат {Name} наносит урон");
                index.Add(random.Next(0, numberSolder));
            }

            return index;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - наносит урон в одну цель, здоровье среднее.");
        }
    }
}
