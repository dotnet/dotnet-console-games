using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

if (Console.WindowWidth < 80 ||
	Console.WindowHeight < 25)
{
	Console.WriteLine("Console Size is too small!");
	Console.WriteLine("Minimum required Width: 80 Height: 25");
	Console.WriteLine("Recommended Width: 120 Height: 30");
}

Console.WriteLine("Warning! Game can be inefficient on large console sizes");
Thread.Sleep(TimeSpan.FromSeconds(2));

// Variable game settings
int gameDelay = 30;
double gunXStretch = 1;
int crosshairSpeed = 2;
bool gunEnabled = true;
bool bulletsEnabled = false;
bool gunOutlineEnabled = false;

const char NULL_CHAR = '\0';
const char EMPTY_CHAR = '-';
const int BARREL_LENGTH = 10;

bool inMenu = false;
bool fireGun = false;
bool gunSelected = true;
bool crosshairSelected = false;
bool gameOver = false;
int frame = 0;
int score = 0;
int ammoCount = 5;
int spawnDelay = 100;
int grassLevel = Sprites.ScreenHeight - 4;
char[,] screenBuffer = new char[Sprites.ScreenWidth, Sprites.ScreenHeight];
Random rng = new();
List<Bird> birds = new();
List<Bullet> bullets = new();
StringBuilder screenGraphic = new();
Stopwatch timer = new();
Point crosshair   = new(Sprites.ScreenWidth / 2,     Sprites.ScreenHeight / 3);
Point LeftAncor   = new(Sprites.ScreenWidth / 2 - 3, Sprites.ScreenHeight - 2);
Point middleAncor = new(Sprites.ScreenWidth / 2,     Sprites.ScreenHeight - 2);
Point RightAncor  = new(Sprites.ScreenWidth / 2 + 3, Sprites.ScreenHeight - 2);

Console.CursorVisible = false;

