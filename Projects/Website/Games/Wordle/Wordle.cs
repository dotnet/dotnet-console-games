using System;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Games.Wordle;

public class Wordle
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		Exception? exception = null;

		try
		{
			if (Resources.FiveLetterWords is null || Resources.FiveLetterWords.Length is 0)
			{
				await Console.WriteLine("Error: Missing \"FiveLetterWords.txt\" embedded resource.");
				await Console.WriteLine("Press enter to continue...");
				await Console.ReadLine();
				return;
			}

		PlayAgain:
			Console.ForegroundColor = ConsoleColor.White;
			Console.BackgroundColor = ConsoleColor.Black;
			await Console.Clear();
			await Console.WriteLine("""
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
			string word = Resources.FiveLetterWords[Random.Shared.Next(Resources.FiveLetterWords.Length)].ToUpperInvariant();
			char[] letters = [' ', ' ', ' ', ' ', ' '];
		GetInput:
			await Console.SetCursorPosition(3 + cursor * 4, 2 + guess * 2);
			ConsoleKey key = (await Console.ReadKey(true)).Key;
			switch (key)
			{
				case >= ConsoleKey.A and <= ConsoleKey.Z:
					await ClearMessageText();
					await Console.SetCursorPosition(3 + cursor * 4, 2 + guess * 2);
					char c = (char)(key - ConsoleKey.A + 'A');
					letters[cursor] = c;
					await Console.Write(c);
					cursor = Math.Min(cursor + 1, 4);
					goto GetInput;
				case ConsoleKey.LeftArrow:
					cursor = Math.Max(cursor - 1, 0);
					goto GetInput;
				case ConsoleKey.RightArrow:
					cursor = Math.Min(cursor + 1, 4);
					goto GetInput;
				case ConsoleKey.Enter:
					if (letters.Any(l => l < 'A' || l > 'Z') || !Resources.FiveLetterWords.Contains(new string(letters).ToLowerInvariant()))
					{
						await ClearMessageText();
						await Console.SetCursorPosition(0, 19);
						await Console.WriteLine(" You must input a valid word.");
						goto GetInput;
					}
					bool correct = true;
					for (int i = 0; i < 5; i++)
					{
						await Console.SetCursorPosition(2 + i * 4, 2 + guess * 2);
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
						await Console.Write($" {letters[i]} ");
						Console.BackgroundColor = ConsoleColor.Black;
					}
					if (correct)
					{
						await ClearMessageText();
						await Console.SetCursorPosition(0, 19);
						await Console.WriteLine(" You win!");
						if (await PlayAgainCheck())
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
						await ClearMessageText();
						await Console.SetCursorPosition(0, 19);
						await Console.WriteLine($" You lose! Word: {word}");
						if (await PlayAgainCheck())
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
			await Console.Clear();
			await Console.WriteLine(exception?.ToString() ?? "Wordle was closed.");
			await Console.Refresh();
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

		async Task<bool> PlayAgainCheck()
		{
			await Console.WriteLine($" Play again [enter] or quit [escape]?");
		GetPlayAgainInput:
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.Enter:
					return true;
				case ConsoleKey.Escape:
					return false;
				default:
					goto GetPlayAgainInput;
			}
		}

		async Task ClearMessageText()
		{
			await Console.SetCursorPosition(0, 19);
			await Console.WriteLine("                                      ");
			await Console.WriteLine("                                      ");
		}
	}
}
