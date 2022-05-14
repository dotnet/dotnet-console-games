using System;
using System.Collections.Generic;
using System.Globalization;

List<Card> deck;
List<Card> discardPile;
List<PlayerHand> playerHands;
List<Card> dealerHand;
int playerMoney = 100;
const int minimumBet = 2;
const int maximumBet = 500;
int previousBet = 10;
int bet;
int activeHand;
State state = State.IntroScreen;
State previousState = default;

try
{
	Initialize();
	while (playerMoney > minimumBet)
	{

		activeHand = 0;
		DefaultBet();
	GetInput:
		Render();
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter:
				FinishEnterHandle:
				switch (state)
				{
					case State.IntroScreen:
						state = State.PlaceBet;
						break;
					case State.PlaceBet:
						playerMoney -= bet;
						InitializeRound();
						state = State.ConfirmHand;
						break;
					case State.ConfirmHand:
						if (ScoreCards(playerHands[activeHand].Cards) is 21)
						{
							playerMoney += (playerHands[activeHand].Bet / 2) * 3;
							playerHands[activeHand].Resolved = true;
							state = State.ConfirmDealtBlackjack;
							goto GetInput;
						}
						state = State.ChooseMove;
						goto GetInput;
					case State.ConfirmSplit:
						#warning TODO: check for dealt blackjack
						state = State.ChooseMove;
						break;
					case State.ConfirmDealtBlackjack:
						ProgressStateAfterHandCompletion();
						goto GetInput;
					case State.ConfirmBust:
						ProgressStateAfterHandCompletion();
						goto GetInput;
					case State.ConfirmDraw:
						ProgressStateAfterDealerAction();
						break;
					case State.ConfirmLoss:
						ProgressStateAfterDealerAction();
						break;
					case State.ConfirmDealerCardFlip:
						ProgressStateAfterDealerAction();
						break;
					case State.ConfirmDealerDraw:
						ProgressStateAfterDealerAction();
						break;
					case State.ConfirmWin:
						ProgressStateAfterDealerAction();
						break;
					case State.ShuffleDiscardIntoDeck:
						state = previousState;
						goto FinishEnterHandle;
				}
				goto GetInput;
			case ConsoleKey.DownArrow:
				if (state is State.PlaceBet)
				{
					bet = Math.Max(minimumBet, bet - 2);
				}
				goto GetInput;
			case ConsoleKey.UpArrow:
				if (state is State.PlaceBet)
				{
					bet = Math.Min(Math.Min(maximumBet, playerMoney), bet + 2);
					if (bet % 2 is 1)
					{
						bet--;
					}
				}
				goto GetInput;
			case ConsoleKey.D1: // stay
				if (state is State.ChooseMove)
				{
					ProgressStateAfterHandCompletion();
				}
				break;
			case ConsoleKey.D2: // hit
				if (state is State.ChooseMove)
				{
					playerHands[activeHand].Cards.Add(deck[^1]);
					deck.RemoveAt(deck.Count - 1);
					if (ScoreCards(playerHands[activeHand].Cards) > 21)
					{
						playerHands[activeHand].Resolved = true;
						state = State.ConfirmBust;
					}
				}
				break;
			case ConsoleKey.D3: // double down
				if (state is State.ChooseMove)
				{
					throw new NotImplementedException();
				}
				break;
			case ConsoleKey.D4: // split
				if (state is State.ChooseMove && CanSplit())
				{
					playerMoney -= playerHands[activeHand].Bet;
					playerHands.Add(new());
					playerHands[^1].Bet = playerHands[activeHand].Bet;
					playerHands[activeHand + 1].Cards.Add(playerHands[activeHand].Cards[^1]);
					playerHands[activeHand].Cards.RemoveAt(playerHands[activeHand].Cards.Count - 1);
					playerHands[activeHand].Cards.Add(deck[^1]);
					deck.RemoveAt(deck.Count - 1);
					playerHands[activeHand + 1].Cards.Add(deck[^1]);
					deck.RemoveAt(deck.Count - 1);
					state = State.ConfirmSplit;
				}
				break;
			case ConsoleKey.Escape:
				return;
			default:
				goto GetInput;
		}

	}
}
finally
{
	Console.CursorVisible = true;
	Console.Clear();
	Console.Write("Blackjack was closed.");
}

