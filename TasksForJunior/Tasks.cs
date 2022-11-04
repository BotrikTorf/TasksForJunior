using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Player player = new Player(10);
            Seller seller = new Seller(-1);
            Shop shop = new Shop(player, seller);
            shop.Trade();
        }
    }
    enum ProductNamePrice
    {
        sword = 7,
        axe = 8,
        hammer = 9,
        apple = 1,
        pear = 2,
        bread = 3,
        water = 4,
        milk = 5,
        tea = 6
    }

    abstract class Peolple
    {
        protected Peolple(int money) => Money = money;

        public int Money { get; private set; } = 0;

        protected int GiveMoney(int requestedAmount)
        {
            if (requestedAmount < Money)
            {
                Money -= requestedAmount;
                return requestedAmount;
            }
            else
            {
                return 0;
            }
        }

        protected void TakeMoney(int money) => Money += money;
    }

    class Player : Peolple
    {
        private List<Cell> _cells;

        public Player(int money = 0) : base(money) => _cells = new List<Cell>();

        public int Money => base.Money;

        public void TakeProduct(Cell product)
        {
            bool haveProduct = false;

            foreach (var cell in _cells)
            {
                if (cell.Name == product.Name)
                {
                    haveProduct = true;
                    cell.TakeProduct();
                }
            }

            if (!haveProduct)
            {
                _cells.Add(product);
            }
        }

        public new void TakeMoney(int money) => base.TakeMoney(money);

        public new int GiveMoney(int requestedAmount) => base.GiveMoney(requestedAmount);

        public Cell GiveProduct(string name)
        {
            Cell tempProduct = null;

            foreach (var product in _cells)
            {
                if (product.Name == name)
                {
                    if (product.Amount>1)
                    {
                        product.SetProduct();
                        tempProduct = new Cell(product.Name, product.Cost, 1);
                    }
                    else
                    {
                        tempProduct = new Cell(product.Name, product.Cost, 1);
                        _cells.Remove(product);
                    }

                    break;
                }
            }

            return tempProduct;
        }
        public void ShowThings()
        {
            if (_cells.Count > 0)
                foreach (var product in _cells)
                    Console.WriteLine($"Наименование товара: {product.Name}, цена товара: {product.Cost}, количество {product.Amount}");
            else
                Console.WriteLine("Вещей нет.");
        }
    }

    class Seller : Peolple
    {
        private List<Cell> _cells;
        private Random _random = new Random();
        private int _minNumberProduct = 2;
        private int _maxNumberProduct = 6;

        public Seller(int money = 0) : base(money)
        {
            _cells = new List<Cell>();

            foreach (var product in Enum.GetValues(typeof(ProductNamePrice)))
            {
                _cells.Add(new Cell(product.ToString(), (int)product, _random.Next(_minNumberProduct, _maxNumberProduct)));
            }
        }

        public int Money => base.Money;

        public new void TakeMoney(int money) => base.TakeMoney(money);

        public new int GiveMoney(int requestedAmount) => base.GiveMoney(requestedAmount);

        public Cell GiveProduct(string name)
        {
            Cell tempCell = null;

            foreach (var cell in _cells)
            {
                if (cell.Name == name)
                {
                    tempCell = cell;
                    break;
                }
            }

            if (tempCell != null)
            {
                if (tempCell.Amount>0)
                {
                    tempCell.SetProduct();
                    tempCell = new Cell(tempCell.Name, tempCell.Cost, 1);
                }
                else
                {
                    Console.WriteLine($"{name} на продажу нет нет");
                }
            }
            else
            {
                Console.WriteLine($"С таким {name} названием товара нет.");
            }

            return tempCell;
        }

        public void TakeProduct(Cell cell)
        {
            foreach (var product in _cells)
            {
                if (cell.Name == product.Name)
                {
                    product.TakeProduct();
                    break;
                }  
            }
        }

        public void Show()
        {
            foreach (var cell in _cells)
            {
                if (cell.Amount != 0)
                {
                    Console.WriteLine($"Наименование - {cell.Name}.\n" +
                                      $"Стоимость - {cell.Cost}.\n" +
                                      $"Количество - {cell.Amount}.");
                }
            }
        }
    }

    class Product
    {
        private string _name;
        private int _cost;
        public Product(string name = null, int cost = 0)
        {
            _name = name;
            _cost = cost;
        }

        public string Name { get => _name; }
        public int Cost { get => _cost; }
    }

    class Cell : Product
    {
        public Cell(string name = null, int cost = 0, int amount = 0) : base(name, cost) => Amount = amount;

        public int Amount { get; private set; }
        public string Name => base.Name;
        public int Cost => base.Cost;

        public void TakeProduct() => Amount++;

        public void SetProduct()
        {
            if (Amount>0)
                Amount--;

        }
    }
    class Shop
    {
        private Player _player = new Player();
        private Seller _seller = new Seller();
        private Cell _cell;

        public Shop(Player player, Seller seller)
        {
            _player = player;
            _seller = seller;
        }

        public void Trade()
        {
            const string CommandExit = "exit";
            const string CommandShowProduct = "show";
            const string CommandBuy = "buy";
            const string CommandSell = "sell";
            const string CommandViewBalance = "balance";
            const string CommandShowThings = "player";

            bool isTrade = true;

            Console.WriteLine("Добро пожаловать в наш маленький магазинчик!\n" +
                              $"{CommandShowProduct} - посмотреть товар.\n" +
                              $"{CommandBuy} - покупка товара.\n" +
                              $"{CommandSell} - продажа товара.\n" +
                              $"{CommandExit} - прекращениe торговли.\n" +
                              $"{CommandViewBalance} - просмотр баланса у продовца и игрока.\n" +
                              $"{CommandShowThings} - показать вещи игрока.");

            while (isTrade)
            {
                Console.Write("Введите команду: ");

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isTrade = false;
                        break;
                    case CommandShowProduct:
                        _seller.Show();
                        break;
                    case CommandBuy:
                        Buy();
                        break;
                    case CommandSell:
                        Sell();
                        break;
                    case CommandViewBalance:
                        ViewBalance();
                        break;
                    case CommandShowThings:
                        _player.ShowThings();
                        break;
                    default:
                        Console.WriteLine("Вы не правельно ввели команду! Попробуйте еще раз");
                        break;
                }
            }
        }

        private void ViewBalance() => Console.WriteLine($"Баланс денег у игрока: {_player.Money}.\n" +
                                                        $"Баланс денег у продовца: {_seller.Money}.");

        private void Sell()
        {
            Console.Write("Ведите название товара: ");

            Cell product = _player.GiveProduct(Console.ReadLine());

            if (product == null)
            {
                Console.WriteLine("Такого товара нет! Вы ввели неправельно нозвание товара или у игрока такого товара нет.");
            }
            else if (_seller.GiveMoney(product.Cost) == 0)
            {
                _player.TakeProduct(product);
                Console.WriteLine("У продовца не хватает денег.");
            }
            else
            {
                _seller.TakeProduct(product);
                _player.TakeMoney(product.Cost);
            }
        }

        private void Buy()
        {
            Console.Write("Ведите название товара: ");

            _cell = _seller.GiveProduct(Console.ReadLine());

            if (_cell== null)
            {
                Console.WriteLine("Такого товара нет! В магазине скупили весь товар или вы ввели неправильно название товара.");
            }
            else if (_player.GiveMoney(_cell.Cost) == 0)
            {
                _seller.TakeProduct(_cell);
                Console.WriteLine("У игрока не хватает денег.");
            }
            else
            {
                _player.TakeProduct(_cell);
                _seller.TakeMoney(_cell.Cost);
            }
        }
    }
}
