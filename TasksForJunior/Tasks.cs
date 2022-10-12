using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandAddDossier = "add";
            const string CommandShowAllDossiers = "all";
            const string CommandDeleteDossier = "delete";
            const string CommandExit = "exit";
            bool isWork = true;

            Dictionary<string, string> dossiers = new Dictionary<string, string>();

            Console.WriteLine("Добро пожаловать в программу кадровый учет.");
            Console.WriteLine($"Для добавления досье введите команду {CommandAddDossier},\n" +
                $"Для просмотра полного списка всех рабртающих людей введите команду {CommandShowAllDossiers},\n" +
                $"Для удаления досье введите команду {CommandDeleteDossier},\n" +
                $"Для выхода из программы введите команду {CommandExit}.");

            while (isWork)
            {
                Console.Write("Введите команду: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandAddDossier:
                        AddDossier(dossiers);
                        break;
                    case CommandShowAllDossiers:
                        ShowAllDossiers(dossiers);
                        break;
                    case CommandDeleteDossier:
                        DeletesDossier(dossiers);
                        break;
                    case CommandExit:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Вы не правильно ввели команду! Попробуйте еще раз.");
                        break;
                }
            }
        }

        private static void DeletesDossier(Dictionary<string, string> dossiers)
        {
            Console.Write("Введите ФИО работника которого вы хотите удалить: ");
            string fullName = Console.ReadLine();

            if (dossiers.ContainsKey(fullName))
            {
                dossiers.Remove(fullName);
                Console.WriteLine("Удаление прошло успешно.");
            }
            else
            {
                Console.WriteLine("Сотрудника с таким ФИО нет.");
            }
        }

        private static void ShowAllDossiers(Dictionary<string, string> dossiers)
        {
            int number = 1;

            foreach (var person in dossiers)
            {
                Console.WriteLine($"{number}. {person.Key} - {person.Value}.");
                number++;
            }
        }

        static void AddDossier(Dictionary<string, string> dossiers)
        {
            Console.Write("Введите ФИО работника: ");
            string fullName = Console.ReadLine();

            if (dossiers.ContainsKey(fullName))
            {
                Console.WriteLine("Сотрудник с таким ФИО существует. Повторный ввод недопустим.");
            }
            else
            {
                Console.Write("Введите профессию работника: ");
                string profession = Console.ReadLine();
                dossiers.Add(fullName, profession);
            }

        }
    }
}
