using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Website.Games.Word_Search;

public class Word_Search
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		char[,] board = new char[20, 20];
		List<(int Left, int Top)> showWordSelections = [];
		List<(int Left, int Top)> selections = [];

		string[] wordArray = default!;
		string currentWord = default!;
		(int Left, int Top) cursor = (0, 0);

		InitializeWords();
	PlayAgain:
		InitializeBoard();
		await Console.Clear();
		while (true)
		{
			await RenderBoard();
			await Console.Write($"""

				Highlight the word "{currentWord}" above.

				Controls:
				- arrow keys: move cursor
				- enter: highlight characters
				- backspace: clear highlighted characters
				- home: new word search
				- end: give up and show word
				- escape: close game
				""");
			await Console.SetCursorPosition(2 * cursor.Left, cursor.Top);
			Console.CursorVisible = true;
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.LeftArrow: cursor.Left = cursor.Left <= 0 ? board.GetLength(0) - 1 : cursor.Left - 1; break;
				case ConsoleKey.RightArrow: cursor.Left = cursor.Left >= board.GetLength(0) - 1 ? 0 : cursor.Left + 1; break;
				case ConsoleKey.UpArrow: cursor.Top = cursor.Top <= 0 ? board.GetLength(1) - 1 : cursor.Top - 1; break;
				case ConsoleKey.DownArrow: cursor.Top = cursor.Top >= board.GetLength(1) - 1 ? 0 : cursor.Top + 1; break;
				case ConsoleKey.Backspace: selections.Clear(); break;
				case ConsoleKey.Home: goto PlayAgain;
				case ConsoleKey.End:
					selections.Clear();
					selections.AddRange(showWordSelections);
					await Console.Clear();
					await RenderBoard();
					await Console.Write($"""

						Here is where "{currentWord}" was hiding.

						Controls:
						- enter/home: play again
						- escape: close game
						""");
					while (true)
					{
						switch ((await Console.ReadKey(true)).Key)
						{
							case ConsoleKey.Enter or ConsoleKey.Home: goto PlayAgain;
							case ConsoleKey.Escape: goto Close;
						}
					}
				case ConsoleKey.Escape: goto Close;
				case ConsoleKey.Enter:
					if (!selections.Remove(cursor))
					{
						selections.Add(cursor);
						selections.Sort();
						if (UserFoundTheWord())
						{
							await Console.Clear();
							await RenderBoard();
							await Console.Write($"""

								You found "{currentWord}"! You win!

								Controls:
								- enter/home: play again
								- escape: close game
								""");
							while (true)
							{
								switch ((await Console.ReadKey(true)).Key)
								{
									case ConsoleKey.Enter or ConsoleKey.Home: goto PlayAgain;
									case ConsoleKey.Escape: goto Close;
								}
							}
						}
					}
					break;
			}
		}
	Close:
		await Console.Clear();
		await Console.WriteLine("Word Search was closed.");
		await Console.Refresh();

		void InitializeWords()
		{
			wordArray = Resources.Words!.Select(word => word.ToUpper()).ToArray();
		}

		void InitializeBoard()
		{
			selections.Clear();

			for (int i = 0; i < board.GetLength(1); i++)
			{
				for (int j = 0; j < board.GetLength(0); j++)
				{
					board[j, i] = (char)('A' + Random.Shared.Next(26));
				}
			}

			currentWord = wordArray[Random.Shared.Next(wordArray.Length)];

			// choose a random orientation for the word (down, right, left, up, down-right, down-left, up-right, or up-left)
			bool r((int Left, int Top) location) => location.Left + currentWord.Length < board.GetLength(0);
			bool d((int Left, int Top) location) => location.Top + currentWord.Length < board.GetLength(1);
			bool l((int Left, int Top) location) => location.Left - currentWord.Length >= 0;
			bool u((int Left, int Top) location) => location.Top - currentWord.Length >= 0;
			bool dr((int Left, int Top) location) => d(location) && r(location);
			bool dl((int Left, int Top) location) => d(location) && l(location);
			bool ur((int Left, int Top) location) => u(location) && r(location);
			bool ul((int Left, int Top) location) => u(location) && l(location);
			(Func<(int Left, int Top), bool> Validator, (int Left, int Top) Adjustment) orientation = Random.Shared.Next(8) switch
			{
				0 => (d, (0, 1)),
				1 => (r, (1, 0)),
				2 => (u, (0, -1)),
				3 => (l, (-1, 0)),
				4 => (dr, (1, 1)),
				5 => (dl, (-1, 1)),
				6 => (ur, (1, -1)),
				7 => (ul, (-1, -1)),
				_ => throw new NotImplementedException(),
			};

			// choose a random starting location that is valid for the orientation
			List<(int Left, int Top)> possibleLocations = [];
			for (int i = 0; i < board.GetLength(1); i++)
			{
				for (int j = 0; j < board.GetLength(0); j++)
				{
					if (orientation.Validator((j, i)))
					{
						possibleLocations.Add((j, i));
					}
				}
			}
			(int Left, int Top) randomLocation = possibleLocations[Random.Shared.Next(possibleLocations.Count)];

			showWordSelections.Clear();
			for (int i = 0; i < currentWord.Length; i++)
			{
				showWordSelections.Add(randomLocation);
				board[randomLocation.Left, randomLocation.Top] = currentWord[i];
				randomLocation = (randomLocation.Left + orientation.Adjustment.Left, randomLocation.Top + orientation.Adjustment.Top);
			}
		}

		async Task RenderBoard()
		{
			Console.CursorVisible = false;
			await Console.SetCursorPosition(0, 0);
			for (int i = 0; i < board.GetLength(1); i++)
			{
				for (int j = 0; j < board.GetLength(0); j++)
				{
					if (selections.Contains((j, i)))
					{
						(Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
					}
					await Console.Write(board[j, i]);
					if (selections.Contains((j, i)))
					{
						(Console.ForegroundColor, Console.BackgroundColor) = (Console.BackgroundColor, Console.ForegroundColor);
					}
					if (j < board.GetLength(1) - 1)
					{
						await Console.Write(' ');
					}
				}
				await Console.WriteLine();
			}
		}

		bool UserFoundTheWord()
		{
			// make sure all the selections are in a straight line
			if (selections.Count > 1)
			{
				(int Left, int Top) adjustment = (selections[1].Left - selections[0].Left, selections[1].Top - selections[0].Top);
				if (adjustment.Left > 1 || adjustment.Left < -1 || adjustment.Top > 1 || adjustment.Top < -1)
				{
					return false;
				}
				for (int i = 2; i < selections.Count; i++)
				{
					if ((selections[i].Left - selections[i - 1].Left, selections[i].Top - selections[i - 1].Top) != adjustment)
					{
						return false;
					}
				}
			}

			char[] chars = selections.Select(location => board[location.Left, location.Top]).ToArray();
			string charsString = new(chars);
			Array.Reverse(chars);
			string charsStringReverse = new(chars);
			return charsString == currentWord || charsStringReverse == currentWord;
		}

	}
}