try
{
	timer.Start();
	while (!gameOver)
	{
		Console.Title = $"FPS: {(int)(frame / timer.Elapsed.TotalSeconds)}";

		if (inMenu)
		{
			Console.Clear();
		}
		while (inMenu)
		{
			string menuDisplay =
				 "Press Corresponding Number to Edit/Select variables" + Sprites.NEWLINE_CHAR + Sprites.NEWLINE_CHAR +
				 "  Currently selected variable: " + (gunSelected ? "[1]" : crosshairSelected ? "[2]" : "[3]") + Sprites.NEWLINE_CHAR + Sprites.NEWLINE_CHAR +
				$"[1]        Gun X Axis Stretch: {gunXStretch:F}      " + Sprites.NEWLINE_CHAR + Sprites.NEWLINE_CHAR +
				$"[2]  Crosshair Movement Speed: {crosshairSpeed}     " + Sprites.NEWLINE_CHAR + Sprites.NEWLINE_CHAR +
				$"[3] Game Delay (Milliseconds): {gameDelay}          " + Sprites.NEWLINE_CHAR + Sprites.NEWLINE_CHAR +
				$"[4]           Bullets Enabled: {bulletsEnabled}-    " + Sprites.NEWLINE_CHAR + Sprites.NEWLINE_CHAR +
				$"[5]  Gun Outline Mode Enabled: {gunOutlineEnabled}- " + Sprites.NEWLINE_CHAR + Sprites.NEWLINE_CHAR +
				$"[6]               Gun Enabled: {gunEnabled}-        " + Sprites.NEWLINE_CHAR + Sprites.NEWLINE_CHAR +
				"Press [^] arrow to increase and [v] arrow to decrease ";

			DrawToScreenWithColour(1, 4, ConsoleColor.Yellow, menuDisplay.ToCharArray());
			DrawToScreenWithColour(1, 1, ConsoleColor.White, ("[ESC] Quit" + Sprites.NEWLINE_CHAR + "[ENTER] Exit Menu").ToCharArray());
			
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.D1: gunSelected = true; crosshairSelected = false; break;
				case ConsoleKey.D2: gunSelected = false; crosshairSelected = true; break;
				case ConsoleKey.D3: gunSelected = false; crosshairSelected = false; break;
				case ConsoleKey.D4: bulletsEnabled = !bulletsEnabled; break;
				case ConsoleKey.D5: gunOutlineEnabled = !gunOutlineEnabled; break;
				case ConsoleKey.D6: gunEnabled = !gunEnabled; break;
				case ConsoleKey.Enter: inMenu = false; continue;
				case ConsoleKey.Escape: return;

				case ConsoleKey.UpArrow:
					if (gunSelected)
					{
						gunXStretch += 0.1;
					}
					else if (crosshairSelected)
					{
						crosshairSpeed++;
					}
					else
					{
						gameDelay++;
					}
					break;
				case ConsoleKey.DownArrow:
					if (gunSelected)
					{
						gunXStretch -= 0.1;
					}
					else if (crosshairSelected)
					{
						crosshairSpeed--;
					}
					else
					{
						gameDelay--;
					}
					break;
			}

			timer.Restart();
			frame = 0;
		}

		if (Console.KeyAvailable)
		{
			switch (Console.ReadKey(true).Key)
			{
				case ConsoleKey.UpArrow: crosshair.Y -= crosshairSpeed; break;
				case ConsoleKey.DownArrow: crosshair.Y += crosshairSpeed; break;
				case ConsoleKey.LeftArrow: crosshair.X -= crosshairSpeed; break;
				case ConsoleKey.RightArrow: crosshair.X += crosshairSpeed; break;
				case ConsoleKey.Spacebar: fireGun = true; break;
				case ConsoleKey.Enter: inMenu = true; continue;
				case ConsoleKey.Escape: return;
			}

			if (crosshair.X < 2)
			{
				crosshair.X += crosshairSpeed;
			}
			if (crosshair.X > Sprites.ScreenWidth - Sprites.Enviroment.CrosshairWidth + 2)
			{
				crosshair.X -= crosshairSpeed;
			}

			if (crosshair.Y < 2)
			{
				crosshair.Y += crosshairSpeed;
			}
			if (crosshair.Y > Sprites.ScreenHeight - Sprites.Enviroment.CrosshairHeight)
			{
				crosshair.Y -= crosshairSpeed;
			}
		}
		while (Console.KeyAvailable)
		{
			Console.ReadKey(true);
		}

		WriteToBuffer(0, 0, Sprites.Border);
		WriteToBuffer(1, grassLevel, Sprites.Enviroment.Grass);
		WriteToBuffer(Sprites.Enviroment.TreeWidth - Sprites.Enviroment.TreeWidth / 2, grassLevel - Sprites.Enviroment.TreeHeight, Sprites.Enviroment.Tree);
		WriteToBuffer(Sprites.ScreenWidth - Sprites.Enviroment.BushWidth * 2, grassLevel - Sprites.Enviroment.BushHeight, Sprites.Enviroment.Bush);
		WriteToBuffer(1, 1, "[ENTER] Menu".ToCharArray());

		double theta = Math.Atan2(middleAncor.Y - crosshair.Y, middleAncor.X - crosshair.X);
		int xGunOffset = -(int)Math.Floor(Math.Cos(theta) * BARREL_LENGTH);
		int yGunOffset = -(int)Math.Floor(Math.Sin(theta) * BARREL_LENGTH);
		Point gunTopOffset = new((int)(xGunOffset * gunXStretch), yGunOffset);

		if (gunEnabled)
		{
			if (gunOutlineEnabled)
			{
				DrawLine(RightAncor, RightAncor + gunTopOffset);
				DrawLine(LeftAncor, LeftAncor + gunTopOffset);
				DrawLine(RightAncor + gunTopOffset, LeftAncor + gunTopOffset);
			}
			else
			{
				for (int i = LeftAncor.X; i <= RightAncor.X; i++)
				{
					Point gunBottomOffset = new(i, middleAncor.Y);
					DrawLine(gunBottomOffset, gunBottomOffset + gunTopOffset);
				}
			}
		}

		DrawToScreen(screenBuffer);
		DrawGUI();

		if (bulletsEnabled)
		{
			if (fireGun)
			{
				bullets.Add(new Bullet(middleAncor + gunTopOffset, theta));
				ammoCount--;
			}

			for (int i = 0; i < bullets.Count; i++)
			{
				bullets[i].UpdatePosition();

				if (bullets[i].OutOfBounds)
				{
					bullets.RemoveAt(i);
					continue;
				}

				foreach (Bird bird in birds)
				{
					if (!bird.IsDead &&
					   (bird.Contains((int)bullets[i].X[0], (int)bullets[i].Y[0]) ||
						bird.Contains((int)bullets[i].X[1], (int)bullets[i].Y[1])))
					{
						bird.IsDead = true;
						ammoCount += 2;
						score += 350;
					}
				}

				DrawToScreenWithColour((int)bullets[i].X[0], (int)bullets[i].Y[0], ConsoleColor.DarkGray, '█');
				DrawToScreenWithColour((int)bullets[i].X[1], (int)bullets[i].Y[1], ConsoleColor.DarkGray, '█');
			}
		}
		else
		{
			if (fireGun && ammoCount > 0)
			{
				foreach (Bird bird in birds)
				{
					if (!bird.IsDead && bird.Contains(crosshair.X, crosshair.Y))
					{
						bird.IsDead = true;
						ammoCount += 2;
						score += 150;
					}
				}
				ammoCount--;
			}
		}

		fireGun = false;

		foreach (Bird bird in birds)
		{
			DrawToScreenWithColour(bird.X, bird.Y, ConsoleColor.Red, bird.Direction is -1 ? Sprites.Bird.LeftSprites[bird.Frame] : Sprites.Bird.RightSprites[bird.Frame]);
			if (frame % 2 is 0)
			{
				bird.IncrementFrame();
				if (bird.IsDead)
				{
					bird.Y++;
				}
				else
				{
					bird.X += bird.Direction;
				}
			}
		}

		for (int i = birds.Count; i-- > 0;)
		{
			if (birds[i].Y > Sprites.ScreenHeight ||
			   (birds[i].Direction is -1 && birds[i].X < -Sprites.Bird.Width) ||
			   (birds[i].Direction is  1 && birds[i].X > Sprites.ScreenWidth + Sprites.Bird.Width))
			{
				birds.RemoveAt(i);
			}
		}

		if (frame % spawnDelay is 0)
		{
			if (rng.Next(50) > 25)
			{
				birds.Add(new Bird(Sprites.ScreenWidth, rng.Next(1, grassLevel - Sprites.Bird.Height), -1));
			}
			else
			{
				birds.Add(new Bird(-Sprites.Bird.Width, rng.Next(1, grassLevel - Sprites.Bird.Height), 1));
			}
			if (spawnDelay > 60)
			{
				spawnDelay--;
			}
		}

		if (ammoCount > 5)
		{
			ammoCount = 5;
		}

		DrawToScreenWithColour(crosshair.X - Sprites.Enviroment.CrosshairHeight / 2, crosshair.Y - Sprites.Enviroment.CrosshairWidth / 2, fireGun ? ConsoleColor.DarkYellow : ConsoleColor.Blue, Sprites.Enviroment.Crosshair);
		Thread.Sleep(TimeSpan.FromMilliseconds(gameDelay));
		frame++;

		gameOver = ammoCount is 0 && bullets.Count is 0;
	}

	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.SetCursorPosition(1, 1);
	Console.WriteLine("Game Over!     ");
	Console.SetCursorPosition(1, 2);
	Console.WriteLine($"Score: {score}");
	Console.SetCursorPosition(1, 3);
	Console.WriteLine("Press [ESC] to quit");

	while (Console.ReadKey(true).Key != ConsoleKey.Escape)
	{
		continue;
	}

	void DrawGUI()
	{
		int x = Sprites.ScreenWidth - 19;
		int y = grassLevel;

		string topFrame = '╔' + new string('═', 17) + '╣';
		string ammoFrame = string.Format("║  Ammo:{0,-10}", string.Concat(Enumerable.Repeat(" |", ammoCount))) + '║';
		string scoreFrame = string.Format("║ Score: {0,-9}", score) + '║';
		string bottomFrame = '╩' + new string('═', 17) + '╝';

		Console.SetCursorPosition(x, y);
		Console.Write(topFrame);
		Console.SetCursorPosition(x, ++y);
		Console.Write(ammoFrame);
		Console.SetCursorPosition(x, ++y);
		Console.Write(scoreFrame);
		Console.SetCursorPosition(x, ++y);
		Console.Write(bottomFrame);
	}
	void DrawLine(Point start, Point end)
	{
		/// Bresenhams line algorithm
		int x = start.X;
		int y = start.Y;
		int dx = Math.Abs(start.X - end.X);
		int dy = -Math.Abs(start.Y - end.Y);
		int sx = start.X < end.X ? 1 : -1;
		int sy = start.Y < end.Y ? 1 : -1;
		int error = dx + dy;
		while (true)
		{
			WriteToBuffer(x, y, '▓'); // ░▒▓█

			if (x == end.X && y == end.Y)
			{
				return;
			}

			float error2 = error * 2;
			if (error2 >= dy)
			{
				if (x == end.X)
				{
					break;
				}

				error += dy;
				x += sx;
			}
			if (error2 <= dx)
			{
				if (y == end.Y)
				{
					break;
				}

				error += dx;
				y += sy;
			}
		}
	}
	void DrawToScreen(char[,] array)
	{
		for (int y = 0; y < Sprites.ScreenHeight; y++)
		{
			for (int x = 0; x < Sprites.ScreenWidth; x++)
			{
				if (array[x, y] is NULL_CHAR)
				{
					screenGraphic.Append(' ');
				}
				else
				{
					screenGraphic.Append(array[x, y]);
				}
			}
		}
		Console.SetCursorPosition(0, 0);
		Console.Write(screenGraphic);

		screenBuffer = new char[Sprites.ScreenWidth, Sprites.ScreenHeight]; //Array.Clear(screenBuffer, 0, screenBuffer.Length);
		screenGraphic.Clear();
	}
	void WriteToBuffer(int xPos, int yPos, params char[] characters)
	{
		int x = xPos;
		int y = yPos;
		for (int i = 0; i < characters.Length; i++)
		{
			if (characters[i] == Sprites.NEWLINE_CHAR)
			{
				y++;
				x = xPos;
				continue;
			}
			if (char.IsWhiteSpace(characters[i]) && screenBuffer[x, y] != NULL_CHAR && !char.IsWhiteSpace(screenBuffer[x, y]))
			{
				x++;
				continue;
			}

			screenBuffer[x, y] = characters[i];
			x++;
		}
	}
	void DrawToScreenWithColour(int xPos, int yPos, ConsoleColor colour, params char[] characters)
	{
		int x = xPos;
		int y = yPos;
		Console.ForegroundColor = colour;

		for (int i = 0; i < characters.Length; i++)
		{
			if (characters[i] == Sprites.NEWLINE_CHAR)
			{
				y++;
				x = xPos;
				continue;
			}

			if (char.IsWhiteSpace(characters[i]))
			{
				x++;
				continue;
			}

			if (x >= 1 && x < Sprites.ScreenWidth - 1 &&
				y >= 1 && y < Sprites.ScreenHeight - 1)
			{
				if (characters[i] is EMPTY_CHAR)
				{
					Console.SetCursorPosition(x, y);
					Console.Write(' ');
				}
				else
				{
					Console.SetCursorPosition(x, y);
					Console.Write(characters[i]);
				}
			}

			x++;
		}

		Console.ForegroundColor = ConsoleColor.White;
	}
}
finally
{
	Console.CursorVisible = true;
	Console.ResetColor();
	Console.Clear();
}
struct Point
{
	public int X;
	public int Y;
	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}
	public static Point operator +(Point a, Point b)
		=> new Point(a.X + b.X, a.Y + b.Y);
}
class Bird
{
	public int X;
	public int Y;
	public int Frame = 0;
	public int Direction = 0;
	public bool IsDead = false;
	public Bird(int x, int y, int direction)
	{
		X = x;
		Y = y;
		Direction = direction;
	}
	public void IncrementFrame()
	{
		if (IsDead)
		{
			Frame = 4;
		}
		else
		{
			Frame++;
			Frame %= 4;
		}
	}
	public bool Contains(int x, int y)
	{
		return
			(x >= X) &&
			(y >= Y) &&
			(y < Y + Sprites.Bird.Height) &&
			(x < X + Sprites.Bird.Width);
	}
}
class Bullet
{
	public bool OutOfBounds = false;
	public double[] X = new double[2];
	public double[] Y = new double[2];

