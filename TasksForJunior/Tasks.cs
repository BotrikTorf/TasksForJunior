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

        public Deck()
        {
            _cards = new List<Card>();
            string[] suit = { "club", "diamond", "heart", "spade" };
            string[] rank = { "6", "7", "8", "9", "10", "Ase", "Jack", "King", "Queen" };

            for (int i = 0; i < rank.Length; i++)
            {
                for (int j = 0; j < suit.Length; j++)
                {
                    _cards.Add(new Card(suit[j], rank[i]));
                }
            }
        }

        public int CardsCount { get { return _cards.Count; } }

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

        public void TakeCard(Card card)
        {
            if (card == null)
                Console.WriteLine("Игрок ничего не отдал. У него нет карт.");
            else
                _cards.Add(card);
        }

        public void Show()
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

        public Player(string name)
        {
            Name = name;
            _cards = new List<Card>();
        }

        public string Name { get; private set; }

        public Card GiveCard()
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

        public void TakeCard(Card card)
        {
            if (card == null)
                Console.WriteLine("Крупье не дал карту. В колоде нет карт");
            else
                _cards.Add(card);
        }

        public void ShowCards()
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
        
        public Croupier() => _deck = new Deck();

        public Card GiveCard
        {
            get { return _deck.CardsCount > 0 ? _deck.GiveCard() : null; }
        }

        public void TakeCard(Card card) => _deck.TakeCard(card);

        public void ShowDeck() => _deck.Show();
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
                        _croupier.TakeCard(_player.GiveCard());
                        break;
                    case CommandCardsDeckSee:
                        _croupier.ShowDeck();
                        break;
                    case CommandCardsPlayerSee:
                        _player.ShowCards();
                        break;
                    case CommandCardTake:
                        _player.TakeCard(_croupier.GiveCard);
                        break;
                    default:
                        Console.WriteLine("Вы не правильно ввели команду. Попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
