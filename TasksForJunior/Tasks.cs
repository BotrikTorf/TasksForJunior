using System;


namespace TasksForJunior
{


    class Tasks
    {
        static void Main()
        {
            User user1 = new User(1, 3);
            User user2 = new User();
            Renderer unitDraw = new Renderer();

            unitDraw.RendererUser(user2, '@');
            Console.ReadKey();
            unitDraw.RendererUser(user1, '#');
            Console.ReadKey();
            user2.SetCoordinates(5, 1000);
            unitDraw.RendererUser(user2, '@');
            Console.ReadKey();
        }
    }

    class User
    {
        public int CoordinateX { get; private set; }

        public int CoordinateY { get; private set; }

        public User()
        {
            CoordinateX = 0;
            CoordinateX = 0;
        }

        public User(int coordinateX, int coordinateY)
        {
            SetCoordinates(coordinateX, coordinateY);
        }

        public void SetCoordinates(int coordinateX, int coordinateY)
        {
            if (ChecksPositions(coordinateX, coordinateY))
            {
                CoordinateX = coordinateX;
                CoordinateY = coordinateY;
            }
            else
            {
                CoordinateX = 0;
                CoordinateY = 0;
            }
        }

        private bool ChecksPositions(int coordinateX, int coordinateY)
        {
            if (coordinateX >= 0 && coordinateX < Console.WindowWidth && coordinateY >= 0 && coordinateY < Console.WindowHeight)
            {
                return true;
            }
            else
            {
                Console.SetCursorPosition(0, 10);
                Console.WriteLine($"Координаты указаны не верно. Будут установлены координаты: CoordinateX = 0; CoordinateY = 0");
                return false;
            }
        }
    }

    class Renderer
    {
        public void RendererUser(User user, char picture)
        {
            Console.ReadKey();
            Console.Clear();
            Console.SetCursorPosition(user.CoordinateX, user.CoordinateY);
            Console.Write(picture);
        }
    }
}