void Render()
{
	Console.CursorVisible = false;
	Console.Clear();
	Console.WriteLine();
	Console.WriteLine("  Blackjack");
	Console.WriteLine();
	if (state is State.IntroScreen)
	{
		Console.WriteLine("  This is the Blackjack card game. The goal is to get closer");
		Console.WriteLine("  to 21 than the dealer without busting (going over 21). At the");
		Console.WriteLine("  start of each round you will place a bet, then the dealer will");
		Console.WriteLine("  deal you two cards and himself two cards (one face up and one");
		Console.WriteLine("  face down). Then you will choose your move: stay, hit, double");
		Console.WriteLine("  down, or split (up to 3 times) until your turn is complete.");
		Console.WriteLine("  Then the dealer will reveal his face down card and draw cards");
		Console.WriteLine("  as needed in attempts to be closer to 21 as you.");
		Console.WriteLine();
		Console.WriteLine("  Bets must be in multiples of $2.");
		Console.WriteLine();
		Console.WriteLine("  Card Values: Ace (1 or 11), Jack (10), Queen (10), King(10),");
		Console.WriteLine("  and all other cards use their number value (eg. 3H -> 3).");
		Console.WriteLine();
		Console.WriteLine("  Press [escape] to close the game at any time.");
		Console.WriteLine();
		Console.WriteLine("  Press [enter] to continue...");
		return;
	}
	Console.WriteLine($"  Cards In Dealer Deck: {deck.Count}");
	Console.WriteLine($"  Cards In Discard Pile: {discardPile.Count}");
	Console.WriteLine($"  Your Money: ${playerMoney}");
	if (state is not State.IntroScreen && state is not State.PlaceBet)
	{
		Console.WriteLine();
		Console.WriteLine($"  Dealer Hand:");
		for (int i = 0; i < Card.RenderHeight; i++)
		{
			Console.Write("    ");
			for (int j = 0; j < dealerHand.Count; j++)
			{
				string s = dealerHand[j].Render()[i];
				Console.Write(j < dealerHand.Count - 1 ? s[..5] : s);
			}
			Console.WriteLine();
		}
		Console.WriteLine();
		Console.WriteLine($"  Your Hand{(playerHands.Count > 1 ? "s" : "")}:");
		for (int hand = 0; hand < playerHands.Count; hand++)
		{
			for (int i = 0; i < Card.RenderHeight; i++)
			{
				if (hand == activeHand)
				{
					Console.Write(i == Card.RenderHeight / 2 ? "  > " : "    ");
				}
				else
				{
					Console.Write("    ");
				}
				for (int j = 0; j < playerHands[hand].Cards.Count; j++)
				{
					string s = playerHands[hand].Cards[j].Render()[i];
					Console.Write(j < playerHands[hand].Cards.Count - 1 ? s[..5] : s);
				}
				Console.WriteLine();
			}
			Console.WriteLine($"    Hand Score: {ScoreCards(playerHands[hand].Cards)}");
			Console.WriteLine($"    Hand Bet: ${playerHands[hand].Bet}");
		}
	}
	Console.WriteLine();
	switch (state)
	{
		case State.PlaceBet:
			Console.WriteLine("  Place your bet...");
			Console.WriteLine("  Use [up] or [down] arrows to increase or decrease bet.");
			Console.WriteLine("  Use [enter] to place your bet.");
			Console.WriteLine($"  Bet (${minimumBet}-${maximumBet}): ${bet}");
			break;
		case State.ShuffleDiscardIntoDeck:
			Console.WriteLine("  Discard pile was shuffled back into deck.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ConfirmDealtBlackjack:
			Console.WriteLine("  You were dealt a blackjack (21). You win this hand!");
			Console.WriteLine("  Your bet was payed out.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ConfirmHand:
			Console.WriteLine("  The dealer deals the hands.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ChooseMove:
			Console.WriteLine("  Choose your move...");
			Console.WriteLine("  [1] Stay");
			Console.WriteLine("  [2] Hit");
			Console.WriteLine("  [3] Double Down");
			if (CanSplit())
			{
				Console.WriteLine("  [4] Split");
			}
			break;
		case State.ConfirmBust:
			Console.WriteLine($"  Bust! Your hand ({ScoreCards(playerHands[activeHand].Cards)}) is greater than 21.");
			Console.WriteLine("  You lose this bet.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ConfirmSplit:
			Console.WriteLine("  You split your hand and the dealer dealt you an additional");
			Console.WriteLine("  card to each split.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ConfirmDealerDraw:
			Console.WriteLine("  The dealer drew a card to his hand.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ConfirmDealerCardFlip:
			Console.WriteLine("  The dealer flipped over his second card.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ConfirmLoss:
			Console.WriteLine($"  Lost! The dealer ({ScoreCards(dealerHand)}) beat your hand ({ScoreCards(playerHands[activeHand].Cards)}).");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ConfirmDraw:
			Console.WriteLine($"  Draw! This hand was equal to the dealer's hand ({ScoreCards(dealerHand)}).");
			Console.WriteLine($"  Your bet was returned to you.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		case State.ConfirmWin:
			Console.WriteLine($"  Win! The dealer {(ScoreCards(dealerHand) > 21 ? "busted" : "stands")} ({ScoreCards(dealerHand)}).");
			Console.WriteLine($"  Your bet was payed out.");
			Console.WriteLine("  Press [enter] to continue...");
			break;
		default:
			throw new NotImplementedException();
	}
}

bool CanSplit() =>
	playerMoney >= playerHands[activeHand].Bet &&
	playerHands[activeHand].Cards.Count is 2 &&
	playerHands[activeHand].Cards[0].Value == playerHands[activeHand].Cards[1].Value &&
	playerHands.Count < 4;

int ScoreCards(List<Card> cards)
{
	int score = 0;
	int numberOfAces = 0;
	foreach (Card card in cards)
	{
		if (card.Value is Value.King or Value.Queen or Value.Jack)
		{
			score += 10;
		}
		else if (card.Value is not Value.Ace)
		{
			score += (int)card.Value;
		}
		else
		{
			numberOfAces++;
		}
	}
	if (numberOfAces is 0)
	{
		return score;
	}
	int scoreWithAn11 = score + 11 + (numberOfAces - 1);
	if (scoreWithAn11 <= 21)
	{
		return scoreWithAn11;
	}
	else
	{
		return score + numberOfAces;
	}
}

void Initialize()
{
	discardPile = new();
	playerHands = new();
	dealerHand = new();
	deck = new();
	foreach (Suit suit in Enum.GetValues<Suit>())
	{
		foreach (Value value in Enum.GetValues<Value>())
		{
			deck.Add(new()
			{
				Suit = suit,
				Value = value,
				FaceUp = true,
			});
		}
	}
	Shuffle(deck);
}

void InitializeRound()
{
	playerHands = new();
	playerHands.Add(new());
	playerHands[0].Bet = bet;
	playerHands[0].Cards.Add(DrawCard());
	playerHands[0].Cards.Add(DrawCard());
	dealerHand = new();
	dealerHand.Add(DrawCard());
	dealerHand.Add(DrawCard());
	dealerHand[^1].FaceUp = false;
}

bool UnresolvedHands()
{
	bool unresolvedHands = false;
	foreach (PlayerHand hand in playerHands)
	{
		if (!hand.Resolved)
		{
			unresolvedHands = true;
			break;
		}
	}
	return unresolvedHands;
}

void ProgressStateAfterHandCompletion()
{
	if (!UnresolvedHands())
	{
		discardPile.AddRange(dealerHand);
		foreach (PlayerHand hand in playerHands)
		{
			discardPile.AddRange(hand.Cards);
		}
		InitializeRound();
		state = State.PlaceBet;
		return;
	}
	do
	{
		activeHand++;
	} while (activeHand < playerHands.Count - 1 && ScoreCards(playerHands[activeHand].Cards) > 21);
	if (activeHand < playerHands.Count - 1)
	{
		state = State.ChooseMove;
	}
	else
	{
		activeHand = 0;
		dealerHand[^1].FaceUp = true;
		state = State.ConfirmDealerCardFlip;
	}
}

void ProgressStateAfterDealerAction()
{
	if (!UnresolvedHands())
	{
		state = State.PlaceBet;
		return;
	}
	for (int i = 0; i < playerHands.Count; i++)
	{
		if (!playerHands[i].Resolved)
		{
			if (ScoreCards(dealerHand) > 21)
			{
				activeHand = i;
				playerMoney += playerHands[activeHand].Bet * 2;
				playerHands[activeHand].Resolved = true;
				state = State.ConfirmWin;
				return;
			}
			else if (ScoreCards(playerHands[i].Cards) < ScoreCards(dealerHand))
			{
				activeHand = i;
				playerHands[activeHand].Bet = 0;
				playerHands[activeHand].Resolved = true;
				state = State.ConfirmLoss;
				return;
			}
			else if (ScoreCards(playerHands[i].Cards) == ScoreCards(dealerHand))
			{
				activeHand = i;
				playerMoney += playerHands[activeHand].Bet;
				playerHands[activeHand].Bet = 0;
				playerHands[activeHand].Resolved = true;
				state = State.ConfirmDraw;
				return;
			}
		}
	}
	if (DealerNeedsToDraw())
	{
		dealerHand.Add(DrawCard());
		state = State.ConfirmDealerDraw;
		return;
	}
	for (int i = 0; i < playerHands.Count; i++)
	{
		if (!playerHands[i].Resolved)
		{
			if (ScoreCards(dealerHand) < ScoreCards(playerHands[activeHand].Cards))
			{
				activeHand = i;
				playerMoney += playerHands[activeHand].Bet * 2;
				playerHands[activeHand].Resolved = true;
				state = State.ConfirmWin;
				return;
			}
		}
	}
}

bool DealerNeedsToDraw()
{
	bool needToDraw = false;
	foreach (PlayerHand hand in playerHands)
	{
		if (!hand.Resolved && ScoreCards(hand.Cards) <= 21 && ScoreCards(hand.Cards) > ScoreCards(dealerHand))
		{
			needToDraw = true;
			break;
		}
	}
	return needToDraw && ScoreCards(dealerHand) < 17;
}

void DefaultBet()
{
	bet = Math.Min(playerMoney, previousBet);
	if (bet % 2 is 1)
	{
		bet--;
	}
}

void Shuffle(List<Card> cards)
{
	for (int i = 0; i < cards.Count; i++)
	{
		int swap = Random.Shared.Next(cards.Count);
		(cards[i], cards[swap]) = (cards[swap], cards[i]);
	}
}

void ShuffleDiscardIntoDeck()
{
	deck.AddRange(discardPile);
	discardPile.Clear();
	Shuffle(deck);
}

Card DrawCard()
{
	if (deck.Count <= 0)
	{
		ShuffleDiscardIntoDeck();
		previousState = state;
		state = State.ShuffleDiscardIntoDeck;
	}
	Card card = deck[^1];
	deck.RemoveAt(deck.Count - 1);
	return card;
}

class Card
{
	public Suit Suit;
	public Value Value;
	public bool FaceUp = true;

	public const int RenderHeight = 7;

	public string[] Render()
	{
		if (!FaceUp)
		{
			return new string[]
			{
				$"┌───────┐",
				$"│███████│",
				$"│███████│",
				$"│███████│",
				$"│███████│",
				$"│███████│",
				$"└───────┘",
			};
		}

		char suit = Suit.ToString()[0];

		string a = Value switch
		{
			Value.Ace   => $"A{suit} ",
			Value.Ten   => $"10{suit}",
			Value.Jack  => $"J{suit} ",
			Value.Queen => $"Q{suit} ",
			Value.King  => $"K{suit} ",
			_ => $"{((int)Value).ToString(CultureInfo.InvariantCulture)}{suit} ",
		};

		string b = Value switch
		{
			Value.Ace   => " A",
			Value.Ten   => "10",
			Value.Jack  => " J",
			Value.Queen => " Q",
			Value.King  => " K",
			_ => " " + ((int)Value).ToString(CultureInfo.InvariantCulture),
		} + suit;

		return new string[]
		{
			$"┌───────┐",
			$"│{a}    │",
			$"│       │",
			$"│       │",
			$"│       │",
			$"│    {b}│",
			$"└───────┘",
		};
	}
}

class PlayerHand
{
	public List<Card> Cards = new();
	public int Bet;
	public bool Resolved = false;
}

enum Suit
{
	Hearts,
	Clubs,
	Spades,
	Diamonds,
}

enum Value
{
	Ace   = 01,
	Two   = 02,
	Three = 03,
	Four  = 04,
	Five  = 05,
	Six   = 06,
	Seven = 07,
	Eight = 08,
	Nine  = 09,
	Ten   = 10,
	Jack  = 11,
	Queen = 12,
	King  = 13,
}

enum State
{
	IntroScreen,
	PlaceBet,
	ShuffleDiscardIntoDeck,
	ConfirmHand,
	ConfirmDealtBlackjack,
	ChooseMove,
	ConfirmBust,
	ConfirmSplit,
	ConfirmDealerDraw,
	ConfirmDealerCardFlip,
	ConfirmLoss,
	ConfirmDraw,
	ConfirmWin,
}