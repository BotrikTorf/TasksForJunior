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

    class Subject
    {
        private int _price;
        private string _title;

        public Subject(int price, string title)
        {
            _price = price;
            _title = title;
        }

        public int Price { get { return _price; } private set { } }
        public string Title { get { return _title; } private set { } }

        public void Show()
        {
            Console.WriteLine($"Название: {_title}\n" +
                              $"Стоимость: {_price}");
        }
    }

    class FoodManna : Subject
    {
        int _amountManna;

        public FoodManna(int amountManna, int price, string title) : base(price, title)
        {
            _amountManna = amountManna;
        }

        public int AmountManna { get { return _amountManna; } private set { } }

        public new void Show()
        {
            base.Show();
            Console.WriteLine($"Восполняет манну на {_amountManna} единиц.");
        }
    }

    class FoodHP : Subject
    {
        int _amountHP;

        public FoodHP(int amountHP, int price, string title) : base(price, title)
        {
            _amountHP = amountHP;
        }

        public int AmountHP { get { return _amountHP; } private set { } }

        public new void Show()
        {
            base.Show();
            Console.WriteLine($"Восполняет очки жизни на {_amountHP} единиц.");
        }
    }

    class Weapon : Subject
    {
        private int _maxDamage;
        private int _minDamage;

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
        private List<Weapon> _weapons;
        private List<FoodManna> _foodsManna;
        private List<FoodHP> _foodsHP;

        public Player()
        {
            _weapons = new List<Weapon>(10);
            _foodsHP = new List<FoodHP>(50);
            _foodsManna = new List<FoodManna>(50);
        }

        public void PurchaseWeapons(Weapon weapon)
        {
            _weapons.Add(weapon);
        }

        public void PurchaseFoodManna(FoodManna foodManna)
        {
            _foodsManna.Add(foodManna);
        }

        public void PurchaseFoodHP(FoodHP foodHp)
        {
            _foodsHP.Add(foodHp);
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

        private string[] oneHandedWeapon = { "OneHandedSword", "OneHandedAxe", "OneHandedHammer" };
        private string[] twoHandedWeapon = { "TwoHandedSword", "TwoHandedAxe", "TwoHandedHammer" };
        private string[] foodsNameHP = { "Apple", "Pear", "Bread" };
        private string[] foodsNameManna = { "Water", "Milk", "Tea" };

        private List<Weapon> _weapons;
        private List<FoodManna> _foodsManna;
        private List<FoodHP> _foodsHP;
        private Random _random = new Random();

        public Salesman()
        {
            _weapons = new List<Weapon>();

            for (int i = 0; i < oneHandedWeapon.Length; i++)
            {
                int lowerMinDamage = 1;
                int upperMinDamage = 3;
                int lowerMaxDamage = 4;
                int upperMaxDamage = 6;
                int minDamage = _random.Next(lowerMinDamage, upperMinDamage);
                int maxDamage = _random.Next(lowerMaxDamage, upperMaxDamage);
                _weapons.Add(new Weapon(maxDamage, minDamage, PriceOneWeapon, oneHandedWeapon[i]));
            }

            for (int i = 0; i < twoHandedWeapon.Length; i++)
            {
                int lowerMinDamage = 10;
                int upperMinDamage = 13;
                int lowerMaxDamage = 14;
                int upperMaxDamage = 16;
                int minDamage = _random.Next(lowerMinDamage, upperMinDamage);
                int maxDamage = _random.Next(lowerMaxDamage, upperMaxDamage);
                _weapons.Add(new Weapon(maxDamage, minDamage, PriceTwoWeapon, twoHandedWeapon[i]));
            }

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

        public void SaleWeapon(int number)
        {
            _weapons.RemoveAt(number);
        }

        public void SaleFoodHP(int number)
        {
            _foodsHP.RemoveAt(number);
        }

        public void SaleFoodManna(int number)
        {
            _foodsManna.RemoveAt(number);
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
    }

    class Bargaining
    {
        private const string CommandWeapon = "weapons";
        private const string CommandFoodManna = "manna";
        private const string CommandFoodHP = "hp";
        private const string CommandExit = "exit";

        private Player _player;
        private Salesman _salesman;

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
                Console.Write("Введите команду: ");

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

        private void TradinHP(List<FoodHP> foodsHP)
        {
            bool isWork = true;
            int serialNumber = 0;

            Console.WriteLine("Представляю вам список товара: ");

            foreach (var product in foodsHP)
            {
                Console.WriteLine($"Порядковый номер: {serialNumber}");
                product.Show();
                serialNumber++;
            }

            Console.WriteLine("Для покупки товара введите порядковый номер!\n" +
                              "Для выхода из этого меню введите любую букву.");

            while (isWork)
            {
                Console.Write($"Порядковый номер: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (number < foodsHP.Count)
                    {
                        _player.PurchaseFoodHP(foodsHP[number]);
                        _salesman.SaleFoodHP(number);
                    }
                    else
                    {
                        Console.WriteLine("Товара с таким номером нет.");
                    }
                }
                else
                {
                    isWork = false;
                }
            }
        }

        private void TradinManna(List<FoodManna> foodsManna)
        {
            bool isWork = true;
            int serialNumber = 0;

            Console.WriteLine("Представляю вам список товара: ");

            foreach (var product in foodsManna)
            {
                Console.WriteLine($"Порядковый номер: {serialNumber}");
                product.Show();
                serialNumber++;
            }

            Console.WriteLine("Для покупки товара введите порядковый номер!\n" +
                              "Для выхода из этого меню введите любую букву.");

            while (isWork)
            {
                Console.Write($"Порядковый номер: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (number < foodsManna.Count)
                    {
                        _player.PurchaseFoodManna(foodsManna[number]);
                        _salesman.SaleFoodManna(number);
                    }
                    else
                    {
                        Console.WriteLine("Товара с таким номером нет.");
                    }
                }
                else
                {
                    isWork = false;
                }
            }
        }

        private void TradinWeapon(List<Weapon> weapons)
        {
            bool isWork = true;
            int serialNumber = 0;

            Console.WriteLine("Представляю вам список товара: ");

            foreach (var product in weapons)
            {
                Console.WriteLine($"Порядковый номер: {serialNumber}");
                product.Show();
                serialNumber++;
            }

            Console.WriteLine("Для покупки товара введите порядковый номер!\n" +
                              "Для выхода из этого меню введите любую букву.");

            while (isWork)
            {
                Console.Write($"Порядковый номер: ");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (number < weapons.Count)
                    {
                        _player.PurchaseWeapons(weapons[number]);
                        _salesman.SaleWeapon(number);
                    }
                    else
                    {
                        Console.WriteLine("Товара с таким номером нет.");
                    }
                }
                else
                {
                    isWork = false;
                }
            }
        }
    }
}
