using System;
using System.Threading.Tasks;
using Point = System.ValueTuple<int, int>;

namespace Website.Games.Draw;

public class Draw
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		try
		{
			const int drawingWidth = 11;
			const int drawingHeight = 11;
			Point origin = (drawingHeight / 2, drawingWidth / 2);
			(int Height, int Width)? previousConsoleSize = null;
			Random random = new();

			await Console.Clear();
		Reset:
			Point cursor = origin;
			bool[,] currentDrawing = new bool[drawingHeight, drawingWidth];
			bool[,] goalDrawing = GenerateRandomDrawing();
			while (DrawingsMatch())
			{
				goalDrawing = GenerateRandomDrawing();
			}
			while (!DrawingsMatch())
			{
				(int Height, int Width) currentConsoleSize = (Console.WindowHeight, Console.WindowWidth);
				if (currentConsoleSize != previousConsoleSize)
				{
					await Console.Clear();
					previousConsoleSize = currentConsoleSize;
				}
				await Render();
				await Console.WriteLine(@"
  Make the left drawing match the right drawing.
  Use the arrow keys or WASD to draw.           
  Use [end] or [home] to generate a new drawing.");
				await Console.SetCursorPosition(cursor.Item2 + 3, cursor.Item1 + 4);
			GetInput:
				Console.CursorVisible = true;
				switch ((await Console.ReadKey(true)).Key)
				{
					case ConsoleKey.UpArrow or ConsoleKey.W: cursor.Item1--; break;
					case ConsoleKey.LeftArrow or ConsoleKey.A: cursor.Item2--; break;
					case ConsoleKey.DownArrow or ConsoleKey.S: cursor.Item1++; break;
					case ConsoleKey.RightArrow or ConsoleKey.D: cursor.Item2++; break;
					case ConsoleKey.Home or ConsoleKey.End: goto Reset;
					case ConsoleKey.Escape: return;
					default: goto GetInput;
				}
				switch (cursor)
				{
					case ( < 0, _): cursor.Item1 = 0; break;
					case ( >= drawingHeight, _): cursor.Item1 = drawingHeight - 1; break;
					case (_, < 0): cursor.Item2 = 0; break;
					case (_, >= drawingWidth): cursor.Item2 = drawingWidth - 1; break;
					default:
						currentDrawing[cursor.Item1, cursor.Item2] = !currentDrawing[cursor.Item1, cursor.Item2];
						break;
				}
			}
			await Render();
			await Console.WriteLine(@"
  **********************************************
           You matched the drawings!!!          
       Play again [enter] or quit [escape]?     ");
		GetEnterOrEscape:
			Console.CursorVisible = false;
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.Enter: goto Reset;
				case ConsoleKey.Escape: return;
				default: goto GetEnterOrEscape;
			}

			bool DrawingsMatch()
			{
				for (int h = 0; h < drawingHeight; h++)
				{
					for (int w = 0; w < drawingWidth; w++)
					{
						if (currentDrawing[h, w] != goalDrawing[h, w])
						{
							return false;
						}
					}
				}
				return true;
			}

			async Task Render()
			{
				string horizontal = new('═', drawingWidth / 2);
				await Console.SetCursorPosition(0, 0);
				await Console.WriteLine();
				await Console.WriteLine("  Draw");
				await Console.WriteLine();
				await Console.WriteLine("  ╔" + horizontal + "╬" + horizontal + "╗ ╔" + horizontal + "╬" + horizontal + "╗");
				for (int h = 0; h < drawingHeight; h++)
				{
					await Console.Write(h == drawingHeight / 2 ? "  ╬" : "  ║");
					for (int w = 0; w < drawingWidth; w++)
					{
						await Console.Write(currentDrawing[h, w] ? '█' : '.');
					}
					await Console.Write(h == drawingHeight / 2 ? "╬ ╬" : "║ ║");
					for (int w = 0; w < drawingWidth; w++)
					{
						await Console.Write(goalDrawing[h, w] ? '█' : '.');
					}
					await Console.WriteLine(h == drawingHeight / 2 ? "╬" : "║");
				}
				await Console.WriteLine("  ╚" + horizontal + "╬" + horizontal + "╝ ╚" + horizontal + "╬" + horizontal + "╝");
			}

			bool[,] GenerateRandomDrawing()
			{
				bool[,] drawing = new bool[drawingHeight, drawingWidth];
				int points = random.Next(3, 12);
				Point a = origin;
				for (int i = 0; i < points; i++)
				{
					Point b = new(
						random.Next(drawingHeight),
						random.Next(drawingWidth));
					DrawLine(a, b);
					drawing[b.Item1, b.Item2] = false;
					a = b;
				}
				DrawLine(a, origin);
				return drawing;

				void DrawLine(Point a, Point b)
				{
					while (a != b)
					{
						if (Math.Abs(a.Item1 - b.Item1) > Math.Abs(a.Item2 - b.Item2))
						{
							a.Item1 = a.Item1 > b.Item1 ? a.Item1 - 1 : a.Item1 + 1;
						}
						else
						{
							a.Item2 = a.Item2 > b.Item2 ? a.Item2 - 1 : a.Item2 + 1;
						}
						drawing[a.Item1, a.Item2] = !drawing[a.Item1, a.Item2];
					}
				}
			}
		}
		finally
		{
			Console.CursorVisible = true;
			await Console.Clear();
			await Console.Write("Draw was closed.");
			await Console.Refresh();
		}
	}
}
