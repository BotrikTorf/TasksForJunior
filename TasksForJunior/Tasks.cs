using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Database database = new Database();
            database.Work();
        }
    }

    class Player
    {
        static private int _idLastNumber = 0;
        private int _maxLevel = 100;
        private int _minLevel = 1;
        private int _level;

        public Player(string name, int level = 1)
        {
            FullName = name;
            Level = level;
            IsBaned = false;
            _idLastNumber++;
            IdNumber = _idLastNumber;
        }

        public string FullName { get; private set; }
        public bool IsBaned { get; private set; }
        public int IdNumber { get; private set; }
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value >= _minLevel && value <= _maxLevel)
                    _level = value;
                else
                    _level = _minLevel;
            }
        }

        public void Ban()
        {
            IsBaned = true;
        }

        public void Unban()
        {
            IsBaned = false;
        }

        public void Show()
        {
            Console.WriteLine($"Индивидуальный номер: {IdNumber}\n" +
                              $"ФИО игрока: {FullName}\n" +
                              $"Уровень игрока: {_level}\n" +
                              $"Игрок забанен: {IsBaned}\n");
        }
    }

    class Database
    {
        private List<Player> _playersBase;

        public Database()
        {
            _playersBase = new List<Player>();
        }

        public void Work()
        {
            const string CommandExit = "exit";
            const string CommandLookBase = "look";
            const string CommandBanPlayer = "ban";
            const string CommandUnbanPlayer = "unban";
            const string CommandDeletePlayer = "delete";
            const string CommandAddPlayer = "add";
            bool isWork = true;

            Console.WriteLine("Добро пожаловать в базу данных игроков!");
            Console.WriteLine($"Для работы с базой данных игроков используются команды:\n" +
                              $"{CommandExit} - выходит из программы\n" +
                              $"{CommandLookBase} - посмотреть всех игроков\n" +
                              $"{CommandBanPlayer} - поставить бан игроку\n" +
                              $"{CommandUnbanPlayer} - снять бан у игрока\n" +
                              $"{CommandDeletePlayer} - удалить игрока\n" +
                              $"{CommandAddPlayer} - добовить игрока в базу данных.");

            while (isWork)
            {
                Console.WriteLine("Введите команду:");

                switch (Console.ReadLine())
                {
                    case CommandExit:
                        isWork = false;
                        break;
                    case CommandLookBase:
                        Show();
                        break;
                    case CommandBanPlayer:
                        BanPlayer();
                        break;
                    case CommandUnbanPlayer:
                        UnbanPlayer();
                        break;
                    case CommandDeletePlayer:
                        DeletesPlayer();
                        break;
                    case CommandAddPlayer:
                        AddPlayer();
                        break;
                    default:
                        Console.WriteLine("Вы не правельно ввели команду!");
                        break;
                }
            }
        }

        private void AddPlayer()
        {
            Console.Write("Введите ФИО игрока которого хотите добавить: ");
            string fullName = Console.ReadLine();
            Console.Write($"Введите начальный уровень игрока, если уровень игрока не будет введен, то ему присвоется 1 уровень : ");
            int.TryParse(Console.ReadLine(), out int level);

            Player player = new Player(fullName, level);

            Console.WriteLine("Вы добавили игрока:");
            player.Show();
            _playersBase.Add(player);
        }

        private void BanPlayer()
        {
            Console.Write("Введите индивидуальный номер игрока которого хотите забанить: ");

            if (TryGetPlayer(out Player player))
            {
                Console.WriteLine("Вы забанили игрока:");
                player.Ban();
                player.Show();
            }
        }

        private void UnbanPlayer()
        {
            Console.Write("Введите индивидуальный номер игрока которого хотите разбанить: ");

            if (TryGetPlayer(out Player player))
            {
                Console.WriteLine("Вы разбанили игрока:");
                player.Unban();
                player.Show();
            }
        }

        private void DeletesPlayer()
        {
            Console.Write("Введите индивидуальный номер игрока которого хотите удалить: ");

            if (TryGetPlayer(out Player player))
            {
                player.Show();
                _playersBase.Remove(player);
            }
        }

        private void Show()
        {
            if (_playersBase.Count > 0)
            {
                for (int i = 0; i < _playersBase.Count; i++)
                {
                    Console.WriteLine($"Порядковый номер: {i + 1}");
                    _playersBase[i].Show();
                }
            }
            else
            {
                Console.WriteLine("В базе нет игроков!");
            }
        }

        private bool TryGetPlayer(out Player player)
        {
            player = null;
            string idNamber = Console.ReadLine();

            if (int.TryParse(idNamber, out int result))
            {
                foreach (var playerBase in _playersBase)
                {
                    if (playerBase.IdNumber == result)
                    {
                        player = playerBase;
                    }
                }
            }
            else
            {
                Console.WriteLine("Вы не ввели индивидуальный номер игрока!");
            }

            if (player == null)
            {
                Console.WriteLine($"Игрока с индивидуальным {idNamber} не существует");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

