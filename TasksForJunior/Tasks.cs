using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandSeeCardsDeck = "deck";
            const string CommandSeeCardsPlayer = "player";
            const string CommandTakeCard = "take";
            const string CommandGiveCards = "give";
            const string CommandMaxCardPlayer = "max";
            const string CommandExit = "exit";
            bool isWorKProgram = true;

            Console.Write("Введите имя игрока: ");
            string namePlayer = Console.ReadLine();
            Player player = new Player(namePlayer);
            Console.WriteLine();
            Deck deck = new Deck();

            Console.WriteLine($"Правила пользования программой:\n" +
                $"Для просмотра колоды карт наберите команду: {CommandSeeCardsDeck}\n" +
                $"Для просмотра карт которые есть у игрока наберите команду: {CommandSeeCardsPlayer}\n" +
                $"По умолчанию игрок может взять не более {player.MaxCard} Для изменения максимального " +
                $"количества карт для игрока наберите команду {CommandMaxCardPlayer}\n" +
                $"Чтобы взять карту с колоды наберите команду {CommandTakeCard}\n" +
                $"Чтобы вернуть карты игрока в колоду наберите команду {CommandGiveCards}\n" +
                $"Для завершения работы программы наберите команду {CommandExit}");
            Console.WriteLine();


            while (isWorKProgram)
            {
                Console.Write("Введите команду: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandExit:
                        isWorKProgram = false;
                        break;
                    case CommandGiveCards:
                        player.GivesCardsDeck(deck);
                        break;
                    case CommandMaxCardPlayer:
                        ChangeMaxNumberCard(player);
                        break;
                    case CommandSeeCardsDeck:
                        deck.ShowDeck();
                        break;
                    case CommandSeeCardsPlayer:
                        player.ShowCard();
                        break;
                    case CommandTakeCard:
                        player.TakesCard(deck);
                        break;
                    default:
                        Console.WriteLine("Вы не правильно ввели команду. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void ChangeMaxNumberCard(Player player)
        {
            int number = 0;
            bool isNumber = false;

            while (isNumber == false)
            {
                Console.Write("Укажите число карт которое может быть у игрока: ");

                if (int.TryParse(Console.ReadLine(), out int tempNumber))
                {
                    number = tempNumber;
                    isNumber = true;
                }
                else
                {
                    Console.WriteLine("Вы не правильно ввели число!");
                }
            }

            player.ChangeMaxNumberCard(number);
            Console.WriteLine($"Игрок может взять {player.MaxCard} карт.");
        }
    }

    class Card
    {
        public string Suit { get; private set; }

        public string Rank { get; private set; }

        public Card(string suit, string rank)
        {
            Suit = suit;
            Rank = rank;
        }
    }

    class Deck
    {
        private string[] _cardSuit = new string[] { "club", "diamond", "heart", "spade" };
        private string[] _cardRank = new string[] { "6", "7", "8", "9", "10", "Ase", "Jack", "King", "Queen" };
        private List<Card> _cards;
        private Random _random = new Random();

        public int CardsDeck { get { return _cards.Count; } }

        public Deck()
        {
            _cards = new List<Card>();

            for (int i = 0; i < _cardRank.Length; i++)
            {
                for (int j = 0; j < _cardSuit.Length; j++)
                {
                    Card card = new Card(_cardSuit[j], _cardRank[i]);
                    _cards.Add(card);
                }
            }
        }

        public Card GiveCard()
        {
            int randomCard = _random.Next(0, _cards.Count);
            Card cardTemp = _cards[randomCard];
            _cards.RemoveAt(randomCard);
            return cardTemp;
        }

        public void AcceptsCardsPlayer(Card card)
        {
            _cards.Add(card);
        }

        public void ShowDeck()
        {
            if (_cards.Count > 0)
            {
                Console.WriteLine("В колоде есть карты:");

                foreach (var card in _cards)
                {
                    Console.WriteLine($"{card.Rank} - {card.Suit}");
                }

                Console.WriteLine($"Всего карт {_cards.Count} штук.");
                Console.WriteLine("______________________________");
            }
            else
            {
                Console.WriteLine("В колоде нет карт.");
            }
        }
    }

    class Player
    {
        private List<Card> _cards;

        public int MaxCard { get; private set; } = 10;

        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
            _cards = new List<Card>();
        }

        public void ChangeMaxNumberCard(int number)
        {
            MaxCard = number;
        }

        public void TakesCard(Deck deck)
        {
            if (_cards.Count < MaxCard && deck.CardsDeck > 0)
            {
                _cards.Add(deck.GiveCard());
            }
            else if (_cards.Count == MaxCard)
            {
                Console.WriteLine($"Вы больше не можете взять карту. У вас привышен лимит в {MaxCard} карт");
            }
            else
            {
                Console.WriteLine("В колоде закончились карты");
            }
        }

        public void ShowCard()
        {
            if (_cards.Count > 0)
            {
                Console.WriteLine("У игрока на руках есть карты:");

                foreach (var card in _cards)
                {
                    Console.WriteLine($"{card.Rank} - {card.Suit}");
                }

                Console.WriteLine($"Всего карт {_cards.Count} штук.");
                Console.WriteLine("______________________________");
            }
            else
            {
                Console.WriteLine("У игрока нет карт.");
            }
        }

        public void GivesCardsDeck(Deck deck)
        {
            foreach (var card in _cards)
            {
                deck.AcceptsCardsPlayer(card);
            }

            _cards.Clear();
        }
    }
}
