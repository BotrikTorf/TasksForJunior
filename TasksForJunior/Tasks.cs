using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Aquarium aquarium = new Aquarium();
            bool isAquariumExists = true;
            string commandAnsver = "y";

            aquarium.ShowFishes();

            while (isAquariumExists)
            {
                Console.Write($"Если вы хотите добавить рыбку, то введите \"{commandAnsver}\": ");

                if (Console.ReadLine().ToLower() == commandAnsver)
                {
                    bool canKeepCreating = true;

                    while (canKeepCreating)
                    {
                        aquarium.AddFish();

                        Console.WriteLine($"Если вы хотите продолжить заселять аквариум, то введите \"{commandAnsver}\": ");

                        if (Console.ReadLine().ToLower() != commandAnsver)
                            canKeepCreating = false;
                    }
                }
                else
                {
                    Console.WriteLine();
                }

                aquarium.ShowFishes();
                aquarium.RemoveFish();
                Console.WriteLine();
                aquarium.RemoveDeadFish();
                Console.Write($"Если вы хотите выйте из аквариума то введите \"{commandAnsver}\": ");

                if (Console.ReadLine().ToLower() == commandAnsver)
                    isAquariumExists = false;

                aquarium.LiveOneYear();
            }
        }
    }

    class Aquarium
    {
        private int _maxFishes = 20;
        private List<Fish> _fishes;

        public Aquarium()
        {
            _fishes = new List<Fish>();
        }

        public void AddFish()
        {
            List<Fish> fishesList = new List<Fish>
            {
                new Neon(),
                new Guppy(),
                new Angel(),
                new Barbus()
            };

            Console.WriteLine($" В аквариуме может плавать не более {_maxFishes} рыбок, сейчас в аквариуме {_fishes.Count} рыбок.");

            if (_fishes.Count < _maxFishes)
            {
                Console.WriteLine("Выберите рыбу которую хотите добавить(введите номер из списка):");
                int numberFish = 0;

                foreach (var fish in fishesList)
                {
                    Console.WriteLine($"{numberFish}. {fish.Name}");
                    numberFish++;
                }

                numberFish = ReadInt(fishesList.Count);

                _fishes.Add(fishesList[numberFish]);
            }
            else
            {
                Console.WriteLine("Вы не можете добавить в аквариум рыбок. Он полон.");
            }
        }

        public void RemoveFish()
        {
            string commandAnsver = "y";

            Console.Write($"Если вы хотите удалить рыбку, то нажмите {commandAnsver}: ");

            if (Console.ReadLine().ToLower() == commandAnsver)
            {
                Console.Write("Введите номер рыбки которую вы хотите удалить: ");
                _fishes.RemoveAt(ReadInt(_fishes.Count));
            }
        }

        public void RemoveDeadFish()
        {
            bool haveDeadFifh = IsFoundDeadFish();

            if (haveDeadFifh)
            {
                Console.WriteLine("В аквариуме есть мертвая рыба. Ее необходимо удалить.");

                int index = 0;

                while (index < _fishes.Count)
                {
                    if (_fishes[index].MaxAge <= _fishes[index].Age)
                    {
                        _fishes[index].ShowFeatures();

                        string ansver = "y";

                        Console.Write($"Данная рыбка мертва. Для удаления введите {ansver}: ");

                        if (Console.ReadLine().ToLower() == ansver)
                            _fishes.RemoveAt(index);
                        else
                            Console.WriteLine("Мертвая рыба осталась плавать в аквариуме!");
                    }

                    index++;
                }
            }
        }

        public void LiveOneYear()
        {
            foreach (var fish in _fishes)
                fish.IncreasesAge();
        }

        public void ShowFishes()
        {
            if (_fishes.Count == 0)
            {
                Console.WriteLine("В аквариуме нет рыб!");
            }
            else
            {
                int number = 0;

                foreach (var fish in _fishes)
                {
                    Console.Write($"{number}. ");
                    fish.ShowFeatures();
                    number++;
                }
            }
        }

        private int ReadInt(int count)
        {
            bool isChangeNumber = false;
            int number = 0;

            while (isChangeNumber == false)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result < count && result >= 0)
                    {
                        isChangeNumber = true;
                        number = result;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не правельно число, попробуйте еще раз");
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели число, попробуйте еще раз");
                }
            }

            return number;
        }

        private bool IsFoundDeadFish()
        {
            bool haveDeadFifh = false;

            foreach (var fish in _fishes)
                if (fish.MaxAge <= fish.Age)
                    haveDeadFifh = true;

            return haveDeadFifh;
        }
    }

    class Fish
    {
        public Fish(int maxAge, string name = null)
        {
            MaxAge = maxAge;
            Name = name;
            Age = 0;
        }

        public string Name { get; }

        public float Age { get; private set; }

        public int MaxAge { get; }

        public void ShowFeatures()
        {
            Console.WriteLine($"{Name} -название рыбки," +
                $" {Age} - возраст, {MaxAge} - максимальный возраст");
        }

        public void IncreasesAge()
        {
            if (Age < MaxAge)
                Age++;
            else
                Console.WriteLine($"Рыбка {Name} достигла сваего максимального возраста и умерла! Удалите ее из аквариума.");
        }
    }

    class Neon : Fish
    {
        public Neon() : base(5, "Neon") { }
    }

    class Guppy : Fish
    {
        public Guppy() : base(4, "Guppy") { }
    }

    class Angel : Fish
    {
        public Angel() : base(10, "Angel") { }
    }

    class Barbus : Fish
    {
        public Barbus() : base(4, "Barbus") { }
    }
}
