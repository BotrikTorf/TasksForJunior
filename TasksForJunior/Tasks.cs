using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {


        }
    }
    class Player
    {
        private int _level;

        public string FullNamePlayer { get; private set; }

        public bool IsBanPlaeyer { get; private set; }

        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value > 0 && value <= 100)
                    _level = value;
                else
                    _level = 1;
            }
        }

        public Player(string name, int level, bool isBan)
        {
            FullNamePlayer = name;
            Level = level;
            IsBanPlaeyer = isBan;
        }

        public void IsBan(bool isBan)
        {
            IsBanPlaeyer = isBan;
        }

        public void FullName(string fullName)
        {
            FullNamePlayer = fullName;
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
       
        private int _randomKeyNumber
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
            _playerBase.Add(_randomKeyNumber, player2);
        }

        public void AddPlayer(string name, int level, bool isBan)
        {
            Player player = new Player(name, level, isBan);
            _playerBase.Add(_randomKeyNumber, player);
        }

        public void SetsBun(int serialNumber)
        {
            if (_playerBase.ContainsKey(serialNumber))
                _playerBase[serialNumber].IsBan(true);
            else
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
        }

        public void RemovesBun(int serialNumber)
        {
            if (_playerBase.ContainsKey(serialNumber))
                _playerBase[serialNumber].IsBan(false);
            else
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
        }

        public void DeletesPlayer(int serialNumber)
        {
            if (_playerBase.ContainsKey(serialNumber))
                _playerBase.Remove(serialNumber);
            else
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
        }
        public void ShowBase()
        {
            int serialNumber = 1;

            foreach (var player in _playerBase)
            {
                Console.WriteLine($"Порядковый номер: {serialNumber}\n" +
                    $"Индивидуальный номер: {player.Key}\n" +
                    $"ФИО игрока: {player.Value.FullNamePlayer}\n" +
                    $"Уровень игрока: {player.Value.Level}\n" +
                    $"Игрок забанен: {player.Value.IsBanPlaeyer}\n");
                serialNumber++;
            }
        }


    }
}
