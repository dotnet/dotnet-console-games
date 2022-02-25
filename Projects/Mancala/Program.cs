using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

int[] pitsAndStores;
int[] changes;
bool closeRequested;
State state;
int selection;
Random random;

try
{
	closeRequested = false;
	random = new Random();
	Console.BackgroundColor = ConsoleColor.Black;
	Console.ForegroundColor = ConsoleColor.White;
	Console.Clear();
	Initialize();
GetInput:
	if (state is State.OutOfMovesConfimation)
	{
		MoveAllSeedsToStores();
	}
	Console.CursorVisible = false;
	Render();
	if (closeRequested)
	{
		return;
	}
	Console.CursorVisible = false;
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.LeftArrow:
			if (state is State.MoveSelection or State.InvalidMove)
			{
				selection = Math.Max(0, selection - 1);
				state = State.MoveSelection;
			}
			goto GetInput;
		case ConsoleKey.RightArrow:
			if (state is State.MoveSelection or State.InvalidMove)
			{
				selection = Math.Min(5, selection + 1);
				state = State.MoveSelection;
			}
			goto GetInput;
		case ConsoleKey.Enter:
			switch (state)
			{
				case State.MoveSelection:
					if (pitsAndStores[selection] > 0)
					{
						state = Move(selection)
							? State.MoveConfirmationAndMoveAgain
							: State.MoveConfirmation;
					}
					else
					{
						state = State.InvalidMove;
					}
					goto GetInput;
				case State.MoveConfirmationAndMoveAgain:
					for (int i = 0; i < changes.Length; i++)
					{
						changes[i] = 0;
					}
					state = IsGameOver()
						? State.OutOfMovesConfimation
						: State.MoveSelection;
					goto GetInput;
				case State.MoveConfirmation or State.OpponentMoveConfirmationMoveAgain:
					for (int i = 0; i < changes.Length; i++)
					{
						changes[i] = 0;
					}
					state =
						IsGameOver() ? State.OutOfMovesConfimation :
						OpponentMove() 
							? State.OpponentMoveConfirmationMoveAgain
							: State.OpponentMoveConfirmation;
					goto GetInput;
				case State.OpponentMoveConfirmation:
					for (int i = 0; i < changes.Length; i++)
					{
						changes[i] = 0;
					}
					state = IsGameOver()
						? State.OutOfMovesConfimation
						: State.MoveSelection;
					goto GetInput;
				case State.OutOfMovesConfimation:
					for (int i = 0; i < changes.Length; i++)
					{
						changes[i] = 0;
					}
					state = State.GameOverConfirmation;
					goto GetInput;
				case State.GameOverConfirmation:
					Initialize();
					goto GetInput;
			}
			goto GetInput;
		case ConsoleKey.Escape:
			closeRequested = true;
			return;
		default:
			goto GetInput;
	}
}
finally
{
	Console.ResetColor();
	Console.Clear();
	Console.Write("Mancala closed.");
	Console.CursorVisible = true;
}

void Initialize()
{
	state = State.MoveSelection;
	selection = 0;
	pitsAndStores = new int[14];
	changes = new int[14];
	for (int i = 0; i < pitsAndStores.Length; i++)
	{
		pitsAndStores[i] = i is 6 or 13 ? 0 : 4;
		changes[i] = 0;
	}
}

bool Move(int pit)
{
	bool isOpponent = pit > 6;
	int[] pitsBefore = (int[])pitsAndStores.Clone();
	int count = pitsAndStores[pit];
	pitsAndStores[pit] = 0;
	changes[pit] = -count;
	int j = 0;
	for (int i = 0, skipped = 0; i < count + skipped; i++)
	{
		j = (i + pit + 1) % pitsAndStores.Length;
		if ((isOpponent && j is 6) || (!isOpponent && j is 13))
		{
			skipped++;
		}
		else
		{
			pitsAndStores[j]++;
		}
	}
	if (isOpponent && j > 6 && j < 13 && pitsBefore[j] is 0 && pitsAndStores[j] is 1)
	{
		int mirrorPit = 13 - j - 1;
		if (pitsAndStores[mirrorPit] > 0)
		{
			pitsAndStores[13] += pitsAndStores[mirrorPit];
			pitsAndStores[13] += pitsAndStores[j];
			pitsAndStores[mirrorPit] = 0;
			pitsAndStores[j] = 0;
		}
	}
	if (!isOpponent && j < 6 && j >= 0 && pitsBefore[j] is 0 && pitsAndStores[j] is 1)
	{
		int mirrorPit = 6 - j + 6;
		if (pitsAndStores[mirrorPit] > 0)
		{
			pitsAndStores[6] += pitsAndStores[mirrorPit];
			pitsAndStores[6] += pitsAndStores[j];
			pitsAndStores[mirrorPit] = 0;
			pitsAndStores[j] = 0;
		}
	}
	for (int i = 0; i < pitsAndStores.Length; i++)
	{
		changes[i] = pitsAndStores[i] - pitsBefore[i];
	}
	return (isOpponent && j is 13) || (!isOpponent && j is 6);
}

