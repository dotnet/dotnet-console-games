using System;
using System.Collections.Generic;
using System.Threading;

string[] frames = { "▌", "▐", };
string[] deathFrames = { "X", "X", "X", "X", "X", };
List<Note> notes;
List<Note> deadNotes;
TimeSpan delayTime = TimeSpan.FromMilliseconds(34);
TimeSpan spawnTimeMin = TimeSpan.FromMilliseconds(250);
TimeSpan spawnTimeMax = TimeSpan.FromMilliseconds(2000);
int targetLeft = 5;
int remainingMisses = 5;
(int Top, ConsoleKey Key)[] tracks =
{
	(4, ConsoleKey.UpArrow),
	(7, ConsoleKey.LeftArrow),
	(10, ConsoleKey.DownArrow),
	(13, ConsoleKey.RightArrow),
};

try
{
	Random random = new();
	int bufferwidth = Console.BufferWidth;
	Console.CursorVisible = false;
	DateTime lastSpawn;
	TimeSpan spawnTime;
	int score;
	int misses;

	void NewNote()
	{
		notes.Add(new Note()
		{
			Top = tracks[random.Next(tracks.Length)].Top,
			Frame = 0,
			Left = Console.BufferWidth - 1,
		});
		lastSpawn = DateTime.Now;
		spawnTime = TimeSpan.FromMilliseconds(random.Next((int)spawnTimeMin.TotalMilliseconds, (int)spawnTimeMax.TotalMilliseconds));
	}

	void RenderMisses()
	{
		Console.SetCursorPosition(0, tracks[^1].Top + 3);
		Console.WriteLine("Remaining Misses: " + (remainingMisses - misses));
	}

PlayAgain:
	misses = 0;
	score = 0;
	notes = new List<Note>();
	deadNotes = new List<Note>();
	Console.Clear();
	Console.WriteLine("Rythm");
	Console.WriteLine();
	Console.WriteLine("Press enter to play...");
	{
	GetInput:
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter: break;
			case ConsoleKey.Escape:
				Console.Clear();
				Console.WriteLine("RRythm Closed.");
				return;
			default: goto GetInput;
		}
	}
	Console.Clear();
	Console.WriteLine("Rythm");
	Console.WriteLine();
	Console.WriteLine("Time your button presses...");
	foreach (var (Top, Key) in tracks)
	{
		Console.SetCursorPosition(0, Top - 1);
		Console.Write(new string('_', Console.BufferWidth));
		Console.SetCursorPosition(targetLeft, Top + 1);
		Console.Write($"^ {Key}");
	}
	RenderMisses();
	NewNote();
	while (true)
	{
	NextKey:
		while (Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey().Key;
			if (key is ConsoleKey.Escape)
			{
				Console.Clear();
				Console.WriteLine("RRythm Closed.");
				return;
			}
			foreach (var (Top, Key) in tracks)
			{
				if (key == Key)
				{
					foreach (Note note in notes)
					{
						if (note.Left > targetLeft + 2)
						{
							break;
						}
						if (note.Top == Top && Math.Abs(note.Left - targetLeft) < 3)
						{
							notes.Remove(note);
							note.Frame = deathFrames.Length - 1;
							deadNotes.Add(note);
							score++;
							goto NextKey;
						}
					}
					if (++misses >= remainingMisses)
					{
						goto GameOver;
					}
					RenderMisses();
					break;
				}
			}
		}
		if (bufferwidth != Console.BufferWidth)
		{
			Console.Clear();
			Console.Write("RRythm closed. Console was resized.");
			return;
		}
		if (notes[0].Left <= 0)
		{
			Console.SetCursorPosition(0, notes[0].Top);
			Console.Write(" ");
			notes.RemoveAt(0);
			if (++misses >= remainingMisses)
			{
				goto GameOver;
			}
			RenderMisses();
		}
		for (int i = 0; i < deadNotes.Count; i++)
		{
			if (deadNotes[i].Frame < 0)
			{
				Console.SetCursorPosition(deadNotes[i].Left, deadNotes[i].Top);
				Console.Write(" ");
				deadNotes.RemoveAt(i--);
			}
			else
			{
				Console.SetCursorPosition(deadNotes[i].Left, deadNotes[i].Top);
				Console.Write(deathFrames[deadNotes[i].Frame]);
				deadNotes[i].Frame--;
			}
		}
		foreach (Note note in notes)
		{
			note.Frame--;
			if (note.Frame < 0)
			{
				Console.SetCursorPosition(note.Left, note.Top);
				Console.Write(" ");
				note.Frame = frames.Length - 1;
				note.Left--;
			}
			Console.SetCursorPosition(note.Left, note.Top);
			Console.Write(frames[note.Frame]);
		}
		if (DateTime.Now - lastSpawn > spawnTime)
		{
			NewNote();
		}
		Thread.Sleep(delayTime);
	}
GameOver:
	Console.Clear();
	Console.WriteLine("Rythm");
	Console.WriteLine();
	Console.WriteLine("Score: " + score);
	Console.WriteLine();
	Console.WriteLine("Play Again [enter], or quit [escape]?");
	{
	GetInput:
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter: goto PlayAgain;
			case ConsoleKey.Escape:
				Console.Clear();
				Console.WriteLine("RRythm Closed.");
				return;
			default: goto GetInput;
		}
	}
}
finally
{
	Console.CursorVisible = true;
}

public class Note
{
	public int Top;
	public int Left;
	public int Frame;
}
