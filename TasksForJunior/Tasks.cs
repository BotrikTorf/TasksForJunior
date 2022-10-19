using System;
using System.Collections.Generic;

namespace TasksForJunior
{
    class Tasks
    {
        static void Main()
        {
            const string CommandExit = "exit";
            bool isWorkProgram = true;

            Croupier croupier = new Croupier();

            Console.Write("Введите имя игрока: ");
            string namePlayer = Console.ReadLine();

            Player player = new Player(namePlayer);

            Console.WriteLine();

            Game game = new Game(croupier, player);

            Console.WriteLine($"Правила пользования программой:\n" +
                $"Для просмотра колоды карт наберите команду: {game.CardsDeckSee}\n" +
                $"Для просмотра карт которые есть у игрока наберите команду: {game.CardsPlayerSee}\n" +
                $"Чтобы взять карту с колоды наберите команду {game.CardTake}\n" +
                $"Чтобы вернуть карту игрока в колоду наберите команду {game.CardGive}\n" +
                $"Для завершения работы программы наберите команду {CommandExit}");
            Console.WriteLine();

            while (isWorkProgram)
            {
                Console.Write("Введите команду: ");
                string command = Console.ReadLine();

                if (command == CommandExit)
                    isWorkProgram = false;
                else
                    game.GamesPlayer(command);
            }
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

        internal Card GiveCard
        {
            get
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
        }

        internal void ReturnCard(Card card)
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
                if (_deck.CardsDeck > 0)
                    return _deck.GiveCard;
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

        private Croupier _croupier;
        private Player _player;

        public string CardsDeckSee { get { return CommandCardsDeckSee; } }
        public string CardsPlayerSee { get { return CommandCardsPlayerSee; } }
        public string CardTake { get { return CommandCardTake; } }
        public string CardGive { get { return CommandCardGive; } }

        public Game(Croupier croupier, Player player)
        {
            _croupier = croupier;
            _player = player;
        }

        public void GamesPlayer(string command)
        {
            switch (command)
            {
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
