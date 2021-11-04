using System;
using System.Text;
using System.Threading;

class Program
{
	static readonly int width = 50;
	static readonly int height = 30;

	static int windowWidth;
	static int windowHeight;
	static readonly Random random = new();
	static char[,] scene;
	static int score;
	static int carPosition;
	static int carVelocity;
	static bool gameRunning;
	static bool keepPlaying = true;
	static bool consoleSizeError = false;
	static int previousRoadUpdate;

	static void Main()
	{
		Console.CursorVisible = false;
		try
		{
			Initialize();
			LaunchScreen();
			while (keepPlaying)
			{
				InitializeScene();
				while (gameRunning)
				{
					if (Console.WindowHeight < height || Console.WindowWidth < width)
					{
						consoleSizeError = true;
						keepPlaying = false;
						break;
					}
					HandleInput();
					Update();
					Render();
					if (gameRunning)
					{
						Thread.Sleep(TimeSpan.FromMilliseconds(33));
					}
				}
				if (keepPlaying)
				{
					GameOverScreen();
				}
			}
			Console.Clear();
			if (consoleSizeError)
			{
				Console.WriteLine("Console/Terminal window is too small.");
				Console.WriteLine($"Minimum size is {width} width x {height} height.");
				Console.WriteLine("Increase the size of the console window.");
			}
			Console.WriteLine("Drive was closed.");
		}
		finally
		{
			Console.CursorVisible = true;
		}
	}

	static void Initialize()
	{
		windowWidth = Console.WindowWidth;
		windowHeight = Console.WindowHeight;
		if (OperatingSystem.IsWindows())
		{
			if (windowWidth < width && OperatingSystem.IsWindows())
			{
				windowWidth = Console.WindowWidth = width + 1;
			}
			if (windowHeight < height && OperatingSystem.IsWindows())
			{
				windowHeight = Console.WindowHeight = height + 1;
			}
			Console.BufferWidth = windowWidth;
			Console.BufferHeight = windowHeight;
		}
	}

	static void LaunchScreen()
	{
		Console.Clear();
		Console.WriteLine("This is a driving game.");
		Console.WriteLine();
		Console.WriteLine("Stay on the road!");
		Console.WriteLine();
		Console.WriteLine("Use A, W, and D to control your velocity.");
		Console.WriteLine();
		Console.Write("Press [enter] to start...");
		PressEnterToContinue();
	}

	static void InitializeScene()
	{
		const int roadWidth = 10;
		gameRunning = true;
		carPosition = width / 2;
		carVelocity = 0;
		int leftEdge = (width - roadWidth) / 2;
		int rightEdge = leftEdge + roadWidth + 1;
		scene = new char[height, width];
		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if (j < leftEdge || j > rightEdge)
				{
					scene[i, j] = '.';
				}
				else
				{
					scene[i, j] = ' ';
				}
			}
		}
	}

	static void Render()
	{
		StringBuilder stringBuilder = new(width * height);
		for (int i = height - 1; i >= 0; i--)
		{
			for (int j = 0; j < width; j++)
			{
				if (i == 1 && j == carPosition)
				{
					stringBuilder.Append(
						!gameRunning ? 'X' :
						carVelocity < 0 ? '<' :
						carVelocity > 0 ? '>' :
						'^');
				}
				else
				{
					stringBuilder.Append(scene[i, j]);
				}
			}
			stringBuilder.AppendLine();
		}
		Console.SetCursorPosition(0, 0);
		Console.Write(stringBuilder);
	}

	static void HandleInput()
	{
		while (Console.KeyAvailable)
		{
			ConsoleKey key = Console.ReadKey(true).Key;
			switch (key)
			{
				case ConsoleKey.A or ConsoleKey.LeftArrow:
					carVelocity = -1;
					break;
				case ConsoleKey.D or ConsoleKey.RightArrow:
					carVelocity = +1;
					break;
				case ConsoleKey.W or ConsoleKey.UpArrow or ConsoleKey.S or ConsoleKey.DownArrow:
					carVelocity = 0;
					break;
				case ConsoleKey.Escape:
					gameRunning = false;
					keepPlaying = false;
					break;
				case ConsoleKey.Enter:
					Console.ReadLine();
					break;
			}
		}
	}

	static void GameOverScreen()
	{
		Console.SetCursorPosition(0, 0);
		Console.WriteLine("Game Over");
		Console.WriteLine($"Score: {score}");
		Console.WriteLine($"Play Again (Y/N)?");
	GetInput:
		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.Y:
				keepPlaying = true;
				break;
			case ConsoleKey.N or ConsoleKey.Escape:
				keepPlaying = false;
				break;
			default:
				goto GetInput;
		}
	}

	static void Update()
	{
		for (int i = 0; i < height - 1; i++)
		{
			for (int j = 0; j < width; j++)
			{
				scene[i, j] = scene[i + 1, j];
			}
		}
		int roadUpdate = 
			random.Next(5) < 4 ? previousRoadUpdate :
			random.Next(3) - 1;
		if (roadUpdate is -1 && scene[height - 1, 0] is ' ') roadUpdate = 1;
		if (roadUpdate is 1 && scene[height - 1, width - 1] is ' ') roadUpdate = -1;
		switch (roadUpdate)
		{
			case -1: // left
				for (int i = 0; i < width - 1; i++)
				{
					scene[height - 1, i] = scene[height - 1, i + 1];
				}
				scene[height - 1, width - 1] = '.';
				break;
			case 1: // right
				for (int i = width - 1; i > 0; i--)
				{
					scene[height - 1, i] = scene[height - 1, i - 1];
				}
				scene[height - 1, 0] = '.';
				break;
		}
		previousRoadUpdate = roadUpdate;
		carPosition += carVelocity;
		if (scene[1, carPosition] is not ' ')
		{
			gameRunning = false;
		}
		score++;
	}

	static void PressEnterToContinue()
	{
	GetInput:
		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key)
		{
			case ConsoleKey.Enter:
				break;
			case ConsoleKey.Escape:
				keepPlaying = false;
				break;
			default: goto GetInput;
		}
	}
}