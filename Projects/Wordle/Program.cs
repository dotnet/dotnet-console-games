using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

Exception? exception = null;

try
{
	const string wordsResource = "Wordle.FiveLetterWords.txt";
	Assembly assembly = Assembly.GetExecutingAssembly();
	List<string> words = new();
	{
		using Stream stream = assembly.GetManifestResourceStream(wordsResource)!;
		if (stream is null)
		{
			Console.WriteLine("Error: Missing \"FiveLetterWords.txt\" embedded resource.");
			Console.WriteLine("Press enter to continue...");
			Console.ReadLine();
			return;
		}
		using StreamReader streamReader = new(stream);
		while (!streamReader.EndOfStream)
		{
			string line = streamReader.ReadLine()!;
			words.Add(line);
		}
	}

PlayAgain:
	Console.ForegroundColor = ConsoleColor.White;
	Console.BackgroundColor = ConsoleColor.Black;
	Console.Clear();
	Console.WriteLine("""
		 Wordle
		 ╔═══╦═══╦═══╦═══╦═══╗
		 ║   ║   ║   ║   ║   ║
		 ╠═══╬═══╬═══╬═══╬═══╣
		 ║   ║   ║   ║   ║   ║
		 ╠═══╬═══╬═══╬═══╬═══╣
		 ║   ║   ║   ║   ║   ║
		 ╠═══╬═══╬═══╬═══╬═══╣
		 ║   ║   ║   ║   ║   ║
		 ╠═══╬═══╬═══╬═══╬═══╣
		 ║   ║   ║   ║   ║   ║
		 ╠═══╬═══╬═══╬═══╬═══╣
		 ║   ║   ║   ║   ║   ║
		 ╚═══╩═══╩═══╩═══╩═══╝
		 Controls:
		 - a b, c, ... y, z: input letters
		 - left/right arrow: move cursor
		 - enter: submit or confirm
		 - escape: exit
		""");
	int guess = 0;
	int cursor = 0;
	string word = words[Random.Shared.Next(words.Count)].ToUpperInvariant();
	char[] letters = [' ', ' ', ' ', ' ', ' '];
GetInput:
	Console.SetCursorPosition(3 + cursor * 4, 2 + guess * 2);
	ConsoleKey key = Console.ReadKey(true).Key;
	switch (key)
	{
		case >= ConsoleKey.A and <= ConsoleKey.Z:
			ClearMessageText();
			Console.SetCursorPosition(3 + cursor * 4, 2 + guess * 2);
			char c = (char)(key - ConsoleKey.A + 'A');
			letters[cursor] = c;
			Console.Write(c);
			cursor = Math.Min(cursor + 1, 4);
			goto GetInput;
		case ConsoleKey.LeftArrow:
			cursor = Math.Max(cursor - 1, 0);
			goto GetInput;
		case ConsoleKey.RightArrow:
			cursor = Math.Min(cursor + 1, 4);
			goto GetInput;
		case ConsoleKey.Enter:
			if (letters.Any(l => l < 'A' || l > 'Z') || !words.Contains(new string(letters).ToLowerInvariant()))
			{
				ClearMessageText();
				Console.SetCursorPosition(0, 19);
				Console.WriteLine(" You must input a valid word.");
				goto GetInput;
			}
			bool correct = true;
			for (int i = 0; i < 5; i++)
			{
				Console.SetCursorPosition(2 + i * 4, 2 + guess * 2);
				if (word[i] == letters[i])
				{
					Console.BackgroundColor = ConsoleColor.DarkGreen;
				}
				else if (CheckForYellow(i, word, letters))
				{
					correct = false;
					Console.BackgroundColor = ConsoleColor.DarkYellow;
				}
				else
				{
					correct = false;
					Console.BackgroundColor = ConsoleColor.DarkGray;
				}
				Console.Write($" {letters[i]} ");
				Console.BackgroundColor = ConsoleColor.Black;
			}
			if (correct)
			{
				ClearMessageText();
				Console.SetCursorPosition(0, 19);
				Console.WriteLine(" You win!");
				if (PlayAgainCheck())
				{
					goto PlayAgain;
				}
				else
				{
					return;
				}
			}
			else
			{
				letters = [' ', ' ', ' ', ' ', ' '];
				guess++;
				cursor = 0;
			}
			if (guess > 5)
			{
				ClearMessageText();
				Console.SetCursorPosition(0, 19);
				Console.WriteLine($" You lose! Word: {word}");
				if (PlayAgainCheck())
				{
					goto PlayAgain;
				}
				else
				{
					return;
				}
			}
			goto GetInput;
		case ConsoleKey.Escape:
			return;
		case ConsoleKey.End or ConsoleKey.Home:
			goto PlayAgain;
		default:
			goto GetInput;
	}
}
catch (Exception e)
{
	exception = e;
	throw;
}
finally
{
	Console.ResetColor();
	Console.Clear();
	Console.WriteLine(exception?.ToString() ?? "Wordle was closed.");
}

bool CheckForYellow(int index, string word, char[] letters)
{
	int letterCount = 0;
	int incorrectCountBeforeIndex = 0;
	int correctCount = 0;
	for (int i = 0; i < word.Length; i++)
	{
		if (word[i] == letters[index])
		{
			letterCount++;
		}
		if (letters[i] == letters[index] && word[i] == letters[index])
		{
			correctCount++;
		}
		if (i < index && letters[i] == letters[index] && word[i] != letters[index])
		{
			incorrectCountBeforeIndex++;
		}
	}
	return letterCount - correctCount - incorrectCountBeforeIndex > 0;
}

bool PlayAgainCheck()
{
	Console.WriteLine($" Play again [enter] or quit [escape]?");
GetPlayAgainInput:
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.Enter:
			return true;
		case ConsoleKey.Escape:
			return false;
		default:
			goto GetPlayAgainInput;
	}
}

void ClearMessageText()
{
	Console.SetCursorPosition(0, 19);
	Console.WriteLine("                                      ");
	Console.WriteLine("                                      ");
}