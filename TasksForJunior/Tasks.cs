using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    enum City
    {
        Минск,
        Брест,
        Витебск,
        Гомель,
        Гродно,
        Могилев,
        Барановичи,
        Орша,
        Полоцк,
        Осиповичи,
        Калинковичи
    }

    enum WagonCapacity
    {
        Малый = 40,
        Средний = 50,
        Большой = 60
    }

    class Direction
    {
        private string _stationStart = null;
        private string _stationEnd = null;
        private readonly List<String> _stations;

        public Direction()
        {
            _stations = new List<string>();

            foreach (var city in Enum.GetValues(typeof(City)))
                _stations.Add(city.ToString());
        }

        public string StationStart { get { return _stationStart; } private set { } }
        public string StationEnd { get { return _stationEnd; } private set { } }

        public void Show()
        {
            int serialNumber = 0;

            foreach (var station in _stations)
            {
                Console.WriteLine($"{serialNumber}. {station}");
                serialNumber++;
            }
        }

        public bool SetStartStation(int number)
        {
            if (number < _stations.Count)
            {
                _stationStart = _stations[number];
                _stations.RemoveAt(number);
                return false;
            }
            else
            {
                Console.WriteLine("Такой станции нет.");
                return true;
            }
        }

        public bool SetEndStation(int number)
        {
            if (number < _stations.Count)
            {
                _stationEnd = _stations[number];
                _stations.RemoveAt(number);
                return false;
            }
            else
            {
                Console.WriteLine("Такой станции нет.");
                return true;
            }
        }
    }

    class Ticket
    {
        private const int MinNamberTickets = 0;
        private const int MaxNamberTickets = 1000;
        private readonly Random _random = new Random();

        public int SoldTickets() => _random.Next(MinNamberTickets, MaxNamberTickets);
    }

    class Train
    {
        private readonly Dictionary<int, string> _wagons = new Dictionary<int, string>();

        public Train()
        {
            foreach (var wagon in Enum.GetValues(typeof(WagonCapacity)))
            {
                _wagons.Add((int)wagon, wagon.ToString());
            }
        }

        public int TrainComposition { get; private set; }
        public int WagonsUsedTrain { get; private set; }
        public string NameTrainComposition { get; private set; }

        public void Show()
        {
            Console.WriteLine("У вас есть в наличии вагоны:");

            foreach (var wagon in _wagons)
            {
                Console.WriteLine($"{wagon.Key} вместимостью {wagon.Value} человек");
            }
        }

        public void CreationTrainComposition(int tickeds)
        {
            bool isCreation = true;

            while (isCreation)
            {
                Console.Write("Ведите вместимость вагона из которых вы будете формировать состав:");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (_wagons.ContainsKey(result))
                    {
                        isCreation = false;
                        TrainComposition = (int)Math.Truncate((double)tickeds / result) + 1;
                        WagonsUsedTrain = result;
                        NameTrainComposition = _wagons[result];
                    }
                    else
                    {
                        Console.WriteLine($"Вагона с {result} посадочными местами нет! Повторите попытку.");
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели число.");
                }
            }
        }
    }

    class Terminal
    {
        private int _tickeds;
        private readonly Ticket _ticket = new Ticket();
        private Direction _direction;
        private Train _train;

        public void WorkStart()
        {
            bool isWork = true;

            Console.WriteLine("Добро пожаловать в терминал.");

            while (isWork)
            {
                _direction = new Direction();

                SetStartingStation(_direction);
                SetEndStation(_direction);
                Console.WriteLine($"Вы выбрали направление {_direction.StationStart} - {_direction.StationEnd}");
                _tickeds = _ticket.SoldTickets();
                Console.WriteLine($"Вы продали {_tickeds}");

                _train = new Train();

                _train.Show();
                _train.CreationTrainComposition(_tickeds);
                Console.WriteLine($"Для посадки {_tickeds} пасажиров сформирован состав из {_train.TrainComposition} {_train.NameTrainComposition} вагонов. " +
                                  $"В каждом вагоне {_train.WagonsUsedTrain} посадочных мест.\n");
                isWork = Work();
            }
        }

        private void SetStartingStation(Direction direction)
        {
            Console.WriteLine("Выберете город из списка с которого начинается маршрут:");
            direction.Show();

            bool isEnteredStantionStart = true;

            while (isEnteredStantionStart)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                    isEnteredStantionStart = direction.SetStartStation(number);
                else
                    Console.WriteLine("Вы не ввели номер станции. Попробуйте еще раз.");
            }
        }

        private void SetEndStation(Direction direction)
        {
            bool isEnteredStantionEnd = true;

            Console.WriteLine("Выберете город из списка на котором заканчиватся маршрут:");
            direction.Show();

            while (isEnteredStantionEnd)
            {
                if (int.TryParse(Console.ReadLine(), out int number))
                    isEnteredStantionEnd = direction.SetEndStation(number);
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
