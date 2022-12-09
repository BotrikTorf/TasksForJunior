using System;
using System.Collections.Generic;
using System.Linq;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            CarService carService = new CarService();
            carService.Servis(new Car("engine", 100f));
            carService.Servis(new Car("sleeve", 20f));
            carService.Servis(new Car("sleeve", 10f));
            carService.Servis(new Car("sleeve", 150f));
            carService.Servis(new Car("wipers", 100f));
            carService.Servis(new Car("tyre", 100f));
            carService.Servis(new Car("cardan shaft", 100f));
        }
    }

    class CarService
    {
        private float _money;
        private Stock _stock;

        public CarService()
        {
            _money = 100f;
            _stock = new Stock();
        }

        public void Servis(Car car)
        {
            Part part = new Part(); 
            Console.WriteLine("Добро пожаловать в автосервис!\n" +
                $"Вам требуется заменить деталь {car.NamePart}.\n" +
                "Проверим есть ли такая деталь на складе: ");
            part = _stock.GetPart(car.NamePart);

            if (part.Name == null)
            {
                if (_money >= part.CostWork)
                {
                    Console.WriteLine("Приносим свои извинения но такой детали нет на складе. " +
                                      $"Вам будет выплачена сумма равная: {part.CostWork}");
                    car.GetMoney(part.CostWork);
                    _money -= part.CostWork;
                }
                else
                {
                    Console.WriteLine("Извините но в автосервисе нет денег чтобы вам выплатить неустойку");
                }
            }
            else
            {
                Console.WriteLine($"Была заменена деталь: {part.Name}.\n" +
                                  $"Стоимость детали: {part.Price}.\n" +
                                  $"Стоимость работы: {part.CostWork}.\n" +
                                  $"И того с вас: {part.CostWork + part.Price}");
                _money += car.GiveMoney(part.Price + part.CostWork);
            }

            Console.WriteLine("Спасибо что посетили наш сервис.\n");
        }
    }

    class Stock
    {
        private List<Container> _containers;

        public Stock()
        {
            _containers = new List<Container>();
            Create();
        }

        public Part GetPart(string name)
        {
            Part getPart = new Part();

            foreach (var container in _containers)
            {
                if (container.Name == name && container.Amount > 0)
                {
                    getPart = container.Part;
                    container.ReduceNumberParts();
                    Console.WriteLine("Деталь имеется.");
                }
            }

            if (getPart.Name == null)
            {
                Console.WriteLine("Детали нет.");
            }

            return getPart;
        }

        private void Create()
        {
            _containers.Add(new Container(new Part(100, "engine"), 3));
            _containers.Add(new Container(new Part(10, "sleeve"), 2));
            _containers.Add(new Container(new Part(20, "lever"), 5));
            _containers.Add(new Container(new Part(50, "brake pads"), 3));
            _containers.Add(new Container(new Part(40, "tyre"), 3));
            _containers.Add(new Container(new Part(5, "wipers"), 5));
        }
    }

    class Container
    {
        public Container(Part part, int amount)
        {
            Part = part;
            Amount = amount;
        }

        public string Name
        {
            get
            {
                return Part.Name;
            }
        }

        public Part Part { get; }

        public int Amount { get; private set; }

        public void ReduceNumberParts()
        {
            Amount--;
        }
    }

    class Part
    {
        private float _factorWork = 0.2f; 
        public Part()
        {
            Price = 0;
            Name = null;
            CostWork = 10;
        }
        public Part(float price, string name)
        {
            Price = price;
            Name = name;
            CostWork = price * _factorWork;
        }

        public float Price { get; }

        public string Name { get; }

        public float CostWork { get; }
    }

    class Car
    {
        public Car(string namePart, float money)
        {
            NamePart = namePart;
            Money = money;
        }

        public string NamePart { get; }

        public float Money { get; private set; }

        public void GetMoney(float money)
        {
            Money += money;
        }

        public float GiveMoney(float money)
        {
            if (Money >= money)
            {
                Money -= money;
                return money;
            }
            else
            {
                return 0f;
            }
        }
    }
}
