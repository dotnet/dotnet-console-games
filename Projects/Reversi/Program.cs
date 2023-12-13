using System;
using System.Collections.Generic;
using System.Text;

(int Left, int Top)[] directions =
[
	( 1,  0), // right
	(-1,  0), // left
	( 0,  1), // down
	( 0, -1), // up
	( 1,  1), // down-right
	(-1, -1), // up-left
	( 1, -1), // up-right
	(-1,  1), // down-left
];

Console.OutputEncoding = Encoding.UTF8;
PlayAgain:
(int Left, int Top) cursor = default;
bool?[,] board;
HashSet<(int Left, int Top)> validMoves = [];
bool turn = true;
bool? playerColor = null;
Console.Clear();
InitializeBoard();
RenderBoard();
Console.Write($"""

	 When you place a piece on the board all
	 your oponent pieces you jump will be converted
	 into your pieces. Have the most pieces at the
	 end of the game and you win. You and your
	 opponent will be randomly assigned a color of
	 black or white. The white player always moves first.

	 Controls:
	 - enter: start match
	 - escape: close game
	""");
MainMenuInput:
Console.CursorVisible = false;
switch (Console.ReadKey(true).Key)
{
	case ConsoleKey.Escape: goto Close;
	case ConsoleKey.Enter: break;
	default: goto MainMenuInput;
}
playerColor = Random.Shared.Next(2) is 0;
Console.Clear();
RenderBoard();
Console.Write($"""

	 You are {(playerColor.Value ? "● white" : "○ black")} and your opponent
	 is {(playerColor.Value ? "○ black" : "● white")}.

	 Controls:
	 - enter: continue
	 - escape: close game
	""");
ColorConfirmInput:
Console.CursorVisible = false;
switch (Console.ReadKey(true).Key)
{
	case ConsoleKey.Escape: goto Close;
	case ConsoleKey.Enter: break;
	default: goto ColorConfirmInput;
}
while (true)
{
	UpdateValidMoves(turn);
	if (validMoves.Count is 0)
	{
		turn = !turn;
		UpdateValidMoves(turn);
		if (validMoves.Count is 0)
		{
			Console.Clear();
			RenderBoard();
			int playerScore = GetScore(playerColor.Value);
			int opponentScore = GetScore(!playerColor.Value);
			string endGameState =
				playerScore > opponentScore ? "Win" :
				playerScore < opponentScore ? "Lose" :
				"Tie";
			Console.Write($"""

				 No more valid moves. Game Over.

				 You {endGameState}!

				 Controls:
				 - enter: continue
				 - escape: close game
				""");
		GameOverInput:
			Console.CursorVisible = false;
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.Escape: goto Close;
				case ConsoleKey.Enter: goto PlayAgain;
				default: goto GameOverInput;
			}
		}
		Console.Clear();
		RenderBoard();
		Console.Write($"""

				 {(turn == playerColor ? "Your Opponent" : "You")} has no valid moves.
				 Turn skipped.

				 Controls:
				 - enter: continue
				 - escape: close game
				""");
	ConfirmTurnSkip:
		Console.CursorVisible = false;
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Escape: goto Close;
			case ConsoleKey.Enter: break;
			default: goto ConfirmTurnSkip;
		}
		goto SkipTurn;
	}
SkipTurn:
	if (turn == playerColor)
	{
		Console.Clear();
		RenderBoard();
		Console.Write($"""

			 Controls:
			 - arrow keys: move cursor
			 - enter: place piece at '+' valid move
			 - escape: close game
			""");
		Console.SetCursorPosition(2 * cursor.Left, cursor.Top);
		Console.CursorVisible = false;
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.LeftArrow: cursor.Left = cursor.Left <= 0 ? board.GetLength(0) - 1 : cursor.Left - 1; break;
			case ConsoleKey.RightArrow: cursor.Left = cursor.Left >= board.GetLength(0) - 1 ? 0 : cursor.Left + 1; break;
			case ConsoleKey.UpArrow: cursor.Top = cursor.Top <= 0 ? board.GetLength(1) - 1 : cursor.Top - 1; break;
			case ConsoleKey.DownArrow: cursor.Top = cursor.Top >= board.GetLength(1) - 1 ? 0 : cursor.Top + 1; break;
			case ConsoleKey.Escape: goto Close;
			case ConsoleKey.Enter:
				if (validMoves.Contains(cursor))
				{
					PlaceMove(cursor, playerColor.Value);
					Console.Clear();
					RenderBoard();
					Console.Write($"""

						 You played a piece.

						 Controls:
						 - enter: continue
						 - escape: close game
						""");
				MoveConfirmInput:
					Console.CursorVisible = false;
					switch (Console.ReadKey(true).Key)
					{
						case ConsoleKey.Escape: goto Close;
						case ConsoleKey.Enter: break;
						default: goto MoveConfirmInput;
					}
					turn = !turn;
				}
				break;
		}
	}
	else
	{
		ComputerMove();
		Console.Clear();
		RenderBoard();
		Console.Write($"""

			 Your opponent played a piece.

			 Controls:
			 - enter: continue
			 - escape: close game
			""");
	OpponentMoveConfirmInput:
		Console.CursorVisible = false;
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Escape: goto Close;
			case ConsoleKey.Enter: turn = !turn; break;
			default: goto OpponentMoveConfirmInput;
		}
	}
}
Close:
Console.Clear();
Console.WriteLine("Reversi was closed.");
Console.CursorVisible = true;

