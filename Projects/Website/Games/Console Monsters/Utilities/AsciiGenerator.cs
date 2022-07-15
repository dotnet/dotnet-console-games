using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Items;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Characters;
using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Screens.Menus;
using Website.Games.Console_Monsters.Enums;
using Website.Games.Console_Monsters.Utilities;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters.Utilities;

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
			_ => throw new NotImplementedException(),
		};
}