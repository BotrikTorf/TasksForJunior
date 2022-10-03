using System;


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
            const string CommandLastNameSearch = "search";
            string[] fullNames = new string[0];
            string[] jobTitles = new string[0];
            bool isWork = true;

            Console.WriteLine($"Добро пожаловать в программу по составлениею досье на сотрудника:\n" +
                $"для просмотра команды предприятия (Ф.И.О сотрудника - должность) наберите команду {CommandFull}\n" +
                $"для поиски сотрудника по фамилии наберите команду {CommandLastNameSearch}\n" +
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
                        AddsEmployee(ref fullNames, ref jobTitles);
                        break;
                    case CommandDeleteEmployee:
                        DeletesEmployee(ref fullNames, ref jobTitles);
                        break;
                    case CommandFull:
                        TeamView(fullNames, jobTitles);
                        break;
                    case CommandLastNameSearch:
                        LastNameSearch(fullNames);
                        break;
                    default:
                        Console.WriteLine($"Вы неправильно набрали команду! Повторите еще раз или наберите {CommandExit} " +
                            $"для выхода из программы.");
                        break;
                }
            }
        }

        private static void LastNameSearch(string[] fullNames)
        {
            string tempLastName = null;
            Console.Write("Ведите фамилию сотрудника которого вы хотите найти в спаске: ");
            string lastName = Console.ReadLine();

            foreach (string names in fullNames)
            {
                if (ReturnsLastName(names.ToLower()) == lastName.ToLower())
                    tempLastName = names;
            }

            if (tempLastName != null)
                Console.WriteLine($"Есть сотрудник с фамилией {lastName}, его ФИО: {tempLastName}");
            else
                Console.WriteLine($"Сотрудника с фамилией {lastName} в списке сотрудников нет!");
        }

        static void TeamView(string[] fullNames, string[] jobTitles)
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

        static string[] ArrayGrowth(string[] array)
        {
            string[] temp = array;
            string[] arrayReady = new string[temp.Length + 1];

            for (int i = 0; i < temp.Length; i++)
            {
                arrayReady[i] = temp[i];
            }

            return arrayReady;
        }

        static string[] DeletesEmptyEntry(string[] array)
        {
            string[] temp = new string[array.Length - 1];
            int number = 0;
            bool thereEmptyCells = false;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    thereEmptyCells = true;
                }
            }

            if (thereEmptyCells)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] != null)
                    {
                        temp[number] = array[i];
                        number++;
                    }
                }

                return temp;
            }
            else
            {
                Console.WriteLine("Пустых записей нет!");
                return array;
            }
        }

        static void AddsEmployee(ref string[] fullNames, ref string[] jobTitles)
        {
            Console.Write("Введите фамилию, имя и отчество нового сотрудника через пробел: ");
            fullNames = ArrayGrowth(fullNames);
            fullNames[fullNames.Length - 1] = Console.ReadLine();
            Console.Write("Введите должность вашего сотрудника: ");
            jobTitles = ArrayGrowth(jobTitles);
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
                Console.Write("Для удаления сотрудника введите его порядковый номер или фамилию: ");
                string stringInput = Console.ReadLine();
                int number;

                if (int.TryParse(stringInput, out number))
                {
                    fullNames[number - 1] = null;
                    jobTitles[number - 1] = null;
                }
                else
                {
                    for (int i = 0; i < fullNames.Length; i++)
                    {
                        if (ReturnsLastName(fullNames[i]) == stringInput)
                        {
                            fullNames[i] = null;
                            jobTitles[i] = null;
                        }
                    }
                }

                fullNames = DeletesEmptyEntry(fullNames);
                jobTitles = DeletesEmptyEntry(jobTitles);
            }
        }

        static string ReturnsLastName(string name)
        {
            string[] fullName = name.Split(new char[] { ' ' });
            return fullName[0];
        }
    }
}
