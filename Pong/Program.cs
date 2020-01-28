using System;
using System.Threading;

class Program
{
	public class Ball
	{
		public float X;
		public float Y;
		public float dX;
		public float dY;
	}

	static readonly int width = Console.WindowWidth;
	static readonly int height = Console.WindowHeight;

	//static readonly float multiplier = 1.25f;
	static readonly Random random = new Random();
	static readonly TimeSpan delay = TimeSpan.FromMilliseconds(70);
	static readonly int paddleSize = height / 4;

	static int scoreA = 0;
	static int scoreB = 0;
	static Ball ball;
	static int paddleA = height / 3;
	static int paddleB = height / 3;
	
	static void Main()
	{
		Console.WriteLine("This game is still in development...");
		Console.WriteLine("It is playable at the moment but has issues.");
		Console.WriteLine("Press Enter To Continue...");
		Console.ReadLine();

		Console.CursorVisible = false;
		while (scoreA < 3 && scoreB < 3)
		{
			ball = CreateNewBall();
			while (true)
			{
				Console.Clear();

				#region Update Ball

				var (X2, Y2) = (ball.X + ball.dX, ball.Y + ball.dY);
				// Collisions With Up/Down Walls
				if (Y2 < 0 || Y2 > height)
				{
					ball.dY = -ball.dY;
					Y2 = ball.Y + ball.dY;
				}
				// Collisions With Paddles
				if (((int)X2 == 2 && (int)Y2 >= paddleA && (int)Y2 <= paddleA + paddleSize) ||
					((int)X2 == width - 2 && (int)Y2 >= paddleB && (int)Y2 <= paddleB + paddleSize))
				{
					ball.dX = -ball.dX;
					//ball.dX *= multiplier;
					//ball.dY *= multiplier;
				}
				// Collisions With Left/Right Walls
				if (X2 < 0)
				{
					scoreB++;
					break;
				}
				if (X2 > width)
				{
					scoreA++;
					break;
				}
				// Updading Ball Position
				ball.X += ball.dX;
				ball.Y += ball.dY;
				Console.SetCursorPosition((int)ball.X, (int)ball.Y);
				Console.Write('O');

				#endregion

				#region Update Player Paddle

				if (Console.KeyAvailable)
				{
					switch (Console.ReadKey(true).Key)
					{
						case ConsoleKey.UpArrow: paddleA = Math.Max(paddleA - 1, 0); break;
						case ConsoleKey.DownArrow: paddleA = Math.Min(paddleA + 1, height - paddleSize); break;
						case ConsoleKey.Escape:
							Console.Clear();
							Console.Write("Pong was closed.");
							return;
					}
				}
				while (Console.KeyAvailable)
				{
					Console.ReadKey(true);
				}

				#endregion

				#region Update Computer Paddle

				if (random.Next(3) == 0)
				{
					if (ball.Y < paddleB + (paddleSize / 2))
					{
						paddleB = Math.Max(paddleB - 1, 0);
					}
					else if (ball.Y > paddleB + (paddleSize / 2))
					{
						paddleB = Math.Min(paddleB + 1, height - paddleSize);
					}
				}
				else if (random.Next(10) == 0)
				{
					if (ball.Y < paddleB + (paddleSize / 2))
					{
						paddleB = Math.Min(paddleB + 1, height - paddleSize);
					}
					else if (ball.Y > paddleB + (paddleSize / 2))
					{
						paddleB = Math.Max(paddleB - 1, 0);
					}
				}

				#endregion

				#region Render Paddles

				for (int i = 0; i < paddleSize; i++)
				{
					Console.SetCursorPosition(2, paddleA + i);
					Console.Write('█');
					Console.SetCursorPosition(width - 2, paddleB + i);
					Console.Write('█');
				}

				#endregion

				Thread.Sleep(delay);
			}
		}
		Console.Clear();
		if (scoreA > scoreB)
		{
			Console.Write("You win.");
		}
		if (scoreA > scoreB)
		{
			Console.Write("You lose.");
		}
	}

	static Ball CreateNewBall()
	{
		float randomFloat = (float)random.NextDouble() * 0.9f;
		float dx = Math.Max(randomFloat, 1f - randomFloat);
		float dy = 1f - dx;
		float x = width / 2;
		float y = height / 2;
		if (random.Next(2) == 0)
		{
			dx = -dx;
		}
		if (random.Next(2) == 0)
		{
			dy = -dy;
		}
		return ball = new Ball
		{
			X = x,
			Y = y,
			dX = dx,
			dY = dy,
		};
	}
}
