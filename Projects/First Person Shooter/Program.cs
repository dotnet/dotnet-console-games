using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

PlayAgain:
bool closeRequested = false;
bool screenLargeEnough = true;
int screenWidth = 120;
int screenHeight = 40;
float fov = 3.14159f / 4.0f;
float depth = 16.0f;
float speed = 5.0f;
float rotationSpeed = 0.28f;
int score = 0;
float fps = default;
bool mapVisible = true;
bool statsVisible = true;
Weapon equippedWeapon = Weapon.Pistol;
TimeSpan pistolShootAnimationTime = TimeSpan.FromSeconds(0.2f);
TimeSpan shotgunShootAnimationTime = TimeSpan.FromSeconds(0.5f);
TimeSpan gameTime = TimeSpan.FromSeconds(60);
char[,] screen = new char[screenWidth, screenHeight];
float[,] depthBuffer = new float[screenWidth, screenHeight];
List<(float X, float Y)> enemies = new()
{
	(13.5f, 09.5f),
};
bool gameOver = false;
bool backToMenu = false;

string[] map =
[
	// (0,0)              (+,0)
	"███████████████████████████",
	"█              ███        █",
	"█     █         █         █",
	"█     █                ██ █",
	"█ █████    █              █",
	"█                         █",
	"█                 ███     █",
	"█    ██                   █",
	"█           ███           █",
	"█                         █",
	"█                    ██████",
	"█    ███     ^            █",
	"█                         █",
	"███████████████████████████",
	// (0,+)              (+,+)
];

float playerA = default;
float playerX = default;
float playerY = default;
for (int i = 0; i < map.Length; i++)
{
	for (int j = 0; j < map[i].Length; j++)
	{
		if (map[i][j] is '^' or '<' or '>' or 'v')
		{
			playerY = i + .5f;
			playerX = j + .5f;
			playerA = map[i][j] switch
			{
				'^' => 4.71f,
				'>' => 0.00f,
				'<' => 3.14f,
				'v' => 1.57f,
				_ => throw new NotImplementedException(),
			};
		}
	}
}

string[] enemySprite1 =
[
	"!!!!╭─────╮!!!!",
	"!(O)│ ‾o‾ │(O)!",
	"╭─╨─╯╔═══╗╰─╨─╮",
	"│ ╭╮╔╝   ╚╗╭╮ │",
	"╰─╯╔╝     ╚╗╰─╯",
	"!!!╚╗     ╔╝!!!",
	"!!╭╯╚╗   ╔╝╰╮!!",
	"!!│ ╭╚═══╝╮ │!!",
	"!!╰─╯!!!!!╰─╯!!",
];

string[] enemySprite2 =
[
	"!!!!╭───────╮!!!!",
	"!(O)│  ‾o‾  │(O)!",
	"╭─╨─╯ ╔═══╗ ╰─╨─╮",
	"│ ╭─╮╔╝   ╚╗╭─╮ │",
	"╰─╯!╔╝     ╚╗!╰─╯",
	"!!!!║       ║!!!!",
	"!!!!╚╗     ╔╝!!!!",
	"!!!╭╯╚╗   ╔╝╰╮!!!",
	"!!!│ ╭╚═══╝╮ │!!!",
	"!!!╰─╯!!!!!╰─╯!!!",
];

string[] enemySprite3 =
[
	"!!╔═╗╭─────────╮╔═╗!!",
	"!!║O║│   - -   │║O║!!",
	"!!╚╦╝│    O    │╚╦╝!!",
	"╭──╨─╯ ╔═════╗ ╰─╨──╮",
	"│  ╭─╮╔╝     ╚╗╭─╮  │",
	"│  │!╔╝       ╚╗!│  │",
	"╰──╯!║         ║!╰──╯",
	"!!!!!║         ║!!!!!",
	"!!!!!╚╗       ╔╝!!!!!",
	"!!!╭─╯╚╗     ╔╝╰─╮!!!",
	"!!!│  ╭╚═════╝╮  │!!!",
	"!!!│  │!!!!!!!│  │!!!",
	"!!!╰──╯!!!!!!!╰──╯!!!",
];

