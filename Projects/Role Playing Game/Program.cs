using System;
using System.Text;
using System.Threading;

namespace Role_Playing_Game
{
	public partial class Program
	{
		static Character character;
		static char[][] map;
		static DateTime previoiusRender = DateTime.Now;

		const string moveString =   "Move: arrow keys or (w, a, s, d)";
		const string statusString = "Check Status: [enter]";
		const string quitString =   "Quit: [escape]";

		public static string[] text = new[]
		{
			moveString,
			statusString,
			quitString,
		};

		public static void Main()
		{
			try
			{
				map = Maps.Town;
				character = new();
				{
					var (i, j) = FindTileInMap(map, 'X').Value;
					character.I = i * 7;
					character.J = j * 4;
				}
				character.MapAnimation = Sprites.IdleRight;

				Console.SetCursorPosition(0, 0);
				Console.Clear();
				Console.WriteLine();
				Console.WriteLine(" Role Playing Game");
				Console.WriteLine();
				Console.WriteLine(" You are about to embark on an epic adventure.");
				Console.WriteLine();
				Console.WriteLine(" Note: This game is a work in progress.");
				Console.WriteLine();
				Console.Write(" Press [enter] to begin...");
				if (!PressEnterToContiue())
				{
					return;
				}

				while (true)
				{
					// update character animation
					if (character.MapAnimation == Sprites.RunUp && character.MapAnimationFrame is 2 or 4 or 6) character.J--;
					if (character.MapAnimation == Sprites.RunDown && character.MapAnimationFrame is 2 or 4 or 6) character.J++;
					if (character.MapAnimation == Sprites.RunLeft) character.I--;
					if (character.MapAnimation == Sprites.RunRight) character.I++;
					character.MapAnimationFrame++;

					if (character.Moved)
					{
						if (map == Maps.Town)
						{
							switch (map[character.TileJ][character.TileI])
							{
								case 'i':
									Console.Clear();
									Console.WriteLine();
									Console.WriteLine(" You enter the inn and stay the night...");
									Console.WriteLine();
									Console.WriteLine(" ZzzZzzZzz...");
									Console.WriteLine();
									Console.WriteLine(" Your health is restored.");
									Console.WriteLine();
									Console.Write(" Press [enter] to continue...");
									character.Health = character.MaxHealth;
									if (!PressEnterToContiue())
									{
										return;
									}
									break;
								case 's':
									Console.Clear();
									Console.WriteLine();
									Console.WriteLine(" You enter the store...");
									Console.WriteLine();
									Console.WriteLine(" \"My store isn't ready yet! Get out!,\" yelled the shop keeper.");
									Console.WriteLine();
									Console.Write(" Press [enter] to continue...");
									character.Health = character.MaxHealth;
									if (!PressEnterToContiue())
									{
										return;
									}
									break;
								case 'c':
									OpenChest();
									break;
								case '1':
									map = Maps.Field;
									{
										var (i, j) = FindTileInMap(map, '0').Value;
										character.I = i * 7;
										character.J = j * 4;
									}
									character.Moved = false;
									break;
							}
						}
						else if (map == Maps.Field)
						{
							switch (map[character.TileJ][character.TileI])
							{
								case '0':
									map = Maps.Town;
									{
										var (i, j) = FindTileInMap(map, '1').Value;
										character.I = i * 7;
										character.J = j * 4;
									}
									character.Moved = false;
									break;
								case '2':
									map = Maps.Castle;
									{
										var (i, j) = FindTileInMap(map, '1').Value;
										character.I = i * 7;
										character.J = j * 4;
									}
									character.Moved = false;
									break;
								case 'c':
									OpenChest();
									break;
							}
						}
						else if (map == Maps.Castle)
						{
							switch (map[character.TileJ][character.TileI])
							{
								case 'c':
									OpenChest();
									break;
								case '1':
									map = Maps.Field;
									{
										var (i, j) = FindTileInMap(map, '2').Value;
										character.I = i * 7;
										character.J = j * 4;
									}
									character.Moved = false;
									break;
							}
						}
					}

					// handle user input
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
									if (IsValidCharacterMapTile(tileI, tileJ))
									{
										switch (key)
										{
											case ConsoleKey.UpArrow or ConsoleKey.W:
												character.J--;
												character.MapAnimation = Sprites.RunUp;
												break;
											case ConsoleKey.DownArrow or ConsoleKey.S:
												character.J++;
												character.MapAnimation = Sprites.RunDown;
												break;
											case ConsoleKey.LeftArrow or ConsoleKey.A:
												character.MapAnimation = Sprites.RunLeft;
												break;
											case ConsoleKey.RightArrow or ConsoleKey.D:
												character.MapAnimation = Sprites.RunRight;
												break;
										}
									}
								}
								break;
							case ConsoleKey.Enter:
								Console.Clear();
								Console.WriteLine();
								Console.WriteLine(" Status");
								Console.WriteLine();
								Console.WriteLine($" Level:      {character.Level}");
								Console.WriteLine($" Experience: {character.Experience}/{character.ExperienceToNextLevel}");
								Console.WriteLine($" Health:     {character.Health}/{character.MaxHealth}");
								Console.WriteLine($" Gold:       {character.Gold}");
								Console.WriteLine();
								Console.Write(" Press [enter] to continue...");
								if (!PressEnterToContiue())
								{
									return;
								}
								break;
							case ConsoleKey.Escape:
								Console.Clear();
								Console.WriteLine("Role Playing Game was closed.");
								return;
						}
					}
					RenderWorldMapView();

