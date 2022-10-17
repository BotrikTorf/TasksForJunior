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
            bool isWork = true;

            string[] playersName = new string[10];
            Player[] players = new Player[10];
            DataBase dataBase = new DataBase();

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
                              $"{CommandDeletePlayer} - команда удаляет игрока по индивидуальному номеру\n");

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
                        BunPlayer(dataBase);
                        break;
                    case CommandUnbunPlayer:
                        UnbunPlayer(dataBase);
                        break;
                    case CommandDeletePlayer:
                        DeletesPlayer(dataBase);
                        break;
                    default:
                        Console.WriteLine("Вы не правельно ввели команду!");
                        break;
                }
            }
        }

        private static void DeletesPlayer(DataBase dataBase)
        {
            bool isDelete = false;

            while (isDelete == false)
            {
                Console.WriteLine("Введите индивидуальный номер игрока которого хотите разбанить:");

                if (int.TryParse(Console.ReadLine(), out int result))
                    isDelete = dataBase.DeletesPlayer(result);
                else
                    Console.WriteLine("Вы не ввели индивидуальный номер игрока!");
            }
        }

        private static void UnbunPlayer(DataBase dataBase)
        {
            bool isBun = false;

            while (isBun == false)
            {
                Console.WriteLine("Введите индивидуальный номер игрока которого хотите разбанить:");

                if (int.TryParse(Console.ReadLine(), out int result))
                    isBun = dataBase.RemovesBun(result);
                else
                    Console.WriteLine("Вы не ввели индивидуальный номер игрока!");
            }
        }

        private static void BunPlayer(DataBase dataBase)
        {
            bool isBun = false;

            while (isBun == false)
            {
                Console.WriteLine("Введите индивидуальный номер игрока которого хотите забанить:");

                if (int.TryParse(Console.ReadLine(), out int result))
                    isBun = dataBase.SetsBun(result);
                else
                    Console.WriteLine("Вы не ввели индивидуальный номер игрока!");
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

        public DataBase()
        {
            _playerBase = new Dictionary<int, Player>();
        }

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

        public void AddPlayer(Player player2)
        {
            _playerBase.Add(RandomKeyNumber, player2);
        }

        public void AddPlayer(string name, int level, bool isBan)
        {
            Player player = new Player(name, level, isBan);
            _playerBase.Add(RandomKeyNumber, player);
        }

        public bool SetsBun(int serialNumber)
        {
            if (_playerBase.ContainsKey(serialNumber))
            {
                Console.WriteLine("Вы забанили игрока:");
                _playerBase[serialNumber].IsPutBan();
                _playerBase[serialNumber].Show();
                return true;
            }
            else
            {
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
                return false;
            }
        }

        public bool RemovesBun(int serialNumber)
        {
            if (_playerBase.ContainsKey(serialNumber))
            {
                Console.WriteLine("Вы разбанили игрока:");
                _playerBase[serialNumber].IsRemoveBan();
                _playerBase[serialNumber].Show();
                return true;
            }
            else
            {
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
                return false;
            }

            
        }

        public bool DeletesPlayer(int serialNumber)
        {
            if (_playerBase.ContainsKey(serialNumber))
            {
                Console.WriteLine("Вы удалили игрока:");
                _playerBase[serialNumber].Show();
                _playerBase.Remove(serialNumber);
                return true;
            }
            else
            {
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
                return false;
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
