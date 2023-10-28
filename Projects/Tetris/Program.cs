using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

Console.OutputEncoding = Encoding.UTF8;
Console.CursorVisible = false;
Stopwatch Stopwatch = Stopwatch.StartNew();

bool DEBUGCONTROLS = false;

string[] FIELD = new[]
{
	"╭──────────────────────────────╮",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"│                              │",
	"╰──────────────────────────────╯"
};

string[] NEXTTETROMINO = new[]
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

string[] SCORE = new[]{
	"╭─────────╮",
	"│         │",
	"╰─────────╯"
};

string[] PAUSE = new[]{
	"█████╗ ███╗ ██╗██╗█████╗█████╗",
	"██╔██║██╔██╗██║██║██╔══╝██╔══╝",
	"█████║█████║██║██║ ███╗ █████╗",
	"██╔══╝██╔██║██║██║   ██╗██╔══╝",
	"██║   ██║██║█████║█████║█████╗",
	"╚═╝   ╚═╝╚═╝╚════╝╚════╝╚════╝",
};

string[][] TETROMINOS = new[]
{
	new[]{
		"╭─╮",
		"╰─╯",
		"╭─╮",
		"╰─╯",
		"╭─╮",
		"╰─╯",
		"╭─╮",
		"╰─╯"
	},
	new[]{
		"╭─╮      ",
		"╰─╯      ",
		"╭─╮╭─╮╭─╮",
		"╰─╯╰─╯╰─╯"
	},
	new[]{
		"      ╭─╮",
		"      ╰─╯",
		"╭─╮╭─╮╭─╮",
		"╰─╯╰─╯╰─╯"
	},
	new[]{
		"╭─╮╭─╮",
		"╰─╯╰─╯",
		"╭─╮╭─╮",
		"╰─╯╰─╯"
	},
	new[]{
		"   ╭─╮╭─╮",
		"   ╰─╯╰─╯",
		"╭─╮╭─╮   ",
		"╰─╯╰─╯   "
	},
	new[]{
		"   ╭─╮   ",
		"   ╰─╯   ",
		"╭─╮╭─╮╭─╮",
		"╰─╯╰─╯╰─╯"
	},
	new[]{
		"╭─╮╭─╮   ",
		"╰─╯╰─╯   ",
		"   ╭─╮╭─╮",
		"   ╰─╯╰─╯"
	},
};

string[] PLAYFIELD = (string[])FIELD.Clone();
const int BORDER = 1;
int FallSpeedMilliSeconds = 1000;
bool CloseGame = false;
int Score = 0;
int PauseCount = 0;
int TextColor = 0;
GameStatus GameStatus = GameStatus.Gameover;

int INITIALTETROMINOX = (PLAYFIELD[0].Length / 2) - 3;
int INITIALTETROMINOY = 1;
Tetromino TETROMINO = new()
{
	Shape = TETROMINOS[Random.Shared.Next(0, TETROMINOS.Length)],
	Next = TETROMINOS[Random.Shared.Next(0, TETROMINOS.Length)],
	X = INITIALTETROMINOX,
	Y = INITIALTETROMINOY
};

AutoResetEvent AutoEvent = new(false);
Timer? FallTimer = null;
GameStatus = GameStatus.Playing;

Console.WriteLine();
Console.WriteLine("""
	     ██████╗█████╗██████╗█████╗ ██╗█████╗
	     ╚═██╔═╝██╔══╝╚═██╔═╝██╔═██╗██║██╔══╝
	       ██║  █████╗  ██║  █████╔╝██║ ███╗
	       ██║  ██╔══╝  ██║  ██╔═██╗██║   ██╗
	       ██║  █████╗  ██║  ██║ ██║██║█████║
	       ╚═╝  ╚════╝  ╚═╝  ╚═╝ ╚═╝╚═╝╚════╝

	    Controls:
	    WASD or ARROW to move
	    Q or E to spin left or right
	    P to paused the game, press enter
	    key to resume
	    R to change Text color
	
	    Press escape to close the game at any time.
	
	    Press enter to start tetris...
	""");
