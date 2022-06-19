using Checkers;
using System.Diagnostics;
using System.Drawing;
using System.IO;

Console.CursorVisible = false;

// HACK: Set to true to create output file for game analysis
const bool CreateOutputFileForAnalysis = false;

if (CreateOutputFileForAnalysis)
#pragma warning disable CS0162
{
    var prefix = Path.GetTempPath();
    var traceFile = $"{prefix}checkers_game_{Guid.NewGuid()}.txt";

    Trace.Listeners.Add(new TextWriterTraceListener(File.Create(traceFile)));
}
#pragma warning restore CS0162

Trace.AutoFlush = true;
Console.OutputEncoding = System.Text.Encoding.UTF8;
var sw = new Stopwatch();
sw.Start();
LoggingHelper.LogStart();
Game? game = null;
var numberOfPlayers = 0;
var gameState = GameState.IntroScreen;
(int X, int Y) selection = (4, 5);

while (gameState != GameState.Stopped)
{
    switch (gameState)
    {
        case GameState.IntroScreen:
            ShowIntroScreenAndGetOption();
            gameState = GameState.GameInProgress;
            break;
        case GameState.GameInProgress:
            RunGameLoop();
            gameState = GameState.GameOver;
            break;
        case GameState.GameOver:
            HandleGameOver();
            gameState = GameState.Stopped;
            break;
        default:
            throw new NotImplementedException();
    }
}

void ShowIntroScreenAndGetOption()
{
    Console.Clear();
    Console.WriteLine("CHECKERS");
    Console.WriteLine();
    Console.WriteLine("Checkers is  played on an 8x8  board between two sides commonly known as black");
    Console.WriteLine("and white. The objective is  simple - capture  all  your opponent's pieces. An");
    Console.WriteLine("alternative way to  win is to trap  your opponent so that  they have no  valid");
    Console.WriteLine("moves left.");
    Console.WriteLine();
    Console.WriteLine("Black starts first and players take it  in turns to move their pieces  forward");
    Console.WriteLine("across the board diagonally. Should a piece  reach the other side of the board");
    Console.WriteLine("the piece becomes a `king` and can  then move diagonally backwards as  well as");
    Console.WriteLine("forwards.");
    Console.WriteLine();
    Console.WriteLine("Pieces are captured by jumping over them diagonally. More than one enemy piece");
    Console.WriteLine("can be captured in the same turn by the same piece.");
    Console.WriteLine();
    Console.WriteLine("Moves  are selected  with the arrow keys. Use the [enter] button to select the");
    Console.WriteLine("from and to squares. Invalid moves are ignored.");
    Console.WriteLine();
    Console.WriteLine("3 modes of play are possible depending on the number of players entered:");
    Console.WriteLine("    0 - black and white are controlled by the computer");
    Console.WriteLine("    1 - black is controlled by the player and white by the computer");
    Console.WriteLine("    2 - allows 2 players");
    Console.WriteLine();
    Console.Write("Enter the number of players (0-2): ");

	var entry = Console.ReadLine()?.Trim();
	while (entry is not "0" and not "1" and not "2")
	{
		Console.WriteLine("Invalid Input. Try Again.");
		Console.Write("Enter the number of players (0-2): ");
		entry = Console.ReadLine()?.Trim();
	}
	numberOfPlayers = entry[0] - '0';
}

void RunGameLoop()
{
	game = new Game();
	PlayerHelper.AssignPlayersToSide(numberOfPlayers, game);
	while (game.GameWinner == PieceColour.NotSet)
	{
		var currentPlayer = game.Players.FirstOrDefault(x => x.Side == game.CurrentGo);
		if (currentPlayer != null && currentPlayer.ControlledBy == PlayerControl.Human)
		{
			(int X, int Y)? from = null;
			(int X, int Y)? to = null;
			while (from is null || to is null)
			{
				(int X, int Y) screenSelection = DisplayHelper.GetScreenPositionFromBoardPosition(selection);
				Console.SetCursorPosition(screenSelection.X - 1, screenSelection.Y);
				Console.Write("[");
				Console.SetCursorPosition(screenSelection.X + 1, screenSelection.Y);
				Console.Write("]");

				ConsoleKey key = Console.ReadKey(true).Key;

				var screenPreviousSelection = DisplayHelper.GetScreenPositionFromBoardPosition(selection);
				Console.SetCursorPosition(screenPreviousSelection.X - 1, screenPreviousSelection.Y);
				Console.Write(" ");
				Console.SetCursorPosition(screenPreviousSelection.X + 1, screenPreviousSelection.Y);
				Console.Write(" ");

				switch (key)
				{
					case ConsoleKey.DownArrow:  selection.Y = Math.Min(7, selection.Y + 1); break;
					case ConsoleKey.UpArrow:    selection.Y = Math.Max(0, selection.Y - 1); break;
					case ConsoleKey.LeftArrow:  selection.X = Math.Max(0, selection.X - 1); break;
					case ConsoleKey.RightArrow: selection.X = Math.Min(7, selection.X + 1); break;
					case ConsoleKey.Enter:
						if (from is null)
						{
							from = (selection.X, selection.Y);
						}
						else
						{
							to = (selection.X, selection.Y);
						}
						break;
					case ConsoleKey.Escape:
						from = null;
						to = null;
						break;
				}
			}

			var actualFromPiece = game.GameBoard.GetPieceAt(from.Value.X, from.Value.Y);
			if (actualFromPiece == null || actualFromPiece.Side != game.CurrentGo)
			{
				from = null;
				to = null;
			}
			if (from != null && to != null)
			{
				_ = game.NextRound(from, to);
			}
			else
			{
				Console.SetCursorPosition(19, 12);
				Console.Write(new string(' ', 10));
				Console.SetCursorPosition(19, 13);
				Console.Write(new string(' ', 10));
			}
		}
		else
		{
			game.NextRound();
		}

		PressAnyKeyToContinue();
	}
	LoggingHelper.LogOutcome(game.GameWinner);
}

void HandleGameOver()
{
	if (game != null)
	{
		LoggingHelper.LogMoves(game.MovesSoFar);
	}
	LoggingHelper.LogFinish();
	sw.Stop();
	if (game != null)
	{
		Display.DisplayWinner(game.GameWinner);
	}
}

void PressAnyKeyToContinue()
{
	(int left, int top) = (Console.CursorLeft, Console.CursorTop);
	Console.Write("Press any key to cotinue...");
	Console.ReadKey(true);
	Console.SetCursorPosition(left, top);
	Console.Write("                           ");
}
