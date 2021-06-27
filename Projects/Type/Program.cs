using System;
using System.IO;
using System.Reflection;
using Towel;
using Towel.DataStructures;

int Score = 0;
Random Random = new();
string[] WordPool;
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

#region Loading

const string wordsResource = "Type.Words.txt";
Assembly assembly = Assembly.GetExecutingAssembly();
using Stream stream = assembly.GetManifestResourceStream(wordsResource);
if (stream is null)
{
	Console.WriteLine("Error: Missing \"Words.txt\" embedded resource.");
	ConsoleHelper.PromptPressToContinue();
	return;
}
ListArray<string> words = new();
using StreamReader streamReader = new(stream);
while (!streamReader.EndOfStream)
{
	string word = streamReader.ReadLine();
	words.Add(word);
}
WordPool = words.ToArray();

#endregion

try
{
	if (OperatingSystem.IsWindows())
	{
		if (Console.BufferWidth < 80) Console.BufferWidth = 80;
		if (Console.WindowWidth < 80) Console.WindowWidth = 80;
		if (Console.BufferHeight < 25) Console.BufferHeight = 25;
		if (Console.WindowHeight < 25) Console.WindowHeight = 25;
	}
	Console.WriteLine("Type the words in order as they appear as fast as you can. The ");
	Console.WriteLine("ammount of time you have to type each word will reduce with each ");
	Console.WriteLine("word you complete. Run out of time and it is game over. Good Luck!");
	ConsoleHelper.PromptPressToContinue();
	Console.BackgroundColor = ConsoleColor.Black;
	Console.Clear();
	Console.CursorVisible = false;
	GetWord();
	GetWord();
	GetWord();
	Render();
	TimePerWord = TimePerCharacter * Words[0].String.Length;
	WordStart = DateTime.Now;
	while (true)
	{
		Console.SetCursorPosition(Words[0].Left + position, Words[0].Top);
		var key = Console.ReadKey(true);
		if (key.Key is ConsoleKey.Escape)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine($"Type was closed.");
			ConsoleHelper.PromptPressToContinue();
			return;
		}
		TimeSpan timeSpan = DateTime.Now - WordStart;
		if (timeSpan > TimePerWord)
		{
			Console.Clear();
			Console.ForegroundColor = ConsoleColor.White;
			Console.WriteLine($"Game over. Score: {Score}.");
			ConsoleHelper.PromptPressToContinue();
			return;
		}
		if (!char.IsLetter(key.KeyChar) || key.KeyChar != Words[0].String[position])
		{
			continue;
		}
		Score++;
		Console.SetCursorPosition(Words[0].Left + position, Words[0].Top + 1);
		Console.Write(' ');
		position++;
		if (position >= Words[0].String.Length)
		{
			NextWord();

		}
		Render();
	}
}
finally
{
	Console.ResetColor();
	Console.Clear();
	Console.CursorVisible = true;
}

void GetWord()
{
	string w = Random.Choose(WordPool);
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

void NextWord()
{
	ClearCurrentWord();
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

void ClearCurrentWord()
{
	Console.SetCursorPosition(Words[0].Left, Words[0].Top);
	Console.Write(new string(' ', Words[0].String.Length));
	Console.SetCursorPosition(Words[0].Left, Words[0].Top + 1);
	Console.Write(new string(' ', Words[0].String.Length));
}

void Render()
{
	for (int i = 0; i < Words.Count; i++)
	{
		var word = Words[i];
		Console.SetCursorPosition(word.Left, word.Top);
		Console.ForegroundColor = i < Colors.Length ? Colors[i] : Colors[^1];
		Console.Write(word.String);
	}
	Console.ForegroundColor = ConsoleColor.White;
	Console.SetCursorPosition(Words[0].Left + position, Words[0].Top + 1);
	Console.Write('^');
}
