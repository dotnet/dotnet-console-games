﻿Encoding encoding = Console.OutputEncoding;

try
{
	Console.OutputEncoding = Encoding.UTF8;
	Game game = ShowIntroScreenAndGetOption();
	Console.Clear();
	RunGameLoop(game);
	RenderGameState(game, promptPressKey: true);
	Console.ReadKey(true);
}
finally
{
	Console.OutputEncoding = encoding;
	Console.CursorVisible = true;
	Console.Clear();
	Console.Write("Checkers was closed.");
}

Game ShowIntroScreenAndGetOption()
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
	return new Game(humanPlayerCount);
}

void RunGameLoop(Game game)
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
						from = HumanMoveSelection(game);
					}
					to = HumanMoveSelection(game, selectionStart: from);
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

		RenderGameState(game, playerMoved: currentPlayer, promptPressKey: true);
		Console.ReadKey(true);
	}
}

void RenderGameState(Game game, Player? playerMoved = null, (int X, int Y)? selection = null, bool promptPressKey = false)
{
	const char BlackPiece = '○';
	const char BlackKing  = '☺';
	const char WhitePiece = '◙';
	const char WhiteKing  = '☻';
	const char Vacant     = '·';

	Console.CursorVisible = false;
	Console.SetCursorPosition(0, 0);
	StringBuilder sb = new();
	sb.AppendLine();
	sb.AppendLine("  Checkers");
	sb.AppendLine();
	sb.AppendLine($"    ╔═══════════════════╗");
	sb.AppendLine($"  8 ║  {B(0, 7)} {B(1, 7)} {B(2, 7)} {B(3, 7)} {B(4, 7)} {B(5, 7)} {B(6, 7)} {B(7, 7)}  ║ {BlackPiece} = Black");
	sb.AppendLine($"  7 ║  {B(0, 6)} {B(1, 6)} {B(2, 6)} {B(3, 6)} {B(4, 6)} {B(5, 6)} {B(6, 6)} {B(7, 6)}  ║ {BlackKing} = Black King");
	sb.AppendLine($"  6 ║  {B(0, 5)} {B(1, 5)} {B(2, 5)} {B(3, 5)} {B(4, 5)} {B(5, 5)} {B(6, 5)} {B(7, 5)}  ║ {WhitePiece} = White");
	sb.AppendLine($"  5 ║  {B(0, 4)} {B(1, 4)} {B(2, 4)} {B(3, 4)} {B(4, 4)} {B(5, 4)} {B(6, 4)} {B(7, 4)}  ║ {WhiteKing} = White King");
	sb.AppendLine($"  4 ║  {B(0, 3)} {B(1, 3)} {B(2, 3)} {B(3, 3)} {B(4, 3)} {B(5, 3)} {B(6, 3)} {B(7, 3)}  ║");
	sb.AppendLine($"  3 ║  {B(0, 2)} {B(1, 2)} {B(2, 2)} {B(3, 2)} {B(4, 2)} {B(5, 2)} {B(6, 2)} {B(7, 2)}  ║ Taken:");
	sb.AppendLine($"  2 ║  {B(0, 1)} {B(1, 1)} {B(2, 1)} {B(3, 1)} {B(4, 1)} {B(5, 1)} {B(6, 1)} {B(7, 1)}  ║ {game.TakenCount(White),2} x {WhitePiece}");
	sb.AppendLine($"  1 ║  {B(0, 0)} {B(1, 0)} {B(2, 0)} {B(3, 0)} {B(4, 0)} {B(5, 0)} {B(6, 0)} {B(7, 0)}  ║ {game.TakenCount(Black),2} x {BlackPiece}");
	sb.AppendLine($"    ╚═══════════════════╝");
	sb.AppendLine($"       A B C D E F G H");
	sb.AppendLine();
	if (selection is not null)
	{
		sb.Replace(" $ ", $"[{ToChar(game.Board[selection.Value.X, selection.Value.Y])}]");
	}
	if (game.Board.Aggressor is not null)
	{
		sb.Replace(" @ ", $"<{ToChar(game.Board.Aggressor)}>");
		sb.Replace("@ ",  $"{ToChar(game.Board.Aggressor)}>");
		sb.Replace(" @",  $"<{ToChar(game.Board.Aggressor)}");
	}
	PieceColor? wc = game.Winner;
	PieceColor? mc = playerMoved?.Color;
	PieceColor? tc = game.Turn;
	// Note: these strings need to match in length
	// so they overwrite each other.
	string w = $"  *** {wc} wins ***";
	string m = $"  {mc} moved       ";
	string t = $"  {tc}'s turn      ";
	sb.AppendLine(
		game.Winner is not null ? w :
		playerMoved is not null ? m :
		t);
	string p = "  Press any key to continue...";
	string s = "                              ";
	sb.AppendLine(promptPressKey ? p : s);
	Console.Write(sb);

	char B(int x, int y) =>
		(x, y) == selection ? '$' :
		(game.Board.Aggressor is not null && game.Board[x, y] == game.Board.Aggressor) ? '@' :
		ToChar(game.Board[x, y]);

	static char ToChar(Piece? piece) =>
		piece is null ? Vacant :
		(piece.Color, piece.Promoted) switch
		{
			(Black, false) => BlackPiece,
			(Black, true)  => BlackKing,
			(White, false) => WhitePiece,
			(White, true)  => WhiteKing,
			_ => throw new NotImplementedException(),
		};
}

(int X, int Y)? HumanMoveSelection(Game game, (int X, int y)? selectionStart = null)
{
	(int X, int Y) selection = selectionStart ?? (3, 3);
	while (true)
	{
		RenderGameState(game, selection: selection);
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
