using System;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string FullName = "name";
            const string JobTitle = "title";
            const string Full = "full";
            const string AddEmployee = "add";
            const string DeleteEmployee = "delete";
            const string Exit = "exit";
            string[] fullName = new string[0];
            string[] jobTitle = new string[0];
            bool isWork = true;

            Console.WriteLine($"Добро пожаловать в программу по составлениею досье на сотрудника:\n" +
                $"для просмотра списка сотрудников наберите команду {FullName}\n" +
                $"для просмотра профессий на предприятии наберите команду {JobTitle}\n" +
                $"для просмотра команды предприятия (Ф.И.О сотрудника - должность) наберите команду {Full}\n" +
                $"для добовления сотрудника наберите команду {AddEmployee}\n" +
                $"для удалить сотрудника наберите команду {DeleteEmployee}.\n" +
                $"для выхода и программы наберите команду {Exit}");

            while (isWork)
            {
                Console.Write("Для дольнейшей работы наберите нужную вам команду: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case Exit:
                        isWork = false;
                        break;
                    case AddEmployee:
                        AddsEmployee(ref fullName, ref jobTitle);
                        break;
                    case DeleteEmployee:
                        DeletesEmployee(ref fullName, ref jobTitle);
                        break;
                    case Full:
                        TeamView(fullName, jobTitle);
                        break;
                    case FullName:
                        ViewEmployees(fullName);
                        break;
                    case JobTitle:
                        ViewingPositions(jobTitle);
                        break;
                    default:
                        Console.WriteLine($"Вы неправильно набрали команду! Повторите еще раз или наберите {Exit} " +
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
            Console.Write("Введите фамилию и имя нового сотрудника:");
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
                Console.Write("Для удаления сотрудника введите его порядковый номер или фамилию: ");
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
