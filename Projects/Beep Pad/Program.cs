using System;
using System.Collections.Generic;

ConsoleColor ForegroundColor = Console.ForegroundColor;
ConsoleColor BackgroundColor = Console.BackgroundColor;

int ButtonTimeSpan = 500; // milliseconds
int CodeLength = 5;
(int X, int Y) Position = default;

// C major scale, starting with middle C
int[] frequencies =
[
	262,
	294,
	330,
	349,
	392,
	440,
	494,
	523,
	587,
];

if (!OperatingSystem.IsWindows())
{
	Console.WriteLine("Unfortunately this game is not supported on ");
	Console.WriteLine("your operating system. It is Windows only. :(");
	Console.WriteLine("Press enter to close...");
GetInput:
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.Enter:  break;
		case ConsoleKey.Escape: return;
		default: goto GetInput;
	}
	Console.Clear();
	return;
}

try
{
	Console.CursorVisible = false;
	Console.BackgroundColor = ConsoleColor.Black;
	Console.ForegroundColor = ConsoleColor.White;
	Queue<int> inputedCode = new();
	int[] answerCode = GetRandomCode();
	ShuffleFrequencies();
	RenderGame();
	PlayAnswerAudio(answerCode);
	while (true)
	{
		RenderGame();
		Console.SetCursorPosition(Position.X * 4 + 6, Position.Y * 2 + 4);
		Console.CursorVisible = true;
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.UpArrow:    Position.Y = Position.Y is 0 ? 2 : Position.Y - 1; break;
			case ConsoleKey.DownArrow:  Position.Y = Position.Y is 2 ? 0 : Position.Y + 1; break;
			case ConsoleKey.LeftArrow:  Position.X = Position.X is 0 ? 2 : Position.X - 1; break;
			case ConsoleKey.RightArrow: Position.X = Position.X is 2 ? 0 : Position.X + 1; break;
			case ConsoleKey.Spacebar: PlayAnswerAudio(answerCode); break;
			case ConsoleKey.Enter:
				int button = GetButton(Position);
				Console.Write('█');
				Console.CursorVisible = false;
				Console.Beep(frequencies[button - 1], ButtonTimeSpan);
				inputedCode.Enqueue(button);
				if (inputedCode.Count > CodeLength)
				{
					inputedCode.Dequeue();
				}
				if (InputMatchesAnswer(inputedCode, answerCode))
				{
					RenderGame(false);
					Console.WriteLine("    You Win!");
					Console.WriteLine();
					Console.WriteLine("    Press Enter To Close...");
					Console.ReadLine();
					Console.Clear();
					return;
				}
				break;
			case ConsoleKey.Escape:
				Console.Clear();
				Console.Write("BeepPad was closed.");
				return;
		}
	}
}
finally
{
	Console.CursorVisible = true;
	Console.BackgroundColor = BackgroundColor;
	Console.ForegroundColor = ForegroundColor;
}

void RenderGame(bool includeInstructions = true)
{
	Console.Clear();
	Console.WriteLine();
	Console.WriteLine("    Beep Pad");
	Console.WriteLine();
	Console.WriteLine("    ╔═══╦═══╦═══╗");
	Console.WriteLine("    ║ 7 ║ 8 ║ 9 ║");
	Console.WriteLine("    ╠═══╬═══╬═══╣");
	Console.WriteLine("    ║ 4 ║ 5 ║ 6 ║");
	Console.WriteLine("    ╠═══╬═══╬═══╣");
	Console.WriteLine("    ║ 1 ║ 2 ║ 3 ║");
	Console.WriteLine("    ╚═══╩═══╩═══╝");
	Console.WriteLine();
	if (!includeInstructions)
	{
		return;
	}
	Console.WriteLine("    Replicate the code of the audio.");
	Console.WriteLine();
	Console.Write("    - Press ");
	WriteHighlighted("spacebar");
	Console.WriteLine(" to repeat the audio.");
	Console.Write("    - Press the ");
	WriteHighlighted("arrow keys");
	Console.Write(" and ");
	WriteHighlighted("enter");
	Console.WriteLine(" to select buttons.");
	Console.Write("    - Press ");
	WriteHighlighted("escape");
	Console.WriteLine(" to close.");
	Console.WriteLine();
	Console.WriteLine("You should hear audio. If you do not hear audio,");
	Console.WriteLine("this game is not compatible with your system.");
}

void WriteHighlighted(string @string)
{
	Console.BackgroundColor = ConsoleColor.White;
	Console.ForegroundColor = ConsoleColor.Black;
	Console.Write(@string);
	Console.BackgroundColor = ConsoleColor.Black;
	Console.ForegroundColor = ConsoleColor.White;
}

int GetButton((int x, int y) position) => position switch
{
	(0, 0) => 7,
	(0, 1) => 4,
	(0, 2) => 1,
	(1, 0) => 8,
	(1, 1) => 5,
	(1, 2) => 2,
	(2, 0) => 9,
	(2, 1) => 6,
	(2, 2) => 3,
	_ => throw new NotImplementedException(),
};

void ShuffleFrequencies()
{
	for (int i = 0; i < frequencies.Length; i++)
	{
		int randomIndex = Random.Shared.Next(frequencies.Length);
		(frequencies[randomIndex], frequencies[i]) = (frequencies[i], frequencies[randomIndex]);
	}
}

int[] GetRandomCode()
{
	int[] possibilities = [1, 2, 3, 4, 5, 6, 7, 8, 9];
	for (int i = 0; i < CodeLength; i++)
	{
		int randomIndex = Random.Shared.Next(possibilities.Length - i);
		(possibilities[possibilities.Length - i - 1], possibilities[randomIndex]) = (possibilities[randomIndex], possibilities[possibilities.Length - i - 1]);
	}
	return possibilities[0..CodeLength];
}

bool InputMatchesAnswer(Queue<int> input, int[] answer)
{
	if (input.Count < answer.Length)
	{
		return false;
	}
	int i = 0;
	foreach (int inputButton in input)
	{
		if (answer[i++] != inputButton)
		{
			return false;
		}
	}
	return true;
}

void PlayAnswerAudio(int[] answer)
{
	foreach (int button in answer)
	{
		Console.Beep(frequencies[button - 1], ButtonTimeSpan);
	}
}
