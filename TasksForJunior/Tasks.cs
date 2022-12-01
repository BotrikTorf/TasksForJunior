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

            while (isAquariumExists)
            {
                aquarium.ShowFish();

                Console.Write($"Если вы хотите добавить рыбку, то введите \"{ansver}\": ");

                if (Console.ReadLine().ToLower() == ansver)
                {
                    bool canKeepCreating = true;

                    while (canKeepCreating)
                    {
                        aquarium.AddFish();

                        Console.WriteLine($"Если вы хотите продолжить заселять аквариум, то введите \"{ansver}\": ");

                        if (Console.ReadLine().ToLower() == ansver)
                            canKeepCreating = false;
                    }

                }


                Console.Write($"Если вы хотите выйте из аквариума то введите \"{ansver}\": ");

                if (Console.ReadLine().ToLower() == ansver)
                    isAquariumExists = false;
            }




        }
    }

    class Aquarium
    {
        private int _maxFish = 20;
        private List<Fish> _fishs = new List<Fish>();
        public Aquarium()
        {

        }

        public void AddFish()
        {
            List<Fish> _fishsList = new List<Fish>
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

                foreach (var fish in _fishsList)
                {
                    Console.WriteLine($"{numberFish}. {fish.Name}");
                    numberFish++;
                }

                bool isChangeNumber = false;
                int number = 0;

                while (isChangeNumber == false)
                {
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        if (result < _fishsList.Count && result >= 0)
                        {
                            isChangeNumber = true;
                            number = result;
                        }
                        else
                        {
                            Console.WriteLine("Ва ввели не правельно число, попробуйте еще раз");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Вы не ввели число, попробуйте еще раз");
                    }
                }


            }
            else
            {
                Console.WriteLine("Вы не можете добавить в аквариум рыбок. Он полон.");
            }
        }

        private float ChangeAge()
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
                    if (result <= fish.MaxAge && result > minAge)
                    {
                        isChangeAge = true;
                        age = result;
                    }
                    else
                    {
                        Console.WriteLine("Ва ввели не правельно число");
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели число, попробуйте еще раз");
                }
            }

            return age;
        }



        public void ShowFish()
        {
            if (_fishs.Count == 0)
                Console.WriteLine("В аквариуме нет рыб!");
            else
                foreach (var fish in _fishs)
                    fish.ShowFeatures();
        }
    }

    class Fish
    {
        static int _number = 0;

        public Fish(int maxAge, string name = null)
        {
            MaxAge = maxAge;
            Name = name;
            Age = 0;
            _number++;
        }

        public Fish(float age, int maxAge, string name = null)
        {
            MaxAge = maxAge;
            Name = name;
            Age = age;
            _number++;
        }

        public string Name { get; }

        public float Age { get; private set; }

        public int MaxAge { get; }

        public void ShowFeatures()
        {
            Console.WriteLine($"Номер рыбки: {_number}, {Name} -название рыбки," +
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

        public Neon(float age) : base(age, 10, "Angel") { }
    }

    class Guppy : Fish
    {
        public Guppy() : base(4, "Guppy") { }

        public Guppy(float age) : base(age, 10, "Angel") { }
    }

    class Angel : Fish
    {
        public Angel() : base(10, "Angel") { }

        public Angel(float age) : base(age, 10, "Angel") { }
    }

    class Barbus : Fish
    {
        public Barbus() : base(4, "Barbus") { }

        public Barbus(float age) : base(age, 10, "Angel") { }
    }
}
