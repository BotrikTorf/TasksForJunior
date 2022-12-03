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
            string ansver = "y";

            aquarium.ShowFish();

            while (isAquariumExists)
            {
                Console.Write($"Если вы хотите добавить рыбку, то введите \"{ansver}\": ");

                if (Console.ReadLine().ToLower() == ansver)
                {
                    bool canKeepCreating = true;

                    while (canKeepCreating)
                    {
                        aquarium.AddFish();

                        Console.WriteLine($"Если вы хотите продолжить заселять аквариум, то введите \"{ansver}\": ");

                        if (Console.ReadLine().ToLower() != ansver)
                            canKeepCreating = false;
                    }
                }
                else
                {
                    Console.WriteLine();
                }

                aquarium.ShowFish();
                aquarium.RemoveFish();
                Console.WriteLine();
                aquarium.RemoveDeadFish();
                Console.Write($"Если вы хотите выйте из аквариума то введите \"{ansver}\": ");

                if (Console.ReadLine().ToLower() == ansver)
                    isAquariumExists = false;

                aquarium.LiveOneYear();
            }
        }
    }

    class Aquarium
    {
        private int _maxFish = 20;
        private List<Fish> _fishs;

        public Aquarium()
        {
            _fishs = new List<Fish>();
        }

        public void AddFish()
        {
            List<Fish> fishsList = new List<Fish>
            {
                new Neon(),
                new Guppy(),
                new Angel(),
                new Barbus()
            };

            Console.WriteLine($" В аквариуме может плавать не более {_maxFish} рыбок, сейчас в аквариуме {_fishs.Count} рыбок.");

            if (_fishs.Count < _maxFish)
            {
                Console.WriteLine("Выберите рыбу которую хотите добавить(введите номер из списка):");
                int numberFish = 0;

                foreach (var fish in fishsList)
                {
                    Console.WriteLine($"{numberFish}. {fish.Name}");
                    numberFish++;
                }

                numberFish = ChooseNumberFish(fishsList.Count);

                if (fishsList[numberFish] is Neon)
                    _fishs.Add(new Neon(ChangeAge(fishsList[numberFish])));
                else if (fishsList[numberFish] is Guppy)
                    _fishs.Add(new Guppy(ChangeAge(fishsList[numberFish])));
                else if (fishsList[numberFish] is Angel)
                    _fishs.Add(new Angel(ChangeAge(fishsList[numberFish])));
                else
                    _fishs.Add(new Barbus(ChangeAge(fishsList[numberFish])));
            }
            else
            {
                Console.WriteLine("Вы не можете добавить в аквариум рыбок. Он полон.");
            }
        }

        public void RemoveFish()
        {
            string ansver = "y";

            Console.Write($"Если вы хотите удалить рыбку, то нажмите {ansver}: ");

            if (Console.ReadLine().ToLower() == ansver)
            {
                Console.Write("Введите номер рыбки которую вы хотите удалить: ");

                bool isNumberCorrect = false;

                while (isNumberCorrect == false)
                {
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        if (result < _fishs.Count && result >= 0)
                        {
                            isNumberCorrect = true;
                            _fishs.RemoveAt(result);
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
            }
        }

        public void RemoveDeadFish()
        {
            bool haveDeadFifh = IsFoundDeadFish();

            if (haveDeadFifh)
            {
                Console.WriteLine("В аквариуме есть мертвая рыба. Ее необходимо удалить.");

                int index = 0;

                while (index < _fishs.Count)
                {
                    if (_fishs[index].MaxAge <= _fishs[index].Age)
                    {
                        _fishs[index].ShowFeatures();

                        string ansver = "y";

                        Console.Write($"Данная рыбка мертва. Для удаления введите {ansver}: ");

                        if (Console.ReadLine().ToLower() == ansver)
                            _fishs.RemoveAt(index);
                        else
                            Console.WriteLine("Мертвая рыба осталась плавать в аквариуме!");
                    }

                    index++;
                }
            }
        }

        public void LiveOneYear()
        {
            foreach (var fish in _fishs)
                fish.IncreasesAge();
        }

        public void ShowFish()
        {
            if (_fishs.Count == 0)
            {
                Console.WriteLine("В аквариуме нет рыб!");
            }
            else
            {
                int number = 0;

                foreach (var fish in _fishs)
                {
                    Console.Write($"{number}. ");
                    fish.ShowFeatures();
                    number++;
                }
            }
        }

        private int ChooseNumberFish(int count)
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

        private float ChangeAge(Fish fish)
        {
            bool isChangeAge = false;
            int minAge = 0;
            float age = 0f;

            Console.Write("Укажите возраст рыбки которую вы хотите добавить в аквариум, " +
                             $"ее возраст не дожен привышать {fish.MaxAge} лет: ");

            while (isChangeAge == false)
            {
                if (float.TryParse(Console.ReadLine(), out float result))
                {
                    if (result <= fish.MaxAge && result >= minAge)
                    {
                        isChangeAge = true;
                        age = result;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не правельно число");
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели число, попробуйте еще раз");
                }
            }

            return age;
        }

        private bool IsFoundDeadFish()
        {
            bool haveDeadFifh = false;

            foreach (var fish in _fishs)
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

        public Fish(float age, int maxAge, string name = null)
        {
            MaxAge = maxAge;
            Name = name;
            Age = age;
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

        public Neon(float age) : base(age, 5, "Neon") { }
    }

    class Guppy : Fish
    {
        public Guppy() : base(4, "Guppy") { }

        public Guppy(float age) : base(age, 4, "Guppy") { }
    }

    class Angel : Fish
    {
        public Angel() : base(10, "Angel") { }

        public Angel(float age) : base(age, 10, "Angel") { }
    }

    class Barbus : Fish
    {
        public Barbus() : base(4, "Barbus") { }

        public Barbus(float age) : base(age, 4, "Barbus") { }
    }
}
