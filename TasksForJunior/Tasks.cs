using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandExit = "exit";
            string[] text =
            {
                "Программа - Описание алгоритма решения задачи на языке ЭВМ (специолист).",
                "ПРОГРАММИСТ - Специалист по программированию.",
                "Стажёр - Временно работает в компании, часто бесплатно. Ему редко дают писать боевой код, чаще он просто учится и набирается опыта.",
                "ученик - Человек, который учится чему-нибудь у кого-нибудь.",
                "джун — стартовая позиция в программировании.",
                "Мидл - Пишет программы самостоятельно.",
                "Сеньор - Занимается архитектурой, взаимодействием систем и высокоуровневыми вещами.",
            };
            string key;
            bool isWork = true;

            Dictionary<string, string> dictionarys = ConvertsOneArrayToDictionary(text);
            Console.WriteLine($"Если вы хотите выйти из словоря наберите команду \"{CommandExit}\".");

            while (isWork)
            {
                key = RequestKeyword();

                if (key == CommandExit)
                    isWork = false;
                else
                    ShowMeaningWord(dictionarys, key);
            }
        }

        static void ShowMeaningWord(Dictionary<string, string> dictionarys, string key)
        {
            if (dictionarys.ContainsKey(key))
                Console.WriteLine($" Значение слова {key} - {dictionarys[key]}");
            else
                Console.WriteLine("Значения такого слова нет");
        }

        static string RequestKeyword()
        {
            Console.Write("Введите слово значение которого Вы хотите узнать: ");
            string word = Console.ReadLine();
            return word.ToLower();
        }

        static Dictionary<string, string> ConvertsOneArrayToDictionary(string[] text)
        {
            Dictionary<string, string> tempDictionary = new Dictionary<string, string>();

            for (int i = 0; i < text.Length; i++)
            {
                string[] temp = text[i].Split(new char[] { '-' });

                if (temp.Length == 2)
                {
                    temp[0] = temp[0].Trim().ToLower();
                    temp[1] = temp[1].Trim();
                    tempDictionary.Add(temp[0], temp[1]);
                }
                else
                {
                    if (temp.Length > 2)
                    {
                        for (int j = 2; j < temp.Length; j++)
                        {
                            temp[1] = temp[1] + "-" + temp[j];
                        }

                        temp[0] = temp[0].Trim().ToLower();
                        temp[1] = temp[1].Trim();
                        tempDictionary.Add(temp[0], temp[1]);
                    }
                }

            }

            return tempDictionary;
        }
    }
}
