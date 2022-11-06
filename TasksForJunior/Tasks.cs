using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Player player = new Player(10);
            Seller seller = new Seller(0);
            Shop shop = new Shop(player, seller);
            shop.Trade();
        }
    }

    abstract class Human
    {
        private readonly List<Cell> _cells;
        protected Human(int money)
        {
            _cells = new List<Cell>();
            Money = money;
        }

        public int Money { get; private set; } = 0;

        protected int GiveMoney(int requestedAmount)
        {
            if (requestedAmount < Money)
            {
                Money -= requestedAmount;
                return requestedAmount;
            }

            return 0;
        }

        protected void TakeMoney(int money) => Money += money;

        protected Item GiveProduct(string name)
        {
            Item tempProduct = null;

            foreach (var cell in _cells)
            {
                if (cell.NameItem == name)
                {
                    if (cell.Amount == 1)
                    {
                        tempProduct = new Item(cell.NameItem, cell.CostItem);
                        _cells.Remove(cell);
                    }
                    else
                    {
                        cell.ReduceNumberItem();
                        tempProduct = new Item(cell.NameItem, cell.CostItem);
                    }

                    break;
                }
            }

            return tempProduct;
        }

        public bool HaveItem(string name, out int cost)
        {
            foreach (var cell in _cells)
            {
                if (cell.NameItem == name)
                {
                    cost = cell.CostItem;
                    return true;
                }
            }

            cost = 0;
            return false;
        }

        protected void TakeProduct(Item item, int amount = 1)
        {
            bool haveProduct = false;

            foreach (var cell in _cells)
            {
                if (cell.NameItem == item.Name)
                {
                    haveProduct = true;
                    cell.IncreaseNumberItem();
                    break;
                }
            }

            if (haveProduct==false)
            {
                _cells.Add(new Cell(item.Name, item.Cost, amount));
            }
        }

        public void ShowProducts()
        {
            if (_cells.Count > 0)
            {
                foreach (var cell in _cells)
                    if (cell.Amount != 0)
                        Console.WriteLine($"Наименование товара: {cell.NameItem}, цена товара: {cell.CostItem}, количество {cell.Amount}");
            }
            else
            {
                Console.WriteLine("Вещей нет.");
            }
        }
    }

    class Player : Human
    {
        public Player(int money = 0) : base(money) { }

        public void BuyItem(Item item) => TakeProduct(item);

        public int GiveMoneyItem(int number) => GiveMoney(number);

        public bool ChecksMoney(int cost)
        {
            return cost <= Money;
        }
    }

    class Seller : Human
    {
        public Seller(int money = 0) : base(money)
        {
            Random random = new Random();
            int minNumberProduct = 2;
            int maxNumberProduct = 6;

            Dictionary<string, int> productsDictionary = new Dictionary<string, int>()
            {
                { "Apple", 1},
                { "Pear", 2},
                { "Bread", 3},
                { "Water", 4},
                { "Milk", 5},
                { "Tea", 6},
                { "Sword", 7},
                { "Axe", 8},
                { "Hammer", 9}
            };

            foreach (var product in productsDictionary)
            {
                Item item = new Item(product.Key, product.Value);
                base.TakeProduct(item, random.Next(minNumberProduct, maxNumberProduct));
            }
        }

        public Item SaleItem(string name) => GiveProduct(name);

        public void TakeMoneyItem(int number) => TakeMoney(number);
    }

    class Item
    {
        public Item(string name = null, int cost = 0)
        {
            Name = name;
            Cost = cost;
        }

        public string Name { get; }
        public int Cost { get; }
    }

    class Cell
    {
        private Item _item;

        public Cell(string name = null, int cost = 0, int amount = 0)
        {
            _item = new Item(name, cost);
            Amount = amount;
        }

        public int Amount { get; private set; }

        public string NameItem => _item.Name;

        public int CostItem => _item.Cost;

        public void IncreaseNumberItem() => Amount++;

        public void ReduceNumberItem()
        {
            if (Amount > 0)
                Amount--;
        }
    }
    class Shop
    {
        private Player _player = new Player();
        private Seller _seller = new Seller();

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
            const string CommandViewBalance = "balance";
            const string CommandShowThings = "player";

            bool isTrade = true;

            Console.WriteLine("Добро пожаловать в наш маленький магазинчик!\n" +
                              $"{CommandShowProduct} - посмотреть товар.\n" +
                              $"{CommandBuy} - покупка товара.\n" +
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
                        _seller.ShowProduct();
                        break;
                    case CommandBuy:
                        Buy();
                        break;
                    case CommandViewBalance:
                        ViewBalance();
                        break;
                    case CommandShowThings:
                        _player.ShowProduct();
                        break;
                    default:
                        Console.WriteLine("Вы не правельно ввели команду! Попробуйте еще раз");
                        break;
                }
            }
        }

        private void ViewBalance() => Console.WriteLine($"Баланс денег у игрока: {_player.Money}.\n" +
                                                        $"Баланс денег у продовца: {_seller.Money}.");

        private void Buy()
        {
            Console.Write("Ведите название товара: ");

            string name = Console.ReadLine().ToLower();

            if (name.Length >0)
            {
                name = name[0].ToString().ToUpper() + name.Substring(1);

                if (_seller.HaveItem(name, out int cost))
                {
                    if (_player.ChecksMoney(cost))
                    {
                        _seller.TakeMoneyItem(_player.GiveMoneyItem(cost));
                        _player.BuyItem(_seller.SaleItem(name));
                    }
                    else
                    {
                        Console.WriteLine("У игрока не хватает денег.");
                    }
                }
                else
                {
                    Console.WriteLine("Такого товара нет! В магазине скупили весь товар или вы ввели неправильно название товара.");
                }
            }
            else
            {
                Console.WriteLine("Вы ничего не ввели");
            }
        }
    }
}