Console.CursorVisible = false;
StartGame();
Console.Clear();

FallTimer = new Timer(TetrominoFall, AutoEvent, FallSpeedMilliSeconds, FallSpeedMilliSeconds);

while (!CloseGame)
{
	if (CloseGame)
	{
		break;
	}

	PlayerControl();
	if (GameStatus is GameStatus.Playing)
	{
		DrawFrame();
		SleepAfterRender();
	}
}

void PlayerControl()
{
	while (Console.KeyAvailable && GameStatus is GameStatus.Playing)
	{
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.A or ConsoleKey.LeftArrow:
				if (Collision(Direction.Left)) break;
				TETROMINO.X -= 3;
				break;
			case ConsoleKey.D or ConsoleKey.RightArrow:
				if (Collision(Direction.Right)) break;
				TETROMINO.X += 3;
				break;
			case ConsoleKey.S or ConsoleKey.DownArrow:
				FallTimer.Change(0, FallSpeedMilliSeconds);
				break;
			case ConsoleKey.E:
				TetrominoSpin(Direction.Right);
				break;
			case ConsoleKey.Q:
				TetrominoSpin(Direction.Left);
				break;
			case ConsoleKey.P:
				PauseGame();
				break;
			case ConsoleKey.R:
				TextColor++;
				if (TextColor is 16) TextColor = 1;
				Console.ForegroundColor = (ConsoleColor)TextColor;
				break;

			//DEBUG
			case ConsoleKey.Spacebar:
				if (!DEBUGCONTROLS) return;
				PLAYFIELD = (string[])FIELD.Clone();
				break;
			case ConsoleKey.I:
				if (!DEBUGCONTROLS) return;
				TETROMINO.Shape = TETROMINOS[0];
				break;
			case ConsoleKey.J:
				if (!DEBUGCONTROLS) return;
				TETROMINO.Shape = TETROMINOS[1];
				break;
			case ConsoleKey.L:
				if (!DEBUGCONTROLS) return;
				TETROMINO.Shape = TETROMINOS[2];
				break;
			case ConsoleKey.O:
				if (!DEBUGCONTROLS) return;
				TETROMINO.Shape = TETROMINOS[3];
				break;
			case ConsoleKey.C:
				if (!DEBUGCONTROLS) return;
				TETROMINO.Shape = TETROMINOS[4];
				break;
			case ConsoleKey.T:
				if (!DEBUGCONTROLS) return;
				TETROMINO.Shape = TETROMINOS[5];
				break;
			case ConsoleKey.Z:
				if (!DEBUGCONTROLS) return;
				TETROMINO.Shape = TETROMINOS[6];
				break;
			case ConsoleKey.X:
				if (!DEBUGCONTROLS) return;
				Score += 10;
				break;
		}
	}
}

