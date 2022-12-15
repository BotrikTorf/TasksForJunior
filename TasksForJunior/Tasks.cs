using System;
using System.Collections.Generic;
using System.Linq;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            List<Patient> patients = CreateListPatients();
            const string ConstFullName = "1";
            const string ConstAge = "2";
            const string ConstExit = "3";
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Добро пожаловать в программу.\n" +
                                  $"Для отсортировки пациентов больницы по ФИО введите: {ConstFullName}.\n" +
                                  $"Для отсортировки пациентов больницы по возрасту введите: {ConstAge}.\n" +
                                  "Для Если хотите выбрать пациентов с определенной болезнью, то введите название болезни.\n" +
                                  $"Если хотите из программы то введите {ConstExit}");

                string value = Console.ReadLine();

                if (value == ConstFullName)
                {
                    ShowFullName(patients);
                }
                else if (value == ConstAge)
                {
                    ShowAge(patients);
                }
                else if (value == ConstExit)
                {
                    isWork = false;
                }
                else
                {
                    ShowPatients(patients, value);
                }
            }
        }

        private static void ShowPatients(List<Patient> patients, string value)
        {
            var showPatients = patients.Where(patient => patient.Disease == value);
            Show(showPatients.ToList());
        }

        private static void ShowAge(List<Patient> patients)
        {
            var showPatients = patients.OrderBy(patient => patient.Age);
            Show(showPatients.ToList());
        }

        private static void ShowFullName(List<Patient> patients)
        {
            //var showPatients = from patient in patients
            //                   orderby patient.Surname, patient.Name, patient.MiddleName
            //                   select patient;

            var showPatients = patients.OrderBy(patient => patient.MiddleName).OrderBy(patient => patient.Name)
                .OrderBy(patient => patient.Surname);

            Show(showPatients.ToList());
        }

        private static List<Patient> CreateListPatients()
        {
            List<string> surnames = new List<string>
                { "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев", "Петров", "Соколов", "Михайлов" };
            List<string> names = new List<string>
                { "Александр", "Алексей", "Андрей", "Артем", "Виктор", "Даниил", "Дмитрий", "Егор" };
            List<string> middleNames = new List<string>
            { "Александрович", "Алексеевич", "Андреевич", "Анатольевич", "Викторович", "Данилович", "Дмитриевич",
                "Егорович" };
            List<string> diseases = new List<string>
                { "Сахарный диабет", "Заболевания легких", "Болезнь почек", "Анемия", "Дефицит железа" };
            List<Patient> patients = new List<Patient>();
            Random random = new Random();
            int count = 100;
            int minAge = 20;
            int maxAge = 100;

            for (int i = 0; i < count; i++)
            {
                patients.Add(new Patient(
                    surnames[random.Next(0, surnames.Count)],
                    names[random.Next(0, names.Count)],
                    middleNames[random.Next(0, middleNames.Count)],
                    diseases[random.Next(0, diseases.Count)],
                    random.Next(minAge, maxAge + 1)));
            }

            return patients;
        }

        static void Show(List<Patient> patients)
        {
            int number = 1;

            foreach (var patient in patients)
            {
                Console.Write($"{number:000}. ");
                patient.Show();
                number++;
            }

            Console.WriteLine();
        }
    }

    class Patient
    {
        public Patient(string surname, string name, string middleName, string disease, int age)
        {
            Surname = surname;
            Name = name;
            MiddleName = middleName;
            Disease = disease;
            Age = age;
        }

        public string Surname { get; }

        public string Name { get; }

        public string MiddleName { get; }

        public string Disease { get; }

        public int Age { get; }

        public void Show()
        {
            Console.WriteLine($"{Surname} {Name} {MiddleName}, {Age} лет, болезнь: {Disease}");
        }
    }
}
