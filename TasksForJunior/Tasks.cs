using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            string[] arrayOne = new string[20];
            string[] arrayTwo = new string[20];
            List<string> list = new List<string>();

            FillsArray(arrayOne);
            ShowCollection(arrayOne);
            MergesArray(arrayOne, list);
            FillsArray(arrayTwo);
            ShowCollection(arrayTwo);
            MergesArray(arrayTwo, list);
            ShowColLection(list);
        }

        private static void ShowColLection(List<string> list)
        {
            foreach (var listItem in list)
            {
                Console.Write($"{listItem}\t");
            }
            Console.WriteLine();
        }

        private static void ShowCollection(string[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write($"{array[i]}\t");
            }
            Console.WriteLine();
        }

        private static void MergesArray(string[] array, List<string> list)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (list.Contains(array[i]) == false)
                {
                    list.Add(array[i]);
                }
            }
        }

        static void FillsArray(string[] array)
        {
            Random random = new Random();
            int min = 0;
            int max = 15;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(min, max).ToString();
            }
        }
    }
}
