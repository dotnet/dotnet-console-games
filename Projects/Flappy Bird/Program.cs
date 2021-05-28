using System;
using System.Collections.Generic;
using System.Threading;

const float Gravity = .5f;
const int PipeWidth = 8;
const int PipeGapHeight = 6;
const int SpaceBetweenPipes = 45;
const string BirdUp = @"~(v')>";
const string BirdDown = @"~(^')>";

int OriginalWidth = Console.WindowWidth;
int OriginalHeight = Console.WindowHeight;

TimeSpan Sleep = TimeSpan.FromMilliseconds(90);
Random Random = new();
List<(int X, int GapY)> Pipes = new();

int Width;
int Height;
float BirdX;
float BirdY;
float BirdDY;
int Frame;
int PipeFrame;

try
{
PlayAgain:
	Console.Clear();
	Pipes.Clear();
	if (OperatingSystem.IsWindows())
	{
		Width = Console.WindowWidth = 120;
		Height = Console.WindowHeight = 30;
	}
	else
	{
		Width = Console.WindowWidth;
		Height = Console.WindowHeight;
	}
	BirdX = Width / 6;
	BirdY = Height / 2;
	BirdDY = 0;
	Frame = 0;
	PipeFrame = SpaceBetweenPipes;
	Console.CursorVisible = false;
	// Starting Input
	RenderBird();
	Console.SetCursorPosition((int)BirdX - 10, (int)BirdY + 1);
	Console.Write("Press Space To Flap");
StartingInput:
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.Spacebar:
			BirdDY = -2;
			break;
		case ConsoleKey.Escape:
			Console.Clear();
			Console.Write("Flappy Bird was closed.");
			return;
		default:
			goto StartingInput;
	}
	Console.SetCursorPosition((int)BirdX - 10, (int)BirdY + 1);
	Console.Write("                   ");
	// Game Loop
	while (true)
	{
		// Check For Game Over
		if (Console.WindowHeight != Height || Console.WindowWidth != Width)
		{
			Console.Clear();
			Console.Write("You resized the console. Flappy Bird was closed.");
			return;
		}
		if (Frame == int.MaxValue)
		{
			Console.SetCursorPosition(0, Height - 1);
			Console.Write("You win! Score: " + Frame + ".");
			break;
		}
		if (!(BirdY < Height - 1 && BirdY > 0) || IsBirdCollidingWithPipe())
		{
			Console.SetCursorPosition(0, Height - 1);
			Console.Write("Game Over. Score: " + Frame + ".");
			Console.Write(" Play Again [enter], or quit [escape]?");
		GetPlayAgainInput:
			ConsoleKey key = Console.ReadKey(true).Key;
			if (key is ConsoleKey.Enter)
			{
				goto PlayAgain;
			}
			else if (!(key is ConsoleKey.Escape))
			{
				goto GetPlayAgainInput;
			}
			Console.Clear();
			break;
		}
		// Updates
		{
			// Pipes
			{
				// Erase (previous frame)
				foreach (var (X, GapY) in Pipes)
				{
					int x = X + PipeWidth / 2;
					if (x >= 0 && x < Width)
					{
						for (int y = 0; y < Height; y++)
						{
							Console.SetCursorPosition(x, y);
							Console.Write(' ');
						}
					}
				}
				// Update
				for (int i = 0; i < Pipes.Count; i++)
				{
					Pipes[i] = (Pipes[i].X - 1, Pipes[i].GapY);
				}
				if (Pipes.Count > 0 && Pipes[0].X < -PipeWidth)
				{
					Pipes.RemoveAt(0);
				}
				if (PipeFrame >= SpaceBetweenPipes)
				{
					int gapY = Random.Next(0, Height - PipeGapHeight - 1 - 6) + 3;
					Pipes.Add((Width + PipeWidth / 2, gapY));
					PipeFrame = 0;
				}
				// Render (current frame)
				foreach (var (X, GapY) in Pipes)
				{
					int x = X - PipeWidth / 2;
					for (int y = 0; y < Height; y++)
					{
						if (x > 0 && x < Width - 1 && (y < GapY || y > GapY + PipeGapHeight))
						{
							Console.SetCursorPosition(x, y);
							Console.Write('█');
						}
					}
				}
				RenderBird();
				PipeFrame++;
			}
			// Bird
			{
				// Erase (previous frame)
				{
					bool verticalVelocity = BirdDY < 0;
					Console.SetCursorPosition((int)(BirdX) - 3, (int)BirdY);
					Console.Write("      ");
				}
				// Update
				while (Console.KeyAvailable)
				{
					switch (Console.ReadKey(true).Key)
					{
						case ConsoleKey.Spacebar:
							BirdDY = -2;
							break;
						case ConsoleKey.Escape:
							Console.Clear();
							Console.Write("Flappy Bird was closed.");
							return;
					}
				}
				BirdY += BirdDY;
				BirdDY += Gravity;
				// Render (current frame)
				RenderBird();
			}
			Frame++;
		}
		Thread.Sleep(Sleep);
	}
}
finally
{
	Console.CursorVisible = true;
	Console.WindowWidth = OriginalWidth;
	Console.WindowHeight = OriginalHeight;
}

bool IsBirdCollidingWithPipe()
{
	foreach (var (X, GapY) in Pipes)
	{
		if (Math.Abs(X - BirdX) < PipeWidth / 2 + 3 && ((int)BirdY < GapY || (int)BirdY > GapY + PipeGapHeight))
		{
			return true;
		}
	}
	return false;
}

void RenderBird()
{
	if ((int)BirdY < Height - 1 && (int)BirdY >= 0)
	{
		bool verticalVelocity = BirdDY < 0;
		Console.SetCursorPosition((int)BirdX - 3, (int)BirdY);
		Console.Write(verticalVelocity ? BirdUp : BirdDown);
	}
}
