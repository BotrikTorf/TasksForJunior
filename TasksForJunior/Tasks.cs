using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static Random _random = new Random();
        static void Main()
        {
            Queue<Buyer> buyers = CreatingCustomerQueues();

            CalculateClients(buyers);
            Console.ReadLine();
        }

        private static void CalculateClients(Queue<Buyer> buyers)
        {
            int numberBuyer = 1;

            while (buyers.Count > 0)
            {
                Console.WriteLine($"В очереде {buyers.Count} покупателей.");
                Buyer buyer = buyers.Dequeue();
                Console.WriteLine($"У {numberBuyer} покупателя {buyer.Money} монет. Он взял товар для попукупки:");
                buyer.ShowProduct();

                if (buyer.ThisMadePayment(CalculateTotalCostGoods(buyer.SubmitProductList())))
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

                        if (buyer.ThisMadePayment(CalculateTotalCostGoods(buyer.SubmitProductList())))
                        {
                            Console.WriteLine("У покупателя хватило денег на все товары. Он удаляется из очереди.");
                            haveMoney = true;
                        }
                    }
                }

                numberBuyer++;
            }
        }

        private static int CalculateTotalCostGoods(List<Product> products)
        {
            int totalCost = 0;

            foreach (var product in products)
                totalCost += product.Price;

            return totalCost;
        }

        private static Queue<Buyer> CreatingCustomerQueues()
        {
            int minLengthQueue = 3;
            int maxLengthQueue = 5;
            int lengthQueue = _random.Next(minLengthQueue, maxLengthQueue);
            Queue<Buyer> buyers = new Queue<Buyer>();

            for (int i = 0; i < lengthQueue; i++)
                buyers.Enqueue(new Buyer(GreateShoppingList()));

            return buyers;
        }

        private static List<Product> GreateShoppingList()
        {
            List<Product> products = new List<Product>();
            int minNumberProducts = 0;
            int maxNumberProducts = 5;
            int numberProducts = _random.Next(minNumberProducts, maxNumberProducts);

            for (int i = 0; i <= numberProducts; i++)
                products.Add(NewProduct());

            return products;
        }

        private static Product NewProduct()
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
        List<Product> _products = new List<Product>();
        Random _random = new Random();
        public Buyer(List<Product> products)
        {
            int minMoney = 3;
            int maxMoney = 15;
            _money = _random.Next(minMoney, maxMoney);
            _products = products;
        }

        public int Money { get { return _money; } }

        public void ShowProduct()
        {
            foreach (var product in _products)
            {
                Console.WriteLine($"{product.Name} стоит {product.Price}");
            }
        }

        public bool ThisMadePayment(int sum)
        {
            if (sum <= _money)
            {
                _money -= sum;
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Product> SubmitProductList() => _products;

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
