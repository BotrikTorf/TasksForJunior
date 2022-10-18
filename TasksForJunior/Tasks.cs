using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandExit = "exit";
            const string CommandLookBase = "look";
            const string CommandBunPlayer = "bun";
            const string CommandUnbunPlayer = "unbun";
            const string CommandDeletePlayer = "delete";
            const string CommandAddPlayer = "add";
            bool isWork = true;

            DataBase dataBase = new DataBase();

            string[] playersName = new string[10];
            Player[] players = new Player[10];

            for (int i = 0; i < 10; i++)
            {
                playersName[i] = "player" + i.ToString();
                players[i] = new Player(playersName[i], i, false);
                dataBase.AddPlayer(players[i]);
            }

            Console.WriteLine("Добро пожаловать в базу данных игроков!");
            Console.WriteLine($"Для работы с базой данных игроков используются команды:\n" +
                              $"{CommandExit} - выходит из программы\n" +
                              $"{CommandLookBase} - данная команда позваляет просмотреть всю базу данных по игрокам\n" +
                              $"{CommandBunPlayer} - данная команда ставит бан по индивидуальному номеру игрока\n" +
                              $"{CommandUnbunPlayer} - данная команда снимает бан по индивидуальному номеру игрока\n" +
                              $"{CommandDeletePlayer} - команда удаляет игрока по индивидуальному номеру\n" +
                              $"{CommandAddPlayer} - добовляет игрока в базу данных.");

            while (isWork)
            {
                Console.WriteLine("Введите команду:");

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isWork = false;
                        break;
                    case CommandLookBase:
                        dataBase.Show();
                        break;
                    case CommandBunPlayer:
                        dataBase.SetsBun();
                        break;
                    case CommandUnbunPlayer:
                        dataBase.RemovesBun();
                        break;
                    case CommandDeletePlayer:
                        dataBase.DeletesPlayer();
                        break;
                    case CommandAddPlayer:
                        dataBase.AddPlayer();
                        break;
                    default:
                        Console.WriteLine("Вы не правельно ввели команду!");
                        break;
                }
            }
        }
    }
    class Player
    {
        private const int MaxLevel = 100;
        private int _level;
        public string FullName { get; private set; }
        public bool IsBan { get; private set; }
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value > 0 && value <= MaxLevel)
                    _level = value;
                else
                    _level = 1;
            }
        }

        public Player()
        {
            FullName = null;
            Level = 0;
            IsBan = true;
        }

        public Player(string name, int level, bool isBan)
        {
            FullName = name;
            Level = level;
            IsBan = isBan;
        }

        public void IsPutBan()
        {
            IsBan = true;
        }

        public void IsRemoveBan()
        {
            IsBan = false;
        }

        public void NewsFullName(string fullName)
        {
            FullName = fullName;
        }

        public void Show()
        {
            Console.WriteLine($"Фио игрока: {FullName}\n" +
                              $"Уровень игрока: :{_level}\n" +
                              $"Игрок забанен: {IsBan}\n");
        }
    }

    class DataBase
    {
        private Dictionary<int, Player> _playerBase;
        private Random _random = new Random();
        private int RandomKeyNumber
        {
            get
            {
                bool thereRepetitions = true;
                int idNumber = 0;

                while (thereRepetitions)
                {
                    idNumber = _random.Next(1000, 10000);
                    thereRepetitions = _playerBase.ContainsKey(idNumber);
                }

                return idNumber;
            }
        }

        public DataBase()
        {
            _playerBase = new Dictionary<int, Player>();
        }

        public void AddPlayer(Player player2)
        {
            _playerBase.Add(RandomKeyNumber, player2);
        }

        public void AddPlayer(string name, int level, bool isBan)
        {
            Player player = new Player(name, level, isBan);
            _playerBase.Add(RandomKeyNumber, player);
        }

        public void AddPlayer()
        {
            Player player;
            Console.Write("Введите ФИО игрока которого хотите добавить: ");
            string fullName = Console.ReadLine();
            int level = 0;
            bool isNumber = false;

            while (isNumber == false)
            {
                Console.Write("Введите начальный уровень игрока: ");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    level = result;
                    isNumber = true;
                }
                else
                {
                    Console.WriteLine("Вы не правильно ввели число попробуйте еще раз.");
                }
            }

            player = new Player(fullName, level, false);
            Console.WriteLine("Вы добавили игрока:");
            player.Show();
            _playerBase.Add(RandomKeyNumber, player);
        }

        public void SetsBun()
        {
            bool isBun = false;

            while (isBun == false)
            {
                Console.WriteLine("Введите индивидуальный номер игрока которого хотите забанить:");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (_playerBase.ContainsKey(result))
                    {
                        Console.WriteLine("Вы забанили игрока:");
                        _playerBase[result].IsPutBan();
                        _playerBase[result].Show();
                        isBun = true;
                    }
                    else
                    {
                        Console.WriteLine($"Игрока с индивидуальным {result} не существует");
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели индивидуальный номер игрока!");
                }
            }
        }

        public void RemovesBun()
        {
            bool isBun = false;

            while (isBun == false)
            {
                Console.WriteLine("Введите индивидуальный номер игрока которого хотите разбанить:");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (_playerBase.ContainsKey(result))
                    {
                        Console.WriteLine("Вы разбанили игрока:");
                        _playerBase[result].IsRemoveBan();
                        _playerBase[result].Show();
                        isBun = true;
                    }
                    else
                    {
                        Console.WriteLine($"Игрока с индивидуальным {result} не существует");
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели индивидуальный номер игрока!");
                }
            }
        }

        public void DeletesPlayer()
        {
            bool isDelete = false;

            while (isDelete == false)
            {
                Console.WriteLine("Введите индивидуальный номер игрока которого хотите удалить:");

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (_playerBase.ContainsKey(result))
                    {
                        Console.WriteLine("Вы удалили игрока:");
                        _playerBase[result].Show();
                        _playerBase.Remove(result);
                        isDelete = true;
                    }
                    else
                    {
                        Console.WriteLine($"Игрока с индивидуальным {result} не существует");
                    }
                }
                else
                {
                    Console.WriteLine("Вы не ввели индивидуальный номер игрока!");
                }
            }
        }

        public void Show()
        {
            int serialNumber = 1;

            foreach (var player in _playerBase)
            {
                Console.WriteLine($"Порядковый номер: {serialNumber}\n" +
                                  $"Индивидуальный номер: {player.Key}");
                player.Value.Show();
                serialNumber++;
            }
        }
    }
}
