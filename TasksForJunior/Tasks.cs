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
        private string _fullName;
        private int _level;
        private bool _isBan;

        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }

        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                if (value >= 0)
                {
                    _level = value;
                }
            }
        }

        public bool IsBan
        {
            get
            {
                return _isBan;
            }
            set
            {
                _isBan = value;
            }
        }

        public Player(string name, int level, bool isBan)
        {
            FullName = name;
            Level = level;
            IsBan = isBan;
        }
    }

    class DataBase
    {
        Dictionary<int, Player> dataBase;
        Random random = new Random();

        public DataBase()
        {
            dataBase = new Dictionary<int, Player>();
        }
       
        private int RandomKeyNumber
        {
            get
            {
                bool thereRepetitions = true;
                int idNumber = 0;

                while (thereRepetitions)
                {
                    idNumber = random.Next(1000, 10000);
                    thereRepetitions = dataBase.ContainsKey(idNumber);
                }

                return idNumber;
            }
        }

        public void AddPlayer(Player player2)
        {
            dataBase.Add(RandomKeyNumber, player2);
        }

        public void AddPlayer(string name, int level, bool isBan)
        {
            Player player = new Player(name, level, isBan);
            dataBase.Add(RandomKeyNumber, player);
        }

        public void PutsInABun(int serialNumber)
        {
            if (dataBase.ContainsKey(serialNumber))
                dataBase[serialNumber].IsBan = true;
            else
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
        }

        public void PutsInTheBun(int serialNumber)
        {
            if (dataBase.ContainsKey(serialNumber))
                dataBase[serialNumber].IsBan = false;
            else
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
        }

        public void DeletesPlayer(int serialNumber)
        {
            if (dataBase.ContainsKey(serialNumber))
                dataBase.Remove(serialNumber);
            else
                Console.WriteLine($"Игрока с индивидуальным {serialNumber} не существует");
        }
        public void ShowBase()
        {
            int serialNumber = 1;

            foreach (var itemBase in dataBase)
            {
                Console.WriteLine($"Порядковый номер: {serialNumber}\n" +
                    $"Индивидуальный номер: {itemBase.Key}\n" +
                    $"ФИО игрока: {itemBase.Value.FullName}\n" +
                    $"Уровень игрока: {itemBase.Value.Level}\n" +
                    $"Игрок забанен: {itemBase.Value.IsBan}\n");
                serialNumber++;
            }
        }


    }
}
