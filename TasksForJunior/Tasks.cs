using System;

namespace TasksForJunior
{
    class Tasks
    {
        const ConsoleKey Up = ConsoleKey.UpArrow;
        const ConsoleKey Down = ConsoleKey.DownArrow;
        const ConsoleKey Left = ConsoleKey.LeftArrow;
        const ConsoleKey Right = ConsoleKey.RightArrow;
        static void Main()
        {
            bool isPlaying = true;
            int mobPositionX = 37;
            int mobPositionY = 2;
            int mobDirectioX = 0;
            int mobDirectioY = 0;
            int food = 0;
            int foodMax;
            string typeCell = " ";

            string[] map =
                { "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%",
                  "%    ||     %%               %%        %",
                  "%    ||  $  %%        %%     %%        %",
                  "%    %%     %%    $   %%     %%        %",
                  "%    %%     %%        %%     %%        %",
                  "%    %%     %%        %%     %%   $    %",
                  "%    %%     %%        %%     %%        %",
                  "%    %%     %%   $    %%     %%        %",
                  "%    %%     %%        %%     %%        %",
                  "%    %%  $  %%        %%     %%        %",
                  "%    %%               %%               %",
                  "% #  %%               %%               %",
                  "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%" };

            string[,] mapGame = ConvertsArrayOneToTwoDimensional(map);
            foodMax = CountsFood(mapGame);
            DrawMap(mapGame);
            Console.CursorVisible = false;

            while (isPlaying)
            {
                DrawsCharacter(mobPositionX, mobPositionY);
                ConsoleKeyInfo key = Console.ReadKey(true);
                ReadsKeys(ref mobDirectioX, ref mobDirectioY, key);
                MovesMob(ref mobPositionX, ref mobPositionY, ref typeCell, mobDirectioX, mobDirectioY, mapGame);
                food = CountsFood(food, typeCell);
                OpensAccess(ref food, mobPositionX, mobPositionY, foodMax, mapGame);
                isPlaying = IsEndsGame(isPlaying, typeCell);
            }

            Console.ReadLine();
        }

        private static bool IsEndsGame(bool isPlaying, string typeCell)
        {
            if (typeCell == "#")
            {
                isPlaying = false;
                ShowWinGame();
            }

            return isPlaying;
        }

        static void OpensAccess(ref int food, int mobPositionX, int mobPositionY, int foodMax, string[,] mapGame)
        {
            if (food == foodMax)
            {
                ChangesMap(mapGame);
                Console.Clear();
                DrawMap(mapGame);
                DrawsCharacter(mobPositionX, mobPositionY);
                food++;
            }
        }

        private static int CountsFood(int food, string typeCell)
        {
            if (typeCell == "$")
            {
                food++;
            }

            return food;
        }

        private static void ChangesMap(string[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == "|" || map[i, j] == "$")
                    {
                        map[i, j] = " ";
                    }
                }
            }
        }

        private static void ReadsKeys(ref int mobDirectioX, ref int mobDirectioY, ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case Up:
                    mobDirectioX = 0;
                    mobDirectioY = -1;
                    break;
                case Down:
                    mobDirectioX = 0;
                    mobDirectioY = 1;
                    break;
                case Left:
                    mobDirectioX = -1;
                    mobDirectioY = 0;
                    break;
                case Right:
                    mobDirectioX = 1;
                    mobDirectioY = 0;
                    break;
            }
        }

        private static void MovesMob(ref int mobPositionX, ref int mobPositionY, ref string typeCell, int mobDirectioX, int mobDirectioY, string[,] map)
        {
            if (map[mobPositionY + mobDirectioY, mobPositionX + mobDirectioX] != "%")
            {
                Console.SetCursorPosition(mobPositionX, mobPositionY);
                Console.Write(" ");
                mobPositionX += mobDirectioX;
                mobPositionY += mobDirectioY;
                typeCell = map[mobPositionY, mobPositionX];
                DrawsCharacter(mobPositionX, mobPositionY);
            }
        }

        static void DrawMap(string[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

        static string[,] ConvertsArrayOneToTwoDimensional(string[] array)
        {
            string[,] arrayTemp = new string[array.Length, array[0].Length];

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    arrayTemp[i, j] = array[i][j].ToString();
                }
            }

            return arrayTemp;
        }

        static void ShowWinGame()
        {
            Console.Clear();
            Console.WriteLine("Вы прошли лабиринт!");
        }

        static int CountsFood(string[,] map)
        {
            int food = 0;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i,j] == "$")
                        food++;
                }
            }

            return food;
        }

        static void DrawsCharacter(int mmobPositionX, int mobPositionY)
        {
            Console.SetCursorPosition(mmobPositionX, mobPositionY);
            Console.Write("@");
        }
    }
}
