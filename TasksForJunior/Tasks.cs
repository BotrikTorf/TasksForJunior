using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Terminal terminal = new Terminal();
            terminal.WorkStart();
        }
    }

    class Direction
    {
        public Direction(string stationStart = null, string stationEnd = null)
        {
            StationStart = stationStart;
            StationEnd = stationEnd;
        }

        public string StationStart { get; }
        public string StationEnd { get; }
    }

    class Wagon
    {
        public Wagon(string title, int numberSeats)
        {
            NumberSeats = numberSeats;
            Title = title;
        }

        public int NumberSeats { get; }
        public string Title { get; }
    }

    class Train
    {
        private List<Wagon> _wagons;
        public Train(string route, int capacity, int numberPassengers, int number, List<Wagon> wagons)
        {
            Route = route;
            Capacity = capacity;
            NumberPassengers = numberPassengers;
            Number = number;
            _wagons = wagons;
        }

        public string Route { get; }
        public int Capacity { get; }
        public int NumberPassengers { get; }
        public int Number { get; }

        public void Show()
        {
            Console.WriteLine($"{Number} - номер поезда\n" +
                              $"{Route} - маршрут поезда\n" +
                              $"{Capacity} - вместимость поезда\n" +
                              $"{NumberPassengers} - в поезд село посажиров" +
                              "В состав поезда входят вагоны:");

            foreach (var wagon in _wagons)
            {
                Console.WriteLine($"{wagon.Title} - вагон, в него помещается - {wagon.Title} пассажиров.");
            }
        }
    }

    class Terminal
    {
        private List<Train> _trains = new List<Train>();
        private Random _random = new Random();

        public void WorkStart()
        {
            string commandExit = "exit";
            bool isWork = true;
            int minNamberTicket = 0;
            int maxNamberTicket = 500;

            Console.WriteLine("Добро пожаловать в терминал.");

            while (isWork)
            {
                if (_trains.Count < 1)
                    Console.WriteLine("Вы не отправили ни одного поезда");
                else
                    ShowTrains();

                int ticketsSales = _random.Next(minNamberTicket, maxNamberTicket);

                Direction direction = CreateDirection();

                Console.WriteLine($"Вы выбрали направление {direction.StationStart} - {direction.StationEnd}");
                Console.WriteLine($"Вы продали {ticketsSales}");
                List<Wagon> wagons = CreatesListWagons(ticketsSales);
                SendTrain(ticketsSales, direction, wagons);

                Console.Write($"Если вы хотите выйти из программы то введите слово {commandExit}: ");
                isWork = Console.ReadLine() != commandExit;
            }
        }

        private Direction CreateDirection()
        {
            List<string> stations = new List<string>
            {
                "Minsk", "Brest", "Vitebsk", "Gomel", "Grodno", "Mogilev",
                "Baranovichi", "Orsha", "Polotsk", "Osipovichi", "Kalinkovichi"
            };
            string stationStart;
            string stationEnd;

            ShowStations(stations);

            do
            {
                Console.WriteLine("Выберете город из списка с которого начинается маршрут:");
                stationStart = SelectStation(stations);
                Console.WriteLine("Выберете город из списка на котором заканчиватся маршрут:");
                stationEnd = SelectStation(stations);

                if (stationStart == stationEnd)
                {
                    Console.WriteLine("Вы выбрали один и тот же город или такого города нет. Маршрут не может быть создан.");
                }
            }
            while (stationStart == stationEnd);

            return new Direction(stationStart, stationEnd);
        }

        private string SelectStation(List<string> stations)
        {
            string station = null;
            bool isEnteredStantionStart = true;

            while (isEnteredStantionStart)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    if (number < stations.Count)
                    {
                        isEnteredStantionStart = false;
                        station = stations[number];
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели номер станции. Попробуйте еще раз.");
                }
            }

            return station;
        }

        private void ShowStations(List<string> stations)
        {
            int serialNumber = 0;

            foreach (var station in stations)
            {
                Console.WriteLine($"{serialNumber}. {station}");
                serialNumber++;
            }
        }

        private void SendTrain(int ticketsSales, Direction direction, List<Wagon> wagons)
        {
            string commandAccept = "yes";
            int seats = 0;

            Console.WriteLine("К отправке готов поезд:\n" +
                              $"Номер поезда {_trains.Count}\n" +
                              $"Следующий по маршруту {direction.StationStart} - {direction.StationEnd}\n" +
                              "Состоящий из вагонов:");

            foreach (var wagon in wagons)
            {
                seats += wagon.NumberSeats;

                Console.WriteLine($"{wagon.Title} - вагон, в него помещается - {wagon.Title} пассажиров.");
            }

            Console.Write($"Вместимость поезда {seats}\n" +
                              $"В поезд сядет {ticketsSales} пасажиров.\n" +
                              $"Если все данные верны введите {commandAccept}: ");

            if (Console.ReadLine().ToLower() == commandAccept)
            {
                Train train = new Train(direction.StationStart + " - " + direction.StationEnd,
                    seats, ticketsSales, _trains.Count, wagons);
                _trains.Add(train);
            }
            else
            {
                Console.WriteLine("Отмена вышесозданного поезда.");
            }
        }

        private List<Wagon> CreatesListWagons(int ticketsSales)
        {
            List<Wagon> wagons = new List<Wagon>();
            int seats;

            Dictionary<string, int> wagonsDictionary = new Dictionary<string, int>()
            {
                { "Suite", 24 },
                { "SV", 32 },
                { "Compartment", 36 },
                { "Econom-class", 38 },
                { "Sedentary", 68 }
            };

            Console.WriteLine("У вас есть в наличии вагоны:");

            foreach (var wagon in wagonsDictionary)
            {
                Console.WriteLine($"{wagon.Key} на {wagon.Value} мест");
            }

            do
            {
                wagons.Add(SelectWagon(wagonsDictionary));
                seats = 0;

                foreach (Wagon wagon in wagons)
                {
                    seats += wagon.NumberSeats;
                }
            }
            while (seats < ticketsSales);

            return wagons;
        }

        private Wagon SelectWagon(Dictionary<string, int> wagonsDictionary)
        {
            bool isChoice = true;
            string wagonName = null;
            int numberSeats = 0;

            Console.WriteLine("Какой тип вагонов вы хотите выбрать? Ведите название:");

            while (isChoice)
            {
                wagonName = Console.ReadLine();

                foreach (var wagon in wagonsDictionary)
                {
                    if (wagon.Key == wagonName)
                    {
                        numberSeats = wagon.Value;
                        isChoice = false;
                    }
                }

                if (isChoice)
                {
                    Console.WriteLine("Вы неправильно ввели название типа вагона. Введите заново.");
                }
            }

            return new Wagon(wagonName, numberSeats);
        }

        private void ShowTrains()
        {
            foreach (var train in _trains)
            {
                Console.WriteLine("Вы отправили:");
                train.Show();
            }
        }
    }
}