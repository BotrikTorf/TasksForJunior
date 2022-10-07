using System;
using System.IO;


namespace TasksForJunior
{
    class Tasks
    {
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
                Console.SetCursorPosition(mobPositionX, mobPositionY);
                Console.Write("@");
                ConsoleKeyInfo key = Console.ReadKey(true);

                ControlsCharacter(ref mobDirectioX, ref mobDirectioY, key);
                MovesMob(ref mobPositionX, ref mobPositionY, ref typeCell, mobDirectioX, mobDirectioY, mapGame);

                if (typeCell == "$")
                {
                    food++;
                }

                if (food == foodMax)
                {
                    ChangesMap(mapGame);
                    Console.Clear();
                    DrawMap(mapGame);
                    Console.SetCursorPosition(mobPositionX, mobPositionY);
                    Console.Write("@");
                    food++;
                }

                if (typeCell == "#")
                {
                    isPlaying = false;
                    ShowWinGame();
                }
            }

            Console.ReadLine();
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

        private static void ControlsCharacter(ref int mobDirectioX, ref int mobDirectioY, ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    mobDirectioX = 0;
                    mobDirectioY = -1;
                    break;
                case ConsoleKey.DownArrow:
                    mobDirectioX = 0;
                    mobDirectioY = 1;
                    break;
                case ConsoleKey.LeftArrow:
                    mobDirectioX = -1;
                    mobDirectioY = 0;
                    break;
                case ConsoleKey.RightArrow:
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
                Console.SetCursorPosition(mobPositionX, mobPositionY);
                Console.Write("@");
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
    }
}