string[] enemySprite4 =
[
	"!!╔═╗!╭──────────╮!╔═╗!!",
	"!!║O║!│    - -   │!║O║!!",
	"!!╚╦╝!│     O    │!╚╦╝!!",
	"╭──╨──╯ ╔══════╗ ╰──╨──╮",
	"│      ╔╝      ╚╗      │",
	"│   ╭─╔╝        ╚╗─╮   │",
	"╰───╯╔╝          ╚╗╰───╯",
	"!!!!!║            ║!!!!!",
	"!!!!!╚╗          ╔╝!!!!!",
	"!!!╭──╚╗        ╔╝──╮!!!",
	"!!!│   ╚╗      ╔╝   │!!!",
	"!!!│   ╭╚══════╝╮   │!!!",
	"!!!│   │!!!!!!!!│   │!!!",
	"!!!╰───╯!!!!!!!!╰───╯!!!",
];

string[] enemySprite5 =
[
	"!╔═══╗╭────────────╮╔═══╗!",
	"!║ O ║│    ── ──   │║ O ║!",
	"!╚═╦═╝│      O     │╚═╦═╝!",
	"╭──╨──╯ ╔════════╗ ╰──╨──╮",
	"│      ╔╝        ╚╗      │",
	"│   ╭─╔╝          ╚╗─╮   │",
	"│   │╔╝            ╚╗│   │",
	"╰───╯║              ║╰───╯",
	"!!!!!║              ║!!!!!",
	"!!!!!║              ║!!!!!",
	"!!!!!╚╗            ╔╝!!!!!!",
	"!!╭───╚╗          ╔╝───╮!!",
	"!!│    ╚╗        ╔╝    │!!",
	"!!│    ╭╚════════╝╮    │!!",
	"!!│    │!!!!!!!!!!│    │!!",
	"!!│    │!!!!!!!!!!│    │!!",
	"!!╰────╯!!!!!!!!!!╰────╯!!",
];

string[] enemySprite6 =
[
	"!╔═══╗ ╭─────────────╮ ╔═══╗!",
	"!║ O ║ │    ── ──    │ ║ O ║!",
	"!╚═╦═╝ │      O      │ ╚═╦═╝!",
	"╭──╨───╯ ╔═════════╗ ╰───╨──╮",
	"│       ╔╝         ╚╗       │",
	"│    ╭─╔╝           ╚╗─╮    │",
	"│    │╔╝             ╚╗│    │",
	"╰────╯║               ║╰────╯",
	"!!!!!!║               ║!!!!!!",
	"!!!!!!║               ║!!!!!!",
	"!!!!!!║               ║!!!!!!",
	"!!!!!!╚╗             ╔╝!!!!!!",
	"!!╭────╚╗           ╔╝────╮!!",
	"!!│     ╚╗         ╔╝     │!!",
	"!!│     ╭╚═════════╝╮     │!!",
	"!!│     │!!!!!!!!!!!│     │!!",
	"!!│     │!!!!!!!!!!!│     │!!",
	"!!╰─────╯!!!!!!!!!!!╰─────╯!!",
];

string[] enemySprite7 =
[
	"!!╔═══╗!╭───────────────╮!╔═══╗!!",
	"!!║ O ║!│     ── ──     │!║ O ║!!",
	"!!╚═╦═╝!│       O       │!╚═╦═╝!!",
	"╭───╨───╯ ╔═══════════╗ ╰───╨───╮",
	"│        ╔╝           ╚╗        │",
	"│     ╭─╔╝             ╚╗─╮     │",
	"│     │╔╝               ╚╗│     │",
	"│     │║                 ║│     │",
	"╰─────╯║                 ║╰─────╯",
	"!!!!!!!║                 ║!!!!!!!",
	"!!!!!!!║                 ║!!!!!!!",
	"!!!!!!!║                 ║!!!!!!!",
	"!!!!!!!╚╗               ╔╝!!!!!!!",
	"!!!╭────╚╗             ╔╝────╮!!!",
	"!!!│     ╚╗           ╔╝     │!!!",
	"!!!│     ╭╚═══════════╝╮     │!!!",
	"!!!│     │!!!!!!!!!!!!!│     │!!!",
	"!!!│     │!!!!!!!!!!!!!│     │!!!",
	"!!!│     │!!!!!!!!!!!!!│     │!!!",
	"!!!╰─────╯!!!!!!!!!!!!!╰─────╯!!!",
];

