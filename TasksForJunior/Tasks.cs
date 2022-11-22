using System;
using System.Collections.Generic;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {

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
            AddFish();
        }

        public void AddFish()
        {
            if (_fishs.Count < _maxFish)
            {
                Console.WriteLine("");
                ShowFish(_fishsList);

            }
        }

        public void ShowFish(List<Fish> fishs)
        {
            foreach (var fish in fishs)
                fish.ShowFeatures();
        }
    }

    class Fish
    {
        public Fish(int maxAge = 0, int maxSize = 0, string name = null)
        {
            MaxAge = maxAge;
            MaxSize = maxSize;
            Name = name;
            ChangeAge();
            ChangeSize();
        }

        public string Name { get; }

        public int Age { get; private set; }

        public int MaxAge { get; }

        public int MaxSize { get; }

        public int Size { get; private set; }

        public void ShowFeatures()
        {
            Console.WriteLine($"{Name} -название рыбки," +
                $" {Age} - возраст, {MaxAge} - максимальный возраст, " +
                $"{Size} - размер, {MaxSize}  - максимальный размер.");
        }

        private void ChangeSize()
        {
            bool isChangeSize = false;
            int minSize = 0;

            Console.Write("Укажите длину рыбки которую вы хотите добавить в аквариум, " +
                             $"ее длина не дожен привышать {MaxSize} см: ");

            while (isChangeSize == false)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result <= MaxAge && result > minSize)
                    {
                        Size = result;
                        isChangeSize = true;
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
        }

        private void ChangeAge()
        {
            bool isChangeAge = false;
            int minAge = 0;

            Console.Write("Укажите возраст рыбки которую вы хотите добавить в аквариум, " +
                             $"ее возраст не дожен привышать {MaxAge} лет: ");

            while (isChangeAge == false)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result <= MaxAge && result > minAge)
                    {
                        Age = result;
                        isChangeAge = true;
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
        }
    }

    class Neon : Fish
    {
        //private int _maxAge = 5;
        //private int _maxSize = 3;
        public Neon() : base(5, 3, "Neon") { }
    }

    class Guppy : Fish
    {
        //private int _maxAge = 4;
        //private int _maxSize = 3;
        public Guppy() : base(4, 3, "Guppy") { }
    }

    class Angel : Fish
    {
        //private int _maxAge = 10;
        //private int _maxSize = 15;
        public Angel() : base(10, 15, "Angel") { }
    }

    class Barbus : Fish
    {
        //private int _maxAge = 4;
        //private int _maxSize = 6;
        public Barbus() : base(4, 6, "Barbus") { }


    }
}
