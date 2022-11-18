using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Supermarket supermarket = new Supermarket();
            supermarket.CalculateClients();
        }
    }

    class Supermarket
    {
        private Queue<Buyer> _buyers;
        private Random _random = new Random();
        public Supermarket()
        {
            CreateCustomerQueue();
        }

        public void CalculateClients()
        {
            int numberBuyer = 1;

            while (_buyers.Count > 0)
            {
                Console.WriteLine($"В очереде {_buyers.Count} покупателей.");
                Buyer buyer = _buyers.Dequeue();
                Console.WriteLine($"У {numberBuyer} покупателя {buyer.Money} монет. Он взял товар для попукупки:");
                buyer.ShowProduct();

                if (buyer.CompletedPayment())
                {
                    Console.WriteLine("У покупателя хватило денег на все товары. Он удаляется из очереди.");
                }
                else
                {
                    bool haveMoney = false;

                    while (haveMoney == false)
                    {
                        Console.WriteLine("У покупателя не хватило денег. Он удалил один рандомный товар.");
                        buyer.RemoveRandomProduct();
                        Console.WriteLine("Остался товар для попукупки:");
                        buyer.ShowProduct();

                        if (buyer.CompletedPayment())
                        {
                            Console.WriteLine("У покупателя хватило денег на все товары. Он удаляется из очереди.");
                            haveMoney = true;
                        }
                    }
                }

                numberBuyer++;
            }
        }

        private void CreateCustomerQueue()
        {
            int minLengthQueue = 3;
            int maxLengthQueue = 5;
            int lengthQueue = _random.Next(minLengthQueue, maxLengthQueue);
            Queue<Buyer> buyers = new Queue<Buyer>();

            for (int i = 0; i < lengthQueue; i++)
                buyers.Enqueue(new Buyer(GreateShoppingList()));

            _buyers = buyers;
        }

        private List<Product> GreateShoppingList()
        {
            List<Product> products = new List<Product>();
            int minNumberProducts = 0;
            int maxNumberProducts = 5;
            int numberProducts = _random.Next(minNumberProducts, maxNumberProducts);

            for (int i = 0; i <= numberProducts; i++)
                products.Add(NewProduct());

            return products;
        }

        private Product NewProduct()
        {
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
            Product tempProduct = null;
            List<string> nameProduct = new List<string>();
            int numberProduct;

            foreach (var product in productsDictionary)
                nameProduct.Add(product.Key);

            numberProduct = _random.Next(0, nameProduct.Count);

            foreach (var product in productsDictionary)
                if (product.Key == nameProduct[numberProduct])
                {
                    tempProduct = new Product(product.Key, product.Value);
                    break;
                }

            return tempProduct;
        }
    }

    class Buyer
    {
        private int _money;
        private List<Product> _products = new List<Product>();
        Random _random = new Random();
        public Buyer(List<Product> products)
        {
            int minMoney = 8;
            int maxMoney = 15;
            _money = _random.Next(minMoney, maxMoney);
            _products = products;
        }

        public int Money { get { return _money; } }

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

        public void ShowProduct()
        {
            foreach (var product in _products)
            {
                Console.WriteLine($"{product.Name} стоит {product.Price}");
            }
        }

        public bool CompletedPayment()
        {
            if (CostAllProducts <= _money)
            {
                _money -= CostAllProducts;
                return true;
            }
            else
            {
                return false;
            }
        }

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
