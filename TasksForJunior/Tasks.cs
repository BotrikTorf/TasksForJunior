using System;
using System.Collections.Generic;
using System.Linq;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Salesman salesman = new Salesman();
            Player player = new Player();

            Bargaining bargaining = new Bargaining(player, salesman);
            bargaining.StartTrading();
            salesman.Show();
            player.Show();
        }
    }

    abstract class Subject
    {
        private readonly int _price;
        private readonly string _title;

        public Subject(int price, string title)
        {
            _price = price;
            _title = title;
        }

        public void Show()
        {
            Console.WriteLine($"Название: {_title}\n" +
                              $"Стоимость: {_price}");
        }
    }

    class FoodManna : Subject
    {
        private readonly int _amountManna;

        public FoodManna(int amountManna, int price, string title) : base(price, title)
        {
            _amountManna = amountManna;
        }

        public new void Show()
        {
            base.Show();
            Console.WriteLine($"Восполняет манну на {_amountManna} единиц.");
        }
    }

    class FoodHP : Subject
    {
        private readonly int _amountHP;

        public FoodHP(int amountHP, int price, string title) : base(price, title)
        {
            _amountHP = amountHP;
        }

        public new void Show()
        {
            base.Show();
            Console.WriteLine($"Восполняет очки жизни на {_amountHP} единиц.");
        }
    }

    class Weapon : Subject
    {
        private readonly int _maxDamage;
        private readonly int _minDamage;

        public Weapon(int maxDamage, int minDamage, int price, string title) : base(price, title)
        {
            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }

        public new void Show()
        {
            base.Show();
            Console.WriteLine($"Урон: {_minDamage} - {_maxDamage} единиц.");
        }
    }

    class Player
    {
        private readonly List<Weapon> _weapons;
        private readonly List<FoodManna> _foodsManna;
        private readonly List<FoodHP> _foodsHP;

        public Player()
        {
            _weapons = new List<Weapon>(10);
            _foodsHP = new List<FoodHP>(50);
            _foodsManna = new List<FoodManna>(50);
        }

        public void Purchase(Subject subject)
        {
            if (subject is Weapon weapon)
            {
                _weapons.Add(weapon);
            }
            else if (subject is FoodHP foodHP)
            {
                _foodsHP.Add(foodHP);
            }
            else if (subject is FoodManna foodManna)
            {
                _foodsManna.Add(foodManna);
            }
        }

        public void Show()
        {
            Console.WriteLine("У игрока имеется в наличии:");
            Console.WriteLine("Оружие: ");

            foreach (var weapon in _weapons)
            {
                weapon.Show();
            }

            Console.WriteLine("Еда восполняющая очки жизни: ");

            foreach (var food in _foodsHP)
            {
                food.Show();
            }

            Console.WriteLine("Еда восполняющая очки манны: ");

            foreach (var food in _foodsManna)
            {
                food.Show();
            }
        }
    }

    class Salesman
    {
        private const int PriceOneWeapon = 10;
        private const int PriceTwoWeapon = 20;
        private const int PriceFood = 3;
        private const int OneHandedWeaponDamageRatio = 1;
        private const int TowHandedWeaponDamageRatio = 10;


        private readonly string[] oneHandedWeapon = { "OneHandedSword", "OneHandedAxe", "OneHandedHammer" };
        private readonly string[] twoHandedWeapon = { "TwoHandedSword", "TwoHandedAxe", "TwoHandedHammer" };
        private readonly string[] foodsNameHP = { "Apple", "Pear", "Bread" };
        private readonly string[] foodsNameManna = { "Water", "Milk", "Tea" };

        private readonly List<Weapon> _weapons;
        private readonly List<FoodManna> _foodsManna;
        private readonly List<FoodHP> _foodsHP;
        private readonly Random _random = new Random();

        public Salesman()
        {
            _weapons = new List<Weapon>();

            CreatesWeapon(oneHandedWeapon, PriceOneWeapon, OneHandedWeaponDamageRatio);
            CreatesWeapon(twoHandedWeapon, PriceTwoWeapon, TowHandedWeaponDamageRatio);

            _foodsHP = new List<FoodHP>();

            for (int i = 0; i < foodsNameHP.Length; i++)
            {
                _foodsHP.Add(new FoodHP(i * 10, PriceFood, foodsNameHP[i]));
            }

            _foodsManna = new List<FoodManna>();

            for (int i = 0; i < foodsNameManna.Length; i++)
            {
                _foodsManna.Add(new FoodManna(i * 10, PriceFood, foodsNameManna[i]));
            }
        }

        public List<Weapon> ListWeapons()
        {
            return _weapons;
        }

        public List<FoodHP> ListFoodHps()
        {
            return _foodsHP;
        }

        public List<FoodManna> ListFoodManna()
        {
            return _foodsManna;
        }

        public void Sale(Subject subject)
        {
            if (subject is Weapon weapon)
            {
                _weapons.Remove(weapon);
            }
            else if (subject is FoodHP foodHP)
            {
                _foodsHP.Remove(foodHP);
            }
            else if (subject is FoodManna foodManna)
            {
                _foodsManna.Remove(foodManna);
            }
        }

        public void Show()
        {
            Console.WriteLine("У продовца есть в наличии: ");
            Console.WriteLine("Оружие: ");

            foreach (var weapon in _weapons)
            {
                weapon.Show();
            }

            Console.WriteLine("Еда восполняющая очки жизни: ");

            foreach (var food in _foodsHP)
            {
                food.Show();
            }

            Console.WriteLine("Еда восполняющая очки манны: ");

            foreach (var food in _foodsManna)
            {
                food.Show();
            }
        }

        private void CreatesWeapon(string[] name, int PriceOneWeapon, int damage)
        {
            for (int i = 0; i < name.Length; i++)
            {
                int lowerMinDamage = 1 * damage;
                int upperMinDamage = 3 * damage;
                int lowerMaxDamage = 4 * damage;
                int upperMaxDamage = 6 * damage;
                int minDamage = _random.Next(lowerMinDamage, upperMinDamage);
                int maxDamage = _random.Next(lowerMaxDamage, upperMaxDamage);
                _weapons.Add(new Weapon(maxDamage, minDamage, PriceOneWeapon, name[i]));
            }
        }
    }

    class Bargaining
    {
        private const string CommandWeapon = "weapon";
        private const string CommandFoodManna = "manna";
        private const string CommandFoodHP = "hp";
        private const string CommandExit = "exit";

        private readonly Player _player;
        private readonly Salesman _salesman;

        private bool isTrading = true;

        public Bargaining(Player player, Salesman salesman)
        {
            _player = player;
            _salesman = salesman;
        }

        public void StartTrading()
        {
            Console.WriteLine("Добро пожаловать в наш маленький магазинчик!\n" +
                              $"Если вы хотите посмотреть оружие наберите {CommandWeapon}.\n" +
                              $"Если хотите посмотреть предметы для востановления вашей жизни наберите - {CommandFoodHP}.\n" +
                              $"Если хотите посмотреть предметы для востановления вашей манны наберите - {CommandFoodManna}.\n" +
                              $"Для прекращения торговли наберите - {CommandExit}.");

            while (isTrading)
            {
                Console.Write("Введите команду для просмотра и покупки нужного вам товара: ");

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isTrading = false;
                        break;
                    case CommandFoodHP:
                        TradinHP(_salesman.ListFoodHps());
                        break;
                    case CommandFoodManna:
                        TradinManna(_salesman.ListFoodManna());
                        break;
                    case CommandWeapon:
                        TradinWeapon(_salesman.ListWeapons());
                        break;
                    default:
                        Console.WriteLine("Вы не правельно ввели команду! Попробуйте еще раз");
                        break;
                }
            }
        }

        private void TradinHP(List<FoodHP> foodsHp)
        {
            List<Subject> subject = foodsHp.Cast<Subject>().ToList();

            Tradin(subject);
        }

        private void TradinManna(List<FoodManna> foodsManna)
        {
            List<Subject> subject = foodsManna.Cast<Subject>().ToList();

            Tradin(subject);
        }

        private void TradinWeapon(List<Weapon> weapons)
        {
            List<Subject> subject = weapons.Cast<Subject>().ToList();

            Tradin(subject);
        }

        private void Tradin(List<Subject> subjects)
        {
            int serialNumber = 0;

            Console.WriteLine("Представляю вам список товара: ");

            foreach (var product in subjects)
            {
                Console.WriteLine($"Порядковый номер: {serialNumber}");
                product.Show();
                serialNumber++;
            }

            Console.Write("Для покупки товара введите порядковый номер: ");

            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number < subjects.Count)
                {
                    _player.Purchase(subjects[number]);
                    _salesman.Sale(subjects[number]);
                }
                else
                {
                    Console.WriteLine("Товара с таким номером нет.");
                }
            }
        }
    }
}
