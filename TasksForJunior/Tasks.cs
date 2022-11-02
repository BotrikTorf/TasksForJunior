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
        private int _money;

        public Peolple(int money) => _money = money;

        protected int Money { get { return _money; } private set { } }

        protected int GiveMoney(int requestedAmount)
        {
            if (requestedAmount < _money)
            {
                _money -= requestedAmount;
                return requestedAmount;
            }
            else
            {
                return 0;
            }
        }

        protected void TakeMoney(int money) => _money += money;
    }

    class Player : Peolple
    {
        private List<Product> _products;

        public Player(int money = 0) : base(money) => _products = new List<Product>();

        public int Money { get { return base.Money; } private set { } }

        public void TakeProduct(Product product) => _products.Add(product);

        public new void TakeMoney(int money) => base.TakeMoney(money);

        public new int GiveMoney(int requestedAmount) => base.GiveMoney(requestedAmount);
        
        public Product GiveProduct(string name)
        {
            Product temProduct = new Product();

            bool haveProduct = false;

            foreach (var product in _products)
            {
                if (product.Name == name)
                {
                    temProduct = product;
                    haveProduct = true;
                    break;
                }
            }

            if (haveProduct)
            {
                _products.Remove(temProduct);
                return temProduct;
            }
            else
            {
                return null;
            }
        }
        public void ShowThings()
        {
            if (_products.Count > 0)
                foreach (var product in _products)
                    Console.WriteLine($"Наименование товара: {product.Name}, цена товара: {product.Cost}");
            else
                Console.WriteLine("Вещей нет.");
        }
    }

    class Seller : Peolple
    {
        public Seller(int money = 0) : base(money) { }

        public int Money { get { return base.Money; } private set { } }

        public new void TakeMoney(int money) => base.TakeMoney(money);

        public new int GiveMoney(int requestedAmount) => base.GiveMoney(requestedAmount);
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

        public string Name { get { return _name; } private set { } }
        public int Cost { get { return _cost; } private set { } }
    }

    class Cell
    {
        private List<Product> _products = new List<Product>();
        public Cell()
        {
            foreach (var product in Enum.GetValues(typeof(ProductNamePrice)))
                _products.Add(new Product(product.ToString(), (int)product));
        }

        public Product GivProduct(string name)
        {
            Product temProduct = new Product();

            bool haveProduct = false;

            foreach (var product in _products)
            {
                if (product.Name == name)
                {
                    temProduct = product;
                    haveProduct = true;
                    break;
                }
            }

            if (haveProduct)
            {
                _products.Remove(temProduct);
                return temProduct;
            }
            else
            {
                return null;
            }
        }
        
        public void TakeProduct(Product product) => _products.Add(product);

        public void ShowThings()
        {
            if (_products.Count > 0)
                foreach (var product in _products)
                    Console.WriteLine($"Наименование товара: {product.Name}, цена товара: {product.Cost}");
            else
                Console.WriteLine("В магазине нет това. Все скупили.");
        }
    }

    class Shop
    {
        private Player _player = new Player();
        private Seller _seller = new Seller();
        private Cell _cell = new Cell();

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
                        _cell.ShowThings();
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

            Product product = _player.GiveProduct(Console.ReadLine());

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
                _cell.TakeProduct(product);
                _player.TakeMoney(product.Cost);
            }
        }

        private void Buy()
        {
            Console.Write("Ведите название товара: ");

            Product product = _cell.GivProduct(Console.ReadLine());

            if (product == null)
            {
                Console.WriteLine("Такого товара нет! В магазине скупили весь товар или вы ввели неправильно название товара.");
            }
            else if (_player.GiveMoney(product.Cost) == 0)
            {
                _cell.TakeProduct(product);
                Console.WriteLine("У игрока не хватает денег.");
            }
            else
            {
                _player.TakeProduct(product);
                _seller.TakeMoney(product.Cost);
            }
        }
    }
}
