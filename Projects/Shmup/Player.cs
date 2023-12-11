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

	static readonly string[] Sprite =
	[
		@"   ╱‾╲   ",
		@"  ╱╱‾╲╲  ",
		@" ╱'╲O╱'╲ ",
		@"╱ / ‾ \ ╲",
		@"╲_╱───╲_╱",
	];

	static readonly string[] SpriteUp =
	[
		@"   ╱‾╲   ",
		@"  ╱╱‾╲╲  ",
		@" ╱'╲O╱'╲ ",
		@"╱ / ‾ \ ╲",
		@"╲_╱───╲_╱",
		@"/V\   /V\",
	];

	static readonly string[] SpriteDown =
	[
		@"   ╱‾╲   ",
		@"  ╱╱‾╲╲  ",
		@"-╱'╲O╱'╲-",
		@"╱-/ ‾ \-╲",
		@"╲_╱───╲_╱",
	];

	static readonly string[] SpriteLeft =
	[
		@"   ╱╲   ",
		@"  ╱‾╲╲  ",
		@" ╱╲O╱'╲ ",
		@"╱/ ‾ \ ╲",
		@"╲╱───╲_╱",
	];

	static readonly string[] SpriteRight =
	[
		@"   ╱╲   ",
		@"  ╱╱‾╲  ",
		@" ╱'╲O╱╲ ",
		@"╱ / ‾ \╲",
		@"╲_╱───╲╱",
	];

	static readonly string[] SpriteUpLeft =
	[
		@"   ╱╲   ",
		@"  ╱‾╲╲  ",
		@" ╱╲O╱'╲ ",
		@"╱/ ‾ \ ╲",
		@"╲╱───╲_╱",
		@"/\   /V\",
	];

	static readonly string[] SpriteUpRight =
	[
		@"   ╱╲   ",
		@"  ╱╱‾╲  ",
		@" ╱'╲O╱╲ ",
		@"╱ / ‾ \╲",
		@"╲_╱───╲╱",
		@"/V\   /\",
	];

	static readonly string[] SpriteDownLeft =
	[
		@"   ╱╲   ",
		@"  ╱‾╲╲  ",
		@"-╱╲O╱'╲-",
		@"-/ ‾ \-╲",
		@"╲╱───╲_╱",
	];

	static readonly string[] SpriteDownRight =
	[
		@"   ╱╲   ",
		@"  ╱╱‾╲  ",
		@"-╱'╲O╱╲-",
		@"╱-/ ‾ \-",
		@"╲_╱───╲╱",
	];

	public void Render()
	{
		var (sprite, offset) = GetSpriteAndOffset();
		for (int y = 0; y < sprite.Length; y++)
		{
			int yo = (int)Y + y + offset.Y;
			int yi = sprite.Length - y - 1;
			if (yo >= 0 && yo < Program.frameBuffer.GetLength(1))
			{
				for (int x = 0; x < sprite[y].Length; x++)
				{
					int xo = (int)X + x + offset.X;
					if (xo >= 0 && xo < Program.frameBuffer.GetLength(0))
					{
						Program.frameBuffer[xo, yo] = sprite[yi][x];
					}
				}
			}
		}
	}

	internal (string[] Sprite, (int X, int Y) offset) GetSpriteAndOffset()
	{
		return State switch
		{
			States.None                => (Sprite, (-4, -2)),
			States.Up                  => (SpriteUp, (-4, -3)),
			States.Down                => (SpriteDown, (-4, -2)),
			States.Left                => (SpriteLeft, (-3, -2)),
			States.Right               => (SpriteRight, (-4, -2)),
			States.Up | States.Left    => (SpriteUpLeft, (-3, -3)),
			States.Up | States.Right   => (SpriteUpRight, (-4, -3)),
			States.Down | States.Left  => (SpriteDownLeft, (-3, -2)),
			States.Down | States.Right => (SpriteDownRight, (-4, -2)),
			_ => throw new NotImplementedException(),
		};
	}
}
