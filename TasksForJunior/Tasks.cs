using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            string commandContinue = "yes";

            List<Character> characters = new List<Character>()
            {
                new Warrior(),
                new Thief(),
                new Berserker(),
                new Magician(),
                new Priest()
            };

            int serialNumber = 1;

            foreach (var character in characters)
            {
                Console.Write($"{serialNumber}. ");
                character.ShowDescription();
                serialNumber++;
            }

            bool haveFight = true;

            while (haveFight)
            {
                Character characterOne = SelectionCharacter();
                Character characterTwo = SelectionCharacter();

                Console.Write("Вы выбрали:\nПервый боец:");
                characterOne.ShowDescription();
                Console.Write("Второй боец: ");
                characterTwo.ShowDescription();

                StartFight(characterOne, characterTwo);

                Console.Write($"Для продолжения введите {commandContinue}: ");

                if (Console.ReadLine().ToLower() != commandContinue)
                {
                    haveFight = false;
                }

                Console.WriteLine();
            }
        }

        static Character SelectionCharacter()
        {
            List<Character> characters = new List<Character>()
            {
                new Warrior(),
                new Thief(),
                new Berserker(),
                new Magician(),
                new Priest()
            };
            Character character = null;

            while (character == null)
            {
                Console.Write("Введите номер персонажа для боя: ");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result > 0 && result <= characters.Count)
                        character = characters[result - 1];
                    else
                        Console.WriteLine("Персонажа под таким номером не существует.");
                }
                else
                {
                    Console.WriteLine("Вы не правильно ввели номер.");
                }
            }

            return character;
        }

        static void StartFight(Character characterOne, Character characterTwo)
        {
            Random random = new Random();
            int physicalDamage = 0;
            int magicDamage = 0;

            while (characterOne.Health > 0 && characterTwo.Health > 0)
            {
                Console.WriteLine("Противники кружат по полю и наблюдают друг за другом!");

                if (random.Next(1, 3) == 1)
                {
                    Console.WriteLine("Первый соперник наносит удар");
                    characterOne.Strike(ref physicalDamage, ref magicDamage);
                    characterTwo.TakeDamage(ref physicalDamage, ref magicDamage);
                    Console.WriteLine("Второй соперник отвечает!");
                    characterTwo.Strike(ref physicalDamage, ref magicDamage);
                    characterOne.TakeDamage(ref physicalDamage, ref magicDamage);
                    characterOne.ShowDescription();
                    characterTwo.ShowDescription();
                }
                else
                {
                    Console.WriteLine("Второй соперник наносит удар!");
                    characterTwo.Strike(ref physicalDamage, ref magicDamage);
                    characterOne.TakeDamage(ref physicalDamage, ref magicDamage);
                    Console.WriteLine("Первый соперник отвечает");
                    characterOne.Strike(ref physicalDamage, ref magicDamage);
                    characterTwo.TakeDamage(ref physicalDamage, ref magicDamage);
                    characterOne.ShowDescription();
                    characterTwo.ShowDescription();
                }
            }

            if (characterOne.Health == characterTwo.Health)
                Console.WriteLine("Ничья");
            else if (characterOne.Health > 0)
                Console.WriteLine($"Выиграл игрок {characterOne.Name}!");
            else
                Console.WriteLine($"Выиграл игрок {characterTwo.Name}!");
        }
    }

    class Character
    {
        private int _health;
        private int _manna;
        private int _maxHealth;
        private int _maxManna;

        public Character(int strength = 0, int intellect = 0,
            int dexterity = 0, string name = null)
        {
            Strength = strength;
            Intellect = intellect;
            Dexterity = dexterity;
            Name = name;
            _health = Strength * 20;
            _manna = Intellect * 10;
            _maxHealth = _health;
            _maxManna = _manna;
            Armor = (int)(Strength * 0.5f + Dexterity * 0.5f);
            MagicArmor = (int)(Dexterity * 0.5F + Intellect * 0.5F);
            MinDamage = 0;
        }

        public int Strength { get; }

        public int Intellect { get; }

        public int Dexterity { get; }

        public string Name { get; }

        public int Manna
        {
            get
            {
                return _manna;
            }
            private protected set
            {
                if (value < 0)
                    _manna = 0;
                else if (value > _maxManna)
                    _manna = _maxManna;
                else
                    _manna = value;
            }
        }

        public int Health
        {
            get
            {
                return _health;
            }
            private protected set
            {
                if (value < 0)
                    _health = 0;
                else if (value > _maxHealth)
                    _health = _maxHealth;
                else
                    _health = value;
            }
        }

        public int Armor { get; }

        public int MagicArmor { get; }

        public int MinDamage { get; }

        public virtual void TakeDamage(ref int physicalDamage, ref int magicDamage) { }

        public virtual void Strike(ref int physicalDamage, ref int magicDamage) { }

        public void ShowDescription()
        {
            Console.WriteLine($"{Name} - класс,\t" +
                              $"{Strength} - сила,\t" +
                              $"{Intellect} - интелект,\t" +
                              $"{Dexterity} - ловкости,\t" +
                              $"{Health} - здоровье,\t" +
                              $"{Manna} - манна,\t" +
                              $"{Armor} - броня,\t" +
                              $"{MagicArmor} - защита от заклинаний");
        }

        public void RegenerateManna()
        {
            double factorRegeneration = 10.0;

            if (_manna < _maxManna)
            {
                int speedRegeneration = (int)(Intellect / factorRegeneration);
                _manna = (_manna + speedRegeneration > _maxManna) ? _maxManna : _manna + speedRegeneration;
            }
        }

        public bool HasCriticalDamage()
        {
            Random random = new Random();
            int minValue = 1;
            int maxValue = Dexterity + Intellect + Strength + 1;
            double coefficientIntelligence = 2.0;

            return (random.Next(minValue, maxValue) <= Dexterity + (int)(Intellect / coefficientIntelligence));
        }
    }

    class Warrior : Character
    {
        private int _spellCost = 51;
        private float _physicalDamageRatio = 1.5f;
        private float _critDamageRatio = 2.5f;

        public Warrior(int strength = 50, int intellect = 10,
            int dexterity = 40, string name = "Warrior") : base(strength, intellect, dexterity, name) { }

        public override void TakeDamage(ref int physicalDamage, ref int magicDamage)
        {
            int physicalIncomingDamage = (physicalDamage - Armor) < MinDamage
                ? MinDamage
                : physicalDamage - Armor;
            int magicIncomingDamage = (magicDamage - MagicArmor) < MinDamage
                ? MinDamage
                : magicDamage - MagicArmor;
            Health -= physicalIncomingDamage + magicIncomingDamage;
        }

        public override void Strike(ref int physicalDamage, ref int magicDamage)
        {
            if (Manna < _spellCost)
                CalculatesDamageFirstAbility(out physicalDamage, out magicDamage);
            else
                CalculatesDamageSecondAbility(out physicalDamage, out magicDamage);

            RegenerateManna();
        }

        private void CalculatesDamageFirstAbility(out int physicalDamage, out int magicDamage)
        {
            magicDamage = MinDamage;
            Console.WriteLine("Наносит прямой удар мечем");
            physicalDamage = HasCriticalDamage()
                ? (int)(Strength * _critDamageRatio)
                : (int)(Strength * _physicalDamageRatio);
        }

        private void CalculatesDamageSecondAbility(out int physicalDamage, out int magicDamage)
        {
            Console.WriteLine($"Использовав {_spellCost} едениц манны повышает шанс крит удара на 50% и бьет боковым ударом");
            Manna -= _spellCost;
            magicDamage = MinDamage;
            if (HasCriticalDamage())
                physicalDamage = (int)(Strength * _critDamageRatio);
            else if (HasCriticalDamage())
                physicalDamage = (int)(Strength * _critDamageRatio);
            else
                physicalDamage = (int)(Strength * _physicalDamageRatio);
        }
    }

    class Thief : Character
    {
        private int _spellCost = 52;
        private float _physicalDamageRatio = 3.7f;
        private float _magicDamageRatio = 2.2f;
        private float _critDamageRatio = 9.6f;

        public Thief(int strength = 20, int intellect = 10,
            int dexterity = 70, string name = "Thief") : base(strength, intellect, dexterity, name) { }

        public override void TakeDamage(ref int physicalDamage, ref int magicDamage)
        {
            int physicalIncomingDamage = (physicalDamage - Armor) < MinDamage ? MinDamage : physicalDamage - Armor;
            int magicIncomingDamage = (magicDamage - MagicArmor) < MinDamage ? MinDamage : magicDamage - MagicArmor;
            Health -= physicalIncomingDamage + magicIncomingDamage;
        }

        public override void Strike(ref int physicalDamage, ref int magicDamage)
        {
            if (Manna < _spellCost)
                CalculatesDamageFirstAbility(out physicalDamage, out magicDamage);
            else
                CalculatesDamageSecondAbility(out physicalDamage, out magicDamage);

            RegenerateManna();
        }

        private void CalculatesDamageFirstAbility(out int physicalDamage, out int magicDamage)
        {
            magicDamage = MinDamage;
            Console.WriteLine("Наносит прямой удар кинжалами");
            physicalDamage = HasCriticalDamage() ? (int)(Strength * _critDamageRatio) : (int)(Strength * _physicalDamageRatio);
        }

        private void CalculatesDamageSecondAbility(out int physicalDamage, out int magicDamage)
        {
            Console.WriteLine($"Использовав {_spellCost} едениц манны повышает урон");
            Manna -= _spellCost;
            magicDamage = (int)(Intellect * _magicDamageRatio);
            physicalDamage = HasCriticalDamage() ? (int)(Strength * _critDamageRatio) : (int)(Strength * _physicalDamageRatio);
        }
    }

    class Berserker : Character
    {
        private int _spellCost = 60;
        private float _physicalDamageRatio = 0.6f;
        private float _critDamageRatio = 2.5f;
        private float _increasedDamage = 1.5f;

        public Berserker(int strength = 80, int intellect = 10,
            int dexterity = 10, string name = "Berserker") : base(strength, intellect, dexterity, name) { }

        public override void TakeDamage(ref int physicalDamage, ref int magicDamage)
        {
            int physicalIncomingDamage = (physicalDamage - Armor) < MinDamage ? MinDamage : physicalDamage - Armor;
            int magicIncomingDamage = (magicDamage - MagicArmor) < MinDamage ? MinDamage : magicDamage - MagicArmor;
            Health -= physicalIncomingDamage + magicIncomingDamage;
        }

        public override void Strike(ref int physicalDamage, ref int magicDamage)
        {
            if (Manna < _spellCost)
                CalculatesDamageFirstAbility(out physicalDamage, out magicDamage);
            else
                CalculatesDamageSecondAbility(out physicalDamage, out magicDamage);

            RegenerateManna();
        }

        private void CalculatesDamageFirstAbility(out int physicalDamage, out int magicDamage)
        {
            magicDamage = MinDamage;
            Console.WriteLine("Наносит рубящий удар топором");
            physicalDamage = HasCriticalDamage() ? (int)(Strength * _critDamageRatio) : (int)(Strength * _physicalDamageRatio);
        }

        private void CalculatesDamageSecondAbility(out int physicalDamage, out int magicDamage)
        {
            Console.WriteLine($"Использовав {_spellCost} едениц манны и бьет круговым ударом " +
                              $"увеличив на {_increasedDamage * 100 - 100}% урон");
            Manna -= _spellCost;
            magicDamage = MinDamage;
            physicalDamage = HasCriticalDamage()
                ? (int)(Strength * _critDamageRatio * _increasedDamage)
                : (int)(Strength * _physicalDamageRatio * _increasedDamage);
        }
    }

    class Magician : Character
    {
        private int _spellCost = 100;
        private float _physicalDamageRatio = 2.0f;
        private float _magicDamageRatio = 2.2f;
        private float _critDamageRatio = 5.5f;
        private int _armorIncreasePercentage = 50;

        public Magician(int strength = 20, int intellect = 70,
            int dexterity = 10, string name = "Magician") : base(strength, intellect, dexterity, name) { }

        public override void TakeDamage(ref int physicalDamage, ref int magicDamage)
        {
            if (Manna > _spellCost)
            {
                int physicalIncomingDamage = physicalDamage - Armor * (int)(1.0 + _armorIncreasePercentage / 100) < 0
                    ? MinDamage
                    : physicalDamage - Armor * (int)(1.0 + _armorIncreasePercentage / 100);
                int magicIncomingDamage = magicDamage - MagicArmor < MinDamage ? MinDamage : magicDamage - MagicArmor;
                Health -= physicalIncomingDamage + magicIncomingDamage;
            }
            else
            {
                int physicalIncomingDamage = physicalDamage - Armor < MinDamage ? MinDamage : physicalDamage - Armor;
                int magicIncomingDamage = magicDamage - MagicArmor < MinDamage ? MinDamage : magicDamage - MagicArmor;
                Health -= physicalIncomingDamage + magicIncomingDamage;
            }
        }

        public override void Strike(ref int physicalDamage, ref int magicDamage)
        {
            if (Manna < _spellCost)
                CalculatesDamageFirstAbility(out physicalDamage, out magicDamage);
            else
                CalculatesDamageSecondAbility(out physicalDamage, out magicDamage);

            RegenerateManna();
        }

        private void CalculatesDamageFirstAbility(out int physicalDamage, out int magicDamage)
        {
            magicDamage = MinDamage;
            Console.WriteLine("Бьет посохом.");
            physicalDamage = HasCriticalDamage() ? (int)(Strength * _critDamageRatio) : (int)(Strength * _physicalDamageRatio);
        }

        private void CalculatesDamageSecondAbility(out int physicalDamage, out int magicDamage)
        {
            Console.WriteLine($"Использовав {_spellCost} едениц манны ставит магическую браню повышая защиту " +
                              $"на {_armorIncreasePercentage}% и наносит урон заклинанием");
            Manna -= _spellCost;
            magicDamage = HasCriticalDamage() ? (int)(Intellect * _critDamageRatio) : (int)(Intellect * _magicDamageRatio);
            physicalDamage = MinDamage;
        }
    }

    class Priest : Character
    {
        private int _spellCost = 200;
        private float _physicalDamageRatio = 3.7f;
        private float _magicDamageRatio = 1.8f;
        private float _critDamageRatio = 3.4f;
        private float _treatmentFactor = 2.5f;

        public Priest(int strength = 15, int intellect = 80,
            int dexterity = 5, string name = "Priest3") : base(strength, intellect, dexterity, name) { }

        public override void TakeDamage(ref int physicalDamage, ref int magicDamage)
        {
            int physicalIncomingDamage = (physicalDamage - Armor) < MinDamage ? MinDamage : physicalDamage - Armor;
            int magicIncomingDamage = (magicDamage - MagicArmor) < MinDamage ? MinDamage : magicDamage - MagicArmor;
            Health -= physicalIncomingDamage + magicIncomingDamage;
        }

        public override void Strike(ref int physicalDamage, ref int magicDamage)
        {
            if (Manna < _spellCost)
                CalculatesDamageFirstAbility(out physicalDamage, out magicDamage);
            else
                CalculatesDamageSecondAbility(out physicalDamage, out magicDamage);

            RegenerateManna();
        }

        private void CalculatesDamageFirstAbility(out int physicalDamage, out int magicDamage)
        {
            magicDamage = MinDamage;
            Console.WriteLine("Наносит посохом");
            physicalDamage = HasCriticalDamage() ? (int)(Strength * _critDamageRatio) : (int)(Strength * _physicalDamageRatio);
        }

        private void CalculatesDamageSecondAbility(out int physicalDamage, out int magicDamage)
        {
            Console.WriteLine($"Использовав {_spellCost} едениц манны наносит урон заклинанием, также лечит себя. ");
            Manna -= _spellCost;
            physicalDamage = MinDamage;
            magicDamage = HasCriticalDamage() ? (int)(Intellect * _critDamageRatio) : (int)(Intellect * _magicDamageRatio);
            Health += (int)(magicDamage / _treatmentFactor);
        }
    }
}
