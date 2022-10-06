using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            bool isPlaying = true;
            int mobX = 37, mobY = 2;
            int food = 0;
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

            string[,] mapGame = ConvertsArray(map);
            DrawMap(mapGame);
            Console.CursorVisible = false;

            while (isPlaying)
            {
                Console.SetCursorPosition(mobX, mobY);
                Console.Write("@");
                ConsoleKeyInfo key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        MovesMob(ref mobX, ref mobY, ref food, 0, -1, mapGame);
                        break;
                    case ConsoleKey.DownArrow:
                        MovesMob(ref mobX, ref mobY, ref food, 0, 1, mapGame);
                        break;
                    case ConsoleKey.LeftArrow:
                        MovesMob(ref mobX, ref mobY, ref food, -1, 0, mapGame);
                        break;
                    case ConsoleKey.RightArrow:
                        MovesMob(ref mobX, ref mobY, ref food, 1, 0, mapGame);
                        break;
                }

                if (mobX == 0 && mobY == 0)
                {
                    isPlaying = false;
                }
            }

            Console.ReadLine();
        }

        private static void MovesMob(ref int mobX, ref int mobY,ref int food, int mobDX, int mobDY, string[,] map)
        {
            if (map[mobY + mobDY, mobX + mobDX] == " " || map[mobY + mobDY, mobX + mobDX] == "$")
            {
                if (map[mobY + mobDY, mobX + mobDX] == "$")
                {
                    food++;
                    MovesMob(ref mobX, ref mobY, mobDX, mobDY);
                }
                else
                {
                    MovesMob(ref mobX, ref mobY, mobDX, mobDY);
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

                    Console.SetCursorPosition(0,0);
                    DrawMap(map);
                    Console.SetCursorPosition(mobX, mobY);
                    Console.Write("@");
                }

            }

            if (map[mobY + mobDY, mobX + mobDX] == "#")
            {
                ShowWinGame(ref mobX, ref mobY);
            }
        }

        private static void MovesMob(ref int mobX, ref int mobY, int mobDX, int mobDY)
        {
            Console.SetCursorPosition(mobX, mobY);
            Console.Write(" ");
            mobX += mobDX;
            mobY += mobDY;
            Console.SetCursorPosition(mobX, mobY);
            Console.Write("@");
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

        static string[,] ConvertsArray(string[] array)
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

        static void ShowWinGame(ref int mobX, ref int mobY)
        {
            mobX = 0;
            mobY = 0;
            Console.Clear();
            Console.WriteLine("Вы прошли лабиринт!");
        }
    }
}
