using System;
using System.Threading.Tasks;
using System.Linq;

namespace Website.Games.Hangman;

public class Hangman
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		Random Random = new();

		string[] Renders =
		{
			#region Frames
			// 0
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"          ║   " + '\n' +
			@"          ║   " + '\n' +
			@"     ███  ║   " + '\n' +
			@"    ══════╩═══",
			// 1
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"          ║   " + '\n' +
			@"     ███  ║   " + '\n' +
			@"    ══════╩═══",
			// 2
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"     ███  ║   " + '\n' +
			@"    ══════╩═══",
			// 3
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      |\  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"     ███  ║   " + '\n' +
			@"    ══════╩═══",
			// 4
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"     ███  ║   " + '\n' +
			@"    ══════╩═══",
			// 5
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"       \  ║   " + '\n' +
			@"     ███  ║   " + '\n' +
			@"    ══════╩═══",
			// 6
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"     / \  ║   " + '\n' +
			@"     ███  ║   " + '\n' +
			@"    ══════╩═══",
			#endregion
		};

		string[] DeathAnimation =
		{
			#region Frames
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"     / \  ║   " + '\n' +
			@"     ███  ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"     / \  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o>  ║   " + '\n' +
			@"     /|   ║   " + '\n' +
			@"      >\  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"     / \  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"     <o   ║   " + '\n' +
			@"      |\  ║   " + '\n' +
			@"     /<   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"     / \  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o>  ║   " + '\n' +
			@"     /|   ║   " + '\n' +
			@"      >\  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o>  ║   " + '\n' +
			@"     /|   ║   " + '\n' +
			@"      >\  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"     / \  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"     <o   ║   " + '\n' +
			@"      |\  ║   " + '\n' +
			@"     /<   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"     <o   ║   " + '\n' +
			@"      |\  ║   " + '\n' +
			@"     /<   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"     <o   ║   " + '\n' +
			@"      |\  ║   " + '\n' +
			@"     /<   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"     /|\  ║   " + '\n' +
			@"     / \  ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      o   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      |   ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      /   ║   " + '\n' +
			@"      \   ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    |__   ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      .   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    \__   ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@"   ____   ║   " + '\n' +
			@"    ══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@"      .   ║   " + '\n' +
			@"    __    ║   " + '\n' +
			@"   /══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      .   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"    _ '   ║   " + '\n' +
			@"  _/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@"      _   ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@"      .   ║   " + '\n' +
			@"          ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      .   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@"      _   ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@"      .   ║   " + '\n' +
			@"          ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"      .   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@"      _   ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      .   ║   " + '\n' +
			@"          ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      '   ║   " + '\n' +
			@" __/══════╩═══",
			//
			@"      ╔═══╗   " + '\n' +
			@"      |   ║   " + '\n' +
			@"      O   ║   " + '\n' +
			@"          ║   " + '\n' +
			@"          ║   " + '\n' +
			@"      _   ║   " + '\n' +
			@" __/══════╩═══",
			#endregion
		};

		if (Resources.Words is null || Resources.Words.Length is 0)
		{
			await Console.WriteLine("Error: Missing \"Words.txt\" embedded resource.");
			await Console.WriteLine("Press enter to continue...");
			await Console.ReadLine();
			return;
		}
		PlayAgain:
		Console.CursorVisible = false;
		await Console.Clear();
		await Console.WriteLine();
		await Console.WriteLine("    Hangman");
		await Console.WriteLine();
		int incorrectGuesses = 0;
		string randomWord = GetRandomWord().ToLower();
		char[] revealedChars = new string('_', randomWord.Length).ToCharArray();
		while (incorrectGuesses < Renders.Length && revealedChars.Contains('_'))
		{
			await RenderGameState();
		GetInput:
			ConsoleKey key = (await Console.ReadKey(true)).Key;
			if (key is ConsoleKey.Escape)
			{
				await Console.Clear();
				await Console.Write("Hangman was closed.");
				await Console.Refresh();
				return;
			}
			if (key < ConsoleKey.A || key > ConsoleKey.Z)
			{
				goto GetInput;
			}
			char guess = (char)(key - ConsoleKey.A + 'a');
			bool correctGuess = false;
			for (int i = 0; i < revealedChars.Length; i++)
			{
				if (revealedChars[i] is '_' && randomWord[i] == guess)
				{
					revealedChars[i] = guess;
					correctGuess = true;
				}
			}
			if (!correctGuess)
			{
				incorrectGuesses++;
			}
		}

		if (incorrectGuesses >= Renders.Length)
		{
			for (int i = 0; i < DeathAnimation.Length; i++)
			{
				await Console.SetCursorPosition(4, 3);
				await Render(DeathAnimation[i]);
				await Console.RefreshAndDelay(TimeSpan.FromMilliseconds(150));
			}
			await Console.WriteLine();
			await Console.WriteLine();
			await Console.WriteLine();
			await Console.WriteLine();
			await Console.WriteLine("    Answer: " + randomWord);
			await Console.WriteLine("    You lose...");
		}
		else
		{
			await RenderGameState();
			await Console.WriteLine();
			await Console.WriteLine("    You win!");
		}
		await Console.WriteLine();
		await Console.WriteLine("    Play Again [enter], or quit [escape]?");
		GetPlayAgainInput:
		switch ((await Console.ReadKey(true)).Key)
		{
			case ConsoleKey.Enter: goto PlayAgain;
			case ConsoleKey.Escape: break;
			default: goto GetPlayAgainInput;
		}
		await Console.Clear();

		string GetRandomWord() => Choose(Random, Resources.Words!);

		T Choose<T>(Random random, params T[] values) => values[random.Next(values.Length)];

		async Task RenderGameState()
		{
			await Console.SetCursorPosition(4, 3);
			Console.CursorLeft = 4;
			await Render(Renders[incorrectGuesses]);
			await Console.WriteLine();
			await Console.WriteLine();
			await Console.Write("    Guess: ");
			foreach (char c in revealedChars)
			{
				await Console.Write(c + " ");
			}
		}

		async Task Render(string @string)
		{
			int x = Console.CursorLeft;
			int y = Console.CursorTop;
			foreach (char c in @string)
			{
				if (c is '\n')
				{
					await Console.WriteLine();
					await Console.SetCursorPosition(x, ++y);
				}
				else
				{
					await Console.Write(c);
				}
			}
		}
	}
}
