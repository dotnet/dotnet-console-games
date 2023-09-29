using System;
using System.Linq;

namespace Shmup.Enemies;

internal class Tank : IEnemy
{
	public float X;
	public float Y;
	public float XVelocity;
	public float YVelocity;
	private string[] Sprite;

	static string[] spriteDown =
	{
		@" ___ ",
		@"|_O_|",
		@"[ooo]",
	};

	static string[] spriteUp =
	{
		@" _^_ ",
		@"|___|",
		@"[ooo]",
	};

	static string[] spriteLeft =
	{
		@"  __ ",
		@"=|__|",
		@"[ooo]",
	};

	static string[] spriteRight =
	{
		@" __  ",
		@"|__|=",
		@"[ooo]",
	};

	internal static int XMax = new[] { spriteDown.Max(s => s.Length), spriteUp.Max(s => s.Length), spriteLeft.Max(s => s.Length), spriteRight.Max(s => s.Length), }.Max();
	internal static int YMax = new[] { spriteDown.Length, spriteUp.Length, spriteLeft.Length, spriteRight.Length, }.Max();

	public void Render()
	{
		for (int y = 0; y < Sprite.Length; y++)
		{
			int yo = (int)Y + y;
			int yi = Sprite.Length - y - 1;
			if (yo >= 0 && yo < Program.frameBuffer.GetLength(1))
			{
				for (int x = 0; x < Sprite[y].Length; x++)
				{
					int xo = (int)X + x;
					if (xo >= 0 && xo < Program.frameBuffer.GetLength(0))
					{
						if (Sprite[yi][x] is not ' ')
						{
							Program.frameBuffer[xo, yo] = Sprite[yi][x];
						}
					}
				}
			}
		}
	}

	public void Update()
	{
		int xDifToPlayer = (int)Program.player.X - (int)X;
		int yDifToPlayer = (int)Program.player.Y - (int)Y;

		Sprite = Math.Abs(xDifToPlayer) > Math.Abs(yDifToPlayer)
			? xDifToPlayer > 0 ? spriteRight : spriteLeft
			: yDifToPlayer > 0 ? spriteUp : spriteDown;

		X += XVelocity;
		Y += YVelocity;
	}

	public bool CollidingWith(int x, int y)
	{
		int xo = x - (int)X;
		int yo = y - (int)Y;
		return
			yo >= 0 && yo < Sprite.Length &&
			xo >= 0 && xo < Sprite[yo].Length &&
			Sprite[yo][xo] is not ' ';
	}

	public bool IsOutOfBounds()
	{
		return
			XVelocity <= 0 && X < -XMax ||
			YVelocity <= 0 && Y < -YMax ||
			XVelocity >= 0 && X > Program.gameWidth + XMax ||
			YVelocity >= 0 && Y > Program.gameHeight + YMax;
	}
}