void InitializeBoard()
{
	bool? _ = null, w = true, b = false;
	board = new bool?[,]
	{
		{ _, _, _, _, _, _, _, _ },
		{ _, _, _, _, _, _, _, _ },
		{ _, _, _, _, _, _, _, _ },
		{ _, _, _, w, b, _, _, _ },
		{ _, _, _, b, w, _, _, _ },
		{ _, _, _, _, _, _, _, _ },
		{ _, _, _, _, _, _, _, _ },
		{ _, _, _, _, _, _, _, _ },
	};
}

void RenderBoard()
{
	StringBuilder render = new();
	render.AppendLine(" Reversi");
	render.AppendLine();
	render.AppendLine(" ┌─────────────────┐");
	for (int i = 0; i < board.GetLength(1); i++)
	{
		render.Append(' ');
		render.Append('│');
		for (int j = 0; j < board.GetLength(0); j++)
		{
			render.Append(
				cursor == (j, i) ? '[' :
				cursor == (j - 1, i) ? ']' :
				' ');
			render.Append(
				validMoves.Contains((j, i)) ? '+' :
				board[j, i] is null ? ' ' :
				board[j, i]!.Value ? '●' : '○');
		}
		render.Append(cursor == (board.GetLength(0) - 1, i) ? ']' : ' ');
		render.Append('│');
		render.AppendLine();
	}
	render.AppendLine(" └─────────────────┘");
	if (playerColor is not null)
	{
		render.AppendLine($"    ●: {GetScore(true)}     ○: {GetScore(false)}");
	}
	Console.SetCursorPosition(0, 0);
	Console.Write(render);
}

int GetScore(bool color)
{
	int score = 0;
	for (int i = 0; i < board.GetLength(1); i++)
	{
		for (int j = 0; j < board.GetLength(0); j++)
		{
			if (board[j, i] == color)
			{
				score++;
			}
		}
	}
	return score;
}

void UpdateValidMoves(bool color)
{
	validMoves.Clear();
	for (int i = 0; i < board.GetLength(1); i++)
	{
		for (int j = 0; j < board.GetLength(0); j++)
		{
			if (board[j, i] is null)
			{
				foreach (var direction in directions)
				{
					bool jump = false;
					(int Left, int Top) location = (j + direction.Left, i + direction.Top);
					while (
						location.Left >= 0 && location.Left < board.GetLength(0) &&
						location.Top >= 0 && location.Top < board.GetLength(1) &&
						board[location.Left, location.Top] == !color)
					{
						jump = true;
						location = (location.Left + direction.Left, location.Top + direction.Top);
					}
					if (location.Left >= 0 && location.Left < board.GetLength(0) &&
						location.Top >= 0 && location.Top < board.GetLength(1) &&
						board[location.Left, location.Top] == color &&
						jump)
					{
						validMoves.Add((j, i));
					}
				}
			}
		}
	}
}

void PlaceMove((int Left, int Top) move, bool color)
{
	board[move.Left, move.Top] = color;
	foreach (var direction in directions)
	{
		bool jump = false;
		(int Left, int Top) location = (move.Left + direction.Left, move.Top + direction.Top);
		while (
			location.Left >= 0 && location.Left < board.GetLength(0) &&
			location.Top >= 0 && location.Top < board.GetLength(1) &&
			board[location.Left, location.Top] == !color)
		{
			jump = true;
			location = (location.Left + direction.Left, location.Top + direction.Top);
		}
		if (location.Left >= 0 && location.Left < board.GetLength(0) &&
			location.Top >= 0 && location.Top < board.GetLength(1) &&
			board[location.Left, location.Top] == color &&
			jump)
		{
			location = (move.Left + direction.Left, move.Top + direction.Top);
			while (
				location.Left >= 0 && location.Left < board.GetLength(0) &&
				location.Top >= 0 && location.Top < board.GetLength(1) &&
				board[location.Left, location.Top] == !color)
			{
				board[location.Left, location.Top] = color;
				location = (location.Left + direction.Left, location.Top + direction.Top);
			}
		}
	}
	validMoves.Clear();
}

void ComputerMove()
{
	(int Left, int Top)[] validMovesArray = [.. validMoves];
	(int Left, int Top) move = validMovesArray[Random.Shared.Next(validMovesArray.Length)];
	cursor = move;
	PlaceMove(move, !playerColor.Value);
}