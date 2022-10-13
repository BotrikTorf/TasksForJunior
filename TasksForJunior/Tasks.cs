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
            Renderer drrawUnit = new Renderer();

            drrawUnit.RendererUser(user1, '#');
            Console.ReadKey();
            drrawUnit.RendererUser(user2, '@');
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

    class Renderer
    {
        public void RendererUser(User user, char picture)
        {
            Console.Clear();
            Console.SetCursorPosition(user.CoordinateY, user.CoordinateY);
            Console.Write(picture);
        }
    }
}
