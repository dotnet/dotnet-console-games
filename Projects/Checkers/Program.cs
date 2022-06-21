using Checkers;
using System.Text;

const char BlackPiece = '○';
const char BlackKing  = '☺';
const char WhitePiece = '◙';
const char WhiteKing  = '☻';

Encoding encoding = Console.OutputEncoding;
Game? game = null;

try
{
	Console.OutputEncoding = Encoding.UTF8;
	ShowIntroScreenAndGetOption();
	RunGameLoop();
	HandleGameOver();
}
finally
{
	Console.OutputEncoding = encoding;
	Console.CursorVisible = true;
	Console.Clear();
	Console.Write("Checkers was closed.");
}

void ShowIntroScreenAndGetOption()
{
	Console.Clear();
	Console.WriteLine();
	Console.WriteLine("  Checkers");
	Console.WriteLine();
	Console.WriteLine("  Checkers is  played on an 8x8  board between two sides commonly known as black");
	Console.WriteLine("  and white. The objective is  simple - capture  all  your opponent's pieces. An");
	Console.WriteLine("  alternative way to  win is to trap  your opponent so that  they have no  valid");
	Console.WriteLine("  moves left.");
	Console.WriteLine();
	Console.WriteLine("  Black starts first and players take it  in turns to move their pieces  forward");
	Console.WriteLine("  across the board diagonally. Should a piece  reach the other side of the board");
	Console.WriteLine("  the piece becomes a `king` and can  then move diagonally backwards as  well as");
	Console.WriteLine("  forwards.");
	Console.WriteLine();
	Console.WriteLine("  Pieces are captured by jumping over them diagonally. More than one enemy piece");
	Console.WriteLine("  can be captured in the same turn by the same piece.");
	Console.WriteLine();
	Console.WriteLine("  Moves  are selected  with the arrow keys. Use the [enter] button to select the");
	Console.WriteLine("  from and to squares. Invalid moves are ignored.");
	Console.WriteLine();
	Console.WriteLine("  3 modes of play are possible depending on the number of players entered:");
	Console.WriteLine("    0 - black and white are controlled by the computer");
	Console.WriteLine("    1 - black is controlled by the player and white by the computer");
	Console.WriteLine("    2 - allows 2 players");
	Console.WriteLine();
	Console.Write("  Enter the number of players (0-2): ");

	string? entry = Console.ReadLine()?.Trim();
	while (entry is not "0" and not "1" and not "2")
	{
		Console.WriteLine("  Invalid Input. Try Again.");
		Console.Write("  Enter the number of players (0-2): ");
		entry = Console.ReadLine()?.Trim();
	}
	int humanPlayerCount = entry[0] - '0';
	game = new Game(humanPlayerCount);
}

void RunGameLoop()
{
	while (game!.Winner is null)
	{
		Player? currentPlayer = game.Players.FirstOrDefault(player => player.Color == game.Turn);
		if (currentPlayer is not null && currentPlayer.IsHuman)
		{
			while (game.Turn == currentPlayer.Color)
			{
				(int X, int Y)? from = null;
				(int X, int Y)? to = null;
				while (to is null)
				{
					while (from is null)
					{
						from = HumanMoveSelection();
					}
					to = HumanMoveSelection(selectionStart: from);
				}
				Piece? piece = null;
				if (from is not null)
				{
					piece = game.Board[from.Value.X, from.Value.Y];
				}
				if (piece is null || piece.Color != game.Turn)
				{
					from = null;
					to = null;
				}
				if (from is not null && to is not null)
				{
					game.NextTurn(from, to);
				}
			}
		}
		else
		{
			game.NextTurn();
		}

		RenderGameState(playerMoved: currentPlayer);
		PressAnyKeyToContinue();
	}
}

void HandleGameOver()
{
	RenderGameState();
}

void PressAnyKeyToContinue()
{
	const string prompt = "  Press any key to cotinue...";
	(int left, int top) = (Console.CursorLeft, Console.CursorTop);
	Console.Write(prompt);
	Console.ReadKey(true);
	Console.SetCursorPosition(left, top);
	Console.Write(new string(' ', prompt.Length));
}

