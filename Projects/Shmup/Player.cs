using System;

namespace Shmup;

internal class Player
{
	[Flags]
	internal enum States
	{
		None  = 0,
		Up    = 1 << 0,
		Down  = 1 << 1,
		Left  = 1 << 2,
		Right = 1 << 3,
	}

	public float X;
	public float Y;
	public States State;

	static string[] neutral =
	{
		@"   ╱‾╲   ",
		@"  ╱╱‾╲╲  ",
		@" ╱'╲O╱'╲ ",
		@"╱ / ‾ \ ╲",
		@"╲_╱───╲_╱",
	};

	const int neutralOffsetX = -4;
	const int neutralOffsetY = -2;

	public void Render()
	{
		for (int y = 0; y < neutral.Length; y++)
		{
			int yo = (int)Y + y + neutralOffsetY;
			int yi = neutral.Length - y - 1;
			if (yo >= 0 && yo < Program.frameBuffer.GetLength(1))
			{
				for (int x = 0; x < neutral[y].Length; x++)
				{
					int xo = (int)X + x + neutralOffsetX;
					if (xo >= 0 && xo < Program.frameBuffer.GetLength(0))
					{
						Program.frameBuffer[xo, yo] = neutral[yi][x];
					}
				}
			}
		}
	}
}
