using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            int[,] array = new int[10, 10];

            FillsArray(array);
            ShowArray(array);
            Console.WriteLine();
            Shuffle(array);
            ShowArray(array);
        }

        private static void Shuffle(int[,] array)
        {
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    int iTemp = random.Next(0, array.GetLength(0));
                    int jTemp = random.Next(0, array.GetLength(1));
                    int tempNumber = array[iTemp, jTemp];
                    array[iTemp, jTemp] = array[i, j];
                    array[i, j] = tempNumber;
                }
            }
        }

        private static void ShowArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write($"{array[i, j]}\t");
                }

                Console.WriteLine();
            }
        }

        private static void FillsArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = (i + 1) * (j + 1);
                }
            }
        }
    }
}
