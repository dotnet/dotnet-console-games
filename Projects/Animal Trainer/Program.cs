namespace Animal_Trainer;

public partial class Program
{
	public static void Main()
	{
		Encoding encoding = Console.OutputEncoding;
		try
		{

			Console.CursorVisible = false;
			Console.OutputEncoding = Encoding.UTF8;
			while (gameRunning)
			{
				UpdateCharacter();
				HandleMapUserInput();
				if (gameRunning)
				{
					Renderer.RenderWorldMapView();
				}
			}
		}
		finally
		{
			Console.OutputEncoding = encoding;
			Console.Clear();
			Console.WriteLine("Animal Trainer was closed.");
			Console.CursorVisible = true;
		}
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
			case '0': Maps.TransitionMapToTown(); break;
			case '1': Maps.TransitionMapToField(); break;
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
}
