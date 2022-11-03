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

    enum City
    {
        Minsk,
        Brest,
        Vitebsk,
        Gomel,
        Grodno,
        Mogilev,
        Baranovichi,
        Orsha,
        Polotsk,
        Osipovichi,
        Kalinkovichi
    }

    enum WagonCapacity
    {
        Small,
        Average,
        Big
    }

    class Direction
    {
        private readonly List<String> _stations;

        public Direction()
        {
            _stations = new List<string>();

            foreach (var city in Enum.GetValues(typeof(City)))
                _stations.Add(city.ToString());
        }

        public string StationStart { get; private set; } = null;
        public string StationEnd { get; private set; } = null;

        public void Show()
        {
            int serialNumber = 0;

            foreach (var station in _stations)
            {
                Console.WriteLine($"{serialNumber}. {station}");
                serialNumber++;
            }
        }

        public bool SetStation(int number)
        {
            if (StationStart == null && number < _stations.Count)
            {
                StationStart = Station(number);
                return false;
            }
            else if (number < _stations.Count)
            {
                StationEnd = Station(number);
                return false;
            }
            else
            {
                Console.WriteLine("Такой станции нет.");
                return true;
            }
        }

        private string Station(int number)
        {
            string tempStetion = _stations[number];
            _stations.RemoveAt(number);
            return tempStetion;
        }
    }

    class Wagon
    {
        private int _minsize = 40;
        public Wagon(string size = null)
        {
            foreach (var wagon in Enum.GetValues(typeof(WagonCapacity)))
            {
                if (size == wagon.ToString())
                    Size = _minsize + (int)wagon * 10;
            }
        }

        public int Size { get; } = 0;
    }

    class Train
    {
        public Train(string route = null, int capacity = 0, int numberPassengers = 0, int item = 0)
        {
            Route = route;
            Capacity = capacity;
            NumberPassengers = numberPassengers;
            Item = item;
        }

        public string Route { get; }
        public int Capacity { get; }
        public int NumberPassengers { get; }
        public int Item { get; }

        public void Show()
        {
            Console.WriteLine($"{Item} - номер поезда\n" +
                              $"{Route} - маршрут поезда\n" +
                              $"{Capacity} - вместимость поезда\n" +
                              $"{NumberPassengers} - в поезд село посажиров");
        }
    }

    class Terminal
    {
        private Direction _direction;
        private List<Train> _trains = new List<Train>();
        private Wagon _wagon;
        private Random _random = new Random();

        public void WorkStart()
        {
            bool isWork = true;

            Console.WriteLine("Добро пожаловать в терминал.");

            while (isWork)
            {
                if (_trains.Count < 1)
                    Console.WriteLine("Вы не отправили ни одного поезда");
                else
                    ShowTrain();

                int minNamberTicket = 0;
                int maxNamberTicket = 1000;
                int ticketsSales = _random.Next(minNamberTicket, maxNamberTicket);

                _direction = new Direction();

                SetStartingStation(_direction);
                SetEndStation(_direction);
                Console.WriteLine($"Вы выбрали направление {_direction.StationStart} - {_direction.StationEnd}");
                Console.WriteLine($"Вы продали {ticketsSales}");
                ChoiceWagons();
                SendTrain(ticketsSales);
                isWork = Work();
            }
        }

        private void SendTrain(int ticketsSales)
        {
            int numberWagons = MinWagonCount(ticketsSales);

            Console.WriteLine("К отправке готов поезд:\n" +
                              $"Номер поезда {_trains.Count}\n" +
                              $"Следующий по маршруту {_direction.StationStart} - {_direction.StationEnd}\n" +
                              $"Вместимость поезда {numberWagons * _wagon.Size}\n" +
                              $"В поезд сядет {ticketsSales} пасажиров.\n");
            Console.Write("Если все данные верны введите yes: ");

            if (Console.ReadLine() == "yes")
            {
                Train train = new Train(_direction.StationStart + " - " + _direction.StationEnd, numberWagons * _wagon.Size,
                    ticketsSales, _trains.Count);
                _trains.Add(train);
            }
            else
            {
                Console.WriteLine("Отмена вышесозданного поезда.");
            }
        }

        private int MinWagonCount(int ticketsSales)
        {
            return (int)Math.Truncate((double)ticketsSales / _wagon.Size) + 1;
        }

        private void ChoiceWagons()
        {
            Console.WriteLine("У вас есть в наличии вагоны:");

            foreach (var wagon in Enum.GetValues(typeof(WagonCapacity)))
            {
                Console.WriteLine(wagon.ToString());
            }

            Console.WriteLine("Какой тип вагонов вы хотите выбрать? Ведите название:");

            bool isChoice = false;

            while (!isChoice)
            {
                _wagon = new Wagon(Console.ReadLine());

                if (_wagon.Size != 0)
                    isChoice = true;
            }
        }

        private void ShowTrain()
        {
            foreach (var train in _trains)
            {
                Console.WriteLine("Вы отправили:");
                train.Show();
            }
        }

        private void SetStartingStation(Direction direction)
        {
            Console.WriteLine("Выберете город из списка с которого начинается маршрут:");
            SetStation(direction);
        }

        private void SetEndStation(Direction direction)
        {
            Console.WriteLine("Выберете город из списка на котором заканчиватся маршрут:");
            SetStation(direction);
        }

        private void SetStation(Direction direction)
        {
            direction.Show();

            bool isEnteredStantionStart = true;

            while (isEnteredStantionStart)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                    isEnteredStantionStart = direction.SetStation(number);
                else
                    Console.WriteLine("Вы не ввели номер станции. Попробуйте еще раз.");
            }
        }

        private bool Work()
        {
            string exit = "exit";

            Console.Write($"Если вы хотите выйти из программы то введите слово {exit}: ");
            return Console.ReadLine() != exit;
        }
    }
}
