using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            Croupier croupier = new Croupier();

            Console.Write("Введите имя игрока: ");
            string namePlayer = Console.ReadLine();

            Player player = new Player(namePlayer);

            Console.WriteLine();

            Game game = new Game(croupier, player);
            game.Play();
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
        private List<Card> _cards;
        private Random _random = new Random();

        public int CardsCount { get { return _cards.Count; } }

        public Deck()
        {
            _cards = new List<Card>();
            string[] _cardsSuit = { "club", "diamond", "heart", "spade" };
            string[] _cardsRank = { "6", "7", "8", "9", "10", "Ase", "Jack", "King", "Queen" };

            for (int i = 0; i < _cardsRank.Length; i++)
            {
                for (int j = 0; j < _cardsSuit.Length; j++)
                {
                    Card card = new Card(_cardsSuit[j], _cardsRank[i]);
                    _cards.Add(card);
                }
            }
        }

        public Card GiveCard()
        {
            if (_cards.Count > 0)
            {
                int randomCard = _random.Next(0, _cards.Count);
                Card cardTemp = _cards[randomCard];
                _cards.RemoveAt(randomCard);
                return cardTemp;
            }
            else
            {
                return null;
            }
        }

        public void ReturnCard(Card card)
        {
            if (card == null)
                Console.WriteLine("Игрок ничего не отдал. У него наверное нет карт.");
            else
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

        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
            _cards = new List<Card>();
        }

        public Card GivesCard
        {
            get
            {
                if (_cards.Count > 0)
                {
                    Card card = _cards[0];
                    _cards.RemoveAt(0);
                    return card;
                }
                else
                {
                    return null;
                }
            }
        }

        public void TakesCard(Card card)
        {
            if (card == null)
                Console.WriteLine("Крупье не дал карту. В колоде нет карт");
            else
                _cards.Add(card);
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
    }

    class Croupier
    {
        private Deck _deck;

        public Card GiveCard
        {
            get
            {
                if (_deck.CardsCount > 0)
                    return _deck.GiveCard();
                else
                    return null;
            }
        }

        public Croupier() => _deck = new Deck();

        public void TakeCard(Card card) => _deck.ReturnCard(card);

        public void ShowDeck() => _deck.ShowDeck();
    }

    class Game
    {
        private const string CommandCardsDeckSee = "deck";
        private const string CommandCardsPlayerSee = "player";
        private const string CommandCardTake = "take";
        private const string CommandCardGive = "give";
        private const string CommandExit = "exit";

        private Croupier _croupier;
        private Player _player;

        public Game(Croupier croupier, Player player)
        {
            _croupier = croupier;
            _player = player;
        }

        public void Play()
        {
            bool isWorkProgram = true;

            Console.WriteLine("Правила пользования программой:\n" +
                              $"Для просмотра колоды карт наберите команду: {CommandCardsDeckSee}\n" +
                              $"Для просмотра карт которые есть у игрока наберите команду: {CommandCardsPlayerSee}\n" +
                              $"Чтобы взять карту с колоды наберите команду {CommandCardTake}\n" +
                              $"Чтобы вернуть карту игрока в колоду наберите команду {CommandCardGive}\n" +
                              $"Для завершения работы программы наберите команду {CommandExit}");
            Console.WriteLine();

            while (isWorkProgram)
            {
                Console.Write("Введите команду: ");
                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandExit:
                        isWorkProgram = false;
                        break;
                    case CommandCardGive:
                        _croupier.TakeCard(_player.GivesCard);
                        break;
                    case CommandCardsDeckSee:
                        _croupier.ShowDeck();
                        break;
                    case CommandCardsPlayerSee:
                        _player.ShowCard();
                        break;
                    case CommandCardTake:
                        _player.TakesCard(_croupier.GiveCard);
                        break;
                    default:
                        Console.WriteLine("Вы не правильно ввели команду. Попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