	private double XOffset;
	private double YOffset;
	public Bullet(Point position, double angle)
	{
		for (int i = 0; i < 2; i++)
		{
			X[i] = position.X;
			Y[i] = position.Y;
		}

		XOffset = -Math.Cos(angle);
		YOffset = -Math.Sin(angle);
	}
	public void UpdatePosition()
	{
		X[1] = X[0];
		Y[1] = Y[0];

		X[0] += XOffset;
		Y[0] += YOffset;

		if (X[0] < 0 || X[0] >= Console.WindowWidth ||
			Y[0] < 0 || Y[0] >= Console.WindowHeight)
		{
			OutOfBounds = true;
		}
	}
}
static class Sprites
{
	public readonly static char NEWLINE_CHAR = '\n';
	public readonly static int ScreenWidth = Console.WindowWidth;
	public readonly static int ScreenHeight = Console.WindowHeight;
	public readonly static int SPRITE_MAXWIDTH = ScreenWidth - 2;
	public readonly static int SPRITE_MAXHEIGHT = ScreenHeight - 2;

	private static string middleBorder = "║" + new string(' ', SPRITE_MAXWIDTH) + "║" + NEWLINE_CHAR;
	public static char[] Border =
	  ("╔" + new string('═', SPRITE_MAXWIDTH) + "╗" + NEWLINE_CHAR +
		string.Concat(Enumerable.Repeat(middleBorder, SPRITE_MAXHEIGHT)) +
		"╚" + new string('═', SPRITE_MAXWIDTH) + "╝").ToCharArray();

