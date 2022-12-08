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

        public void Start()
        {
            bool isOpen = true;
            string commandExit = "e";
            string enteredString;

            Console.Write("Добро пожаловать в маленький зоопарк.\n" +
                $"В зоопарке есть {_aviaries.Count} вальера. " +
                $"Введите номер вальера к которому хотите подойти(от 0 до {_aviaries.Count - 1}): ");

            while (isOpen)
            {
                enteredString = Console.ReadLine();
                ChooseAviary(enteredString);

                if (enteredString == commandExit)
                    isOpen = false;

                Console.WriteLine("Если хотите посетить другой вальер введите номер вальера, " +
                 $"а если хотите выйти из зоопарка введите {commandExit} :");
            }
        }

        private void Create()
        {
            _aviaries.Add(new Aviary(new Wolf()));
            _aviaries.Add(new Aviary(new Lynx()));
            _aviaries.Add(new Aviary(new Elephant()));
            _aviaries.Add(new Aviary(new Sheep()));
        }

        private void ChooseAviary(string enteredString)
        {
            if (int.TryParse(enteredString, out int result))
            {
                if (result < _aviaries.Count && result >= 0)
                {
                    _aviaries[result].Show();
                }
            }
        }
    }
}

class Aviary
{
    List<Animal> _animals;

    public Aviary(Animal animal)
    {
        _animals = new List<Animal>();
        Create(animal);
    }

    public void Show()
    {
        foreach (var animal in _animals)
        {
            animal.Show();
        }
    }

    private void Create(Animal animal)
    {
        _animals.Add(new Animal("Adult", "Male", animal.Name, animal.Sound));
        _animals.Add(new Animal("Adult", "Female", animal.Name, animal.Sound));
        _animals.Add(new Animal("Joey", "Female", animal.Name, animal.Sound));
        _animals.Add(new Animal("Joey", "Male", animal.Name, animal.Sound));
    }
}

class Animal
{
    public Animal()
    {
        Age = null;
        Gender = null;
    }

    public Animal(string age, string gender, string name, string sound)
    {
        Age = age;
        Gender = gender;
        Name = name;
        Sound = sound;
    }

    public string Age { get; }

    public string Gender { get; }

    public string Name { get; private protected set; }

    public string Sound { get; private protected set; }

    public void Show()
    {
        Console.WriteLine($"{Age} {Name} {Gender} пола и издает звук {Sound}.");
    }
}

class Wolf : Animal
{
    public Wolf()
    {
        Name = "Wolf";
        Sound = "U-U-U";
    }
}

class Lynx : Animal
{
    public Lynx()
    {
        Name = "Lynx";
        Sound = "VOU-VOU-VOU";
    }
}

class Elephant : Animal
{
    public Elephant()
    {
        Name = "Elephant";
        Sound = "BUUU-BUUU-BUUU";
    }
}

class Sheep : Animal
{
    public Sheep()
    {
        Name = "Sheep";
        Sound = "BEE-BEE-BEE";
    }
}