using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Website.Games.Rythm;

public class Rythm
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
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
					Top = tracks[Random.Shared.Next(tracks.Length)].Top,
					Frame = 0,
					Left = Console.BufferWidth - 1,
				});
				lastSpawn = DateTime.Now;
				spawnTime = TimeSpan.FromMilliseconds(Random.Shared.Next((int)spawnTimeMin.TotalMilliseconds, (int)spawnTimeMax.TotalMilliseconds));
			}

			async Task RenderMisses()
			{
				await Console.SetCursorPosition(0, tracks[^1].Top + 3);
				await Console.WriteLine("Remaining Misses: " + (remainingMisses - misses));
			}

		PlayAgain:
			misses = 0;
			score = 0;
			notes = new List<Note>();
			deadNotes = new List<Note>();
			await Console.Clear();
			await Console.WriteLine("Rythm");
			await Console.WriteLine();
			await Console.WriteLine("Press enter to play...");
			{
			GetInput:
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Enter: break;
					case ConsoleKey.Escape:
						await Console.Clear();
						await Console.WriteLine("RRythm Closed.");
						await Console.Refresh();
						return;
					default: goto GetInput;
				}
			}
			await Console.Clear();
			await Console.WriteLine("Rythm");
			await Console.WriteLine();
			await Console.WriteLine("Time your button presses...");
			foreach (var (Top, Key) in tracks)
			{
				await Console.SetCursorPosition(0, Top - 1);
				await Console.Write(new string('_', Console.BufferWidth));
				await Console.SetCursorPosition(targetLeft, Top + 1);
				await Console.Write($"^ {Key}");
			}
			await RenderMisses();
			NewNote();
			while (true)
			{
			NextKey:
				while (await Console.KeyAvailable())
				{
					ConsoleKey key = (await Console.ReadKey(true)).Key;
					if (key is ConsoleKey.Escape)
					{
						await Console.Clear();
						await Console.WriteLine("RRythm Closed.");
						await Console.Refresh();
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
							await RenderMisses();
							break;
						}
					}
				}
				if (bufferwidth != Console.BufferWidth)
				{
					await Console.Clear();
					await Console.Write("RRythm closed. Console was resized.");
					return;
				}
				if (notes[0].Left <= 0)
				{
					await Console.SetCursorPosition(0, notes[0].Top);
					await Console.Write(" ");
					notes.RemoveAt(0);
					if (++misses >= remainingMisses)
					{
						goto GameOver;
					}
					await RenderMisses();
				}
				for (int i = 0; i < deadNotes.Count; i++)
				{
					if (deadNotes[i].Frame < 0)
					{
						await Console.SetCursorPosition(deadNotes[i].Left, deadNotes[i].Top);
						await Console.Write(" ");
						deadNotes.RemoveAt(i--);
					}
					else
					{
						await Console.SetCursorPosition(deadNotes[i].Left, deadNotes[i].Top);
						await Console.Write(deathFrames[deadNotes[i].Frame]);
						deadNotes[i].Frame--;
					}
				}
				foreach (Note note in notes)
				{
					note.Frame--;
					if (note.Frame < 0)
					{
						await Console.SetCursorPosition(note.Left, note.Top);
						await Console.Write(" ");
						note.Frame = frames.Length - 1;
						note.Left--;
					}
					await Console.SetCursorPosition(note.Left, note.Top);
					await Console.Write(frames[note.Frame]);
				}
				if (DateTime.Now - lastSpawn > spawnTime)
				{
					NewNote();
				}
				await Console.RefreshAndDelay(delayTime);
			}
		GameOver:
			await Console.Clear();
			await Console.WriteLine("Rythm");
			await Console.WriteLine();
			await Console.WriteLine("Score: " + score);
			await Console.WriteLine();
			await Console.WriteLine("Play Again [enter], or quit [escape]?");
			{
			GetInput:
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.Enter: goto PlayAgain;
					case ConsoleKey.Escape:
						await Console.Clear();
						await Console.WriteLine("RRythm Closed.");
						await Console.Refresh();
						return;
					default: goto GetInput;
				}
			}
		}
		finally
		{
			Console.CursorVisible = true;
		}
	}
	public class Note
	{
		public int Top;
		public int Left;
		public int Frame;
	}
}
