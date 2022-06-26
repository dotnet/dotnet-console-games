using System;
using System.Text;
using System.Threading;

// NOTE:
// Most of the magic numbers are related to the sizes
// of the sprites, which are 7 width by 5 height.

namespace Animal_Trainer;

public partial class Program
{
	static Character character = new();
	static char[][] map = Maps.PaletTown;
	static DateTime previoiusRender = DateTime.Now;
	static bool gameRunning = true;

	private static readonly string[] maptext = new[]
	{
		"Move: arrow keys or (w, a, s, d)",
		"Check Status: [enter]",
		"Quit: [escape]",
	};

	public static void Main()
	{
		try
		{
			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
			Initialize();
			while (gameRunning)
			{
				UpdateCharacter();
				HandleMapUserInput();
				if (gameRunning)
				{
					RenderWorldMapView();
					SleepAfterRender();
				}
			}
		}
		finally
		{
			Console.Clear();
			Console.WriteLine("Animal Trainer was closed.");
			Console.CursorVisible = true;
		}
	}

	static void Initialize()
	{
		map = Maps.PaletTown;
		var (i, j) = FindTileInMap(map, 'X')!.Value;
		character = new()
		{
			I = i * 7,
			J = j * 5,
			MapAnimation = Sprites.Idle
		};
	}

	static void UpdateCharacter()
	{
		if (character.MapAnimation == Sprites.RunUp)    character.J--;
		if (character.MapAnimation == Sprites.RunDown)  character.J++;
		if (character.MapAnimation == Sprites.RunLeft)  character.I--;
		if (character.MapAnimation == Sprites.RunRight) character.I++;
		character.MapAnimationFrame++;

		if (character.Moved)
		{
			HandleCharacterMoved();
			character.Moved = false;
		}
	}

	static void HandleCharacterMoved()
	{
		switch (map[character.TileJ][character.TileI])
		{
			case 'v': EnterVet(); break;
			case '0': TransitionMapToTown(); break;
			case '1': TransitionMapToField(); break;
			case '2': break;
		}
	}