	public static class Enviroment
	{
		#region Ascii Sprites
		public static char[] Grass =
		  ( new string('V', SPRITE_MAXWIDTH)  + NEWLINE_CHAR +
			new string('M', SPRITE_MAXWIDTH) + NEWLINE_CHAR +
			new string('V', SPRITE_MAXWIDTH)).ToCharArray();

		public static char[] Crosshair =
		  ( @"  │  " + NEWLINE_CHAR +
			@" ┌│┐ " + NEWLINE_CHAR +
			@"──O──" + NEWLINE_CHAR +
			@" └│┘ " + NEWLINE_CHAR +
			@"  │  ").ToCharArray();
		public static int CrosshairHeight = 5;
		public static int CrosshairWidth = 5;

		public static char[] Bush =
		  ( @"   (}{{}}}   " + NEWLINE_CHAR +
			@"  {}}{{}'}}  " + NEWLINE_CHAR +
			@"{{}}}{{}}}{}{" + NEWLINE_CHAR +
			@"){}(}'{}}}{}}" + NEWLINE_CHAR +
			@"){}(}{{}}}{})" + NEWLINE_CHAR +
			@" {}}}{{}}}{} ").ToCharArray();
		public static int BushHeight = 6;
		public static int BushWidth = 13;

		public static char[] Tree =
		  (@"     ####           " + NEWLINE_CHAR +
			@"    ######          " + NEWLINE_CHAR +
			@"    ######          " + NEWLINE_CHAR +
			@"     ####    ####   " + NEWLINE_CHAR +
			@"      ||    ######  " + NEWLINE_CHAR +
			@"       ||   /####   " + NEWLINE_CHAR +
			@"        ####/       " + NEWLINE_CHAR +
			@"       ######       " + NEWLINE_CHAR +
			@" ####   ####   #### " + NEWLINE_CHAR +
			@"###### ||||   ######" + NEWLINE_CHAR +
			@" ####  ||||   ######" + NEWLINE_CHAR +
			@"    \\ ||||  //#### " + NEWLINE_CHAR +
			@"     \\|||| //      " + NEWLINE_CHAR +
			@"      \||||//       " + NEWLINE_CHAR +
			@"       ||||/        " + NEWLINE_CHAR +
			@"       ||||         " + NEWLINE_CHAR +
			@"       ||||         " + NEWLINE_CHAR +
			@"       ||||         " + NEWLINE_CHAR +
			@"       ||||         " + NEWLINE_CHAR +
			@"       ||||         ").ToCharArray();
		public static int TreeHeight = 20;
		public static int TreeWidth = 20;
	}

