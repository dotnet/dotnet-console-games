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
					"█▄▄▀"
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
			_ => throw new NotImplementedException(),
		};
}