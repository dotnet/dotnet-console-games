using System;
using System.Text;
using System.Threading.Tasks;

namespace Website.Games.Drive;

public class Drive
{
	public readonly BlazorConsole Console = new();
	public BlazorConsole OperatingSystem;

	public Drive()
	{
		OperatingSystem = Console;
	}

	public async Task Run()
	{
		int width = 50;
		int height = 30;

		int windowWidth;
		int windowHeight;
		char[,] scene;
		int score = 0;
		int carPosition;
		int carVelocity;
		bool gameRunning;
		bool keepPlaying = true;
		bool consoleSizeError = false;
		int previousRoadUpdate = 0;

		Console.CursorVisible = false;
		try
		{
			Initialize();
			await LaunchScreen();
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
					await HandleInput();
					Update();
					await Render();
					if (gameRunning)
					{
						await Console.RefreshAndDelay(TimeSpan.FromMilliseconds(33));
					}
				}
				if (keepPlaying)
				{
					await GameOverScreen();
				}
			}
			await Console.Clear();
			if (consoleSizeError)
			{
				await Console.WriteLine("Console/Terminal window is too small.");
				await Console.WriteLine($"Minimum size is {width} width x {height} height.");
				await Console.WriteLine("Increase the size of the console window.");
			}
			await Console.WriteLine("Drive was closed.");
			await Console.Refresh();
		}
		finally
		{
			Console.CursorVisible = true;
			await Console.Refresh();
		}

		void Initialize()
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

		async Task LaunchScreen()
		{
			await Console.Clear();
			await Console.WriteLine("This is a driving game.");
			await Console.WriteLine();
			await Console.WriteLine("Stay on the road!");
			await Console.WriteLine();
			await Console.WriteLine("Use A, W, and D to control your velocity.");
			await Console.WriteLine();
			await Console.Write("Press [enter] to start...");
			await PressEnterToContinue();
		}

		void InitializeScene()
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

		async Task Render()
		{
			StringBuilder stringBuilder = new(width * height);
			for (int i = height - 1; i >= 0; i--)
			{
				for (int j = 0; j < width; j++)
				{
					if (i is 1 && j == carPosition)
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
				if (i > 0)
				{
					stringBuilder.AppendLine();
				}
			}
			await Console.SetCursorPosition(0, 0);
			await Console.Write(stringBuilder);
		}

		async Task HandleInput()
		{
			while (await Console.KeyAvailable())
			{
				ConsoleKey key = (await Console.ReadKey(true)).Key;
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
						await Console.ReadLine();
						break;
				}
			}
		}

		async Task GameOverScreen()
		{
			await Console.SetCursorPosition(0, 0);
			await Console.WriteLine("Game Over");
			await Console.WriteLine($"Score: {score}");
			await Console.WriteLine($"Play Again (Y/N)?");
		GetInput:
			ConsoleKey key = (await Console.ReadKey(true)).Key;
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

		void Update()
		{
			for (int i = 0; i < height - 1; i++)
			{
				for (int j = 0; j < width; j++)
				{
					scene[i, j] = scene[i + 1, j];
				}
			}
			int roadUpdate =
				Random.Shared.Next(5) < 4 ? previousRoadUpdate :
				Random.Shared.Next(3) - 1;
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
			if (carPosition < 0 || carPosition >= width || scene[1, carPosition] is not ' ')
			{
				gameRunning = false;
			}
			score++;
		}

		async Task PressEnterToContinue()
		{
		GetInput:
			ConsoleKey key = (await Console.ReadKey(true)).Key;
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
}
