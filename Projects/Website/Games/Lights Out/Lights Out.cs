using System;
using System.Text;
using System.Threading.Tasks;

namespace Website.Games.Lights_Out;

public class Lights_Out
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		const int Length = 5;
		const bool O = true;
		const bool X = false;

		bool[][][] levels =
		[
			[ // 1
				[X, X, X, X, X],
				[X, X, X, X, X],
				[O, X, O, X, O],
				[X, X, X, X, X],
				[X, X, X, X, X],
			],
			[ // 2
				[O, X, O, X, O],
				[O, X, O, X, O],
				[X, X, X, X, X],
				[O, X, O, X, O],
				[O, X, O, X, O],
			],
			[ // 3
				[X, O, X, O, X],
				[O, O, X, O, O],
				[O, O, X, O, O],
				[O, O, X, O, O],
				[X, O, X, O, X],
			],
			[ // 4
				[X, X, X, X, X],
				[O, O, X, O, O],
				[X, X, X, X, X],
				[O, X, X, X, O],
				[O, O, X, O, O],
			],
			[ // 5
				[O, O, O, O, X],
				[O, O, O, X, O],
				[O, O, O, X, O],
				[X, X, X, O, O],
				[O, O, X, O, O],
			],
			[ // 6
				[X, X, X, X, X],
				[X, X, X, X, X],
				[O, X, O, X, O],
				[O, X, O, X, O],
				[X, O, O, O, X],
			],
			[ // 7
				[O, O, O, O, X],
				[O, X, X, X, O],
				[O, X, X, X, O],
				[O, X, X, X, O],
				[O, O, O, O, X],
			],
			[ // 8
				[X, X, X, X, X],
				[X, X, O, X, X],
				[X, O, X, O, X],
				[O, X, O, X, O],
				[X, O, X, O, X],
			],
			[ // 9
				[X, O, X, O, X],
				[O, O, O, O, O],
				[X, O, O, O, X],
				[X, O, X, O, O],
				[O, O, O, X, X],
			],
			[ // 10
				[X, O, O, O, X],
				[X, O, O, O, X],
				[X, O, O, O, X],
				[X, X, X, X, X],
				[X, X, X, X, X],
			],
			[ // 11
				[O, X, O, X, O],
				[O, X, O, X, O],
				[O, X, O, X, O],
				[O, X, O, X, O],
				[X, O, O, O, X],
			],
			[ // 12
				[O, O, O, O, O],
				[X, O, X, O, X],
				[O, O, X, O, O],
				[X, O, O, O, X],
				[X, O, X, O, X],
			],
			[ // 13
				[X, X, X, O, X],
				[X, X, O, X, O],
				[X, O, X, O, X],
				[O, X, O, X, X],
				[X, O, X, X, X],
			],
			[ // 14
				[X, X, X, X, X],
				[X, X, X, X, X],
				[X, O, X, X, X],
				[X, O, X, X, X],
				[X, O, X, X, X],
			],
			[ // 15
				[X, X, X, X, X],
				[X, O, X, X, X],
				[X, X, X, X, X],
				[X, O, X, X, X],
				[X, X, X, X, X],
			],
			[ // 16
				[O, X, X, X, X],
				[O, X, X, X, X],
				[O, X, X, X, X],
				[O, X, X, X, X],
				[O, O, O, O, O],
			],
			[ // 17
				[X, X, X, X, X],
				[X, X, X, X, X],
				[X, X, O, X, X],
				[X, O, O, O, X],
				[O, O, O, O, O],
			],
			[ // 18
				[X, X, O, X, X],
				[X, O, X, O, X],
				[O, X, O, X, O],
				[X, O, X, O, X],
				[X, X, O, X, X],
			],
			[ // 19
				[O, X, O, X, O],
				[X, X, X, X, X],
				[O, X, O, X, O],
				[X, X, X, X, X],
				[O, X, O, X, O],
			],
			[ // 20
				[X, X, X, X, X],
				[X, X, X, X, X],
				[O, X, X, X, O],
				[X, X, X, X, X],
				[X, X, X, X, X],
			],
			[ // 21
				[X, O, O, O, O],
				[X, O, X, X, X],
				[X, O, O, O, X],
				[X, O, X, X, X],
				[X, O, X, X, X],
			],
			[ // 22
				[X, O, O, O, X],
				[O, X, X, X, O],
				[O, X, X, X, O],
				[O, X, X, X, O],
				[X, O, O, O, X],
			],
			[ // 23
				[X, X, X, X, X],
				[X, X, X, X, X],
				[X, X, O, O, O],
				[X, X, O, O, X],
				[X, X, O, X, X],
			],
			[ // 24
				[X, X, X, X, X],
				[X, X, X, X, X],
				[O, X, X, X, O],
				[O, O, O, O, O],
				[X, O, X, X, O],
			],
			[ // 25
				[O, X, X, X, X],
				[O, O, X, X, X],
				[O, O, O, X, X],
				[O, O, O, O, X],
				[X, O, O, O, O],
			],
		];

		Console.OutputEncoding = Encoding.UTF8;
		int level = 0;
		bool[][] board =
		[
			[O, O, O, O, O],
			[O, O, O, O, O],
			[O, O, O, O, O],
			[O, O, O, O, O],
			[O, O, O, O, O],
		];
		SetBoard();
		(int Left, int Top) cursor = (0, 0);
		while (true)
		{
			await RenderBoard();
			await Console.Write($"""

				Turn off all the lights. Level {level + 1}.

				Controls:
				- arrow keys: move cursor
				- enter: flip lights
				- backspace: reset level
				- escape: close game
				""");
			await Console.SetCursorPosition(2 * cursor.Left, cursor.Top);
			Console.CursorVisible = false;
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.LeftArrow: cursor.Left = cursor.Left <= 0 ? Length - 1 : cursor.Left - 1; break;
				case ConsoleKey.RightArrow: cursor.Left = cursor.Left >= Length - 1 ? 0 : cursor.Left + 1; break;
				case ConsoleKey.UpArrow: cursor.Top = cursor.Top <= 0 ? Length - 1 : cursor.Top - 1; break;
				case ConsoleKey.DownArrow: cursor.Top = cursor.Top >= Length - 1 ? 0 : cursor.Top + 1; break;
				case ConsoleKey.Backspace: SetBoard(); break;
				case ConsoleKey.Escape: goto Close;
				case ConsoleKey.Enter:
					board[cursor.Top][cursor.Left] = !board[cursor.Top][cursor.Left];
					if (cursor.Top > 0) board[cursor.Top - 1][cursor.Left] = !board[cursor.Top - 1][cursor.Left];
					if (cursor.Top < Length - 1) board[cursor.Top + 1][cursor.Left] = !board[cursor.Top + 1][cursor.Left];
					if (cursor.Left > 0) board[cursor.Top][cursor.Left - 1] = !board[cursor.Top][cursor.Left - 1];
					if (cursor.Left < Length - 1) board[cursor.Top][cursor.Left + 1] = !board[cursor.Top][cursor.Left + 1];
					if (LightsOut())
					{
						await Console.Clear();
						await RenderBoard();
						await Console.Write($"""

							You turned off all the lights!
							Level {level + 1} complete.

							Controls:
							- enter: next level
							- escape: close game
							""");
						switch ((await Console.ReadKey(true)).Key)
						{
							case ConsoleKey.Escape: goto Close;
							case ConsoleKey.Enter:
								level++;
								SetBoard();
								await Console.Clear();
								break;
						}
					}
					break;
			}
		}
	Close:
		await Console.Clear();
		await Console.WriteLine("Lights Out was closed.");
		Console.CursorVisible = true;

		void SetBoard()
		{
			for (int i = 0; i < Length; i++)
			{
				for (int j = 0; j < Length; j++)
				{
					board[i][j] = levels[level][i][j];
				}
			}
		}

		bool LightsOut()
		{
			for (int i = 0; i < Length; i++)
			{
				for (int j = 0; j < Length; j++)
				{
					if (board[i][j])
					{
						return false;
					}
				}
			}
			return true;
		}

		async Task RenderBoard()
		{
			StringBuilder render = new();
			for (int i = 0; i < Length; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					for (int k = 0; k < Length; k++)
					{
						switch (j)
						{
							case 0: render.Append((k, i) == cursor ? "╔═╤═╗" : "╭───╮"); break;
							case 1:
								render.Append((k, i) == cursor ? '╟' : '│');
								render.Append(board[i][k] ? "███" : "   ");
								render.Append((k, i) == cursor ? '╢' : '│');
								break;
							case 2: render.Append((k, i) == cursor ? "╚═╧═╝" : "╰───╯"); break;
						}
					}
					render.AppendLine();
				}
			}
			await Console.SetCursorPosition(0, 0);
			await Console.Write(render);
		}
	}
}
