namespace Console_Monsters.Utilities;

public static class AsciiGenerator
{
	/// <summary>Generates medium sized uppercase ascii text art from a string.</summary>
	public static string[] ToAscii(string @string)
	{
		StringBuilder a = new();
		StringBuilder b = new();
		StringBuilder c = new();

		bool first = true;
		foreach (char @char in @string)
		{
			string[] char_ascii = ToAscii(@char);
			if (!first)
			{
				a.Append(' ');
				b.Append(' ');
				c.Append(' ');
			}
			a.Append(char_ascii[0]);
			b.Append(char_ascii[1]);
			c.Append(char_ascii[2]);
			first = false;
		}

		return new[] { a.ToString(), b.ToString(), c.ToString() };
	}

	/// <summary>Generates medium sized uppercase ascii text art from a char.</summary>
	public static string[] ToAscii(char @char) => char.ToLower(@char) switch
		{
			' ' => new[]
				{
					"    ",
					"    ",
					"    "
				},
			'a' => new[]
				{
					" ▄▄ ",
					"█▄▄█",
					"█  █"
				},
			'b' => new[]
				{
					"▄▄▄ ",
					"█▄▄█",
					"█▄▄█"
				},
			'c' => new[]
				{
					" ▄▄▄",
					"█   ",
					"▀▄▄▄"
				},
			'd' => new[]
				{
					"▄▄▄ ",
					"█  █",
					"█▄▄▀"
				},
			'e' => new[]
				{
					"▄▄▄▄",
					"█▄▄ ",
					"█▄▄▄"
				},
			'f' => new[]
				{
					"▄▄▄▄",
					"█▄▄ ",
					"█   "
				},
			'g' => new[]
				{
					" ▄▄▄ ",
					"█  ▄▄",
					"▀▄▄▄▀"
				},
			'h' => new[]
				{
					"▄  ▄",
					"█▄▄█",
					"█  █"
				},
			'i' => new[]
				{
					"▄",
					"█",
					"█"
				},
			'j' => new[]
				{
					"   ▄",
					"   █",
					"▀▄▄▀"
				},
			'k' => new[]
				{
					"▄  ▄",
					"█▄▀ ",
					"█ ▀▄"
				},
			'l' => new[]
				{
					"▄  ",
					"█  ",
					"█▄▄"
				},
			'm' => new[]
				{
					"▄   ▄",
					"█▀▄▀█",
					"█   █"
				},
			'n' => new[]
				{
					"▄   ▄",
					"█▀▄ █",
					"█  ▀█"
				},
			'o' => new[]
				{
					" ▄▄▄ ",
					"█   █",
					"▀▄▄▄▀"
				},
			'p' => new[]
				{
					"▄▄▄ ",
					"█▄▄▀",
					"█   "
				},
			'q' => new[]
				{
					" ▄▄▄  ",
					"█   █ ",
					"▀▄▄▄▀▄"
				},
			'r' => new[]
				{
					"▄▄▄ ",
					"█▄▄▀",
					"█  █"
				},
			's' => new[]
				{
					"▄▄▄▄",
					"█▄▄▄",
					"▄▄▄█"
				},
			't' => new[]
				{
					"▄▄▄▄▄",
					"  █  ",
					"  █  "
				},
			'u' => new[]
				{
					"▄   ▄",
					"█   █",
					"▀▄▄▄▀"
				},
			'v' => new[]
				{
					"▄   ▄",
					"█   █",
					" ▀▄▀ "
				},
			'w' => new[]
				{
					"▄   ▄",
					"█ ▄ █",
					"▀▄▀▄▀"
				},
			'x' => new[]
				{
					"▄   ▄",
					" ▀▄▀ ",
					"▄▀ ▀▄"
				},
			'y' => new[]
				{
					"▄   ▄",
					" ▀▄▀ ",
					"  █  "
				},
			'z' => new[]
				{
					"▄▄▄▄",
					" ▄▄▀",
					"█▄▄▄"
				},
			'0' => new[]
				{
					" ▄▄▄ ",
					"█▄▀ █",
					"▀▄▄▄▀"
				},
			'1' => new[]
				{
				   "▄▄",
					"█",
					"█"
				},
			'2' => new[]
				{
					"▄▄▄▄",
					"▄▄▄▀",
					"█▄▄▄"
				},
			':' => new[]
				{
					"▄",
					" ",
					"▄"
				},

			// ←   ↑   →   ↓ 
			'↑' => new[]
				{
					" ▄█▄ ",
					"▀ █ ▀",
					"  █  "
				},
			'↓' => new[]
				{
					"  █  ",
					"▄ █ ▄",
					" ▀█▀ "
				},
			'←' => new[]
				{
					"  ▄  ",
					"■█■■■",
					"  ▀  "
				},
			'→' => new[]
				{
					"  ▄  ",
					"■■■█■",
					"  ▀  "
				},
			// the '■' and '□' sprites must be the same size
			'■' => new[]
				{
					"╭───╮",
					"╞═●═╡",
					"╰───╯"
				},
			'□' => new[]
				{
					"     ",
					"     ",
					"     "
				},

			// the '●' and '○' sprites must be the same size
			'●' => new[]
				{
					"╔══╗",
					"║██║",
					"╚══╝"
				},
			'○' => new[]
				{
					"╔══╗",
					"║  ║",
					"╚══╝"
				},

			_ => throw new NotImplementedException(),
		};

	public static string[] Concat(params string[][] sprites)
	{
		StringBuilder a = new();
		StringBuilder b = new();
		StringBuilder c = new();

		foreach (string[] sprite in sprites)
		{
			int length = sprite.Max(l => l is null ? 0 : l.Length);
			for (int i = 0; i < length; i++)
			{
				a.Append(sprite is null || sprite.Length < 1 || sprite[0] is null || sprite[0].Length <= i ? ' ' : sprite[0][i]);
				b.Append(sprite is null || sprite.Length < 2 || sprite[1] is null || sprite[1].Length <= i ? ' ' : sprite[1][i]);
				c.Append(sprite is null || sprite.Length < 3 || sprite[2] is null || sprite[2].Length <= i ? ' ' : sprite[2][i]);
			}
		}

		return new[] { a.ToString(), b.ToString(), c.ToString() };
	}
}