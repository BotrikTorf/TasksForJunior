using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Zoo zoo = new Zoo();
            zoo.Start();
        }
    }

    class Zoo
    {
        List<Aviary> _aviaries;

        public Zoo()
        {
            _aviaries = new List<Aviary>();
            Create();
        }

        private void Create()
        {
            _aviaries.Add(new Aviary("Wolf"));
            _aviaries.Add(new Aviary("Lynx"));
            _aviaries.Add(new Aviary("Elephant"));
            _aviaries.Add(new Aviary("Sheep"));
        }

        public void Start()
        {
            bool isZooOpen = true;
            string exit = "e";

            Console.Write("Добро пожаловать в маленький зоопарк.\n" +
                $"В зоопарке есть {_aviaries.Count} вальера. " +
                $"Введите номер вальера к которому хотите подойти(от 0 до {_aviaries.Count - 1}): ");

            while (isZooOpen)
            {
                ChooseAviary(Console.ReadLine());
                Console.WriteLine("Если хотите посетить другой вальер введите номер вальера, " +
                    $"а если хотите выйти из зоопарка введите {exit} :");

                string enteredString = Console.ReadLine();

                if (enteredString == exit)
                    isZooOpen = false;
                else
                    ChooseAviary(enteredString);
            }
        }

        private void ChooseAviary(string enteredString)
        {
            bool haveChooseAviary = true;

            while (haveChooseAviary)
            {
                if (int.TryParse(enteredString, out int result))
                {
                    if (result < _aviaries.Count && result >= 0)
                    {
                        _aviaries[result].Show();
                        haveChooseAviary = false;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели не правельно число, попробуйте еще раз");
                        enteredString = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели число, попробуйте еще раз");
                    enteredString = Console.ReadLine();
                }
            }
        }
    }

    class Aviary
    {
        List<Animal> _animals;

        public Aviary(string name)
        {
            _animals = new List<Animal>();
            Create(name);
        }

        public void Show()
        {
            foreach (var animal in _animals)
            {
                animal.Show();
            }
        }

        private void Create(string name)
        {
            if (name == "Wolf")
            {
                _animals.Add(new Wolf("Adult", "Male"));
                _animals.Add(new Wolf("Adult", "Female"));
                _animals.Add(new Wolf("Joey", "Female"));
                _animals.Add(new Wolf("Joey", "Male"));
            }
            else if (name == "Lynx")
            {
                _animals.Add(new Lynx("Adult", "Male"));
                _animals.Add(new Lynx("Adult", "Female"));
                _animals.Add(new Lynx("Joey", "Female"));
                _animals.Add(new Lynx("Joey", "Female"));
                _animals.Add(new Lynx("Joey", "Male"));
            }
            else if (name == "Elephant")
            {
                _animals.Add(new Elephant("Adult", "Male"));
                _animals.Add(new Elephant("Adult", "Female"));
                _animals.Add(new Elephant("Joey", "Female"));
            }
            else
            {
                _animals.Add(new Sheep("Adult", "Male"));
                _animals.Add(new Sheep("Adult", "Female"));
                _animals.Add(new Sheep("Joey", "Female"));
                _animals.Add(new Sheep("Joey", "Male"));
            }
        }
    }

    class Animal
    {
        public Animal(string age, string gender, string name, string sound)
        {
            Age = age;
            Gender = gender;
            Name = name;
            Sound = sound;
        }

        public string Age { get; }

        public string Gender { get; }

        public string Name { get; }

        public string Sound { get; }

        public void Show()
        {
            Console.WriteLine($"{Age} {Name} {Gender} пола и издает звук {Sound}.");
        }
    }

    class Wolf : Animal
    {
        public Wolf(string age, string gender) : base(age, gender, "Wolf", "U-U-U") { }
    }

    class Lynx : Animal
    {
        public Lynx(string age, string gender) : base(age, gender, "Lynx", "VOU-VOU-VOU") { }
    }

    class Elephant : Animal
    {
        public Elephant(string age, string gender) : base(age, gender, "Elephant", "BUUU-BUUU-BUUU") { }
    }

    class Sheep : Animal
    {
        public Sheep(string age, string gender) : base(age, gender, "Sheep", "BEE-BEE-BEE") { }
    }
}
