using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Supermarket supermarket = new Supermarket();
            supermarket.ServeCustomers();
        }
    }

    class Supermarket
    {
        private Queue<Buyer> _buyers;
        private Random _random = new Random();

        public Supermarket()
        {
            _buyers = CreateQueueCustomer();
        }

        public void ServeCustomers()
        {
            int numberBuyer = 1;

            while (_buyers.Count > 0)
            {
                Console.WriteLine($"В очереде {_buyers.Count} покупателей.");
                Buyer buyer = _buyers.Dequeue();
                Console.WriteLine($"У {numberBuyer} покупателя {buyer.Money} монет. Он взял товар для покупки:");
                buyer.ShowProducts();

                while (buyer.CanMadePay == false)
                {
                    Console.WriteLine("У покупателя не хватило денег. Он удалил один рандомный товар.");
                    buyer.RemoveRandomProduct();
                    Console.WriteLine("Остался товар для покупки:");
                    buyer.ShowProducts();
                }

                buyer.BuyProducts();
                Console.WriteLine("У покупателя хватило денег на все товары. Он удаляется из очереди.");
                numberBuyer++;
            }
        }

        private Queue<Buyer> CreateQueueCustomer()
        {
            int minLengthQueue = 3;
            int maxLengthQueue = 5;
            int lengthQueue = _random.Next(minLengthQueue, maxLengthQueue);
            Queue<Buyer> buyers = new Queue<Buyer>();

            for (int i = 0; i < lengthQueue; i++)
                buyers.Enqueue(new Buyer(CreateListShopping()));

            return buyers;
        }

        private List<Product> CreateListShopping()
        {
            List<Product> initialProductList = new List<Product>
            {
                {new Product("Apple", 1)},
                {new Product("Pear", 2)},
                {new Product("Bread", 3)},
                {new Product("Water", 4)},
                {new Product("Milk", 5)},
                {new Product("Tea", 6)},
                {new Product("Sword", 7)},
                {new Product("Axe", 8)},
                {new Product("Hammer", 9)},
            };

            List<Product> products = new List<Product>();
            int minNumberProducts = 0;
            int maxNumberProducts = 5;
            int numberProducts = _random.Next(minNumberProducts, maxNumberProducts);

            for (int i = 0; i <= numberProducts; i++)
                products.Add(initialProductList[_random.Next(0, initialProductList.Count)]);

            return products;
        }
    }

    class Buyer
    {
        private List<Product> _products = new List<Product>();
        private Random _random = new Random();

        public Buyer(List<Product> products)
        {
            int minMoney = 8;
            int maxMoney = 15;
            Money = _random.Next(minMoney, maxMoney);
            _products = products;
        }

        public int Money { get; private set; }

        public int CostAllProducts
        {
            get
            {
                int totalCost = 0;

                foreach (var product in _products)
                    totalCost += product.Price;

                return totalCost;
            }
        }

        public bool CanMadePay { get { return CostAllProducts <= Money; } }

        public void ShowProducts()
        {
            foreach (var product in _products)
                Console.WriteLine($"{product.Name} стоит {product.Price}");
        }

        public void BuyProducts() => Money -= CostAllProducts;

        public void RemoveRandomProduct() => _products.Remove(_products[_random.Next(0, _products.Count)]);
    }

    class Product
    {
        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; }

        public int Price { get; }
    }
}