string[] enemySprite8 =
[
	"!!!!!!!!!!╭───────────────────╮!!!!!!!!!!",
	"!!╔═══╗!!!│       ── ──       │!!!╔═══╗!!",
	"!!║ O ║!!!│         O         │!!!║ O ║!!",
	"!!╚═╦═╝!!!│                   │!!!╚═╦═╝!!",
	"╭───╨─────╯ ╔═══════════════╗ ╰─────╨───╮",
	"│          ╔╝               ╚╗          │",
	"│      ╭──╔╝                 ╚╗──╮      │",
	"│      │!╔╝                   ╚╗!│      │",
	"│      │╔╝                     ╚╗│      │",
	"│      │║                       ║│      │",
	"│      │║                       ║│      │",
	"╰──────╯║                       ║╰──────╯",
	"!!!!!!!!║                       ║!!!!!!!",
	"!!!!!!!!║                       ║!!!!!!!",
	"!!!!!!!!║                       ║!!!!!!!",
	"!!!!!!!!║                       ║!!!!!!!",
	"!!!!!!!!╚╗                     ╔╝!!!!!!!",
	"!!!!╭────╚╗                   ╔╝────╮!!!",
	"!!!!│     ╚╗                 ╔╝     │!!!",
	"!!!!│      ╚╗               ╔╝      │!!!",
	"!!!!│      ╭╚═══════════════╝╮      │!!!",
	"!!!!│      │!!!!!!!!!!!!!!!!!│      │!!!",
	"!!!!│      │!!!!!!!!!!!!!!!!!│      │!!!",
	"!!!!│      │!!!!!!!!!!!!!!!!!│      │!!!",
	"!!!!│      │!!!!!!!!!!!!!!!!!│      │!!!",
	"!!!!╰──────╯!!!!!!!!!!!!!!!!!╰──────╯!!!",
];

string[] playerPistol =
[
	"!!!╔═╗!!!",
	"!!!║ ║!!!",
	"╭─╮║ ║!!!",
	"│ │╠═╣╭─╮",
	"│ ╰───╯ │",
	"│    ───╯",
	"│    ───╯",
	"╰╮  ╭──╯!",
];

string[] playerPistolShoot =
[
	@"!!!\V/!!!",
	@"!!!╔═╗!!!",
	@"!!!║ ║!!!",
	@"╭─╮║ ║!!!",
	@"│ │╠═╣╭─╮",
	@"│ ╰───╯ │",
	@"│    ───╯",
	@"│    ───╯",
];

string[] playerShotgun =
[
	"!!!!!╔═╦═╗!!",
	"!!!!!║ ║ ║!!",
	"!!!!!║ ║ ║!!",
	"!!!!╭║ ║ ║╮!",
	"!!!!|║ ║ ║╮!",
	"!!!!|║ ║ ║╮!",
	"!!!/ ║ ║ ║─╮",
	"!!/  ╭─╮ ║ │",
	"!/  /│ ├─╯ │",
	"/  /!╰╮  ╭─╯",
];

string[] playerShotgunShoot =
[
	@"!!!!\\V|V//!",
	@"!!!!\\V|V//!",
	@"!!!!!╔═╦═╗!!",
	@"!!!!!║ ║ ║!!",
	@"!!!!!║ ║ ║!!",
	@"!!!!╭║ ║ ║╮!",
	@"!!!!|║ ║ ║╮!",
	@"!!!!|║ ║ ║╮!",
	@"!!!/ ║ ║ ║─╮",
	@"!!/  ╭─╮ ║ │",
	@"!/  /│ ├─╯ │",
];