void RenderGameState(Player? playerMoved = null, (int X, int Y)? selection = null)
{
	Console.CursorVisible = false;
	Console.Clear();
	Dictionary<(int X, int Y), char> tiles = new();
	foreach (Piece piece in game!.Board.Pieces)
	{
		tiles[(piece.X, piece.Y)] = ToChar(piece);
	}
	char C(int x, int y) => (x, y) == selection ? '$' : tiles.GetValueOrDefault((x, y), '.');
	StringBuilder sb = new();
	sb.AppendLine();
	sb.AppendLine("  Checkers");
	sb.AppendLine();
	sb.AppendLine($"    ╔═══════════════════╗");
	sb.AppendLine($"  8 ║  {C(0, 7)} {C(1, 7)} {C(2, 7)} {C(3, 7)} {C(4, 7)} {C(5, 7)} {C(6, 7)} {C(7, 7)}  ║ {BlackPiece} = Black");
	sb.AppendLine($"  7 ║  {C(0, 6)} {C(1, 6)} {C(2, 6)} {C(3, 6)} {C(4, 6)} {C(5, 6)} {C(6, 6)} {C(7, 6)}  ║ {BlackKing} = Black King");
	sb.AppendLine($"  6 ║  {C(0, 5)} {C(1, 5)} {C(2, 5)} {C(3, 5)} {C(4, 5)} {C(5, 5)} {C(6, 5)} {C(7, 5)}  ║ {WhitePiece} = White");
	sb.AppendLine($"  5 ║  {C(0, 4)} {C(1, 4)} {C(2, 4)} {C(3, 4)} {C(4, 4)} {C(5, 4)} {C(6, 4)} {C(7, 4)}  ║ {WhiteKing} = White King");
	sb.AppendLine($"  4 ║  {C(0, 3)} {C(1, 3)} {C(2, 3)} {C(3, 3)} {C(4, 3)} {C(5, 3)} {C(6, 3)} {C(7, 3)}  ║");
	sb.AppendLine($"  3 ║  {C(0, 2)} {C(1, 2)} {C(2, 2)} {C(3, 2)} {C(4, 2)} {C(5, 2)} {C(6, 2)} {C(7, 2)}  ║ Taken:");
	sb.AppendLine($"  2 ║  {C(0, 1)} {C(1, 1)} {C(2, 1)} {C(3, 1)} {C(4, 1)} {C(5, 1)} {C(6, 1)} {C(7, 1)}  ║ {game.TakenCount(PieceColor.White),2} x {WhitePiece}");
	sb.AppendLine($"  1 ║  {C(0, 0)} {C(1, 0)} {C(2, 0)} {C(3, 0)} {C(4, 0)} {C(5, 0)} {C(6, 0)} {C(7, 0)}  ║ {game.TakenCount(PieceColor.Black),2} x {BlackPiece}");
	sb.AppendLine($"    ╚═══════════════════╝");
	sb.AppendLine($"       A B C D E F G H");
	sb.AppendLine();
	if (selection is not null)
	{
		sb.Replace(" $ ", $"[{tiles.GetValueOrDefault(selection.Value, '.')}]");
	}
	if (game.Winner is not null)
	{
		sb.AppendLine($"  *** {game.Winner} wins ***");
	}
	else if (playerMoved is not null)
	{
		sb.AppendLine($"  {playerMoved.Color} moved");
	}
	else
	{
		sb.AppendLine($"  {game.Turn}'s turn");
	}
	Console.Write(sb);
}

static char ToChar(Piece piece) =>
	(piece.Color, piece.Promoted) switch
	{
		(PieceColor.Black, false) => BlackPiece,
		(PieceColor.Black, true) => BlackKing,
		(PieceColor.White, false) => WhitePiece,
		(PieceColor.White, true) => WhiteKing,
		_ => throw new NotImplementedException(),
	};

(int X, int Y)? HumanMoveSelection((int X, int y)? selectionStart = null)
{
	(int X, int Y) selection = selectionStart ?? (3, 3);
	while (true)
	{
		RenderGameState(selection: selection);
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.DownArrow:  selection.Y = Math.Max(0, selection.Y - 1); break;
			case ConsoleKey.UpArrow:    selection.Y = Math.Min(7, selection.Y + 1); break;
			case ConsoleKey.LeftArrow:  selection.X = Math.Max(0, selection.X - 1); break;
			case ConsoleKey.RightArrow: selection.X = Math.Min(7, selection.X + 1); break;
			case ConsoleKey.Enter:      return selection;
			case ConsoleKey.Escape:     return null;
		}
	}
}
