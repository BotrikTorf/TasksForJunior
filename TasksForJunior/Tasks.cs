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
                    ShowSortedListFullName(patients);
                else if (value == ConstAge)
                    ShowSortedListAge(patients);
                else if (value == ConstExit)
                    isWork = false;
                else
                    ShowPatientsDiagnosed(patients, value);
            }
        }

        private static void ShowPatientsDiagnosed(List<Patient> patients, string value)
        {
            var showPatients = patients.Where(patient => patient.Diagnosis == value);
            Show(showPatients.ToList());
        }

        private static void ShowSortedListAge(List<Patient> patients)
        {
            var showPatients = patients.OrderBy(patient => patient.Age);
            Show(showPatients.ToList());
        }

        private static void ShowSortedListFullName(List<Patient> patients)
        {
            var showPatients = patients.OrderBy(patient => patient.Surname).ThenBy(patient => patient.Name)
                .ThenBy(patient => patient.MiddleName);

            Show(showPatients.ToList());
        }

        private static List<Patient> CreateListPatients()
        {
            List<string> surnames = new List<string>
                { "Аксаков", "Бажов", "Гоголь", "Есенин", "Жуковский", "Лермонтов", "Маршак", "Некрасов" };
            List<string> names = new List<string>
                { "Александр", "Борис", "Виктор", "Георгий", "Даниил", "Евгений", "Жак", "Иван" };
            List<string> middleNames = new List<string>
            { "Александрович", "Борисович", "Викторович", "Гкоргиевич", "Данилович", "Евгениевич",
                "Иванович" };
            List<string> diagnosis = new List<string>
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
                    diagnosis[random.Next(0, diagnosis.Count)],
                    random.Next(minAge, maxAge + 1)));
            }

            return patients;
        }

        static void Show(List<Patient> patients)
        {
            for (int i = 0; i < patients.Count; i++)
            {
                Console.Write($"{i + 1:000}. ");
                patients[i].Show();
            }

            Console.WriteLine();
        }
    }

    class Patient
    {
        public Patient(string surname, string name, string middleName, string diagnosis, int age)
        {
            Surname = surname;
            Name = name;
            MiddleName = middleName;
            Diagnosis = diagnosis;
            Age = age;
        }

        public string Surname { get; }

        public string Name { get; }

        public string MiddleName { get; }

        public string Diagnosis { get; }

        public int Age { get; }

        public void Show()
        {
            Console.WriteLine($"{Surname} {Name} {MiddleName}, {Age} лет, болезнь: {Diagnosis}");
        }
    }
}