void DrawFrame()
{
	bool collision = false;
	int yScope = TETROMINO.Y;
	string[] shapeScope = TETROMINO.Shape;
	string[] nextShapeScope = TETROMINO.Next;
	char[][] frame = new char[PLAYFIELD.Length][];

	//Field
	for (int y = 0; y < PLAYFIELD.Length; y++)
	{
		frame[y] = PLAYFIELD[y].ToCharArray();
	}

	//Draw Tetromino
	for (int y = 0; y < shapeScope.Length && !collision; y++)
	{
		for (int x = 0; x < shapeScope[y].Length; x++)
		{
			int tY = yScope + y;
			int tX = TETROMINO.X + x;
			char charToReplace = PLAYFIELD[tY][tX];
			char charTetromino = shapeScope[y][x];

			if (charTetromino is ' ') continue;

			if (charToReplace is not ' ')
			{
				collision = true;
				break;
			}

			frame[tY][tX] = charTetromino;
		}
	}

	//Draw Preview
	for (int yField = PLAYFIELD.Length - shapeScope.Length - BORDER; yField >= 0; yField -= 2)
	{
		if (CollisionPreview(yField, yScope, shapeScope)) continue;

		for (int y = 0; y < shapeScope.Length && !collision; y++)
		{
			for (int x = 0; x < shapeScope[y].Length; x++)
			{
				int tY = yField + y;

				if (yScope + shapeScope.Length > tY) continue;

				int tX = TETROMINO.X + x;
				char charToReplace = PLAYFIELD[tY][tX];
				char charTetromino = shapeScope[y][x];

				if (charTetromino is ' ') continue;

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

	//Next Square
	for (int y = 0; y < NEXTTETROMINO.Length; y++)
	{
		frame[y] = frame[y].Concat(NEXTTETROMINO[y]).ToArray();
	}

	//Score Square
	for (int y = 0; y < SCORE.Length; y++)
	{
		int sY = NEXTTETROMINO.Length + y;
		frame[sY] = frame[sY].Concat(SCORE[y]).ToArray();
	}

	//Draw Next
	for (int y = 0; y < nextShapeScope.Length; y++)
	{
		for (int x = 0; x < nextShapeScope[y].Length; x++)
		{
			int tY = y + BORDER;
			int tX = PLAYFIELD[y].Length + x + BORDER;
			char charTetromino = nextShapeScope[y][x];
			frame[tY][tX] = charTetromino;
		}
	}

	//Draw Score
	char[] score = Score.ToString().ToCharArray();
	for (int scoreX = score.Length - 1; scoreX >= 0; scoreX--)
	{
		int sY = NEXTTETROMINO.Length + BORDER;
		int sX = frame[sY].Length - (score.Length - scoreX) - BORDER;
		frame[sY][sX] = score[scoreX];
	}

	//Draw Pause
	if (GameStatus is GameStatus.Paused)
	{
		for (int y = 0; y < PAUSE.Length; y++)
		{
			int fY = (PLAYFIELD.Length / 2) + y - PAUSE.Length;
			for (int x = 0; x < PAUSE[y].Length; x++)
			{
				int fX = x + BORDER;

				if (x >= PLAYFIELD[fY].Length) break;

				frame[fY][fX] = PAUSE[y][x];
			}
		}
	}

	//Create Render
	StringBuilder render = new();
	for (int y = 0; y < frame.Length; y++)
	{
		render.AppendLine(new string(frame[y]));
	}

	Console.Clear();
	Console.Write(render);
	Console.CursorVisible = false;
}

char[][] DrawLastFrame(int yS)
{
	bool collision = false;
	int yScope = yS - 2;
	int xScope = TETROMINO.X;
	string[] shapeScope = (string[])TETROMINO.Shape.Clone();
	string[] nextShapeScope = (string[])TETROMINO.Next.Clone();
	char[][] frame = new char[PLAYFIELD.Length][];

	//Field
	for (int y = 0; y < PLAYFIELD.Length; y++)
	{
		frame[y] = PLAYFIELD[y].ToCharArray();
	}

	//Draw Tetromino
	for (int y = 0; y < shapeScope.Length && !collision; y++)
	{
		for (int x = 0; x < shapeScope[y].Length; x++)
		{
			int tY = yScope + y;
			int tX = xScope + x;
			char charToReplace = PLAYFIELD[tY][tX];
			char charTetromino = shapeScope[y][x];

			if (charTetromino is ' ') continue;

			if (charToReplace is not ' ')
			{
				collision = true;
				break;
			}

			frame[tY][tX] = charTetromino;
		}
	}

	return frame;
}

bool Collision(Direction direction)
{
	int xNew = TETROMINO.X;
	int yScope = TETROMINO.Y;
	string[] shapeScope = (string[])TETROMINO.Shape.Clone();
	bool collision = false;

	switch (direction)
	{
		case Direction.Right:
			xNew += 3;
			if (xNew + shapeScope[0].Length > PLAYFIELD[0].Length - BORDER) collision = true;
			break;
		case Direction.Left:
			xNew -= 3;
			if (xNew < BORDER) collision = true;
			break;
		case Direction.None:
			break;
	}

	if (collision) return collision;

	for (int y = 0; y < shapeScope.Length && !collision; y++)
	{
		for (int x = 0; x < shapeScope[y].Length; x++)
		{
			int tY = yScope + y;
			int tX = xNew + x;
			char charToReplace = PLAYFIELD[tY][tX];
			char charTetromino = shapeScope[y][x];

			if (charTetromino is ' ') continue;

			if (charToReplace is not ' ')
			{
				collision = true;
				break;
			}
		}
	}

	return collision;
}

bool CollisionPreview(int initY, int yScope, string[] shape)
{
	int xNew = TETROMINO.X;

	for (int yUpper = initY; yUpper >= yScope; yUpper -= 2)
	{
		for (int y = shape.Length - 1; y >= 0; y -= 2)
		{
			for (int x = 0; x < shape[y].Length; x++)
			{
				int tY = yUpper + y;
				int tX = xNew + x;
				char charToReplace = PLAYFIELD[tY][tX];
				char charTetromino = shape[y][x];

				if (charTetromino is ' ') continue;

				if (charToReplace is not ' ')
				{
					return true;
				}
			}
		}
	}

	return false;
}

void Gameover()
{
	GameStatus = GameStatus.Gameover;
	AutoEvent.Dispose();
	FallTimer.Dispose();

	SleepAfterRender();

	Console.Clear();
	Console.Write($"""

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
	
	     Final Score: {Score}
	     Pause Count: {PauseCount}
	
	     Press enter to play again
	     Press escape to close the game
	""");
	Console.CursorVisible = false;
	StartGame();
	RestartGame();
}

void StartGame(ConsoleKey key = ConsoleKey.Enter)
{
	ConsoleKey input = default;
	while (input != key && !CloseGame)
	{
		input = Console.ReadKey(true).Key;
		if (input is ConsoleKey.Escape)
		{
			CloseGame = true;
			return;
		}
	}
}

void RestartGame()
{
	PLAYFIELD = (string[])FIELD.Clone();
	FallSpeedMilliSeconds = 1000;
	Score = 0;
	TETROMINO = new()
	{
		Shape = TETROMINOS[Random.Shared.Next(0, TETROMINOS.Length)],
		Next = TETROMINOS[Random.Shared.Next(0, TETROMINOS.Length)],
		X = INITIALTETROMINOX,
		Y = INITIALTETROMINOY
	};

	AutoEvent = new AutoResetEvent(false);
	FallTimer = new Timer(TetrominoFall, AutoEvent, FallSpeedMilliSeconds, FallSpeedMilliSeconds);
	GameStatus = GameStatus.Playing;
}

void PauseGame()
{
	PauseCount++;
	FallTimer.Change(Timeout.Infinite, Timeout.Infinite);
	GameStatus = GameStatus.Paused;
	DrawFrame();

	ResumeGame();
}

void ResumeGame(ConsoleKey key = ConsoleKey.Enter)
{
	ConsoleKey input = default;
	while (input != key && !CloseGame)
	{
		input = Console.ReadKey(true).Key;
		if (input is ConsoleKey.Enter && GameStatus is GameStatus.Paused && FallTimer != null)
		{
			FallTimer.Change(0, FallSpeedMilliSeconds);
			GameStatus = GameStatus.Playing;
			return;
		}
	}
}

void AddScoreChangeSpeed(int value)
{
	Score += value;

	FallSpeedMilliSeconds = Score switch
	{
		> 100 => FallSpeedMilliSeconds = 050,
		> 070 => FallSpeedMilliSeconds = 100,
		> 060 => FallSpeedMilliSeconds = 200,
		> 050 => FallSpeedMilliSeconds = 300,
		> 040 => FallSpeedMilliSeconds = 500,
		> 030 => FallSpeedMilliSeconds = 700,
		> 020 => FallSpeedMilliSeconds = 800,
		> 010 => FallSpeedMilliSeconds = 900,
		_ => 1000,
	};
}

void TetrominoFall(object? e)
{
	int yAfterFall = TETROMINO.Y;
	bool collision = false;

	if (TETROMINO.Y + TETROMINO.Shape.Length + 2 > PLAYFIELD.Length) yAfterFall = PLAYFIELD.Length - TETROMINO.Shape.Length + 1;
	else yAfterFall += 2;

	//Y Collision
	for (int xCollision = 0; xCollision < TETROMINO.Shape[0].Length;)
	{
		for (int yCollision = TETROMINO.Shape.Length - 1; yCollision >= 0; yCollision -= 2)
		{
			char exist = TETROMINO.Shape[yCollision][xCollision];

			if (exist is ' ') continue;

			char[] lineYC = PLAYFIELD[yAfterFall + yCollision - 1].ToCharArray();

			if (TETROMINO.X + xCollision < 0 || TETROMINO.X + xCollision > lineYC.Length) continue;

			if (lineYC[TETROMINO.X + xCollision] is not ' ' or '│')
			{
				char[][] lastFrame = DrawLastFrame(yAfterFall);
				for (int y = 0; y < lastFrame.Length; y++)
				{
					PLAYFIELD[y] = new string(lastFrame[y]);
				}

				TETROMINO.X = INITIALTETROMINOX;
				TETROMINO.Y = INITIALTETROMINOY;
				TETROMINO.Shape = TETROMINO.Next;
				TETROMINO.Next = TETROMINOS[Random.Shared.Next(0, TETROMINOS.Length)];

				xCollision = TETROMINO.Shape[0].Length;
				collision = true;
				break;
			}
		}

		xCollision += 3;
	}

	if (!collision) TETROMINO.Y = yAfterFall;

	//Clean Lines
	for (var lineIndex = PLAYFIELD.Length - 1; lineIndex >= 0; lineIndex--)
	{
		string line = PLAYFIELD[lineIndex];
		bool notCompleted = line.Any(e => e is ' ');

		if (lineIndex is 0 || lineIndex == PLAYFIELD.Length - 1) continue;

		if (!notCompleted)
		{
			PLAYFIELD[lineIndex] = "│                              │";
			AddScoreChangeSpeed(1);

			for (int lineM = lineIndex; lineM >= 1; lineM--)
			{
				if (PLAYFIELD[lineM - 1] is "╭──────────────────────────────╮")
				{
					PLAYFIELD[lineM] = "│                              │";
					continue;
				}

				PLAYFIELD[lineM] = PLAYFIELD[lineM - 1];
			}

			lineIndex++;
		}
	}

	//VerifiedCollision
	if (Collision(Direction.None) && FallTimer is not null) Gameover();
}

void TetrominoSpin(Direction spinDirection)
{
	string[] shapeScope = (string[])TETROMINO.Shape.Clone();
	int yScope = TETROMINO.Y;
	string[] newShape = new string[shapeScope[0].Length / 3 * 2];
	int newY = 0;
	int rowEven = 0;
	int rowOdd = 1;

	//Turn
	for (int y = 0; y < shapeScope.Length;)
	{
		switch (spinDirection)
		{
			case Direction.Right:
				SpinRight(newShape, shapeScope, ref newY, rowEven, rowOdd, y);
				break;
			case Direction.Left:
				SpinLeft(newShape, shapeScope, ref newY, rowEven, rowOdd, y);
				break;
		}

		newY = 0;
		rowEven += 2;
		rowOdd += 2;
		y += 2;
	}

	//Verified Collision
	for (int y = 0; y < newShape.Length - 1; y++)
	{
		for (int x = 0; x < newShape[y].Length; x++)
		{
			if (newShape[y][x] is ' ') continue;

			char c = PLAYFIELD[yScope + y][TETROMINO.X + x];
			if (c is not ' ') return;
		}
	}

	TETROMINO.Shape = newShape;
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
			newShape[newY] = newShape[newY].Insert(0, shape[rowEven][x - xS].ToString());
			newShape[newY + 1] = newShape[newY + 1].Insert(0, shape[rowOdd][x - xS].ToString());
		}

		newY += 2;
	}
}

void SleepAfterRender()
{
	TimeSpan sleep = TimeSpan.FromSeconds(1d / 60d) - Stopwatch.Elapsed;
	if (sleep > TimeSpan.Zero)
	{
		Thread.Sleep(sleep);
	}
	Stopwatch.Restart();
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
	Right,
	Left,
	None
}

enum GameStatus
{
	Gameover,
	Playing,
	Paused
}