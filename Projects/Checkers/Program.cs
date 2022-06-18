// See https://aka.ms/new-console-template for more information

using Checkers;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;

Console.CursorVisible = false;

// HACK: Set to true to create output file for game analysis
const bool CreateOutputFileForAnalysis = false;

if (CreateOutputFileForAnalysis)
#pragma warning disable CS0162
{
    var prefix = Path.GetTempPath();
    var traceFile = $"{prefix}checkers_game_{Guid.NewGuid()}.txt";

    Trace.Listeners.Add((new TextWriterTraceListener(File.Create((traceFile)))));
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

var xSelection = 4;
var ySelection = 4;
Point? selectedFromPoint = null;
Point? selectedToPoint = null;

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
                    while (selectedFromPoint == null || selectedToPoint == null)
                    {
                        var selection = DisplayHelper.GetScreenPositionFromBoardPosition(new Point(xSelection, ySelection));
                        Console.SetCursorPosition(selection.X - 1, selection.Y);
                        Console.Write("[");
                        Console.SetCursorPosition(selection.X + 1, selection.Y);
                        Console.Write("]");

                        var oldY = ySelection;
                        var oldX = xSelection;
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.UpArrow:
                                if (ySelection > 1)
                                {
                                    oldY = ySelection;
                                    ySelection--;
                                }
                                break;
                            case ConsoleKey.DownArrow:
                                if (ySelection < 8)
                                {
                                    oldY = ySelection;
                                    ySelection++;
                                }
                                break;
                            case ConsoleKey.LeftArrow:
                                if (xSelection > 1)
                                {
                                    oldX = xSelection;
                                    xSelection--;
                                }
                                break;
                            case ConsoleKey.RightArrow:
                                if (xSelection < 8)
                                {
                                    oldX = xSelection;
                                    xSelection++;
                                }
                                break;
                            case ConsoleKey.Enter:
                                if (selectedFromPoint == null)
                                {
                                    selectedFromPoint = new Point(xSelection, ySelection);
                                }
                                else
                                {
                                    selectedToPoint = new Point(xSelection, ySelection);
                                }
                                break;
                            case ConsoleKey.Escape:
                                selectedFromPoint = null;
                                selectedToPoint = null;
                                break;
                        }

                        var oldSelection = DisplayHelper.GetScreenPositionFromBoardPosition(new Point(oldX, oldY));
                        Console.SetCursorPosition(oldSelection.X - 1, oldSelection.Y);
                        Console.Write(" ");
                        Console.SetCursorPosition(oldSelection.X + 1, oldSelection.Y);
                        Console.Write(" ");

                    }

                    var fromPoint = selectedFromPoint;
                    var toPoint = selectedToPoint;

                    var actualFromPiece =
                        BoardHelper.GetPieceAt(fromPoint.Value.X, fromPoint.Value.Y, game.GameBoard);

                    if (actualFromPiece == null || actualFromPiece.Side != game.CurrentGo)
                    {
                        fromPoint = toPoint = selectedToPoint = selectedFromPoint = null;
                    }

                    if (fromPoint != null && toPoint != null)
                    {
                        _ = game.NextRound(fromPoint, toPoint);
                        selectedFromPoint = selectedToPoint = null;
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
	return entry[0] - '0';
}


