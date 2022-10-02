using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandFullName = "name";
            const string CommandJobTitle = "title";
            const string CommandFull = "full";
            const string CommandAddEmployee = "add";
            const string CommandDeleteEmployee = "delete";
            const string CommandExit = "exit";
            string[] fullName = new string[0];
            string[] jobTitle = new string[0];
            bool isWork = true;

            Console.WriteLine($"Добро пожаловать в программу по составлениею досье на сотрудника:\n" +
                $"для просмотра списка сотрудников наберите команду {CommandFullName}\n" +
                $"для просмотра профессий на предприятии наберите команду {CommandJobTitle}\n" +
                $"для просмотра команды предприятия (Ф.И.О сотрудника - должность) наберите команду {CommandFull}\n" +
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
                        AddsEmployee(ref fullName, ref jobTitle);
                        break;
                    case CommandDeleteEmployee:
                        DeletesEmployee(ref fullName, ref jobTitle);
                        break;
                    case CommandFull:
                        TeamView(fullName, jobTitle);
                        break;
                    case CommandFullName:
                        ViewEmployees(fullName);
                        break;
                    case CommandJobTitle:
                        ViewingPositions(jobTitle);
                        break;
                    default:
                        Console.WriteLine($"Вы неправильно набрали команду! Повторите еще раз или наберите {CommandExit} " +
                            $"для выхода из программы.");
                        break;
                }
            }
        }

        private static void ViewingPositions(string[] jobTitle)
        {
            if (jobTitle.Length == 0)
            {
                Console.WriteLine("У вас никто не работает!");
            }
            else
            {
                string[,] temp = new string[1, 2];
                temp[0, 0] = "1";
                temp[0, 1] = jobTitle[0];

                for (int i = 1; i < jobTitle.Length; i++)
                {
                    string stringTemp = jobTitle[i];
                    bool isNewProfession = true;

                    for (int j = 0; j < temp.GetLength(0); j++)
                    {
                        if (stringTemp == temp[j, 1])
                        {
                            isNewProfession = false;
                        }
                    }

                    if (isNewProfession)
                    {
                        string[,] tempArray = new string[temp.GetLength(0) + 1, temp.GetLength(1)];

                        for (int j = 0; j < temp.GetLength(0); j++)
                        {
                            for (int l = 0; l < temp.GetLength(1); l++)
                            {
                                tempArray[j, l] = temp[j, l];
                            }
                        }

                        temp = tempArray;
                        temp[temp.GetLength(0) - 1, 0] = "1";
                        temp[temp.GetLength(0) - 1, 1] = jobTitle[i];
                    }
                    else
                    {
                        for (int j = 0; j < temp.GetLength(0); j++)
                        {
                            if (temp[j, 1] == jobTitle[i])
                            {
                                temp[j, 0] = (int.Parse(temp[j, 0]) + 1).ToString();
                            }
                        }
                    }
                }

                Console.WriteLine("У вас работает:");

                for (int i = 0; i < temp.GetLength(0); i++)
                {
                    Console.WriteLine($"{temp[i, 0]} человек по професии {temp[i, 1]};");
                }
            }
        }

        static void ViewEmployees(string[] fullName)
        {
            if (fullName.Length == 0)
            {
                Console.WriteLine("У вас никто не работает!");
            }
            else
            {
                for (int i = 0; i < fullName.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {fullName[i]}");
                }
            }
        }

        static void TeamView(string[] fullName, string[] jobTitle)
        {
            if (jobTitle.Length == 0)
            {
                Console.WriteLine("У вас никто не работает!");
            }
            else
            {
                for (int i = 0; i < fullName.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {fullName[i]}\t - {jobTitle[i]}");
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

        static void AddsEmployee(ref string[] fullName, ref string[] jobTitle)
        {
            Console.Write("Введите фамилию, имя и отчество нового сотрудника через пробел: ");
            fullName = ArrayGrowth(fullName);
            fullName[fullName.Length - 1] = Console.ReadLine();
            Console.Write("Введите должность вашего сотрудника: ");
            jobTitle = ArrayGrowth(jobTitle);
            jobTitle[jobTitle.Length - 1] = Console.ReadLine();
        }

        static void DeletesEmployee(ref string[] fullName, ref string[] jobTitle)
        {
            if (jobTitle.Length == 0)
            {
                Console.WriteLine("У вас никто не работает");
            }
            else
            {
                Console.Write("Для удаления сотрудника введите его порядковый номер или ФИО: ");
                string temp = Console.ReadLine();
                int number;

                if (int.TryParse(temp, out number))
                {
                    fullName[number - 1] = null;
                    jobTitle[number - 1] = null;
                }
                else
                {
                    for (int i = 0; i < fullName.Length; i++)
                    {
                        if (fullName[i] == temp)
                        {
                            fullName[i] = null;
                            jobTitle[i] = null;
                        }
                    }
                }

                fullName = DeletesEmptyEntry(fullName);
                jobTitle = DeletesEmptyEntry(jobTitle);
            }
        }
    }
}
