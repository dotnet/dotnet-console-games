using System;
using System.Linq;

class Program
{
	// Console Settings When Launched
	static readonly bool OriginalCursorVisible = Console.CursorVisible;
	static readonly int OriginalWindowWidth = Console.WindowWidth;
	static readonly int OriginalWindowHeight = Console.WindowHeight;
	static readonly ConsoleColor OriginalBackgroundColor = Console.BackgroundColor;
	static readonly ConsoleColor OriginalForegroundColor = Console.ForegroundColor;

	const int CardsInDeck = 52;
	const int HumanPlayer = 0;
	const int PlayerCount = 4;
	const int FullHandCount = 13;
	const int PlaysPerRound = 13;

	static readonly Random Random = new();
	static readonly Player[] Players = new Player[]
	{
		new Player() { Name = "You" }, /*            0 (Human) */
		new Player() { Name = "Dixie Normous" }, /*  1         */
		new Player() { Name = "Dick Long" }, /*      2         */
		new Player() { Name = "Anita Woody" }, /*    3         */
	};

	static Card CardInPlay;
	static Suit? SuitInPlay;
	static bool CloseRequested;

	static void Main()
	{
		try
		{
			Console.CursorVisible = false;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();

			while (!Players.Any(x => x.Score >= 500))
			{
				PlayRound();
				if (CloseRequested)
				{
					return;
				}

				Console.ReadLine();
			}
		}
		finally
		{
			// Revert Changes To Console
			Console.CursorVisible = OriginalCursorVisible;
			Console.WindowWidth = OriginalWindowWidth;
			Console.WindowHeight = OriginalWindowHeight;
			Console.BackgroundColor = OriginalBackgroundColor;
			Console.ForegroundColor = OriginalForegroundColor;
		}
	}

	static void ShuffleDeckAndDealHands()
	{
		// create the deck
		Card[] deck = new Card[CardsInDeck];
		{
			int i = 0;
			foreach (var suit in Enum.GetValues(typeof(Suit)))
			{
				foreach (var value in Enum.GetValues(typeof(Value)))
				{
					deck[i++] = new Card()
					{
						Suit = (Suit)suit,
						Value = (Value)value,
					};
				}
			}
		}
		// shuffle the deck
		for (int i = 0; i < deck.Length; i++)
		{
			int randomIndex = Random.Next(0, deck.Length);
			Card temp = deck[i];
			deck[i] = deck[randomIndex];
			deck[randomIndex] = temp;
		}
		// deal the cards to each player
		for (int i = 0; i < CardsInDeck; i++)
		{
			int player = i / FullHandCount;
			int handIndex = i % FullHandCount;
			Players[player].Hand[handIndex] = deck[i];
		}
	}

	static void PlayRound()
	{
		foreach (Player player in Players)
		{
			player.CurrentRoundScore = 0;
		}
		ShuffleDeckAndDealHands();
		RenderBoard();

	}

	#region Rendering

	static void RenderBoard()
	{
		// draw player hands
		for (int player = 0; player < PlayerCount; player++)
		{
			for (int card = 0; card < FullHandCount; card++)
			{
				RenderHandCard(player, card);
			}
		}
		// draw board center
		Console.SetCursorPosition(24, 12);
		Console.Write("╔════╗");
		Console.SetCursorPosition(24, 13);
		Console.Write("║ " + (CardInPlay is null ? "  " : CardInPlay.ToString()) + " ║");
		Console.SetCursorPosition(24, 14);
		Console.Write("╚════╝");
	}

	static void RenderHandCard(int player, int handIndex)
	{
		// get the screen coordinates of the card to draw
		var (left, top) = (player, handIndex) switch
		{
			(0, _) => (handIndex * 3 + 8, 24),
			(1, _) => (4, handIndex + handIndex / 2 + 4),
			(2, _) => (handIndex * 3 + 8, 2),
			(3, _) => (48, handIndex + handIndex / 2 + 4),
			_ => throw new NotImplementedException(),
		};
		// draw the card
		Console.SetCursorPosition(left, top);
		if (player == HumanPlayer)
		{
			if (Players[player].Hand[handIndex] is null)
			{
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
				Console.Write("  ");
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
				Console.Write(Players[player].Hand[handIndex]);
			}
		}
		else
		{
			if (player % 2 == 0 || handIndex % 2 == 0)
			{
				if (Players[player].Hand[handIndex] is null)
				{
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write("  ");
				}
				else
				{
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.Write("██");
				}
			}
			else
			{
				if (Players[player].Hand[handIndex] is null)
				{
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.Write("  ");
					Console.SetCursorPosition(left, top + 1);
					Console.Write("  ");
				}
				else
				{
					Console.BackgroundColor = ConsoleColor.Black;
					Console.ForegroundColor = ConsoleColor.DarkGray;
					Console.Write("▄▄");
					Console.SetCursorPosition(left, top + 1);
					Console.Write("▀▀");
				}
			}
		}
		Console.BackgroundColor = ConsoleColor.Black;
		Console.ForegroundColor = ConsoleColor.White;
	}

	#endregion

	public class Player
	{
		public Card[] Hand = new Card[FullHandCount];
		public int Score;
		public int CurrentRoundScore;
		public string Name;
	}

	public class Card
	{
		public Value Value;
		public Suit Suit;
		public override string ToString() =>
			new(new char[] { (char)Value, (char)Suit });
	}

	public enum Value
	{
		Ace = 'A',
		Two = '2',
		Three = '3',
		Four = '4',
		Five = '5',
		Six = '6',
		Seven = '7',
		Eight = '8',
		Nine = '9',
		Ten = 'T',
		Jack = 'J',
		Queen = 'Q',
		King = 'K',
	}

	public enum Suit
	{
		Clubs = 'C',
		Diamonds = 'D',
		Hearts = 'H',
		Spades = 'S',
	}
}