int consoleWidth = Console.WindowWidth;
int consoleHeight = Console.WindowHeight;
Stopwatch gameTimeStopwatch;
Stopwatch stopwatch = Stopwatch.StartNew();
Stopwatch? stopwatchShoot = null;
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();
Console.WriteLine("""
	First Person Shooter

	This is a first person shooter target range. You have
	60 seconds to shoot as many targets as you can. Every
	time you shoot a target a new one will spawn somewhere
	in the arena. Good Luck!

	Controls
	- W, A, S, D: move/look
	- Spacebar: shoot
	- 1: equip pistol
	- 2: equip shotgun
	- M: toggle map
	- Tab: toggle stats
	- Escape: exit

	Press any key to begin...
	""");
if (Console.ReadKey(true).Key is not ConsoleKey.Escape)
{
	gameTimeStopwatch = Stopwatch.StartNew();
	Console.Clear();
	stopwatch = Stopwatch.StartNew();
	while (!closeRequested)
	{
		Update();
		if (backToMenu)
		{
			backToMenu = false;
			goto PlayAgain;
		}
		Render();
	}
}
Console.Clear();
Console.Write("First Person Shooter was closed.");

void Update()
{
	if (gameTimeStopwatch.Elapsed > gameTime)
	{
		gameOver = true;
		gameTimeStopwatch.Stop();
	}

	bool u = false;
	bool d = false;
	bool l = false;
	bool r = false;

	while (Console.KeyAvailable)
	{
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter:
				backToMenu = true;
				break;
			case ConsoleKey.Escape: closeRequested = true; return;
			case ConsoleKey.M:
				if (!gameOver)
				{
					mapVisible = !mapVisible;
				}
				break;
			case ConsoleKey.Tab:
				if (!gameOver)
				{
					statsVisible = !statsVisible;
				}
				break;
			case ConsoleKey.D1 or ConsoleKey.NumPad1:
				if (!gameOver && PlayerIsNotBusy())
				{
					equippedWeapon = Weapon.Pistol;
				}
				break;
			case ConsoleKey.D2 or ConsoleKey.NumPad2:
				if (!gameOver && PlayerIsNotBusy())
				{
					equippedWeapon = Weapon.Shotgun;
				}
				break;
			case ConsoleKey.Spacebar:
				if (!gameOver && PlayerIsNotBusy())
				{
					List<(float X, float Y)> defeatedEnemies = [];
					bool spawnEnemy = false;
					foreach (var enemy in enemies)
					{
						float angle = (float)Math.Atan2(enemy.Y - playerY, enemy.X - playerX);
						if (angle < 0) angle += 2f * (float)Math.PI;
						float distance = Vector2.Distance(new(playerX, playerY), new(enemy.X, enemy.Y));

						float fovAngleA = playerA - fov / 2;
						if (fovAngleA < 0) fovAngleA += 2 * (float)Math.PI;

						float diff = angle < fovAngleA && fovAngleA - 2f * (float)Math.PI + fov > angle ? angle + 2f * (float)Math.PI - fovAngleA : angle - fovAngleA;
						float ratio = diff / fov;
						int enemyScreenX = (int)(screenWidth * ratio);

						string[] enemySprite = distance switch
						{
							<= 01f => enemySprite8,
							<= 02f => enemySprite7,
							<= 03f => enemySprite6,
							<= 04f => enemySprite5,
							<= 05f => enemySprite4,
							<= 06f => enemySprite3,
							<= 07f => enemySprite2,
							_ => enemySprite1
						};

						int halfEnemyWidth = enemySprite[0].Length / 2;
						int enemyMinScreenX = enemyScreenX - halfEnemyWidth;
						int enemyMaxScreenX = enemyScreenX + halfEnemyWidth;
						int screenWidthMid = screenWidth / 2;

						switch (equippedWeapon)
						{
							case Weapon.Pistol:
								if (enemyMinScreenX <= screenWidthMid && screenWidthMid <= enemyMaxScreenX)
								{
									defeatedEnemies.Add(enemy);
									spawnEnemy = true;
								}
								break;
							case Weapon.Shotgun:
								if (enemyMinScreenX <= screenWidthMid && screenWidthMid <= enemyMaxScreenX)
								{
									defeatedEnemies.Add(enemy);
									spawnEnemy = true;
								}
								break;
							default:
								throw new NotImplementedException();
						}
					}
					foreach (var enemy in defeatedEnemies)
					{
						enemies.Remove(enemy);
						score++;
					}
					if (spawnEnemy)
					{
						SpawnTarget();
					}
					stopwatchShoot = Stopwatch.StartNew();
				}
				break;
			case ConsoleKey.W:
				if (!gameOver)
				{
					u = true;
				}
				break;
			case ConsoleKey.A:
				if (!gameOver)
				{
					l = true;
				}
				break;
			case ConsoleKey.S:
				if (!gameOver)
				{
					d = true;
				}
				break;
			case ConsoleKey.D:
				if (!gameOver)
				{
					r = true;
				}
				break;
		}
	}

	if (consoleWidth != Console.WindowWidth || consoleHeight != Console.WindowHeight)
	{
		Console.Clear();
		consoleWidth = Console.WindowWidth;
		consoleHeight = Console.WindowHeight;
	}

	screenLargeEnough = consoleWidth >= screenWidth && consoleHeight >= screenHeight;
	if (!screenLargeEnough)
	{
		return;
	}

	float elapsedSeconds = (float)stopwatch.Elapsed.TotalSeconds;
	fps = 1.0f / elapsedSeconds;
	stopwatch.Restart();

	if (OperatingSystem.IsWindows())
	{
		u = u || User32_dll.GetAsyncKeyState('W') is not 0 && !gameOver;
		l = l || User32_dll.GetAsyncKeyState('A') is not 0 && !gameOver;
		d = d || User32_dll.GetAsyncKeyState('S') is not 0 && !gameOver;
		r = r || User32_dll.GetAsyncKeyState('D') is not 0 && !gameOver;
	}

	if (l && !r)
	{
		playerA -= (speed * rotationSpeed) * elapsedSeconds;
		if (playerA < 0)
		{
			playerA %= (float)Math.PI * 2;
			playerA += (float)Math.PI * 2;
		}
	}
	if (r && !l)
	{
		playerA += (speed * rotationSpeed) * elapsedSeconds;
		if (playerA > (float)Math.PI * 2)
		{
			playerA %= (float)Math.PI * 2;
		}
	}
	if (u && !d)
	{
		playerX += (float)Math.Cos(playerA) * speed * elapsedSeconds;
		playerY += (float)Math.Sin(playerA) * speed * elapsedSeconds;
		if (map[(int)playerY][(int)playerX] is '█')
		{
			playerX -= (float)Math.Cos(playerA) * speed * elapsedSeconds;
			playerY -= (float)Math.Sin(playerA) * speed * elapsedSeconds;
		}
	}
	if (d && !u)
	{
		playerX -= (float)(Math.Cos(playerA) * speed * elapsedSeconds);
		playerY -= (float)(Math.Sin(playerA) * speed * elapsedSeconds);
		if (map[(int)playerY][(int)playerX] is '█')
		{
			playerX += (float)Math.Cos(playerA) * speed * elapsedSeconds;
			playerY += (float)Math.Sin(playerA) * speed * elapsedSeconds;
		}
	}
}

