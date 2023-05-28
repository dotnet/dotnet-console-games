using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Website.Games.Darts;

public class Darts
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		bool closeRequested = false;
		State state = State.Main;
		Stopwatch stopwatch = new();
		TimeSpan framerate = TimeSpan.FromSeconds(1d / 60d);
		bool direction = default;
		bool playerGoesFirst = default;
		int x = 0;
		int y = 0;
		int x_max = 38;
		int y_max = 14;
		List<((int X, int Y)? Position, bool Player)> darts = new();
		int computer_x = default;
		int computer_y = default;
		int computer_skip = default;

		try
		{
			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
			while (!closeRequested)
			{
				await Render();
				await Update();
			}
		}
		finally
		{
			Console.CursorVisible = true;
			await Console.Clear();
			await Console.WriteLine("Darts was closed.");
		}

		async Task Update()
		{
			switch (state)
			{
				case State.Main:
					await PressEnterToContinue();
					if (closeRequested)
					{
						return;
					}
					playerGoesFirst = Random.Shared.Next(0, 2) % 2 is 0;
					state = State.ConfirmRandomTurnOrder;
					await Console.Clear();
					break;

				case State.ConfirmRandomTurnOrder:
					await PressEnterToContinue();
					if (closeRequested)
					{
						return;
					}
					state = playerGoesFirst
						? State.PlayerHorizontal
						: State.ComputerHorizontal;
					direction = true;
					x = 0;
					if (!playerGoesFirst)
					{
						computer_x = Random.Shared.Next(x_max + 1);
						computer_skip = Random.Shared.Next(1, 4);
					}
					stopwatch.Restart();
					await Console.Clear();
					break;

				case State.ConfirmPlayerThrow:
					await PressEnterToContinue();
					if (closeRequested)
					{
						return;
					}
					if (darts.Count >= 10)
					{
						state = State.ConfirmGameEnd;
						await Console.Clear();
						break;
					}
					state = State.ComputerHorizontal;
					direction = true;
					x = 0;
					computer_x = Random.Shared.Next(x_max + 1);
					computer_skip = Random.Shared.Next(1, 4);
					stopwatch.Restart();
					await Console.Clear();
					break;

				case State.ConfirmComputerThrow:
					await PressEnterToContinue();
					if (closeRequested)
					{
						return;
					}
					if (darts.Count >= 10)
					{
						state = State.ConfirmGameEnd;
						await Console.Clear();
						break;
					}
					state = State.PlayerHorizontal;
					direction = true;
					x = 0;
					stopwatch.Restart();
					await Console.Clear();
					break;

				case State.PlayerHorizontal or State.ComputerHorizontal:
					if (await KeyPressed() && state is State.PlayerHorizontal)
					{
						if (closeRequested)
						{
							return;
						}
						state = State.PlayerVertical;
						direction = true;
						y = 0;
						stopwatch.Restart();
						await Console.Clear();
						break;
					}
					if (closeRequested)
					{
						return;
					}
					if (direction)
					{
						x++;
					}
					else
					{
						x--;
					}
					if (state is State.ComputerHorizontal && x == computer_x)
					{
						computer_skip--;
						if (computer_skip < 0)
						{
							state = State.ComputerVertical;
							direction = true;
							y = 0;
							stopwatch.Restart();
							computer_y = Random.Shared.Next(y_max + 1);
							computer_skip = Random.Shared.Next(1, 4);
						}
					}
					if (x <= 0 || x >= x_max)
					{
						direction = !direction;
					}
					await ControlFrameRate();
					break;

				case State.PlayerVertical or State.ComputerVertical:
					if (await KeyPressed() && state is State.PlayerVertical)
					{
						if (closeRequested)
						{
							return;
						}
						state = State.ConfirmPlayerThrow;
						(int X, int Y)? position = (x, y);
						for (int i = 0; i < darts.Count; i++)
						{
							if (darts[i].Position == (x, y))
							{
								darts[i] = (null, darts[i].Player);
								position = null;
							}
						}
						darts.Add(new(position, true));
						await Console.Clear();
						break;
					}
					if (closeRequested)
					{
						return;
					}
					if (direction)
					{
						y++;
					}
					else
					{
						y--;
					}
					if (state is State.ComputerVertical && y == computer_y)
					{
						computer_skip--;
						if (computer_skip < 0)
						{
							state = State.ConfirmComputerThrow;
							(int X, int Y)? position = (x, y);
							for (int i = 0; i < darts.Count; i++)
							{
								if (darts[i].Position == (x, y))
								{
									darts[i] = (null, darts[i].Player);
									position = null;
								}
							}
							darts.Add(new(position, false));
							await Console.Clear();
							break;
						}
					}
					if (y <= 0 || y >= y_max)
					{
						direction = !direction;
					}
					await ControlFrameRate();
					break;

				case State.ConfirmGameEnd:
					await PressEnterToContinue();
					if (closeRequested)
					{
						return;
					}
					state = State.Main;
					darts = new();
					break;

				default:
					throw new NotImplementedException();

			}
		}

		async Task ControlFrameRate()
		{
			TimeSpan elapsed = stopwatch.Elapsed;
			if (framerate > elapsed)
			{
				await Console.RefreshAndDelay(framerate - elapsed);
			}
			stopwatch.Restart();
		}

		async Task PressEnterToContinue()
		{
			while (true)
			{
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Enter: return;
					case ConsoleKey.Escape: closeRequested = true; return;
				}
			}
		}

		async Task<bool> KeyPressed()
		{
			bool keyPressed = false;
			while (await Console.KeyAvailable())
			{
				keyPressed = true;
				if ((await Console.ReadKey(true)).Key is ConsoleKey.Escape)
				{
					closeRequested = true;
				}
			}
			return keyPressed;
		}

		async Task Render()
		{
			var render = new StringBuilder();

			if (state is State.Main)
			{
				StringBuilder output = new();
				output.AppendLine();
				output.AppendLine("  Darts");
				output.AppendLine();
				output.AppendLine("  Welcome to Darts. In this game you and the computer will");
				output.AppendLine("  throw darts at a dart board in attempts to get the most ");
				output.AppendLine("  points. If your dart lands on a line, it will round down");
				output.AppendLine("  amongst all the regions it is touching. If your dart lands");
				output.AppendLine("  on another dart it will knock both darts off the board so");
				output.AppendLine("  they will each be worth 0 points. You and the computer each");
				output.AppendLine("  get to throw 5 darts.");
				output.AppendLine();
				output.AppendLine("  Your darts: ○");
				output.AppendLine("  Computer's darts: ●");
				output.AppendLine();
				output.AppendLine("  Press [escape] at any time to close the game.");
				output.AppendLine();
				output.Append("  Press [enter] to begin...");
				await Console.Clear();
				await Console.Write(output);
				return;
			}

			string[] board = new string[]
			{
				"╔═══════╤═══════╤═══════╤═══════╤═══════╗",
				"║       │       │       │       │       ║",
				"║   1   │   2   │   3   │   2   │   1   ║",
				"║      ┌┴┐    ┌─┴─┐   ┌─┴─┐    ┌┴┐      ║",
				"╟──────┤6├────┤ 5 ├───┤ 5 ├────┤6├──────╢",
				"║      └┬┘    └─┬─┘   └─┬─┘    └┬┘      ║",
				"║   2   │   3   │   4   │   3   │   2   ║",
				"║       │       │  ┌─┐  │       │       ║",
				"╟───────┼───────┼──┤9├──┼───────┼───────╢",
				"║       │       │  └─┘  │       │       ║",
				"║   2   │   3   │   4   │   3   │   2   ║",
				"║      ┌┴┐    ┌─┴─┐   ┌─┴─┐    ┌┴┐      ║",
				"╟──────┤6├────┤ 5 ├───┤ 5 ├────┤6├──────╢",
				"║      └┬┘    └─┬─┘   └─┬─┘    └┬┘      ║",
				"║   1   │   2   │   3   │   2   │   1   ║",
				"║       │       │       │       │       ║",
				"╚═══════╧═══════╧═══════╧═══════╧═══════╝",
			};
			for (int i = 0; i < board.Length; i++)
			{
				for (int j = 0; j < board[i].Length; j++)
				{
					foreach (var dart in darts)
					{
						if (dart.Position == (j - 1, i - 1))
						{
							render.Append(dart.Player ? '○' : '●');
							goto DartRendered;
						}
					}
					render.Append(board[i][j]);
				DartRendered:
					continue;
				}
				if (state is State.PlayerHorizontal or State.PlayerVertical or State.ComputerHorizontal or State.ComputerVertical or State.ConfirmPlayerThrow or State.ConfirmComputerThrow)
				{
					render.Append(' ');
					if (i - 1 == y && state is not State.PlayerHorizontal and not State.ComputerHorizontal)
					{
						render.Append("│██│");
					}
					else
					{
						render.Append(i is 0 ? "┌──┐" : i == board.Length - 1 ? "└──┘" : "│  │");
					}
				}
				render.AppendLine();
			}
			if (state is State.PlayerHorizontal or State.PlayerVertical or State.ComputerHorizontal or State.ComputerVertical or State.ConfirmPlayerThrow or State.ConfirmComputerThrow)
			{
				render.AppendLine("┌───────────────────────────────────────┐");
				for (int j = 0; j <= x_max + 2; j++)
				{
					render.Append(
						j - 1 == x ? '█' :
						j is 0 ? '│' :
						j == x_max + 2 ? '│' :
						' ');
				}
				render.AppendLine();
				render.AppendLine("└───────────────────────────────────────┘");
			}
			if (state is State.PlayerHorizontal or State.PlayerVertical)
			{
				render.AppendLine();
				render.AppendLine("  It is your turn.");
				render.Append("  Press any key to aim your ○ dart... ");
			}
			if (state is State.ComputerHorizontal or State.ComputerVertical)
			{
				render.AppendLine();
				render.Append("  Computer's turn. Wait for it to throw it's ● dart.");
			}
			if (state is State.ConfirmRandomTurnOrder)
			{
				render.AppendLine();
				render.AppendLine("  You and the computer flip a coin and decide that ");
				render.AppendLine($"  {(playerGoesFirst ? "you" : "the computer")} will go first.");
				render.AppendLine();
				render.Append("  Press [enter] to continue...");
			}
			if (state is State.ConfirmPlayerThrow)
			{
				render.AppendLine();
				render.AppendLine("  You threw a dart.");
				if (darts[^1].Position is null)
				{
					render.AppendLine();
					render.AppendLine("  Dart collision! Both darts fell off the board.");
				}
				render.AppendLine();
				render.Append("  Press [enter] to continue...");
			}
			if (state is State.ConfirmComputerThrow)
			{
				render.AppendLine();
				render.AppendLine("  Computer threw a dart.");
				if (darts[^1].Position is null)
				{
					render.AppendLine();
					render.AppendLine("  Dart collision! Both darts fell off the board.");
				}
				render.AppendLine();
				render.Append("  Press [enter] to continue...");
			}
			if (state is State.ConfirmGameEnd)
			{
				var (playerScore, computerScore) = CalculateScores();
				render.AppendLine();
				render.AppendLine("  Game Complete! Final Scores...");
				render.AppendLine($"  Your Score: {playerScore}");
				render.AppendLine($"  Computer's Score: {computerScore}");
				render.AppendLine();
				if (playerScore > computerScore)
				{
					render.AppendLine("  You Win!");
				}
				else if (playerScore < computerScore)
				{
					render.AppendLine("  You Lose!");
				}
				else
				{
					render.AppendLine("  Draw!");
				}
				render.AppendLine();
				render.Append("  Press [enter] to return to the main screen...");
			}

			Console.CursorVisible = false;
			await Console.SetCursorPosition(0, 0);
			await Console.Write(render);
		}

		(int PlayerScore, int ComputerScore) CalculateScores()
		{
			string[] scoreBoard = new string[]
			{
				"111111112222222233333332222222211111111",
				"111111112222222233333332222222211111111",
				"111111112222222233333332222222211111111",
				"111111162222225553333355522222261111111",
				"222222223333333344444443333333322222222",
				"222222223333333344444443333333322222222",
				"222222223333333344444443333333322222222",
				"222222223333333344494443333333322222222",
				"222222223333333344444443333333322222222",
				"222222223333333344444443333333322222222",
				"222222223333333344444443333333322222222",
				"111111162222225553333355522222261111111",
				"111111112222222233333332222222211111111",
				"111111112222222233333332222222211111111",
				"111111112222222233333332222222211111111",
			};

			int playerScore = 0;
			int computerScore = 0;

			foreach (var dart in darts)
			{
				if (dart.Position.HasValue)
				{
					if (dart.Player)
					{
						playerScore += scoreBoard[dart.Position.Value.Y][dart.Position.Value.X] - '0';
					}
					else
					{
						computerScore += scoreBoard[dart.Position.Value.Y][dart.Position.Value.X] - '0';
					}
				}
			}

			return (playerScore, computerScore);
		}
	}

	enum State
	{
		Main,
		ConfirmRandomTurnOrder,
		PlayerHorizontal,
		PlayerVertical,
		ConfirmPlayerThrow,
		ComputerHorizontal,
		ComputerVertical,
		ConfirmComputerThrow,
		ConfirmGameEnd,
	}
}
