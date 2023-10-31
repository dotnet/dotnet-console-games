using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Website.Games.Tetris;

public class Tetris
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		#region Constants

		string[] emptyField = new string[42];
		emptyField[0] = "╭──────────────────────────────╮";
		for (int i = 1; i < 41; i++)
		{
			emptyField[i] = "│                              │";
		}
		emptyField[^1] = "╰──────────────────────────────╯";

		string[] nextTetrominoBorder = new[]
		{
			"╭─────────╮",
			"│         │",
			"│         │",
			"│         │",
			"│         │",
			"│         │",
			"│         │",
			"│         │",
			"│         │",
			"╰─────────╯"
		};

		string[] scoreBorder = new[]
		{
			"╭─────────╮",
			"│         │",
			"╰─────────╯"
		};

		string[] pauseRender = new[]
		{
			"█████╗ ███╗ ██╗██╗█████╗█████╗",
			"██╔██║██╔██╗██║██║██╔══╝██╔══╝",
			"█████║█████║██║██║ ███╗ █████╗",
			"██╔══╝██╔██║██║██║   ██╗██╔══╝",
			"██║   ██║██║█████║█████║█████╗",
			"╚═╝   ╚═╝╚═╝╚════╝╚════╝╚════╝",
		};

		string[][] tetrominos = new[]
		{
			new[]{
				"╭─╮",
				"╰─╯",
				"x─╮",
				"╰─╯",
				"╭─╮",
				"╰─╯",
				"╭─╮",
				"╰─╯"
			},
			new[]{
				"╭─╮      ",
				"╰─╯      ",
				"╭─╮x─╮╭─╮",
				"╰─╯╰─╯╰─╯"
			},
			new[]{
				"      ╭─╮",
				"      ╰─╯",
				"╭─╮x─╮╭─╮",
				"╰─╯╰─╯╰─╯"
			},
			new[]{
				"╭─╮╭─╮",
				"╰─╯╰─╯",
				"x─╮╭─╮",
				"╰─╯╰─╯"
			},
			new[]{
				"   ╭─╮╭─╮",
				"   ╰─╯╰─╯",
				"╭─╮x─╮   ",
				"╰─╯╰─╯   "
			},
			new[]{
				"   ╭─╮   ",
				"   ╰─╯   ",
				"╭─╮x─╮╭─╮",
				"╰─╯╰─╯╰─╯"
			},
			new[]{
				"╭─╮╭─╮   ",
				"╰─╯╰─╯   ",
				"   x─╮╭─╮",
				"   ╰─╯╰─╯"
			},
		};

		const int borderSize = 1;

		int initialX = (emptyField[0].Length / 2) - 3;
		int initialY = 1;

		int consoleWidthMin = 44;
		int consoleHeightMin = 43;

		#endregion

		Stopwatch timer = new();
		bool closeRequested = false;
		bool gameOver;
		int score = 0;
		TimeSpan fallSpeed;
		string[] field;
		Tetromino tetromino;
		int consoleWidth = Console.WindowWidth;
		int consoleHeight = Console.WindowHeight;
		bool consoleTooSmallScreen = false;

		Console.OutputEncoding = Encoding.UTF8;
		while (!closeRequested)
		{
			await Console.Clear();
			await Console.Write("""

				     ██████╗█████╗██████╗█████╗ ██╗█████╗
				     ╚═██╔═╝██╔══╝╚═██╔═╝██╔═██╗██║██╔══╝
				       ██║  █████╗  ██║  █████╔╝██║ ███╗
				       ██║  ██╔══╝  ██║  ██╔═██╗██║   ██╗
				       ██║  █████╗  ██║  ██║ ██║██║█████║
				       ╚═╝  ╚════╝  ╚═╝  ╚═╝ ╚═╝╚═╝╚════╝

				    Controls:

				    [A] or [←] move left
				    [D] or [→] move right
				    [S] or [↓] fall faster
				    [Q] spin left
				    [E] spin right
				    [Spacebar] drop
				    [P] pause and unpause
				    [Escape] close game
				    [Enter] start game
				""");
			bool mainMenuScreen = true;
			while (!closeRequested && mainMenuScreen)
			{
				Console.CursorVisible = false;
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Enter: mainMenuScreen = false; break;
					case ConsoleKey.Escape: closeRequested = true; break;
				}
			}
			Initialize();
			await Console.Clear();
			await DrawFrame();
			while (!closeRequested && !gameOver)
			{
				// if user changed the size of the console, we need to clear the console
				if (consoleWidth != Console.WindowWidth || consoleHeight != Console.WindowHeight)
				{
					consoleWidth = Console.WindowWidth;
					consoleHeight = Console.WindowHeight;
					if (!consoleTooSmallScreen)
					{
						await Console.Clear();
						await DrawFrame();
					}
					else
					{
						consoleTooSmallScreen = false;
					}
				}

				// if the console isn't big enough to render the game, pause the game and tell the user
				if (consoleWidth < consoleWidthMin || consoleHeight < consoleHeightMin)
				{
					if (!consoleTooSmallScreen)
					{
						await Console.Clear();
						await Console.Write($"Please increase size of console to at least {consoleWidthMin}x{consoleHeightMin}. Current size is {consoleWidth}x{consoleHeight}.");
						timer.Stop();
						consoleTooSmallScreen = true;
					}
				}
				else if (consoleTooSmallScreen)
				{
					consoleTooSmallScreen = false;
					await Console.Clear();
					await DrawFrame();
				}

				await HandlePlayerInput();
				if (closeRequested || gameOver)
				{
					break;
				}
				if (timer.IsRunning && timer.Elapsed > fallSpeed)
				{
					await TetrominoFall();
					if (closeRequested || gameOver)
					{
						break;
					}
					await DrawFrame();
				}
			}
			if (closeRequested)
			{
				break;
			}
			await Console.Clear();
			await Console.Write($"""

				      ██████╗  █████╗ ██    ██╗█████╗
				     ██╔════╝ ██╔══██╗███  ███║██╔══╝
				     ██║  ███╗███████║██╔██═██║█████╗
				     ██║   ██║██╔══██║██║   ██║██╔══╝
				     ╚██████╔╝██║  ██║██║   ██║█████╗
				      ╚═════╝ ╚═╝  ╚═╝╚═╝   ╚═╝╚════╝
				       ██████╗██╗  ██╗█████╗█████╗
				       ██  ██║██║  ██║██╔══╝██╔═██╗
				       ██  ██║██║  ██║█████╗█████╔╝
				       ██  ██║╚██╗██╔╝██╔══╝██╔═██╗
				       ██████║ ╚███╔╝ █████╗██║ ██║
				       ╚═════╝  ╚══╝  ╚════╝╚═╝ ╚═╝

				     Final Score: {score}

				    [Enter] return to menu
				    [Escape] close game
				""");
				Console.CursorVisible = false;
			bool gameOverScreen = true;
			while (!closeRequested && gameOverScreen)
			{
				Console.CursorVisible = false;
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Enter: gameOverScreen = false; break;
					case ConsoleKey.Escape: closeRequested = true; break;
				}
			}
		}
		await Console.Clear();
		await Console.WriteLine("Tetris was closed.");
		Console.CursorVisible = true;
		await Console.Refresh();

		void Initialize()
		{
			gameOver = false;
			score = 0;
			field = emptyField[..];
			initialX = (field[0].Length / 2) - 3;
			initialY = 1;
			tetromino = new()
			{
				Shape = tetrominos[Random.Shared.Next(0, tetrominos.Length)],
				Next = tetrominos[Random.Shared.Next(0, tetrominos.Length)],
				X = initialX,
				Y = initialY
			};
			fallSpeed = GetFallSpeed();
			timer.Restart();
		}

		async Task HandlePlayerInput()
		{
			while ((await Console.KeyAvailable()) && !closeRequested)
			{
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.A or ConsoleKey.LeftArrow:
						if (timer.IsRunning && !Collision(Direction.Left))
						{
							tetromino.X -= 3;
						}
						await DrawFrame();
						break;
					case ConsoleKey.D or ConsoleKey.RightArrow:
						if (timer.IsRunning && !Collision(Direction.Right))
						{
							tetromino.X += 3;
						}
						await DrawFrame();
						break;
					case ConsoleKey.S or ConsoleKey.DownArrow:
						if (timer.IsRunning)
						{
							await TetrominoFall();
						}
						break;
					case ConsoleKey.E:
						if (timer.IsRunning)
						{
							TetrominoSpin(Direction.Right);
							await DrawFrame();
						}
						break;
					case ConsoleKey.Q:
						if (timer.IsRunning)
						{
							TetrominoSpin(Direction.Left);
							await DrawFrame();
						}
						break;
					case ConsoleKey.P:
						if (timer.IsRunning)
						{
							timer.Stop();
							await DrawFrame();
						}
						else if (!consoleTooSmallScreen)
						{
							timer.Start();
							await DrawFrame();
						}
						break;
					case ConsoleKey.Spacebar:
						if (timer.IsRunning)
						{
							await HardDrop();
						}
						break;
					case ConsoleKey.Escape:
						closeRequested = true;
						return;
				}
			}
		}

		async Task DrawFrame()
		{
			bool collision = false;
			char[][] frame = new char[field.Length][];

			// Field
			for (int y = 0; y < field.Length; y++)
			{
				frame[y] = field[y].ToCharArray();
			}

			// Tetromino
			for (int y = 0; y < tetromino.Shape.Length && !collision; y++)
			{
				for (int x = 0; x < tetromino.Shape[y].Length; x++)
				{
					int tY = tetromino.Y + y;
					int tX = tetromino.X + x;
					char charToReplace = field[tY][tX];
					char charTetromino = tetromino.Shape[y][x];
					if (charTetromino is ' ')
					{
						continue;
					}
					if (charToReplace is not ' ')
					{
						collision = true;
						break;
					}
					if (charTetromino is 'x')
					{
						charTetromino = '╭';
					}
					frame[tY][tX] = charTetromino;
				}
			}

			// Draw Preview
			for (int yField = field.Length - tetromino.Shape.Length - borderSize; yField >= 0; yField -= 2)
			{
				if (CollisionBottom(yField, tetromino.Y, tetromino.Shape))
				{
					continue;
				}
				for (int y = 0; y < tetromino.Shape.Length && !collision; y++)
				{
					for (int x = 0; x < tetromino.Shape[y].Length; x++)
					{
						int tY = yField + y;
						if (tetromino.Y + tetromino.Shape.Length > tY)
						{
							continue;
						}
						int tX = tetromino.X + x;
						char charToReplace = field[tY][tX];
						char charTetromino = tetromino.Shape[y][x];
						if (charTetromino is ' ')
						{
							continue;
						}
						if (charToReplace is not ' ')
						{
							collision = true;
							break;
						}
						frame[tY][tX] = '•';
					}
				}
				break;
			}

			// Next
			for (int y = 0; y < nextTetrominoBorder.Length; y++)
			{
				frame[y] = frame[y].Concat(nextTetrominoBorder[y]).ToArray();
			}
			for (int y = 0; y < tetromino.Next.Length; y++)
			{
				for (int x = 0; x < tetromino.Next[y].Length; x++)
				{
					int tY = y + borderSize;
					int tX = field[y].Length + x + borderSize;
					char charTetromino = tetromino.Next[y][x];
					if (charTetromino is 'x')
					{
						charTetromino = '╭';
					}
					frame[tY][tX] = charTetromino;
				}
			}

			// Score
			for (int y = 0; y < scoreBorder.Length; y++)
			{
				int sY = nextTetrominoBorder.Length + y;
				frame[sY] = frame[sY].Concat(scoreBorder[y]).ToArray();
			}
			char[] scoreRender = score.ToString(CultureInfo.InvariantCulture).ToCharArray();
			for (int scoreX = scoreRender.Length - 1; scoreX >= 0; scoreX--)
			{
				int sY = nextTetrominoBorder.Length + borderSize;
				int sX = frame[sY].Length - (scoreRender.Length - scoreX) - borderSize;
				frame[sY][sX] = scoreRender[scoreX];
			}

			// Pause
			if (!timer.IsRunning)
			{
				for (int y = 0; y < pauseRender.Length; y++)
				{
					int fY = (field.Length / 2) + y - pauseRender.Length;
					for (int x = 0; x < pauseRender[y].Length; x++)
					{
						int fX = x + borderSize;

						if (x >= field[fY].Length) break;

						frame[fY][fX] = pauseRender[y][x];
					}
				}
			}

			StringBuilder render = new();
			for (int y = 0; y < frame.Length; y++)
			{
				render.AppendLine(new string(frame[y]));
			}
			await Console.SetCursorPosition(0, 0);
			await Console.Write(render);
			Console.CursorVisible = false;
		}

		char[][] DrawLastFrame(int yS)
		{
			bool collision = false;
			int yScope = yS - 2;
			int xScope = tetromino.X;
			char[][] frame = new char[field.Length][];
			for (int y = 0; y < field.Length; y++)
			{
				frame[y] = field[y].ToCharArray();
			}
			for (int y = 0; y < tetromino.Shape.Length && !collision; y++)
			{
				for (int x = 0; x < tetromino.Shape[y].Length; x++)
				{
					int tY = yScope + y;
					int tX = xScope + x;
					char charToReplace = field[tY][tX];
					char charTetromino = tetromino.Shape[y][x];
					if (charTetromino is ' ')
					{
						continue;
					}
					if (charToReplace is not ' ')
					{
						collision = true;
						break;
					}
					if (charTetromino is 'x')
					{
						charTetromino = '╭';
					}
					frame[tY][tX] = charTetromino;
				}
			}
			return frame;
		}

		bool Collision(Direction direction)
		{
			int xNew = tetromino.X;
			bool collision = false;
			switch (direction)
			{
				case Direction.Right:
					xNew += 3;
					if (xNew + tetromino.Shape[0].Length > field[0].Length - borderSize)
					{
						collision = true;
					}
					break;
				case Direction.Left:
					xNew -= 3;
					if (xNew < borderSize)
					{
						collision = true;
					}
					break;
				case Direction.None:
					break;
			}
			if (collision)
			{
				return collision;
			}
			for (int y = 0; y < tetromino.Shape.Length && !collision; y++)
			{
				for (int x = 0; x < tetromino.Shape[y].Length; x++)
				{
					int tY = tetromino.Y + y;
					int tX = xNew + x;
					char charToReplace = field[tY][tX];
					char charTetromino = tetromino.Shape[y][x];
					if (charTetromino is ' ')
					{
						continue;
					}
					if (charToReplace is not ' ')
					{
						collision = true;
						break;
					}
				}
			}
			return collision;
		}

		bool CollisionBottom(int initY, int yScope, string[] shape)
		{
			int xNew = tetromino.X;
			for (int yUpper = initY; yUpper >= yScope; yUpper -= 2)
			{
				for (int y = shape.Length - 1; y >= 0; y -= 2)
				{
					for (int x = 0; x < shape[y].Length; x++)
					{
						int tY = yUpper + y;
						int tX = xNew + x;
						char charToReplace = field[tY][tX];
						char charTetromino = shape[y][x];
						if (charTetromino is ' ')
						{
							continue;
						}
						if (charToReplace is not ' ')
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		TimeSpan GetFallSpeed() =>
			TimeSpan.FromMilliseconds(
				score switch
				{
					> 162 => 100,
					> 144 => 200,
					> 126 => 300,
					> 108 => 400,
					> 090 => 500,
					> 072 => 600,
					> 054 => 700,
					> 036 => 800,
					> 018 => 900,
					_ => 1000,
				});

		async Task TetrominoFall()
		{
			int yAfterFall = tetromino.Y;
			bool collision = false;

			if (tetromino.Y + tetromino.Shape.Length + 2 > field.Length)
			{
				yAfterFall = field.Length - tetromino.Shape.Length + 1;
			}
			else
			{
				yAfterFall += 2;
			}

			// Y Collision
			for (int xCollision = 0; xCollision < tetromino.Shape[0].Length;)
			{
				for (int yCollision = tetromino.Shape.Length - 1; yCollision >= 0; yCollision -= 2)
				{
					char exist = tetromino.Shape[yCollision][xCollision];
					if (exist is ' ')
					{
						continue;
					}
					char[] lineYC = field[yAfterFall + yCollision - 1].ToCharArray();
					if (tetromino.X + xCollision < 0 || tetromino.X + xCollision > lineYC.Length)
					{
						continue;
					}
					if (lineYC[tetromino.X + xCollision] is not ' ' or '│')
					{
						char[][] lastFrame = DrawLastFrame(yAfterFall);
						for (int y = 0; y < lastFrame.Length; y++)
						{
							field[y] = new string(lastFrame[y]);
						}
						tetromino.X = initialX;
						tetromino.Y = initialY;
						tetromino.Shape = tetromino.Next;
						tetromino.Next = tetrominos[Random.Shared.Next(0, tetrominos.Length)];
						xCollision = tetromino.Shape[0].Length;
						collision = true;
						break;
					}
				}
				xCollision += 3;
			}

			if (!collision)
			{
				tetromino.Y = yAfterFall;
			}

			// Clean Lines
			int clearedLines = 0;
			for (int lineIndex = field.Length - 1; lineIndex >= 0; lineIndex--)
			{
				string line = field[lineIndex];
				bool notCompleted = line.Any(e => e is ' ');
				if (lineIndex is 0 || lineIndex == field.Length - 1)
				{
					continue;
				}
				if (!notCompleted)
				{
					field[lineIndex] = "│                              │";
					clearedLines++;
					for (int lineM = lineIndex; lineM >= 1; lineM--)
					{
						if (field[lineM - 1] is "╭──────────────────────────────╮")
						{
							field[lineM] = "│                              │";
							continue;
						}
						field[lineM] = field[lineM - 1];
					}
					lineIndex++;
				}
			}
			clearedLines /= 2;
			if (clearedLines > 0)
			{
				int value = clearedLines switch
				{
					1 => 1,
					2 => 3,
					3 => 6,
					4 => 9,
					_ => throw new NotImplementedException(),
				};
				score += value;
				fallSpeed = GetFallSpeed();
			}
			if (Collision(Direction.None))
			{
				gameOver = true;
			}
			else
			{
				await DrawFrame();
				timer.Restart();
			}
		}

		async Task HardDrop()
		{
			int y = tetromino.Y;
			int x = tetromino.X;
			for (int yField = field.Length - tetromino.Shape.Length - borderSize; yField >= 0; yField -= 2)
			{
				if (CollisionBottom(yField, y, tetromino.Shape))
				{
					continue;
				}
				tetromino.Y = yField;
				break;
			}
			await DrawFrame();
			timer.Restart();
		}

		void TetrominoSpin(Direction spinDirection)
		{
			int yScope = tetromino.Y;
			int xScope = tetromino.X;
			string[] newShape = new string[tetromino.Shape[0].Length / 3 * 2];
			int newY = 0;
			int rowEven = 0;
			int rowOdd = 1;

			// Turn
			for (int y = 0; y < tetromino.Shape.Length;)
			{
				switch (spinDirection)
				{
					case Direction.Right:
						SpinRight(newShape, tetromino.Shape, ref newY, rowEven, rowOdd, y);
						break;
					case Direction.Left:
						SpinLeft(newShape, tetromino.Shape, ref newY, rowEven, rowOdd, y);
						break;
				}
				newY = 0;
				rowEven += 2;
				rowOdd += 2;
				y += 2;
			}

			// Old Pivot
			(int y, int x) offsetOP = (0, 0);
			for (int y = 0; y < tetromino.Shape.Length; y += 2)
			{
				for (int x = 0; x < tetromino.Shape[y].Length; x += 3)
				{
					if (tetromino.Shape[y][x] is 'x')
					{
						offsetOP = (y / 2, x / 3);
						y = tetromino.Shape.Length;
						break;
					}
				}
			}

			// New Pivot
			(int y, int x) offsetNP = (0, 0);
			for (int y = 0; y < newShape.Length; y += 2)
			{
				for (int x = 0; x < newShape[y].Length; x += 3)
				{
					if (newShape[y][x] is 'x')
					{
						offsetNP = (y / 2, x / 3);
						y = newShape.Length;
						break;
					}
				}
			}

			yScope += (offsetOP.y - offsetNP.y) * 2;
			xScope += (offsetOP.x - offsetNP.x) * 3;

			// Tetromino Square(O) special case
			if (newShape.Length / 2 == newShape[0].Length / 3)
			{
				yScope = tetromino.Y;
				xScope = tetromino.X;
			}
			// Tetromino I special case
			else if (newShape.Length is 8 && newShape[0].Length is 3 && offsetNP.y is 2)
			{
				newShape[2] = "x─╮";
				newShape[4] = "╭─╮";
				yScope += 2;
			}

			if (xScope < 1 || yScope < 1)
			{
				return;
			}

			// Verified Collision
			for (int y = 0; y < newShape.Length - 1; y++)
			{
				for (int x = 0; x < newShape[y].Length; x++)
				{
					if (newShape[y][x] is ' ')
					{
						continue;
					}
					char c = field[yScope + y][xScope + x];
					if (c is not ' ')
					{
						return;
					}
				}
			}
			tetromino.Y = yScope;
			tetromino.X = xScope;
			tetromino.Shape = newShape;
		}

		void SpinLeft(string[] newShape, string[] shape, ref int newY, int rowEven, int rowOdd, int y)
		{
			for (int x = shape[y].Length - 1; x >= 0; x -= 3)
			{
				for (int xS = 2; xS >= 0; xS--)
				{
					newShape[newY] += shape[rowEven][x - xS];
					newShape[newY + 1] += shape[rowOdd][x - xS];
				}
				newY += 2;
			}
		}

		void SpinRight(string[] newShape, string[] shape, ref int newY, int rowEven, int rowOdd, int y)
		{
			for (int x = 2; x < shape[y].Length; x += 3)
			{
				if (newShape[newY] is null)
				{
					newShape[newY] = "";
					newShape[newY + 1] = "";
				}
				for (int xS = 0; xS <= 2; xS++)
				{
					newShape[newY] = newShape[newY].Insert(0, shape[rowEven][x - xS].ToString(CultureInfo.InvariantCulture));
					newShape[newY + 1] = newShape[newY + 1].Insert(0, shape[rowOdd][x - xS].ToString(CultureInfo.InvariantCulture));
				}
				newY += 2;
			}
		}
	}

	class Tetromino
	{
		public required string[] Shape { get; set; }
		public required string[] Next { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
	}

	enum Direction
	{
		None,
		Right,
		Left,
	}
}
