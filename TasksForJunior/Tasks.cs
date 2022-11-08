using System;
using System.Collections.Generic;
using System.Linq;

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
        public Wagon(string title = null, int numberSeats = 0)
        {
            NumberSeats = numberSeats;
            Title = title;
        }

        public int NumberSeats { get; }
        public string Title { get; }
    }

    class Train
    {
        public Train(string route = null, int capacity = 0, int numberPassengers = 0, int number = 0)
        {
            Route = route;
            Capacity = capacity;
            NumberPassengers = numberPassengers;
            Number = number;
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
                              $"{NumberPassengers} - в поезд село посажиров");
        }
    }

    class Terminal
    {
        private Direction direction;
        private List<Train> trains = new List<Train>();
        private Wagon wagon;
        private Random random = new Random();
        private int minNamberTicket = 0;
        private int maxNamberTicket = 500;

        public void WorkStart()
        {
            bool isWork = true;

            Console.WriteLine("Добро пожаловать в терминал.");

            while (isWork)
            {
                if (trains.Count < 1)
                    Console.WriteLine("Вы не отправили ни одного поезда");
                else
                    ShowTrains();

                int ticketsSales = random.Next(minNamberTicket, maxNamberTicket);

                direction = CreateDirection();

                Console.WriteLine($"Вы выбрали направление {direction.StationStart} - {direction.StationEnd}");
                Console.WriteLine($"Вы продали {ticketsSales}");
                wagon = ChoiceWagon();
                SendTrain(ticketsSales);

                Console.Write($"Если вы хотите выйти из программы то введите слово exit: ");
                isWork = Console.ReadLine() != "exit";
            }
        }

        private Direction CreateDirection()
        {
            List<string> stations = new List<string>()
            {
                "Minsk", "Brest", "Vitebsk", "Gomel", "Grodno", "Mogilev",
                "Baranovichi", "Orsha", "Polotsk", "Osipovichi", "Kalinkovichi"
            };
            string stationStart;
            string stationEnd;

            ShowStations(stations);

            Console.WriteLine("Выберете город из списка с которого начинается маршрут:");
            stationStart = SetStation(stations);
            Console.WriteLine("Выберете город из списка на котором заканчиватся маршрут:");
            stationEnd = SetStation(stations);

            while (stationStart == stationEnd)
            {
                Console.WriteLine("Вы выбрали один и тот же город или такого города нет. Маршрут не может быть создан.");
                Console.WriteLine("Выберете город из списка с которого начинается маршрут:");
                stationStart = SetStation(stations);
                Console.WriteLine("Выберете город из списка на котором заканчиватся маршрут:");
                stationEnd = SetStation(stations);
            }

            return new Direction(stationStart, stationEnd);
        }

        private string SetStation(List<string> stations)
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

        private void SendTrain(int ticketsSales)
        {
            int numberWagons = MinWagonCount(ticketsSales);

            Console.WriteLine("К отправке готов поезд:\n" +
                              $"Номер поезда {trains.Count}\n" +
                              $"Следующий по маршруту {direction.StationStart} - {direction.StationEnd}\n" +
                              $"Вместимость поезда {numberWagons * wagon.NumberSeats}\n" +
                              $"В поезд сядет {ticketsSales} пасажиров.\n");
            Console.Write("Если все данные верны введите yes: ");

            if (Console.ReadLine() == "yes")
            {
                Train train = new Train(direction.StationStart + " - " + direction.StationEnd, numberWagons * wagon.NumberSeats,
                    ticketsSales, trains.Count);
                trains.Add(train);
            }
            else
            {
                Console.WriteLine("Отмена вышесозданного поезда.");
            }
        }

        private int MinWagonCount(int ticketsSales)
        {
            return (int)Math.Truncate((double)ticketsSales / wagon.NumberSeats) + 1;
        }

        private Wagon ChoiceWagon()
        {
            Dictionary<string, int> wagons = new Dictionary<string, int>()
            {
                { "Suite", 24 },
                { "SV", 32 },
                { "Compartment", 36 },
                { "Econom-class", 38 },
                { "Sedentary", 68 }
            };

            Console.WriteLine("У вас есть в наличии вагоны:");

            foreach (var wagon in wagons)
            {
                Console.WriteLine($"{wagon.Key} на {wagon.Value} мест");
            }

            Console.WriteLine("Какой тип вагонов вы хотите выбрать? Ведите название:");

            bool isChoice = true;
            string wagonName = null;
            int numberSeats = 0;

            while (isChoice)
            {
                 wagonName = Console.ReadLine();

                foreach (var wagon in wagons)
                {
                    if (wagon.Key == wagonName)
                    {
                        numberSeats = wagon.Value;
                        isChoice = false;
                        break;
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
            foreach (var train in trains)
            {
                Console.WriteLine("Вы отправили:");
                train.Show();
            }
        }
    }
}
