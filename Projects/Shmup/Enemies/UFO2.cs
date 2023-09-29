using System;
using System.Linq;

namespace Shmup.Enemies;

internal class UFO2 : IEnemy
{
	public float X;
	public float Y;
	public int UpdatesSinceTeleport;
	public int TeleportFrequency = 360;

	private static string[] Sprite =
	{
		@"     _!_     ",
		@"    /_O_\    ",
		@"-==<_‗_‗_>==-",
	};

	internal static int XMax = Sprite.Max(s => s.Length);
	internal static int YMax = Sprite.Length;

	public UFO2()
	{
		X = Random.Shared.Next(Program.gameWidth - XMax) + XMax / 2;
		Y = Random.Shared.Next(Program.gameHeight - YMax) + YMax / 2;
	}

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
		UpdatesSinceTeleport++;
		if (UpdatesSinceTeleport > TeleportFrequency)
		{
			X = Random.Shared.Next(Program.gameWidth - XMax) + XMax / 2;
			Y = Random.Shared.Next(Program.gameHeight - YMax) + YMax / 2;
			UpdatesSinceTeleport = 0;
		}
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
		return !
			(X > 0 &&
			X < Program.gameWidth &&
			Y > 0 &&
			Y < Program.gameHeight);
	}
}