void MoveAllSeedsToStores()
{
	int[] pitsBefore = (int[])pitsAndStores.Clone();
	for (int i = 0; i < 6; i++)
	{
		pitsAndStores[6] += pitsAndStores[i];
		pitsAndStores[i] = 0;
	}
	for (int i = 7; i < 13; i++)
	{
		pitsAndStores[13] += pitsAndStores[i];
		pitsAndStores[i] = 0;
	}
	for (int i = 0; i < pitsAndStores.Length; i++)
	{
		changes[i] = pitsAndStores[i] - pitsBefore[i];
	}
}

bool OpponentMove()
{
	List<int> possibleMoves = new();
	for (int i = 7; i < 13; i++)
	{
		if (pitsAndStores[i] is not 0)
		{
			possibleMoves.Add(i);
		}
	}
	int move = possibleMoves[random.Next(possibleMoves.Count)];
	return Move(move);
}

void Render()
{
	string PitValue(int pit)
	{
		string value = pitsAndStores[pit].ToString(CultureInfo.InvariantCulture);
		return value.Length < 2 ? " " + value : value; 
	}

	void WriteAlignedDifference(int pit, bool isLeft)
	{
		if (changes[pit] < 10 && !isLeft)
		{
			Console.Write(' ');
		}
		WriteDifference(pit);
		if (changes[pit] < 10 && isLeft)
		{
			Console.Write(' ');
		}
	}

	void WriteDifference(int pit)
	{
		string valueString = Math.Abs(changes[pit]).ToString(CultureInfo.InvariantCulture);
		switch (changes[pit])
		{
			case 0:
				Console.Write("  ");
				break;
			case < 0:
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.Write('-');
				Console.Write(valueString);
				Console.ForegroundColor = ConsoleColor.White;
				break;
			case > 0:
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				Console.Write('+');
				Console.Write(valueString);
				Console.ForegroundColor = ConsoleColor.White;
				break;
		}
	}

	EnsureConsoleSize();
	if (closeRequested)
	{
		return;
	}
	Console.SetCursorPosition(0, 0);
	Console.WriteLine();
	Console.WriteLine("  Mancala");
	Console.WriteLine();
	Console.WriteLine("  ╔══════════════════════════════════╗");
	Console.WriteLine($"  ║ |  |[{PitValue(12)}][{PitValue(11)}][{PitValue(10)}][{PitValue(09)}][{PitValue(08)}][{PitValue(07)}]|  | ║");
	Console.Write("  ║ |  | ");
	for (int i = 12; i > 6; i--)
	{
		WriteDifference(i);
		if (i > 7)
		{
			Console.Write(Math.Abs(changes[i]) >= 10 ? " " : "  ");
		}
	}
	if (Math.Abs(changes[7]) < 10)
	{
		Console.Write(' ');
	}
	Console.WriteLine("|  | ║");
	Console.Write($"  ║ |{PitValue(13)}|");
	WriteAlignedDifference(13, true);
	Console.Write("                  ");
	WriteAlignedDifference(6, false);
	Console.WriteLine($"|{PitValue(06)}| ║");
	Console.Write($"  ║ |  | ");
	for (int i = 0; i < 6; i++)
	{
		if (state is State.MoveSelection or State.InvalidMove && i == selection)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(@"\/");
			Console.ForegroundColor = ConsoleColor.White;
		}
		else
		{
			WriteDifference(i);
		}
		if (i < 5)
		{
			Console.Write(Math.Abs(changes[i]) >= 10 ? " " : "  ");
		}
	}
	if (Math.Abs(changes[5]) < 10)
	{
		Console.Write(' ');
	}
	Console.WriteLine("|  | ║");
	Console.WriteLine($"  ║ |  |[{PitValue(00)}][{PitValue(01)}][{PitValue(02)}][{PitValue(03)}][{PitValue(04)}][{PitValue(05)}]|  | ║");
	Console.WriteLine("  ╚══════════════════════════════════╝");
	Console.WriteLine();
	switch (state)
	{
		case State.MoveSelection:
			Console.WriteLine("  Pick your move.                            ");
			Console.WriteLine("                                             ");
			break;
		case State.MoveConfirmationAndMoveAgain:
			Console.WriteLine("  You moved.                                 ");
			Console.WriteLine("  You get another move.                      ");
			break;
		case State.MoveConfirmation:
			Console.WriteLine("  You moved.                                 ");
			Console.WriteLine("                                             ");
			break;
		case State.OpponentMoveConfirmation:
			Console.WriteLine("  Your opponent moved.                       ");
			Console.WriteLine("                                             ");
			break;
		case State.OpponentMoveConfirmationMoveAgain:
			Console.WriteLine("  Your opponent moved.                       ");
			Console.WriteLine("  Your opponent gets another move.           ");
			break;
		case State.InvalidMove:
			Console.WriteLine("  Invalid move.                              ");
			Console.WriteLine("  You must select a non-empty pit.           ");
			break;
		case State.OutOfMovesConfimation:
			bool playerIsEmpty = !pitsAndStores[0..6].Any(seeds => seeds > 0);
			if (playerIsEmpty)
			{
				Console.WriteLine("  You are out of moves. Remaining seeds are  ");
				Console.WriteLine("  added to opponent's store.                 ");
			}
			else
			{
				Console.WriteLine("  Your opponent is out of moves. Remaining   ");
				Console.WriteLine("  seeds are  added to your store.            ");
			}
			break;
		case State.GameOverConfirmation:
			if (pitsAndStores[6] > pitsAndStores[13])
			{
				Console.WriteLine("  Game Over. You Win!                        ");
			}
			else if (pitsAndStores[6] < pitsAndStores[13])
			{
				Console.WriteLine("  Game Over. You Lose!                       ");
			}
			else
			{
				Console.WriteLine("  Game Over. Tie!                            ");
			}
			Console.WriteLine("  Play again [enter] or quit [escape]?       ");
			break;
	}
	Console.WriteLine();
	Console.WriteLine("  Controls...");
	Console.WriteLine("  - left/right arrow: move selection");
	Console.WriteLine("  - enter: confirm");
	Console.WriteLine("  - escape: close");
}

