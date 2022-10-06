﻿using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandFull = "full";
            const string CommandAddEmployee = "add";
            const string CommandDeleteEmployee = "delete";
            const string CommandExit = "exit";
            const string CommandSearchLastName = "search";
            string[] fullNames = new string[0];
            string[] jobTitles = new string[0];
            bool isWork = true;

            Console.WriteLine($"Добро пожаловать в программу по составлениею досье на сотрудника:\n" +
                $"для просмотра команды предприятия (Ф.И.О сотрудника - должность) наберите команду {CommandFull}\n" +
                $"для поиски сотрудника по фамилии наберите команду {CommandSearchLastName}\n" +
                $"для добовления сотрудника наберите команду {CommandAddEmployee}\n" +
                $"для удалить сотрудника наберите команду {CommandDeleteEmployee}.\n" +
                $"для выхода и программы наберите команду {CommandExit}");

            while (isWork)
            {
                Console.Write("Для дольнейшей работы наберите нужную вам команду: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandExit:
                        isWork = false;
                        break;
                    case CommandAddEmployee:
                        AddEmployee(ref fullNames, ref jobTitles);
                        break;
                    case CommandDeleteEmployee:
                        DeletesEmployee(ref fullNames, ref jobTitles);
                        break;
                    case CommandFull:
                        ShowAll( fullNames, jobTitles);
                        break;
                    case CommandSearchLastName:
                        SearchFullName(fullNames);
                        break;
                    default:
                        Console.WriteLine($"Вы неправильно набрали команду! Повторите еще раз или наберите {CommandExit} " +
                            $"для выхода из программы.");
                        break;
                }
            }
        }

        private static void SearchFullName(string[] fullNames)
        {
            bool haveEmployee = false;
            Console.Write("Ведите фамилию сотрудника которого вы хотите найти в спаске: ");
            string lastName = Console.ReadLine();

            for (int i = 0; i < fullNames.Length; i++)
            {
                if (lastName.ToLower() == ReturnsLastName(fullNames[i]))
                {
                    Console.WriteLine(fullNames[i]);
                    haveEmployee = true;
                }
            }

            if (haveEmployee == false)
                Console.WriteLine($"Сотрудника с фамилией {lastName} в списке сотрудников нет!");
        }

        static void ShowAll(string[] fullNames, string[] jobTitles)
        {
            if (jobTitles.Length == 0)
            {
                Console.WriteLine("У вас никто не работает!");
            }
            else
            {
                for (int i = 0; i < fullNames.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {fullNames[i]}\t - {jobTitles[i]}");
                }
            }
        }

        static string[] GrowthArray(string[] array)
        {
            string[] arrayReady = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                arrayReady[i] = array[i];
            }

            return arrayReady;
        }

        static string[] DeletesEmptyEntry(string[] array, int indexArray)
        {
            string[] temp = new string[array.Length - 1];
            int number = 0;

            if (indexArray >= 0 && indexArray <= array.Length)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (indexArray != i)
                    {
                        temp[number] = array[i];
                        number++;
                    }
                }

                return temp;
            }
            else
            {
                return array;
            }
        }

        static void AddEmployee(ref string[] fullNames, ref string[] jobTitles)
        {
            Console.Write("Введите фамилию, имя и отчество нового сотрудника через пробел: ");
            fullNames = GrowthArray(fullNames);
            fullNames[fullNames.Length - 1] = Console.ReadLine();
            Console.Write("Введите должность вашего сотрудника: ");
            jobTitles = GrowthArray(jobTitles);
            jobTitles[jobTitles.Length - 1] = Console.ReadLine();
        }

        static void DeletesEmployee(ref string[] fullNames, ref string[] jobTitles)
        {
            if (jobTitles.Length == 0)
            {
                Console.WriteLine("У вас никто не работает");
            }
            else
            {
                bool isTemp = true;

                while (isTemp)
                {
                    Console.Write("Для удаления сотрудника введите его порядковый номер или ФИО: ");
                    string stringInput = Console.ReadLine();

                    if (int.TryParse(stringInput, out int number))
                        DeletesEntruByNumber(ref isTemp, ref fullNames, ref jobTitles, number);
                    else
                        DeletesEntryByFullName(ref isTemp, ref fullNames, ref jobTitles, stringInput);
                }
            }
        }

        private static void DeletesEntryByFullName(ref bool isTemp, ref string[] fullNames, ref string[] jobTitles, string stringInput)
        {
            for (int i = 0; i < fullNames.Length; i++)
            {
                if (fullNames[i].ToLower() == stringInput.ToLower())
                {
                    fullNames = DeletesEmptyEntry(fullNames, i);
                    jobTitles = DeletesEmptyEntry(jobTitles, i);
                    isTemp = false;
                }
            }

            if (isTemp)
                Console.WriteLine("Вы ввели неправельно ФИО!");
        }

        private static void DeletesEntruByNumber(ref bool isTemp, ref string[] fullNames, ref string[] jobTitles, int number)
        {
            if (number <= fullNames.Length && number >= 1)
            {
                fullNames = DeletesEmptyEntry(fullNames, number - 1);
                jobTitles = DeletesEmptyEntry(jobTitles, number - 1);
                isTemp = false;
            }
            else
            {
                Console.WriteLine("Вы ввели не правельно номер!");
            }
        }

        static string ReturnsLastName(string name)
        {
            string[] fullName = name.Split(new char[] { ' ' });
            return fullName[0].ToLower();
        }
    }
}
