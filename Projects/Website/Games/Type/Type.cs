using System;
using System.Threading.Tasks;
using Towel;
using Towel.DataStructures;
using Console = Website.Console<Website.Games.Type.Type>;
using ConsoleHelper = Website.Console<Website.Games.Type.Type>;

namespace Website.Games.Type;

public class Type
{
	public static async Task Run()
	{
if (Resources.Words is null || Resources.Words.Length is 0)
{
	await Console.WriteLine("Error: Missing \"Words.txt\" embedded resource.");
	await ConsoleHelper.PromptPressToContinue();
	return;
}

int Score = 0;
Random Random = new();
QueueArray<(string String, int Left, int Top)> Words = new();
int position = 0;
TimeSpan TimePerCharacter = TimeSpan.FromMilliseconds(2000);
TimeSpan TimePerCharacterDecrement = TimeSpan.FromMilliseconds(50);
TimeSpan TimePerCharacterMinimum = TimeSpan.FromMilliseconds(50);
TimeSpan TimePerWord;
DateTime WordStart;

ConsoleColor[] Colors =
{
	ConsoleColor.White,
	ConsoleColor.Gray,
	ConsoleColor.DarkGray,
};

try
{
	if (OperatingSystem.IsWindows())
	{
		if (Console.BufferWidth < 80) Console.BufferWidth = 80;
		if (Console.WindowWidth < 80) Console.WindowWidth = 80;
		if (Console.BufferHeight < 25) Console.BufferHeight = 25;
		if (Console.WindowHeight < 25) Console.WindowHeight = 25;
	}
	await Console.WriteLine("Type the words in order as they appear as fast as you can. The ");
	await Console.WriteLine("ammount of time you have to type each word will reduce with each ");
	await Console.WriteLine("word you complete. Run out of time and it is game over. Good Luck!");
	await ConsoleHelper.PromptPressToContinue();
	Console.BackgroundColor = ConsoleColor.Black;
	await Console.Clear();
	Console.CursorVisible = false;
	GetWord();
	GetWord();
	GetWord();
	await Render();
	TimePerWord = TimePerCharacter * Words[0].String.Length;
	WordStart = DateTime.Now;
	while (true)
	{
		await Console.SetCursorPosition(Words[0].Left + position, Words[0].Top);
		var key = await Console.ReadKey(true);
		if (key.Key is ConsoleKey.Escape)
		{
			await Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			await Console.WriteLine($"Type was closed.");
			await ConsoleHelper.PromptPressToContinue();
			return;
		}
		TimeSpan timeSpan = DateTime.Now - WordStart;
		if (timeSpan > TimePerWord)
		{
			await Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			await Console.WriteLine($"Game over. Score: {Score}.");
			await ConsoleHelper.PromptPressToContinue();
			return;
		}
		if (!char.IsLetter(key.KeyChar) || key.KeyChar != Words[0].String[position])
		{
			continue;
		}
		Score++;
		await Console.SetCursorPosition(Words[0].Left + position, Words[0].Top + 1);
		await Console.Write(' ');
		position++;
		if (position >= Words[0].String.Length)
		{
			await NextWord();

		}
		await Render();
	}
}
finally
{
	Console.ResetColor();
	await Console.Clear();
	Console.CursorVisible = true;
}

void GetWord()
{
	string w = Random.Choose(Resources.Words!);
	int width = Math.Min(Console.BufferWidth, Console.WindowWidth) - w.Length;
	int height = Math.Min(Console.BufferHeight, Console.WindowHeight);
	var set = SetHashLinked.New<(int Left, int Top)>();
	ListArray<(int Left, int Top)> list = new(expectedCount: width * height);
	foreach (var word in Words)
	{
		for (int i = 0; i < word.String.Length; i++)
		{
			set.Add((word.Left + i, word.Top));
			set.Add((word.Left + i, word.Top + 1));
		}
	}
	for (int left = 0; left < width; left++)
	{
		for (int top = 0; top < height - 1; top++)
		{
			for (int i = 0; i < w.Length; i++)
			{
				if (set.Contains((left + i, top)) ||
					set.Contains((left + i, top + 1)))
				{
					goto Next;
				}
			}
			list.Add((left, top));
			Next:
			continue;
		}
	}
	var (Left, Top) = list[Random.Next(list.Count)];
	Words.Enqueue((w, Left, Top));
}

async Task NextWord()
{
	await ClearCurrentWord();
	Words.Dequeue();
	GetWord();
	position = 0;
	TimePerCharacter -= TimePerCharacterDecrement;
	if (TimePerCharacter < TimePerCharacterMinimum)
	{
		TimePerCharacter = TimePerCharacterMinimum;
	}
	TimePerWord = TimePerCharacter * Words[0].String.Length;
	WordStart = DateTime.Now;
}

async Task ClearCurrentWord()
{
	await Console.SetCursorPosition(Words[0].Left, Words[0].Top);
	await Console.Write(new string(' ', Words[0].String.Length));
	await Console.SetCursorPosition(Words[0].Left, Words[0].Top + 1);
	await Console.Write(new string(' ', Words[0].String.Length));
}

async Task Render()
{
	for (int i = 0; i < Words.Count; i++)
	{
		var word = Words[i];
		await Console.SetCursorPosition(word.Left, word.Top);
		Console.ForegroundColor = i < Colors.Length ? Colors[i] : Colors[^1];
		await Console.Write(word.String);
	}
	Console.ForegroundColor = ConsoleColor.White;
	await Console.SetCursorPosition(Words[0].Left + position, Words[0].Top + 1);
	await Console.Write('^');
}
	}
}