	public static class Bird
	{
		public static char[][] LeftSprites =
		{ ( @"  _(nn)_  " + NEWLINE_CHAR +
			@"<(o----_)=" + NEWLINE_CHAR +
			@"   (UU)   ").ToCharArray(),

		  ( @"  ______  " + NEWLINE_CHAR +
			@"<(o(UU)_)=" + NEWLINE_CHAR +
			@"          ").ToCharArray(),

		  ( @"  _(nn)_  " + NEWLINE_CHAR +
			@"<(o----_)=" + NEWLINE_CHAR +
			@"   (UU)   ").ToCharArray(),

		  ( @"  ______  " + NEWLINE_CHAR +
			@"<(o(UU)_)=" + NEWLINE_CHAR +
			@"          ").ToCharArray(),

		  ( @"    _    " + NEWLINE_CHAR +
			@" _<(x)__ " + NEWLINE_CHAR +
			@"(--(-)--)" + NEWLINE_CHAR +
			@"(__(_)__)" + NEWLINE_CHAR +
			@"  _/ \_  " ).ToCharArray()
		};
		public static char[][] RightSprites =
		{ ( @"  _(nn)_  " + NEWLINE_CHAR +
			@"=(_----o)>" + NEWLINE_CHAR +
			@"   (UU)   ").ToCharArray(),

		  ( @"  ______  " + NEWLINE_CHAR +
			@"=(_(UU)o)>" + NEWLINE_CHAR +
			@"          ").ToCharArray(),

		  ( @"  _(nn)_  " + NEWLINE_CHAR +
			@"=(_----o)>" + NEWLINE_CHAR +
			@"   (UU)   ").ToCharArray(),

		  ( @"  ______  " + NEWLINE_CHAR +
			@"=(_(UU)o)>" + NEWLINE_CHAR +
			@"          ").ToCharArray(),

		  ( @"    _    " + NEWLINE_CHAR +
			@" __(x)>_ " + NEWLINE_CHAR +
			@"(--(-)--)" + NEWLINE_CHAR +
			@"(__(_)__)" + NEWLINE_CHAR +
			@"  _/ \_  " ).ToCharArray()
		};
		public static int Height = 3;
		public static int Width = 10;
		#endregion
	}
}