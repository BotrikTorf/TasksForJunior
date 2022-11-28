using System;
using System.Collections.Generic;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Aquarium aquarium = new Aquarium();
            aquarium.ShowFish();
            aquarium.AddFish();
        }
    }

    class Aquarium
    {
        private int _maxFish = 20;
        private List<Fish> _fishs = new List<Fish>();
        private List<Fish> _fishsList = new List<Fish>
        {
            new Neon(),
            new Guppy(),
            new Angel(),
            new Barbus()
        };
        public Aquarium()
        {
   
        }

        public void AddFish()
        {
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
                int number =0;

                while (isChangeNumber == false)
                {
                    if (int.TryParse(Console.ReadLine(), out int result))
                    {
                        if (result <_fishsList.Count && result >= 0)
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
        }

        private float ChangeSize(Fish fish)
        {
            bool isChangeSize = false;
            int minSize = 0;
            float size = 0f;

            Console.Write("Укажите длину рыбки которую вы хотите добавить в аквариум, " +
                             $"ее длина не дожен привышать {fish.MaxSize} см: ");

            while (isChangeSize == false)
            {
                if (float.TryParse(Console.ReadLine(), out float result))
                {
                    if (result <= fish.MaxAge && result > minSize)
                    {
                        isChangeSize = true;
                        size = result;
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

            return size;
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
            foreach (var fish in _fishs)
                fish.ShowFeatures();
        }
    }

    class Fish
    {
        private float _age;
        private float _size;

        public Fish(int maxAge, int maxSize, string name = null)
        {
            MaxAge = maxAge;
            MaxSize = maxSize;
            Name = name;
            _age = 0;
            _size = 0;
        }

        public Fish(float age, float size, int maxAge, int maxSize, string name = null)
        {
            MaxAge = maxAge;
            MaxSize = maxSize;
            Name = name;
            Age = age;
            Size = size;
        }

        public string Name { get; }

        public float Age
        {
            get
            {
                return _age < MaxAge ? _age : MaxAge;
            }
            private protected set { }
        }

        public int MaxAge { get; }

        public int MaxSize { get; }

        public float Size
        {
            get
            {
                return _size < MaxSize ? _size : MaxSize;
            }
            private protected set { }
        }

        public void ShowFeatures()
        {
            Console.WriteLine($"{Name} -название рыбки," +
                $" {Age} - возраст, {MaxAge} - максимальный возраст, " +
                $"{Size} - размер, {MaxSize}  - максимальный размер.");
        }

       

       
    }

    class Neon : Fish
    {
        public Neon() : base(5, 3, "Neon") { }

        public Neon(float age, float size) : base(age, size, 10, 15, "Angel") { }
    }

    class Guppy : Fish
    {
        public Guppy() : base(4, 3, "Guppy") { }

        public Guppy(float age, float size) : base(age, size, 10, 15, "Angel") { }
    }

    class Angel : Fish
    {
        public Angel() : base(10, 15, "Angel") { }

        public Angel(float age, float size) : base(age, size, 10, 15, "Angel") { }
    }

    class Barbus : Fish
    {
        public Barbus() : base(4, 6, "Barbus") { }

        public Barbus(float age, float size) : base(age, size, 10, 15, "Angel") { }
    }
}
