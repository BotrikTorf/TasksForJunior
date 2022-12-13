using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            CarService carService = new CarService();
            carService.ServiceCar(new Car("engine", 100f));
            carService.ServiceCar(new Car("sleeve", 20f));
            carService.ServiceCar(new Car("sleeve", 10f));
            carService.ServiceCar(new Car("sleeve", 150f));
            carService.ServiceCar(new Car("wipers", 100f));
            carService.ServiceCar(new Car("tyre", 100f));
            carService.ServiceCar(new Car("cardan shaft", 100f));
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

        public void ServiceCar(Car car)
        {
            Console.WriteLine("Добро пожаловать в автосервис!\n" +
                $"Вам требуется заменить деталь {car.NamePart}.\n" +
                "Проверим есть ли такая запчасть на складе: ");

            if (_stock.HaveSparePart(car.NamePart))
            {
                Part part = _stock.TakePart(car.NamePart);
                float serviceCost = part.CostWork + part.Price;

                Console.WriteLine($"Была заменена деталь: {part.Name}.\n" +
                                  $"Стоимость детали: {part.Price}.\n" +
                                  $"Стоимость работы: {part.CostWork}.\n" +
                                  $"И того с вас: {serviceCost}");

                if (car.CanPay(serviceCost))
                {
                    _money += serviceCost;
                    car.Pay(serviceCost);
                }
            }
            else
            {
                float fine = 20;

                Console.WriteLine("Приносим свои извинения, но такой запчасти нет на складе. " +
                                  $"Вам будет выплачена сумма равная: {fine}");
                car.AcceptMoney(fine);
                _money -= fine;
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

        public bool HaveSparePart(string name)
        {
            bool haveSparePart = false;

            foreach (var container in _containers)
            {
                if (container.Name == name && container.Amount > 0)
                {
                    haveSparePart = true;
                    Console.WriteLine("Запчасть имеется.");
                }
            }

            return haveSparePart;
        }

        public Part TakePart(string name)
        {
            Part getPart = null;

            foreach (var container in _containers)
            {
                if (container.Name == name)
                {
                    getPart = container.Part;
                    container.ReduceNumberParts();
                }
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

        public string Name => Part.Name;

        public Part Part { get; }

        public int Amount { get; private set; }

        public void ReduceNumberParts() => Amount--;
    }

    class Part
    {
        private float _factorWork = 0.2f;

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

        public void AcceptMoney(float money) => Money += money;

        public bool CanPay(float money) => Money >= money;

        public void Pay(float money) => Money -= money;
    }
}