	static void RenderStatusString()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" Animal Status");
		Console.WriteLine();
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void PressEnterToContiue()
	{
		GetInput:
		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.Enter:
				return;
			case ConsoleKey.Escape:
				gameRunning = false;
				return;
			default:
				goto GetInput;
		}
	}

	static void EnterVet()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine(" You enter the vet.");
		Console.WriteLine();
		Console.WriteLine(" All your animals are healed.");
		Console.WriteLine();
		Console.Write(" Press [enter] to continue...");
		PressEnterToContiue();
	}

	static void TransitionMapToTown()
	{
		map = Maps.PaletTown;
		var (i, j) = FindTileInMap(map, '1')!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	static void TransitionMapToField()
	{
		map = Maps.RouteOne1;
		var (i, j) = FindTileInMap(map, '0')!.Value;
		character.I = i * 7;
		character.J = j * 5;
	}

	static void HandleMapUserInput()
	{
		while (Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey(true).Key;
			switch (key)
			{
				case
					ConsoleKey.UpArrow    or ConsoleKey.W or
					ConsoleKey.DownArrow  or ConsoleKey.S or
					ConsoleKey.LeftArrow  or ConsoleKey.A or
					ConsoleKey.RightArrow or ConsoleKey.D:
					if (character.IsIdle)
					{
						var (tileI, tileJ) = key switch
						{
							ConsoleKey.UpArrow    or ConsoleKey.W => (character.TileI, character.TileJ - 1),
							ConsoleKey.DownArrow  or ConsoleKey.S => (character.TileI, character.TileJ + 1),
							ConsoleKey.LeftArrow  or ConsoleKey.A => (character.TileI - 1, character.TileJ),
							ConsoleKey.RightArrow or ConsoleKey.D => (character.TileI + 1, character.TileJ),
							_ => throw new Exception("bug"),
						};
						if (Maps.IsValidCharacterMapTile(map, tileI, tileJ))
						{
							switch (key)
							{
								case ConsoleKey.UpArrow    or ConsoleKey.W: character.MapAnimation = Sprites.RunUp;    break;
								case ConsoleKey.DownArrow  or ConsoleKey.S: character.MapAnimation = Sprites.RunDown;  break;
								case ConsoleKey.LeftArrow  or ConsoleKey.A: character.MapAnimation = Sprites.RunLeft;  break;
								case ConsoleKey.RightArrow or ConsoleKey.D: character.MapAnimation = Sprites.RunRight; break;
							}
						}
					}
					break;
				case ConsoleKey.Enter: RenderStatusString(); break;
				case ConsoleKey.Escape: gameRunning = false; return;
			}
		}
	}

	static void SleepAfterRender()
	{
		// frame rate control targeting 30 frames per second
		DateTime now = DateTime.Now;
		TimeSpan sleep = TimeSpan.FromMilliseconds(33) - (now - previoiusRender);
		if (sleep > TimeSpan.Zero)
		{
			Thread.Sleep(sleep);
		}
		previoiusRender = DateTime.Now;
	}

	static (int I, int J)? FindTileInMap(char[][] map, char c)
	{
		for (int j = 0; j < map.Length; j++)
		{
			for (int i = 0; i < map[j].Length; i++)
			{
				if (map[j][i] == c)
				{
					return (i, j);
				}
			}
		}
		return null;
	}

	static void RenderWorldMapView()
	{
		Console.CursorVisible = false;

		var (width, height) = GetWidthAndHeight();
		int heightCutOff = (int)(height * .80);
		int midWidth = width / 2;
		int midHeight = heightCutOff / 2;

		StringBuilder sb = new(width * height);
		for (int j = 0; j < height; j++)
		{
			if (OperatingSystem.IsWindows() && j == height - 1)
			{
				break;
			}

			for (int i = 0; i < width; i++)
			{
				// console area (below map)
				if (j >= heightCutOff)
				{
					int line = j - heightCutOff - 1;
					int character = i - 1;
					if (i < width - 1 && character >= 0 && line >= 0 && line < maptext.Length && character < maptext[line].Length)
					{
						char ch = maptext[line][character];
						sb.Append(char.IsWhiteSpace(ch) ? ' ' : ch);
					}
					else
					{
						sb.Append(' ');
					}
					continue;
				}

				// map outline
				if (i is 0 && j is 0)
				{
					sb.Append('╔');
					continue;
				}
				if (i is 0 && j == heightCutOff - 1)
				{
					sb.Append('╚');
					continue;
				}
				if (i == width - 1 && j is 0)
				{
					sb.Append('╗');
					continue;
				}
				if (i == width - 1 && j == heightCutOff - 1)
				{
					sb.Append('╝');
					continue;
				}
				if (i is 0 || i == width - 1)
				{
					sb.Append('║');
					continue;
				}
				if (j is 0 || j == heightCutOff - 1)
				{
					sb.Append('═');
					continue;
				}

				// character
				if (i > midWidth - 4 && i < midWidth + 4 && j > midHeight - 3 && j < midHeight + 3)
				{
					int ci = i - (midWidth - 3);
					int cj = j - (midHeight - 2);
					string characterMapRender = character.Render;
					sb.Append(characterMapRender[cj * 8 + ci]);
					continue;
				}

				// tiles

				// compute the map location that this screen pixel represents
				int mapI = i - midWidth  + character.I + 3;
				int mapJ = j - midHeight + character.J + 2;

				// compute the coordinates of the tile
				int tileI = mapI < 0 ? (mapI - 6) / 7 : mapI / 7;
				int tileJ = mapJ < 0 ? (mapJ - 4) / 5 : mapJ / 5;

				// compute the coordinates of the pixel within the tile's sprite
				int pixelI = mapI < 0 ? 6 + ((mapI + 1) % 7) : (mapI % 7);
				int pixelJ = mapJ < 0 ? 4 + ((mapJ + 1) % 5) : (mapJ % 5);

				// render pixel from map tile
				string tileRender = Maps.GetMapTileRender(map, tileI, tileJ);
				char c = tileRender[pixelJ * 8 + pixelI];
				sb.Append(char.IsWhiteSpace(c) ? ' ' : c);
			}
			if (!OperatingSystem.IsWindows() && j < height - 1)
			{
				sb.AppendLine();
			}
		}
		Console.SetCursorPosition(0, 0);
		Console.Write(sb);
	}

	static (int Width, int Height) GetWidthAndHeight()
	{
		RestartRender:
		int width = Console.WindowWidth;
		int height = Console.WindowHeight;
		if (OperatingSystem.IsWindows())
		{
			try
			{
				if (Console.BufferHeight != height) Console.BufferHeight = height;
				if (Console.BufferWidth != width) Console.BufferWidth = width;
			}
			catch (ArgumentOutOfRangeException)
			{
				Console.Clear();
				goto RestartRender;
			}
		}
		return (width, height);
	}
}
