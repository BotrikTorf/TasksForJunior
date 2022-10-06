using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            bool isPlaying = true;
            int mobPositionX = 37; 
            int mobPositionY = 2;
            int mobDX = 0;
            int mobDY = 0;
            int food = 0;
            string typeCell = " ";

            string[] map =
                { "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%",
                  "%    %%     %%               %%        %",
                  "%    %%  $  %%        %%     %%        %",
                  "%    %%     %%    $   %%     %%        %",
                  "%    %%     %%        %%     %%        %",
                  "%    %%     %%        %%     %%   $    %",
                  "%    %%     %%        %%     %%        %",
                  "%    %%     %%   $    %%     %%        %",
                  "%    %%     %%        %%     %%        %",
                  "%    %%  $  %%        %%     %%        %",
                  "%    %%               %%               %",
                  "%    %%               %%               %",
                  "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%" };

            string[,] mapGame = ConvertsArrayOneToTwoDimensional(map);
            DrawMap(mapGame);
            Console.CursorVisible = false;

            while (isPlaying)
            {
                Console.SetCursorPosition(mobPositionX, mobPositionY);
                Console.Write("@");
                ConsoleKeyInfo key = Console.ReadKey(true);

                ControlsCharacter(ref mobDX, ref mobDY, key);
                MovesMob(ref mobPositionX, ref mobPositionY, ref typeCell, mobDX, mobDY, mapGame);
                CheckedFood(ref food, typeCell, mobPositionX, mobPositionY, mapGame);
                ShowWinGame(ref mobPositionX, ref mobPositionY, typeCell);

                if (mobPositionX == 0 && mobPositionY == 0)
                {
                    isPlaying = false;
                }
            }

            Console.ReadLine();
        }

        private static void CheckedFood(ref int food, string typeCell, int X, int Y, string[,] map)
        {
            if (typeCell == "$")
            {
                food++;
            }

            if (food == 5)
            {
                map[1, 5] = " ";
                map[1, 6] = " ";
                map[11, 2] = "#";

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] == "$")
                        {
                            map[i, j] = " ";
                        }
                    }
                }

                Console.SetCursorPosition(0, 0);
                DrawMap(map);
                Console.SetCursorPosition(X, Y);
                Console.Write("@");
            }
        }

        private static void ControlsCharacter(ref int mobDX, ref int mobDY, ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    mobDX = 0;
                    mobDY = -1;
                    break;
                case ConsoleKey.DownArrow:
                    mobDX = 0;
                    mobDY = 1;
                    break;
                case ConsoleKey.LeftArrow:
                    mobDX = -1;
                    mobDY = 0;
                    break;
                case ConsoleKey.RightArrow:
                    mobDX = 1;
                    mobDY = 0;
                    break;
            }
        }

        private static void MovesMob(ref int X, ref int Y,ref string typeCell, int DX, int DY, string[,] map)
        {
            if (map[Y + DY, X + DX] == " " || map[Y + DY, X + DX] == "$" || map[Y + DY, X + DX] == "#")
            {
                Console.SetCursorPosition(X, Y);
                Console.Write(" ");
                X += DX;
                Y += DY;
                typeCell = map[Y, X];
                Console.SetCursorPosition(X, Y);
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
                string temp = array[i];

                for (int j = 0; j < array[i].Length; j++)
                {
                    arrayTemp[i, j] = temp[j].ToString();
                }
            }

            return arrayTemp;
        }

        static void ShowWinGame(ref int mobX, ref int mobY, string typeCell)
        {
            if (typeCell == "#")
            {
                mobX = 0;
                mobY = 0;
                Console.Clear();
                Console.WriteLine("Вы прошли лабиринт!");
            }
        }
    }
}