					// frame rate control
					// world map is crrently targeting 20 frames per second
					DateTime now = DateTime.Now;
					TimeSpan sleep = TimeSpan.FromMilliseconds(50) - (now - previoiusRender);
					if (sleep > TimeSpan.Zero)
					{
						Thread.Sleep(sleep);
					}
					previoiusRender = DateTime.Now;
				}
			}
			finally
			{
				Console.CursorVisible = true;
			}
		}

		static bool IsValidCharacterMapTile(int tileI, int tileJ)
		{
			if (tileJ < 0 || tileJ >= map.Length || tileI < 0 || tileI >= map[tileJ].Length)
			{
				return false;
			}
			return map[tileJ][tileI] switch
			{
				' ' => true,
				'i' => true,
				's' => true,
				'c' => true,
				'e' => true,
				'1' => true,
				'0' => true,
				'g' => true,
				'2' => true,
				'X' => true,
				'k' => true,
				_ => false,
			};
		}

		static bool PressEnterToContiue()
		{
		GetInput:
			ConsoleKey key = Console.ReadKey(true).Key;
			switch (key)
			{
				case ConsoleKey.Enter:
					return true;
				case ConsoleKey.Escape:
					Console.Clear();
					Console.WriteLine("Role Playing Game was closed.");
					return false;
				default: goto GetInput;
			}
		}

		static void OpenChest()
		{
			character.Gold++;
			map[character.TileJ][character.TileI] = 'e';
			Console.Clear();
			Console.WriteLine();
			Console.WriteLine(" You found a chest! You open it and find some gold. :)");
			Console.WriteLine();
			Console.WriteLine($" Gold: {character.Gold}");
			Console.WriteLine();
			Console.Write(" Press [enter] to continue...");
			character.Health = character.MaxHealth;
			if (!PressEnterToContiue())
			{
				return;
			}
		}

		static string GetMapTileRender(int tileI, int tileJ)
		{
			if (tileJ < 0 || tileJ >= map.Length || tileI < 0 || tileI >= map[tileJ].Length)
			{
				if (map == Maps.Field) return Sprites.Mountain;
				return Sprites.Open;
			}

			switch (map[tileJ][tileI])
			{
				case 'w': return Sprites.Water;
				case 'W': return Sprites.Wall_0000;
				case 'b': return Sprites.Building;
				case 't': return Sprites.Tree;
				case ' ' or 'X': return Sprites.Open;
				case 'i': return Sprites.Inn;
				case 's': return Sprites.Store;
				case 'f': return Sprites.Fence;
				case 'c': return Sprites.Chest;
				case 'e': return Sprites.EmptyChest;
				case 'B': return Sprites.Barrels1;
				case '1': return tileJ < Maps.Town.Length / 2 ? Sprites.ArrowUp : Sprites.ArrowDown;
				case 'm': return Sprites.Mountain;
				case '0': return Sprites.Town;
				case 'g': return Sprites.Guard;
				case '2': return Sprites.Castle;
				case 'p': return Sprites.Mountain2;
				case 'T': return Sprites.Tree2;
				case 'k': return Sprites.King;
				default: return Sprites.Error;
			}
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

		RestartRender:

			int width = Console.WindowWidth;
			int height = Console.WindowHeight;

			if (OperatingSystem.IsWindows())
			{
				try
				{
					if (Console.BufferHeight != height)
					{
						Console.BufferHeight = height;
					}
					if (Console.BufferWidth != width)
					{
						Console.BufferWidth = width;
					}
				}
				catch (ArgumentOutOfRangeException)
				{
					Console.Clear();
					goto RestartRender;
				}
			}

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
						if (i < width - 1 && character >= 0 && line >= 0 && line < text.Length && character < text[line].Length)
						{
							char ch = text[line][character];
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
					if (i > midWidth - 4 && i < midWidth + 4 && j > midHeight - 2 && j < midHeight + 3)
					{
						int ci = i - (midWidth - 3);
						int cj = j - (midHeight - 1);
						string characterMapRender = character.Render;
						sb.Append(characterMapRender[cj * 8 + ci]);
						continue;
					}

					// tiles

					// compute the map location that this screen pixel represents
					int mapI = i - midWidth  + character.I + 3;
					int mapJ = j - midHeight + character.J + 1;

					// compute the coordinates of the tile
					int tileI = mapI < 0 ? (mapI - 6) / 7 : mapI / 7;
					int tileJ = mapJ < 0 ? (mapJ - 3) / 4 : mapJ / 4;

					// compute the coordinates of the pixel within the tile
					int pixelI = mapI < 0 ? 6 + ((mapI + 1) % 7) : (mapI % 7);
					int pixelJ = mapJ < 0 ? 3 + ((mapJ + 1) % 4) : (mapJ % 4);

					// render pixel from map tile
					string tileRender = GetMapTileRender(tileI, tileJ);
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
	}
}

public enum CharacterMapAnimation
{
	IdleLeft,
	IdleRight,
	IdleUp,
	IdleDown,
	RunningLeft,
	RunningRight,
	RunningUp,
	RunningDown,
}
