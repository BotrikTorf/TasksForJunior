﻿using System;
using System.Collections.Generic;


namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {

        }
    }

    class Book
    {
        private int _numberPages;
        private int _yearPublishing;
        private DateTime _dateTime = new DateTime();

        public string Name { get; private set; }
        public string Author { get; private set; }
        public int NumberPages
        {
            get { return _numberPages; }
            private set
            {
                if (value >= 0)
                {
                    _numberPages = value;
                }
                else
                {
                    Console.WriteLine("Количество страниц в книге не может быть отрицательным! По умолчанию присвоено значение 0 страниц");
                    _numberPages = 0;
                }
            }
        }
        public int YearPublishing
        {
            get { return _yearPublishing; }
            private set
            {
                if (value <= _dateTime.Year)
                {
                    _yearPublishing = value;
                }
                else
                {
                    Console.WriteLine($"Книга не может быть напечатана раньше {_dateTime.Year}. По умолчанию присвоено значение 0");
                    _yearPublishing = 0;
                }
            }
        }
        public string Genre { get; private set; }

        public Book()
        {
            Name = null;
            Author = null;
            NumberPages = 0;
            YearPublishing = 0;
            Genre = null;
        }

        public Book(string genre, string name, string author, int nunberPage, int yearPublishing)
        {
            Name = name;
            Genre = genre;
            Author = author;
            NumberPages = nunberPage;
            YearPublishing = yearPublishing;
        }

        public void ChangesName(string name) => Name = name;

        public void ChangesGenre(string genre) => Genre = genre;

        public void ChangesAuthor(string author) => Author = author;

        public void ChangesNumberPages(int nunberPage) => NumberPages = nunberPage;

        public void ChangesYearPublishing(int yearPublishing) => YearPublishing = yearPublishing;
    }

    class Library
    {
        private Dictionary<int, Book> _library;
        Random _random = new Random();
        private int _randomKey
        {
            get
            {
                bool isRepeated = true;
                int idNamber = 0;

                while (isRepeated)
                {
                    idNamber = _random.Next(0, 10000);
                    if (_library.ContainsKey(idNamber) == false)
                        isRepeated = false;
                }

                return idNamber;
            }
        }

        public Library()
        {
            _library = new Dictionary<int, Book>();
        }

        public Library(Dictionary<int, Book> library)
        {
            _library = new Dictionary<int, Book>();

            foreach (var book in library)
            {
                _library.Add(_randomKey, book.Value);
            }
        }

        public void AddBook(Book book)
        {
            _library.Add(_randomKey, book);
        }

        public void AddBook(string genre, string name, string author, int nunberPage, int yearPublishing)
        {
            Book book = new Book(genre, name, author, nunberPage, yearPublishing);
            _library.Add(_randomKey, book);
        }

        public void AddBook(Dictionary<int, Book> library)
        {
            foreach (var book in library)
            {
                _library.Add(_randomKey, book.Value);
            }
        }

        public void ShowLibrary()
        {
            int idNumber = 1;

            foreach (var book in _library)
            {
                Console.WriteLine($"Порядковый номер: {idNumber}\n" +
                    $"Индивидуальный номер: {book.Key}\n" +
                    $"Название книги: {book.Value.Name}\n" +
                    $"Автор книги: {book.Value.Author}\n" +
                    $"Жанр в котором написана книга: {book.Value.Genre}\n" +
                    $"Год издания: {book.Value.YearPublishing}\n" +
                    $"Количество страниц в книге: {book.Value.NumberPages}\n");
                idNumber++;
            }
        }

        public void SearchNameBook(string name)
        {
            int idNumber = 1;

            foreach (var book in _library)
            {
                if (book.Value.Name == name)
                {
                    Console.WriteLine($"Порядковый номер: {idNumber}\n" +
                        $"Индивидуальный номер: {book.Key}\n" +
                        $"Название книги: {book.Value.Name}\n" +
                        $"Автор книги: {book.Value.Author}\n" +
                        $"Жанр в котором написана книга: {book.Value.Genre}\n" +
                        $"Год издания: {book.Value.YearPublishing}\n" +
                        $"Количество страниц в книге: {book.Value.NumberPages}\n");
                    idNumber++;
                }
            }

            if (idNumber == 1)
            {
                Console.WriteLine($"Книга {name} не найдена.");
            }
        }

        public void SearchKeyBook(int key)
        {
            int idNumber = 1;

            foreach (var book in _library)
            {
                if (book.Key == key)
                {
                    Console.WriteLine($"Порядковый номер: {idNumber}\n" +
                        $"Индивидуальный номер: {book.Key}\n" +
                        $"Название книги: {book.Value.Name}\n" +
                        $"Автор книги: {book.Value.Author}\n" +
                        $"Жанр в котором написана книга: {book.Value.Genre}\n" +
                        $"Год издания: {book.Value.YearPublishing}\n" +
                        $"Количество страниц в книге: {book.Value.NumberPages}\n");
                    idNumber++;
                }
            }

            if (idNumber == 1)
            {
                Console.WriteLine($"Книга с {key} индивидуальным номером не найдена.");
            }
        }

        public void SearchAuthorBook(string author)
        {
            int idNumber = 1;

            foreach (var book in _library)
            {
                if (book.Value.Author == author)
                {
                    Console.WriteLine($"Порядковый номер: {idNumber}\n" +
                        $"Индивидуальный номер: {book.Key}\n" +
                        $"Название книги: {book.Value.Name}\n" +
                        $"Автор книги: {book.Value.Author}\n" +
                        $"Жанр в котором написана книга: {book.Value.Genre}\n" +
                        $"Год издания: {book.Value.YearPublishing}\n" +
                        $"Количество страниц в книге: {book.Value.NumberPages}\n");
                    idNumber++;
                }
            }

            if (idNumber == 1)
            {
                Console.WriteLine($"Книги {author} не найдены.");
            }
        }

        public void SearchGenreBook(string genre)
        {
            int idNumber = 1;

            foreach (var book in _library)
            {
                if (book.Value.Genre == genre)
                {
                    Console.WriteLine($"Порядковый номер: {idNumber}\n" +
                        $"Индивидуальный номер: {book.Key}\n" +
                        $"Название книги: {book.Value.Name}\n" +
                        $"Автор книги: {book.Value.Author}\n" +
                        $"Жанр в котором написана книга: {book.Value.Genre}\n" +
                        $"Год издания: {book.Value.YearPublishing}\n" +
                        $"Количество страниц в книге: {book.Value.NumberPages}\n");
                    idNumber++;
                }
            }

            if (idNumber == 1)
            {
                Console.WriteLine($"Книги {genre} жанра не найдены.");
            }
        }

        public void SearchYearPublishingBook(int yearPublishing)
        {
            int idNumber = 1;

            foreach (var book in _library)
            {
                if (book.Value.YearPublishing == yearPublishing)
                {
                    Console.WriteLine($"Порядковый номер: {idNumber}\n" +
                        $"Индивидуальный номер: {book.Key}\n" +
                        $"Название книги: {book.Value.Name}\n" +
                        $"Автор книги: {book.Value.Author}\n" +
                        $"Жанр в котором написана книга: {book.Value.Genre}\n" +
                        $"Год издания: {book.Value.YearPublishing}\n" +
                        $"Количество страниц в книге: {book.Value.NumberPages}\n");
                    idNumber++;
                }
            }

            if (idNumber == 1)
            {
                Console.WriteLine($"Книги {yearPublishing} издания не найдены.");
            }
        }
    }
}
