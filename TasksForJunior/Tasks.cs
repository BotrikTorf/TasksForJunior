using System;


namespace TasksForJunior
{
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

    class Location
    {
        public User User;

        public Location()
        {
            User = new User();
        }

        public Location(User user)
        {
            User = user;
        }

        public void ShowUser()
        {
            Console.SetCursorPosition(User.CoordinateY, User.CoordinateY);
        }
    }

    class Tasks
    {
        static void Main()
        {
            User user1 = new User(1, 3);
            User user2 = new User();
            user2.SetCoordinates(5,10);
            Location location1 = new Location();
            Location location2 = new Location(user1);
            Location location3 = new Location(user2);

            location2.ShowUser();
            Console.ReadKey();
            location1.ShowUser();
            Console.ReadKey();
            location3.ShowUser();
        }
    }
}
