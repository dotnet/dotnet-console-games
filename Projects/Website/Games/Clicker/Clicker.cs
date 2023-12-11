﻿using System;
using System.Globalization;
using System.Numerics;
using System.Threading.Tasks;

namespace Website.Games.Clicker;

public class Clicker
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		char[] keys =
		[
			'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P',
			'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L',
			'Z', 'X', 'C', 'V', 'B', 'N', 'M',
		];

	MainMenu:
		await Console.Clear();
		await Console.WriteLine(
			$"""
			 Clicker

			 In this game you will continually click
			 keys on your keyboard to increase your
			 score, but you cannot press the same key
			 twice in a row. :P You will start with
			 the [{keys[0]}] & [{keys[1]}] keys but unlock additional
			 keys as you click.

			 NOTE FROM DEVELOPER: Do not play this game.
			 Clicker games are nothing but a waste of
			 your time. I only made this game as an
			 educational example.

			 ESC: close game
			 ENTER: continue
			""");
	MainMenuInput:
		Console.CursorVisible = false;
		switch ((await Console.ReadKey(true)).Key)
		{
			case ConsoleKey.Enter: break;
			case ConsoleKey.Escape:
				await Console.Clear();
				await Console.WriteLine("Clicker was closed.");
				await Console.Refresh();
				return;
			default: goto MainMenuInput;
		}

		DateTime start = DateTime.Now;
		BigInteger clicks = 0;
		ConsoleKey previous = default;
		await Console.Clear();
		while (true)
		{
			string clicksString = clicks.ToString(CultureInfo.InvariantCulture);
			int keyCount = clicksString.Length + 1;

			if (keyCount > keys.Length)
			{
				TimeSpan duration = DateTime.Now - start;
				await Console.Clear();
				await Console.WriteLine(
				$"""
				 Clicker

				 You Win!

				 Your time: {duration}

				 ESC: return to main menu
				""");
			GameOverInput:
				Console.CursorVisible = false;
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Escape: goto MainMenu;
					default: goto GameOverInput;
				}
			}

			await Console.SetCursorPosition(0, 0);
			await Console.WriteLine(
				$"""
				 Clicker

				 Clicks: {clicksString}

				 Keys: {string.Join(" ", keys[0..keyCount])}

				 ESC: return to main menu
				""");
		ClickerInput:
			Console.CursorVisible = false;
			ConsoleKey key = (await Console.ReadKey(true)).Key;
			switch (key)
			{
				case >= ConsoleKey.A and <= ConsoleKey.Z:
					int index = Array.IndexOf(keys, (char)key);
					if (index < keyCount && key != previous)
					{
						previous = key;
						clicks += index > 1 ? BigInteger.Pow(10, index - 1) / (index - 1) : 1;
					}
					break;
				case ConsoleKey.Escape: goto MainMenu;
				default: goto ClickerInput;
			}
		}
	}
}
