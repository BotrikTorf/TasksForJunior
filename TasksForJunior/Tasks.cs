﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            DateTime time = DateTime.Today;
            Stock stock = new Stock();

            stock.ShowExpiredProduct(time.Year);
        }
    }

    class Stock
    {
        private List<Product> products;
        public Stock() => products = CreateListProducts();

        public void ShowExpiredProduct(int year)
        {
            var expiredProduct = products.Where(product => (product.Year + product.ExpirationDate) < year);

            for (int i = 0; i < expiredProduct.Count(); i++)
            {

                Console.Write($"{i + 1:000}. ");
                products[i].Show();
            }
        }

        private List<Product> CreateListProducts()
        {
            List<Product> products = new List<Product>();
            int count = 1000;
            int minYear = 1999;
            int maxYear = 2020;
            int minExpirationDate = 2;
            int maxExpirationDate = 10;
            Random random = new Random();
            List<string> names = new List<string>
                { "Тушенка из говядины", "Тушенка из свинины", "Мясные консервы из курицы", "Маринованая кукуруза" , "Рыбные консервы"};

            for (int i = 0; i < count; i++)
                products.Add(new Product(names[random.Next(0, names.Count)],
                    random.Next(minYear, maxYear), random.Next(minExpirationDate, maxExpirationDate)));

            return products;
        }
    }

    class Product
    {
        public Product(string name, int year, int expirationDate)
        {
            Name = name;
            Year = year;
            ExpirationDate = expirationDate;
        }

        public string Name { get; }

        public int Year { get; }

        public int ExpirationDate { get; }

        public void Show()
        {
            Console.WriteLine($"Название: {Name},\t " +
                              $"произведено: {Year},\t " +
                              $"срок годности: {ExpirationDate},\t " +
                              $"срок годности закончился: {Year + ExpirationDate}");
        }
    }
}
