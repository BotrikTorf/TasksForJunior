using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            List<Character> characters = new List<Character>()
            {
                new Warrior(),
                new Thief(),
                new Berserker(),
                new Magician(),
                new Priest()
            };

            foreach (var character in characters)
            {
                character.ShowDescription();
            }
        }
    }

    class Character
    {
        private int _health;
        private int _intellect;
        private int _dexterity;
        private string _name;

        public Character(int health = 0, int intellect = 0,
            int dexterity = 0, string name = null)
        {
            _health = health;
            _intellect = intellect;
            _dexterity = dexterity;
            _name = name;
        }

        public int Health
        {
            get
            {
                return _health;
            }
            set
            {
                if (value >= 0)
                {
                    _health = value;
                }
            }
        }
        public int Intellect
        {
            get
            {
                return _intellect;
            }
            set
            {
                if (value >= 0)
                {
                    _intellect = value;
                }
            }
        }
        public int Dexterity
        {
            get
            {
                return _dexterity;
            }
            set
            {
                if (value >= 0)
                {
                    _dexterity = value;
                }
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != null)
                {
                    _name = value;
                }
            }
        }

        public virtual void TakeDamage(int damage) { }

        public virtual void ShowDescription() { }

        public virtual int Strike() => 0;

    }

    class Warrior : Character
    {
        public Warrior(int health = 0, int intellect = 0,
            int dexterity = 0, string name = "Warrior")
        {
            base.Health = health;
            base.Intellect = intellect;
            base.Dexterity = dexterity;
            base.Name = name;
        }

        public override void TakeDamage(int damage)
        {

        }

        public override int Strike()
        {
            return 0;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - класс," +
                              $"{Health} - количество жизни," +
                              $"{Intellect} - количество интелекта," +
                              $"{Dexterity} - количество ловкости");
        }
    }

    class Thief : Character
    {
        public Thief(int health = 0, int intellect = 0,
            int dexterity = 0, string name = "Thief")
        {
            base.Health = health;
            base.Intellect = intellect;
            base.Dexterity = dexterity;
            base.Name = name;
        }

        public override void TakeDamage(int damage)
        {

        }

        public override int Strike()
        {
            return 0;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - класс," +
                              $"{Health} - количество жизни," +
                              $"{Intellect} - количество интелекта," +
                              $"{Dexterity} - количество ловкости");
        }
    }

    class Berserker : Character
    {
        public Berserker(int health = 0, int intellect = 0,
            int dexterity = 0, string name = "Berserker")
        {
            base.Health = health;
            base.Intellect = intellect;
            base.Dexterity = dexterity;
            base.Name = name;
        }

        public override void TakeDamage(int damage)
        {

        }

        public override int Strike()
        {
            return 0;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - класс," +
                              $"{Health} - количество жизни," +
                              $"{Intellect} - количество интелекта," +
                              $"{Dexterity} - количество ловкости");
        }
    }

    class Magician : Character
    {
        public Magician(int health = 0, int intellect = 0,
            int dexterity = 0, string name = "Magician")
        {
            base.Health = health;
            base.Intellect = intellect;
            base.Dexterity = dexterity;
            base.Name = name;
        }

        public override void TakeDamage(int damage)
        {

        }

        public override int Strike()
        {
            return 0;
        }

        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - класс," +
                              $"{Health} - количество жизни," +
                              $"{Intellect} - количество интелекта," +
                              $"{Dexterity} - количество ловкости");
        }
    }

    class Priest : Character
    {
        public Priest(int health = 0, int intellect = 0,
            int dexterity = 0, string name = "Priest")
        {
            base.Health = health;
            base.Intellect = intellect;
            base.Dexterity = dexterity;
            base.Name = name;
        }

        public override void TakeDamage(int damage)
        {

        }

        public override int Strike()
        {
            return 0;
        }
        public override void ShowDescription()
        {
            Console.WriteLine($"{Name} - класс," +
                              $"{Health} - количество жизни," +
                              $"{Intellect} - количество интелекта," +
                              $"{Dexterity} - количество ловкости");
        }
    }
}
