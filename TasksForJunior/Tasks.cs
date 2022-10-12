using System;


namespace TasksForJunior
{


    class Tasks
    {
        static void Main()
        {
            User user1 = new User(1, 3);
            User user2 = new User();
            user2.SetCoordinates(5, 10);
            DrawUnit drrawUnit = new DrawUnit();

            drrawUnit.AddUser(user1, '$');
            drrawUnit.ShowUser();
            Console.ReadKey();
            drrawUnit.AddUser(user2, '@');
            drrawUnit.ShowUser();
            Console.ReadKey();

        }
    }

    class User
    {
        private int _coordinateX;
        private int _coordinateY;

        public int CoordinateX
        {
            get
            {
                return _coordinateX;
            }
            private set
            {
                if (value > 0 && value <= Console.WindowWidth)
                {
                    _coordinateX = value;
                }
            }
        }
        public int CoordinateY
        {
            get
            {
                return _coordinateY;
            }
            private set
            {
                if (value > 0 && value <= Console.WindowHeight)
                {
                    _coordinateY = value;
                }
            }
        }
        public User()
        {
            _coordinateX = 0;
            _coordinateX = 0;
        }

        public User(int x, int y)
        {
            CoordinateX = x;
            CoordinateY = y;
        }

        public void SetCoordinates(int x, int y)
        {
            CoordinateX = x;
            CoordinateY = y;
        }
    }

    class DrawUnit
    {
        private char _picture;

        public char Picture
        {
            get
            {
                return _picture;
            }
            private set
            {
                if (value != ' ')
                {
                    _picture = value;
                }
            }
        }

        public User User;

        public DrawUnit()
        {
            User = new User();
            _picture = ' ';
        }

        public DrawUnit(User user, char picture)
        {
            User = user;
            Picture = picture;
        }

        public void AddUser(User user, char picture)
        {
            User = user;
            Picture = picture;
        }

        public void ShowUser()
        {
            Console.Clear();
            Console.SetCursorPosition(User.CoordinateY, User.CoordinateY);
            Console.Write(Picture);
        }
    }
}
