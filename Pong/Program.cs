using System;

namespace Pong
{
	class Program
	{
		static void Main()
		{
			int width = Console.WindowWidth;
			int height = Console.WindowHeight;

			TimeSpan delay = TimeSpan.FromMilliseconds(70);
			bool gameOver = false;
			int ScoreA = 0;
			int ScoreB = 0;
			Random random = new Random();
			int paddleSize = Math.Min(height / 3, 5);

			while (!gameOver)
			{
				var (X, Y) = (0f, 0f);
				var (dX, dY) = (random.Next(0, 2) == 0 ? .1f : -.1f, (float)random.NextDouble());
				var paddleA = ((1f, 0f), (1f, (float)paddleSize));
				var paddleB = ((width - 2f, 0f), (width - 2f, (float)paddleSize));

				while (X > 1 && X < width - 2)
				{
					var (X2, Y2) = (X + dX, Y + dY);
					if (Intersect(((X, Y), (X2, Y2)), paddleA) ||
						Intersect(((X, Y), (X2, Y2)), paddleA))
					{
						dX = -dX;
						X2 = X + dX;
					}



					X += dX;
					Y += dY;
					if (Console.KeyAvailable)
					{
						switch (Console.ReadKey(true).Key)
						{
							case ConsoleKey.UpArrow:
								paddleTopA = Math.Min(height - paddleSize, paddleTopA - 1);
								break;
							case ConsoleKey.DownArrow:
								paddleTopA = Math.Min(height - paddleSize, paddleTopA + 1);
								break;
						}
					}
				}
			}
		}

		public static bool Intersect(
			((float X, float Y) A, (float X, float Y) B) A,
			((float X, float Y) A, (float X, float Y) B) B)
		{
			var intersection = Intersection(A, B);
			if (!intersection.HasValue)
			{
				return false;
			}
			else
			{
				var (X, Y) = intersection.Value;
				return X >= A.A.X && X <= A.B.X;
			}
		}

		public static (float X, float Y)? Intersection(
			((float X, float Y) A, (float X, float Y) B) A,
			((float X, float Y) A, (float X, float Y) B) B)
		{
			var a = A.B.Y - A.A.Y;
			var b = A.A.X - A.B.X;
			var c = a * (A.A.X) + b * (A.A.Y);
			var d = B.B.Y - B.A.Y;
			var e = B.A.X - B.B.X;
			var f = d * (B.A.X) + e * (B.A.Y);
			var g = a * e - d * b;
			return g == 0
				? ((float X, float Y)?)null
				: ((e * c - b * f) / g, (a * f - d * c) / g);
		}
	}
}
