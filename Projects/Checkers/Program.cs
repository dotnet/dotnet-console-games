// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Checkers;
using Checkers.Helpers;
using Checkers.Types;

// HACK: Set to true to create output file for game analysis
const bool createOutputFileForAnalysis = false;

if (createOutputFileForAnalysis)
{
#pragma warning disable CS0162 // Unreachable code detected
	var prefix = Path.GetTempPath();
#pragma warning restore CS0162 // Unreachable code detected
	var traceFile = $"{prefix}checkers_game_{Guid.NewGuid()}.txt";

	Trace.Listeners.Add((new TextWriterTraceListener(File.Create((traceFile)))));
}

Trace.AutoFlush = true;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var sw = new Stopwatch();

sw.Start();
LoggingHelper.LogStart();
Game? game = null;

var numberOfPlayers = 0;

var gameState = GameState.IntroScreen;

while (gameState != GameState.Stopped)
{
	switch (gameState)
	{
		case GameState.IntroScreen:
			numberOfPlayers = ShowIntroScreenAndGetOption();
			gameState = GameState.GameInProgress;

			break;
		case GameState.GameInProgress:

			game = new Game();
			PlayerHelper.AssignPlayersToSide(numberOfPlayers, game);

			while (game.GameWinner == PieceColour.NotSet)
			{
				var currentPlayer = game.Players.FirstOrDefault(x => x.Side == game.CurrentGo);

				if (currentPlayer != null && currentPlayer.ControlledBy == PlayerControl.Human)
				{
					Console.SetCursorPosition(0, 12);
					Console.Write($"Enter FROM square: ");
					var fromSquare = Console.ReadLine();
					Console.Write($"Enter TO square  : ");
					var toSquare = Console.ReadLine();

					if (fromSquare != null)
					{
						var fromPoint = PositionHelper.GetPositionByNotation(fromSquare);

						if (toSquare != null)
						{
							var toPoint = PositionHelper.GetPositionByNotation(toSquare);

							if (fromPoint != null)
							{
								var actualFromPiece =
									BoardHelper.GetPieceAt(fromPoint.Value.X, fromPoint.Value.Y, game.GameBoard);

								if (actualFromPiece == null || actualFromPiece.Side != game.CurrentGo)
								{
									fromPoint = toPoint = null;
								}
							}

							if (fromPoint != null && toPoint != null)
							{
								var moveOutcome = game.NextRound(fromPoint, toPoint);
							}
							else
							{
								Console.SetCursorPosition(19, 12);
								Console.Write(new string(' ', 10));
								Console.SetCursorPosition(19, 13);
								Console.Write(new string(' ', 10));
							}
						}
					}
				}
				else
				{
					game.NextRound();
				}

				Thread.Sleep(100);
			}

			LoggingHelper.LogOutcome(game.GameWinner);

			gameState = GameState.GameOver;

			break;
		case GameState.GameOver:
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

			gameState = GameState.Stopped;

			break;
		default:
			throw new ArgumentOutOfRangeException();
	}
}

int ShowIntroScreenAndGetOption()
{
	var validPlayers = new List<int>() { 0, 1, 2 };
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
	Console.WriteLine("Moves are entered in `algebraic notation` e.g. A1 is the bottom  left  square,");
	Console.WriteLine("and H8 is the top right square. Invalid moves are ignored.");
	Console.WriteLine();
	Console.WriteLine("3 modes of play are possible depending on the number of players entered:");
	Console.WriteLine("    0 - black and white are controlled by the computer");
	Console.WriteLine("    1 - black is controlled by the player and white by the computer");
	Console.WriteLine("    2 - allows 2 players");
	Console.WriteLine();
	Console.Write("Enter the number of players (0-2): ");

	var entry = Console.ReadLine();

	var conversionPassed = int.TryParse(entry, out numberOfPlayers);

	return conversionPassed && validPlayers.Contains(numberOfPlayers) ? numberOfPlayers : 0;
}


