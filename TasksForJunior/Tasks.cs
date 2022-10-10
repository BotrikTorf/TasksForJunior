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
            FillsArray(arrayTwo);
            MergesArray(arrayOne, arrayTwo, list);
            ShowCollection(arrayOne);
            ShowCollection(arrayTwo);
            ShowColLection(list);
        }

        private static void ShowColLection(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write($"{list[i]}\t");
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

        private static void MergesArray(string[] arrayOne, string[] arrayTwo, List<string> list)
        {
            
            for (int i = 0; i < arrayOne.Length; i++)
            {
                bool thereRepetition = true;

                for (int j = 0; j < list.Count; j++)
                {
                    if (arrayOne[i] == list[j])
                        thereRepetition = false;
                }

                if (thereRepetition)
                {
                    list.Add(arrayOne[i]);
                }
            }

            for (int i = 0; i < arrayTwo.Length; i++)
            {
                bool thereRepetition = true;

                for (int j = 0; j < list.Count; j++)
                {
                    if (arrayTwo[i] == list[j])
                        thereRepetition = false;
                }

                if (thereRepetition)
                {
                    list.Add(arrayTwo[i]);
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
