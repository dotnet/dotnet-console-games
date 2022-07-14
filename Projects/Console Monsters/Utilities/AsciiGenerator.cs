namespace Console_Monsters.Utilities;

public static class AsciiGenerator
{
	/// <summary>Generates medium sized uppercase ascii text art from a string.</summary>
	public static string[] ToAscii(string @string)
	{
		StringBuilder top = new();
		StringBuilder middle = new();
		StringBuilder bottom = new();

		foreach (char c in @string)
		{
			string[] char_ascii = ToAscii(c);
			top.Append(char_ascii[0]);
			middle.Append(char_ascii[1]);
			bottom.Append(char_ascii[2]);
		}

		return new[] { top.ToString(), middle.ToString(), bottom.ToString() };
	}

	/// <summary>Generates medium sized uppercase ascii text art from a char.</summary>
	public static string[] ToAscii(char @char) => char.ToLower(@char) switch
		{
			' ' => new[]
				{
					" ",
					" ",
					" "
				},
			'|' => new[]
				{
					"█ ",
					"█ ",
					"█ "
				},
			'a' => new[]
				{
					" ▄▄  ",
					"█▄▄█ ",
					"█  █ "
				},
			'b' => new[]
				{
					"█▀▀▄ ",
					"█■■█ ",
					"█▄▄▀ "
				},
			'c' => new[]
				{
					" ▄▄▄ ",
					"█    ",
					"▀▄▄▄ "
				},
			'd' => new[]
				{
					"█▀▀▄ ",
					"█  █ ",
					"█▄▄▀ "
				},
			'e' => new[]
				{
					"▄▄▄▄ ",
					"█▄▄  ",
					"█▄▄▄ "
				},
			'f' => new[]
				{
					"▄▄▄▄ ",
					"█▄▄  ",
					"█    "
				},
			'g' => new[]
				{
					" ▄▄▄  ",
					"█  ▄▄ ",
					"▀▄▄▄▀ "
				},
			'h' => new[]
				{
					"▄   ▄ ",
					"█▄▄▄█ ",
					"█   █ "
				},
			'i' => new[]
				{
					"▄ ",
					"█ ",
					"█ "
				},
			'j' => new[]
				{
					"   ▄ ",
					"   █ ",
					"▀▄▄▀ "
				},
			'k' => new[]
				{
					"▄  ▄  ",
					"█■█   ",
					"█  ▀▄ "
				},
			'l' => new[]
				{
					"▄    ",
					"█    ",
					"█▄▄▄ "
				},
			'm' => new[]
				{
					"▄   ▄ ",
					"█▀▄▀█ ",
					"█   █ "
				},
			'n' => new[]
				{
					"▄   ▄ ",
					"█▀▄ █ ",
					"█  ▀█ "
				},
			'o' => new[]
				{
					" ▄▄▄  ",
					"█   █ ",
					"▀▄▄▄▀ "
				},
			'p' => new[]
				{
					"▄▄▄  ",
					"█▄▄▀ ",
					"█    "
				},
			'q' => new[]
				{
					" ▄▄▄▄    ",
					"█    █   ",
					"▀▄▄▄■▀■▄ "
				},
			'r' => new[]
				{
					"▄▄▄   ",
					"█▄▄▀  ",
					"█  ▀▄ "
				},
			's' => new[]
				{
					" ▄▄▄ ",
					"▀■■▄ ",
					"■■■▀ "
				},
			't' => new[]
				{
					"▄▄▄▄▄ ",
					"  █   ",
					"  █   "
				},
			'u' => new[]
				{
					"▄   ▄ ",
					"█   █ ",
					"▀▄▄▄▀ "
				},
			'v' => new[]
				{
					"▄     ▄ ",
					" █   █  ",
					"  ▀▄▀   "
				},
			'w' => new[]
				{
					" ▄       ▄ ",
					" █   ▄   █ ",
					"  ▀▄▀ ▀▄▀  "
				},
			'x' => new[]
				{
					"▄   ▄",
					" ▀▄▀ ",
					"▄▀ ▀▄"
				},
			'y' => new[]
				{
					"▄   ▄ ",
					" ▀▄▀  ",
					"  █   "
				},
			'z' => new[]
				{
					"▄▄▄▄▄",
					"  ▄▀",
					"▄█▄▄▄"
				},
			_ => throw new NotImplementedException(),
		};
}