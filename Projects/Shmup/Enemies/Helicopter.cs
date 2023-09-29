using System;
using System.Linq;

namespace Shmup.Enemies;

internal class Helicopter : IEnemy
{
	public float X;
	public float Y;
	public float XVelocity;
	public float YVelocity;
	private int Frame;
	private string[] Sprite = Random.Shared.Next(2) is 0 ? spriteA : spriteB;

	static string[] spriteA =
	{
		@"  ~~~~~+~~~~~",
		@"'\===<[_]L)  ",
		@"     -'-`-   ",
	};

	static string[] spriteB =
	{
		@"  -----+-----",
		@"*\===<[_]L)  ",
		@"     -'-`-   ",
	};

	internal static int XMax = Math.Max(spriteA.Max(s => s.Length), spriteB.Max(s => s.Length));
	internal static int YMax = Math.Max(spriteA.Length, spriteB.Length);

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
		Frame++;
		if (Frame > 10)
		{
			Sprite = Sprite == spriteB ? spriteA : spriteB;
			Frame = 0;
		}
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
