using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Linq;

static class Program
{
	static readonly Random Random = new Random();
	static string[] Words;
	static int incorrectGuesses = 0;
	static string randomWord;
	static char[] revealedChars;

	static void Main()
	{
		#region Load Word Library

		const string wordsResource = "Hangman.Words.txt";
		Assembly assembly = Assembly.GetExecutingAssembly();
		using Stream stream = assembly.GetManifestResourceStream(wordsResource);
		if (stream is null)
		{
			Console.WriteLine("Error: Missing \"Words.txt\" embedded resource.");
			Console.WriteLine("Press enter to continue...");
			Console.ReadLine();
			return;
		}
		List<string> words = new List<string>();
		using StreamReader streamReader = new StreamReader(stream);
		while (!streamReader.EndOfStream)
		{
			string word = streamReader.ReadLine();
			words.Add(word);
		}
		Words = words.ToArray();

		#endregion

		PlayAgain:

		Console.CursorVisible = false;
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine("    Hangman");
		Console.WriteLine();

		incorrectGuesses = 0;
		randomWord = GetRandomWord().ToLower();
		revealedChars = new string('_', randomWord.Length).ToCharArray();

		while (incorrectGuesses < Ascii.Renders.Length && revealedChars.Contains('_'))
		{
			RenderGameState();
		GetInput:
			ConsoleKey key = Console.ReadKey(true).Key;
			if (key is ConsoleKey.Escape)
			{
				Console.Clear();
				Console.Write("Hangman was closed.");
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

		#region Win/Lose

		if (incorrectGuesses >= Ascii.Renders.Length)
		{
			for (int i = 0; i < Ascii.DeathAnimation.Length; i++)
			{
				Console.SetCursorPosition(4, 3);
				Render(Ascii.DeathAnimation[i]);
				Thread.Sleep(TimeSpan.FromMilliseconds(150));
			}
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("    Answer: " + randomWord);
			Console.WriteLine("    You lose...");
		}
		else
		{
			RenderGameState();
			Console.WriteLine();
			Console.WriteLine("    You win!");
		}

		#endregion

		#region Play Again Check

		{
			Console.WriteLine();
			Console.WriteLine("    Play Again [enter], or quit [escape]?");
		GetPlayAgainInput:
			ConsoleKey key = Console.ReadKey(true).Key;
			if (key is ConsoleKey.Enter)
			{
				goto PlayAgain;
			}
			else if (!(key is ConsoleKey.Escape))
			{
				goto GetPlayAgainInput;
			}
			Console.Clear();
		}

		#endregion
	}

	#region Choosing A Random Word

	static string GetRandomWord() => Random.Choose(Words);

	public static T Choose<T>(this Random random, params T[] values) =>
		values[random.Next(values.Length)];

	#endregion

	#region Render

	static void RenderGameState()
	{
		Console.SetCursorPosition(4, 3);
		Console.CursorLeft = 4;
		Render(Ascii.Renders[incorrectGuesses]);
		Console.WriteLine();
		Console.WriteLine();
		Console.Write("    Guess: ");
		foreach (char c in revealedChars)
		{
			Console.Write(c + " ");
		}
	}

	static void Render(string @string)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
		{
			if (c is '\n')
			{
				Console.WriteLine();
				Console.SetCursorPosition(x, ++y);
			}
			else
			{
				Console.Write(c);
			}
		}
	}

	#endregion

	#region Ascii

	static class Ascii
	{
		public static readonly string[] Renders =
		{
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
		};

		public static readonly string[] DeathAnimation =
		{
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
		};
	}

	#endregion
}