void EnsureConsoleSize()
{
	int width = Console.WindowWidth;
	int height = Console.WindowHeight;
	int minWidth = 40;
	int minHeight = 15;
	while (!closeRequested && (width < minWidth || height < minHeight))
	{
		Console.Clear();
		Console.WriteLine("Increase console size and press [enter]...");
		bool enter = false;
		while (!closeRequested && !enter)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.Enter:
					enter = true;
					break;
				case ConsoleKey.Escape:
					closeRequested = true;
					break;
			}
		}
		width = Console.WindowWidth;
		height = Console.WindowHeight;
		Console.Clear();
	}
}

bool IsGameOver()
{
	bool playerEmpty = true;
	for (int i = 0; i < 6; i++)
	{
		if (pitsAndStores[i] is not 0)
		{
			playerEmpty = false;
		}
	}
	bool opponentEmpty = true;
	for (int i = 12; i > 6; i--)
	{
		if (pitsAndStores[i] is not 0)
		{
			opponentEmpty = false;
		}
	}
	return playerEmpty || opponentEmpty;
}

enum State
{
	InvalidMove,
	MoveSelection,
	MoveConfirmation,
	MoveConfirmationAndMoveAgain,
	OpponentMoveConfirmation,
	OpponentMoveConfirmationMoveAgain,
	OutOfMovesConfimation,
	GameOverConfirmation,
}