void Render()
{
	if (!screenLargeEnough)
	{
		Console.CursorVisible = false;
		Console.SetCursorPosition(0, 0);
		Console.WriteLine($"Increase console size...");
		Console.WriteLine($"Current Size: {consoleWidth}x{consoleHeight}");
		Console.WriteLine($"Minimum Size: {screenWidth}x{screenHeight}");
		return;
	}

	for (int y = 0; y < screenHeight; y++)
	{
		for (int x = 0; x < screenWidth; x++)
		{
			depthBuffer[x, y] = float.MaxValue;
		}
	}

	for (int x = 0; x < screenWidth; x++)
	{
		float rayAngle = (playerA - fov / 2.0f) + (x / (float)screenWidth) * fov;

		float stepSize = 0.1f;
		float distanceToWall = 0.0f;

		bool hitWall = false;
		bool boundary = false;

		float eyeX = (float)Math.Cos(rayAngle);
		float eyeY = (float)Math.Sin(rayAngle);

		while (!hitWall && distanceToWall < depth)
		{
			distanceToWall += stepSize;
			int testX = (int)(playerX + eyeX * distanceToWall);
			int testY = (int)(playerY + eyeY * distanceToWall);
			if (testY < 0 || testY >= map.Length || testX < 0 || testX >= map[testY].Length)
			{
				hitWall = true;
				distanceToWall = depth;
			}
			else
			{
				if (map[testY][testX] == '█')
				{
					hitWall = true;
					List<(float, float)> p = new();
					for (int tx = 0; tx < 2; tx++)
					{
						for (int ty = 0; ty < 2; ty++)
						{
							float vy = (float)testY + ty - playerY;
							float vx = (float)testX + tx - playerX;
							float d = (float)Math.Sqrt(vx * vx + vy * vy);
							float dot = (eyeX * vx / d) + (eyeY * vy / d);
							p.Add((d, dot));
						}
					}
					p.Sort((a, b) => a.Item1.CompareTo(b.Item1));
					float fBound = 0.005f;
					if (Math.Acos(p[0].Item2) < fBound) boundary = true;
					if (Math.Acos(p[1].Item2) < fBound) boundary = true;
					if (Math.Acos(p[2].Item2) < fBound) boundary = true;
				}
			}
		}
		int ceiling = (int)((float)(screenHeight / 2.0f) - screenHeight / ((float)distanceToWall));
		int floor = screenHeight - ceiling;

		for (int y = 0; y < screenHeight; y++)
		{
			depthBuffer[x, y] = distanceToWall;

			if (y <= ceiling)
			{
				screen[x, y] = ' ';
			}
			else if (y > ceiling && y <= floor)
			{
				screen[x, y] =
					boundary ? ' ' :
					distanceToWall < depth / 3.00f ? '█' :
					distanceToWall < depth / 1.75f ? '■' :
					distanceToWall < depth / 1.00f ? '▪' :
					' ';
			}
			else
			{
				float b = 1.0f - ((y - screenHeight / 2.0f) / (screenHeight / 2.0f));
				screen[x, y] = b switch
				{
					< 0.20f => '●',
					< 0.40f => '•',
					< 0.60f => '·',
					_ => ' ',
				};
			}
		}
	}

	float fovAngleA = playerA - fov / 2;
	float fovAngleB = playerA + fov / 2;
	if (fovAngleA < 0) fovAngleA += 2 * (float)Math.PI;

	foreach (var enemy in enemies)
	{
		float angle = (float)Math.Atan2(enemy.Y - playerY, enemy.X - playerX);
		if (angle < 0) angle += 2f * (float)Math.PI;
		
		float distance = Vector2.Distance(new(playerX, playerY), new(enemy.X, enemy.Y));

		int ceiling = (int)((float)(screenHeight / 2.0f) - screenHeight / ((float)distance));
		int floor = screenHeight - ceiling;

		string[] enemySprite = distance switch
		{
			<= 01f => enemySprite8,
			<= 02f => enemySprite7,
			<= 03f => enemySprite6,
			<= 04f => enemySprite5,
			<= 05f => enemySprite4,
			<= 06f => enemySprite3,
			<= 07f => enemySprite2,
			_ => enemySprite1
		};

		float diff = angle < fovAngleA && fovAngleA - 2f * (float)Math.PI + fov > angle ? angle + 2f * (float)Math.PI - fovAngleA : angle - fovAngleA;
		float ratio = diff / fov;
		int enemyScreenX = (int)(screenWidth * ratio);
		int enemyScreenY = Math.Min(floor, screen.GetLength(1));

		for (int y = 0; y < enemySprite.Length; y++)
		{
			for (int x = 0; x < enemySprite[y].Length; x++)
			{
				if (enemySprite[y][x] is not '!')
				{
					int screenX = x - enemySprite[y].Length / 2 + enemyScreenX;
					int screenY = y - enemySprite.Length + enemyScreenY;
					if (0 <= screenX && screenX <= screenWidth - 1 && 0 <= screenY && screenY <= screenHeight - 1 && depthBuffer[screenX, screenY] > distance)
					{
						screen[screenX, screenY] = enemySprite[y][x];
						depthBuffer[screenX, screenY] = distance;
					}
				}
			}
		}
	}

	if (statsVisible)
	{
		string[] stats =
		[
			$"x={playerX:0.00}",
			$"y={playerY:0.00}",
			$"a={playerA:0.00}",
			$"fps={fps:0.}",
			$"score={score}",
			$"time={(int)gameTimeStopwatch.Elapsed.TotalSeconds}/{(int)gameTime.TotalSeconds}",
		];
		for (int i = 0; i < stats.Length; i++)
		{
			for (int j = 0; j < stats[i].Length; j++)
			{
				screen[screenWidth - stats[i].Length + j, i] = stats[i][j];
			}
		}
	}

	if (mapVisible)
	{
		for (int y = 0; y < map.Length; y++)
		{
			for (int x = 0; x < map[y].Length; x++)
			{
				screen[x, y] = map[y][x] is '^' or '<' or '>' or 'v' ? ' ' : map[y][x];
			}
		}
		foreach (var enemy in enemies)
		{
			screen[(int)enemy.X, (int)enemy.Y] = 'X';
		}
		screen[(int)playerX, (int)playerY] = playerA switch
		{
			>= 0.785f and < 2.356f => 'v',
			>= 2.356f and < 3.927f => '<',
			>= 3.927f and < 5.498f => '^',
			_ => '>',
		};
	}

	string[] player =
		equippedWeapon is Weapon.Pistol && stopwatchShoot is not null && stopwatchShoot.Elapsed < pistolShootAnimationTime ? playerPistolShoot :
		equippedWeapon is Weapon.Shotgun && stopwatchShoot is not null && stopwatchShoot.Elapsed < shotgunShootAnimationTime ? playerShotgunShoot :
		equippedWeapon is Weapon.Pistol ? playerPistol :
		equippedWeapon is Weapon.Shotgun ? playerShotgun :
		throw new NotImplementedException();
	for (int y = 0; y < player.Length; y++)
	{
		for (int x = 0; x < player[y].Length; x++)
		{
			if (player[y][x] is not '!')
			{
				screen[x + screenWidth / 2 - player[y].Length / 2, screenHeight - player.Length + y] = player[y][x];
			}
		}
	}

	if (gameOver)
	{
		string[] gameOverMessage =
		[
			$"                                        ",
			$"               GAME OVER!               ",
			$"                Score: {score}                ",
			$"   Press [enter] to return to menu...   ",
			$"                                        ",
		];
		int gameOverMessageY = screenHeight / 2 - gameOverMessage.Length / 2;
		foreach (string line in gameOverMessage)
		{
			int gameOverMessageX = screenWidth / 2 - line.Length / 2;
			foreach (char c in line)
			{
				screen[gameOverMessageX, gameOverMessageY] = c;
				gameOverMessageX++;
			}
			gameOverMessageY++;
		}
	}

	StringBuilder render = new();
	for (int y = 0; y < screen.GetLength(1); y++)
	{
		for (int x = 0; x < screen.GetLength(0); x++)
		{
			render.Append(screen[x, y]);
		}
		if (y < screen.GetLength(1) - 1)
		{
			render.AppendLine();
		}
	}
	Console.CursorVisible = false;
	Console.SetCursorPosition(0, 0);
	Console.Write(render);
}

void SpawnTarget()
{
	List<(float X, float Y)> possibleSpawnPoints = [];
	for (int y = 0; y < map.Length; y++)
	{
		for (int x = 0; x < map[y].Length; x++)
		{
			if (map[y][x] is ' ')
			{
				possibleSpawnPoints.Add((x + .5f, y + .5f));
			}
		}
	}
	(float X, float Y) location = possibleSpawnPoints[Random.Shared.Next(possibleSpawnPoints.Count)];
	enemies.Add(location);

}

bool PlayerIsNotBusy() =>
	stopwatchShoot is null || stopwatchShoot.Elapsed > equippedWeapon switch
	{
		Weapon.Pistol => pistolShootAnimationTime,
		Weapon.Shotgun => shotgunShootAnimationTime,
		_ => throw new NotImplementedException(),
	};

partial class User32_dll
{
	[LibraryImport("user32.dll")]
	internal static partial short GetAsyncKeyState(int vKey);
}

enum Weapon
{
	Pistol,
	Shotgun,
}
