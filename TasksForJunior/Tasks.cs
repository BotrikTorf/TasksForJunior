using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandExit = "exit";
            const string CommandLookBase = "look";
            const string CommandBanPlayer = "ban";
            const string CommandUnbanPlayer = "unban";
            const string CommandDeletePlayer = "delete";
            const string CommandAddPlayer = "add";
            bool isWork = true;

            Database database = new Database();

            Console.WriteLine("Добро пожаловать в базу данных игроков!");
            Console.WriteLine($"Для работы с базой данных игроков используются команды:\n" +
                              $"{CommandExit} - выходит из программы\n" +
                              $"{CommandLookBase} - данная команда позваляет просмотреть всю базу данных по игрокам\n" +
                              $"{CommandBanPlayer} - данная команда ставит бан по индивидуальному номеру игрока\n" +
                              $"{CommandUnbanPlayer} - данная команда снимает бан по индивидуальному номеру игрока\n" +
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
                        database.Show();
                        break;
                    case CommandBanPlayer:
                        database.BanPlayer();
                        break;
                    case CommandUnbanPlayer:
                        database.UnbanPlayer();
                        break;
                    case CommandDeletePlayer:
                        database.DeletesPlayer();
                        break;
                    case CommandAddPlayer:
                        database.AddPlayer();
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
        static private int _idNamber = 0;
        public readonly int MaxLevel = 100;
        public readonly int MinLevel = 1;
        private int _level;
        public string FullName { get; private set; }
        public bool IsBan { get; private set; }
        public int IdNumber { get; private set; }
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value >= MinLevel && value <= MaxLevel)
                    _level = value;
                else
                    _level = MinLevel;
            }
        }

        public Player(string name, int level = 1, bool isBan = false)
        {
            FullName = name;
            Level = level;
            IsBan = isBan;
            _idNamber++;
            IdNumber = _idNamber;
        }

        public void Ban()
        {
            IsBan = true;
        }

        public void Unban()
        {
            IsBan = false;
        }

        public void Show()
        {
            Console.WriteLine($"Индивидуальный номер: {IdNumber}\n" +
                              $"ФИО игрока: {FullName}\n" +
                              $"Уровень игрока: {_level}\n" +
                              $"Игрок забанен: {IsBan}\n");
        }
    }

    class Database
    {
        private List<Player> _playersBase;

        public Database()
        {
            _playersBase = new List<Player>();
        }

        public void AddPlayer()
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

        public void BanPlayer()
        {
            Console.WriteLine("Введите индивидуальный номер игрока которого хотите забанить:");

            if (TryGetPlayer(Console.ReadLine(), out Player player))
            {
                Console.WriteLine("Вы забанили игрока:");
                player.Ban();
                player.Show();
                _playersBase.Add(player);
            }
        }

        public void UnbanPlayer()
        {
            Console.WriteLine("Введите индивидуальный номер игрока которого хотите разбанить:");

            if (TryGetPlayer(Console.ReadLine(), out Player player))
            {
                Console.WriteLine("Вы разбанили игрока:");
                player.Unban();
                player.Show();
                _playersBase.Add(player);
            }
        }

        public void DeletesPlayer()
        {
            Console.WriteLine("Введите индивидуальный номер игрока которого хотите удалить:");

            if (TryGetPlayer(Console.ReadLine(), out Player player))
            {
                Console.WriteLine("Вы удалили игрока:");
                player.Show();
            }
        }

        private bool TryGetPlayer(string idNamber, out Player player)
        {
            player = null;

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
                _playersBase.Remove(player);
                return true;
            }
        }

        public void Show()
        {
            int serialNumber = 1;

            if (_playersBase.Count > 0)
            {
                foreach (var player in _playersBase)
                {
                    Console.WriteLine($"Порядковый номер: {serialNumber}");
                    player.Show();
                    serialNumber++;
                }
            }
            else
            {
                Console.WriteLine("В базе нет игроков!");
            }

        }
    }
}
