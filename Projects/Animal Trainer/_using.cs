global using System;
global using System.Linq;
global using System.Text;
global using static Animal_Trainer._using;

namespace Animal_Trainer;

public static class _using
{
	public static Character character = new();
	public static char[][] map = Maps.PaletTown;
	public static DateTime previoiusRender = DateTime.Now;
	public static bool gameRunning = true;

	public static readonly string[] maptext = new[]
	{
		"Move: arrow keys or (w, a, s, d)",
		"Check Status: [enter]",
		"Quit: [escape]",
	};

	static _using()
	{
		map = Maps.PaletTown;
		var (i, j) = Maps.FindTileInMap(map, 'X')!.Value;
		character = new()
		{
			I = i * 7,
			J = j * 5,
			MapAnimation = Sprites.IdlePlayer,
		};
	}
}
