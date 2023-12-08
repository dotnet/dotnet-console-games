using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

char[][][] levels =
[
	// '@': starting player position
	// ' ': open space
	// '~': switchable open space
	// '█': wall
	// '#': switchable wall
	// 'X': spikey death
	// '●': goal

	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                    ●          █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█      @@@@@                                                    █".ToCharArray(),
		"█      @@@@@                                                    █".ToCharArray(),
		"█      @@@@@                                                    █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █        ●        █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                 █                           █                 █".ToCharArray(),
		"█                 █                           █                 █".ToCharArray(),
		"█                 █                           █                 █".ToCharArray(),
		"█                 █                           █                 █".ToCharArray(),
		"█                 █                           █                 █".ToCharArray(),
		"█                 █                                             █".ToCharArray(),
		"█      @@@@@      █                                             █".ToCharArray(),
		"█      @@@@@      █                                             █".ToCharArray(),
		"█      @@@@@      █                                             █".ToCharArray(),
		"█                 █                                             █".ToCharArray(),
		"█                 █                                             █".ToCharArray(),
		"█                 █                                             █".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"                  ███████████                   ".ToCharArray(),
		"              ████           █████              ".ToCharArray(),
		"          ████                    ████          ".ToCharArray(),
		"       ███                            ███       ".ToCharArray(),
		"     ██                                  ██     ".ToCharArray(),
		"    █                                      █    ".ToCharArray(),
		"   █                                        █   ".ToCharArray(),
		"  █                                          █  ".ToCharArray(),
		" █                          █                 █ ".ToCharArray(),
		"█                           █                  █".ToCharArray(),
		"█                           █                  █".ToCharArray(),
		"█                         ██                   █".ToCharArray(),
		"█                       ██                     █".ToCharArray(),
		"█         ●           ██                       █".ToCharArray(),
		" █                  ██                        █ ".ToCharArray(),
		"  █               ██                         █  ".ToCharArray(),
		"   █             █                          █   ".ToCharArray(),
		"    █            █                         █    ".ToCharArray(),
		"     ██          █                       ██     ".ToCharArray(),
		"       ███       █   @@@@@            ███       ".ToCharArray(),
		"          ████   █   @@@@@        ████          ".ToCharArray(),
		"              ████   @@@@@   █████              ".ToCharArray(),
		"                  ███████████                   ".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXX                                                        █".ToCharArray(),
		"█XXXXXXX                                                        █".ToCharArray(),
		"█XXXXXXX                                                        █".ToCharArray(),
		"█XXXXXXX          █XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX          █".ToCharArray(),
		"█XXXXXXX          █         ●                                   █".ToCharArray(),
		"█XXXXXXX          █                                             █".ToCharArray(),
		"█XXXXXXX          █                                             █".ToCharArray(),
		"█XXXXXXX          █                                             █".ToCharArray(),
		"█XXXXXX█          █                                             █".ToCharArray(),
		"████████          ███████████████████████████████████████████████".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█                                                               █".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█          █XXXXXXXXXXX█".ToCharArray(),
		"██████████████████████████████████████████          █████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█          █XXXXXXXXXXX█".ToCharArray(),
		"█    @@@@@                                                      █".ToCharArray(),
		"█    @@@@@                                                      █".ToCharArray(),
		"█    @@@@@                                                      █".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                     ●        X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█      @@@@@                                                   X█".ToCharArray(),
		"█      @@@@@                                                   X█".ToCharArray(),
		"█      @@@@@                                                   X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█                                                              X█".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                    ●        X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X     @@@@@                                                   X█".ToCharArray(),
		"█X     @@@@@                                                   X█".ToCharArray(),
		"█X     @@@@@                                                   X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX               ●        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX                        X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X     @@@@@                                                   X█".ToCharArray(),
		"█X     @@@@@                                                   X█".ToCharArray(),
		"█X     @@@@@                                                   X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                    ●        X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                        XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                        XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                        XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                        XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X     @@@@@              XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X     @@@@@              XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X     @@@@@              XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                        XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                        XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"██████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXX                  X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXX                    X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXX                      X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXX             ●          X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXX                          X█".ToCharArray(),
		"█XXXXXXXXXXXXXXX                          XXX█".ToCharArray(),
		"█XXXXXXXXXXXXX                          XXXXX█".ToCharArray(),
		"█XXXXXXXXXXX                          XXXXXXX█".ToCharArray(),
		"█XXXXXXXXX                          XXXXXXXXX█".ToCharArray(),
		"█XXXXXXX                          XXXXXXXXXXX█".ToCharArray(),
		"█XXXXX                          XXXXXXXXXXXXX█".ToCharArray(),
		"█XXX                          XXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                          XXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                        XXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                      XXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X@@@@@               XXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X@@@@@             XXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X@@@@@           XXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"██████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                                         X                   X█".ToCharArray(),
		"█X                                         X                   X█".ToCharArray(),
		"█X                                         X                   X█".ToCharArray(),
		"█X                                         X         ●         X█".ToCharArray(),
		"█X                                         X                   X█".ToCharArray(),
		"█X                                         X                   X█".ToCharArray(),
		"█X                                         X                   X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                   X                                         X█".ToCharArray(),
		"█X                   X                                         X█".ToCharArray(),
		"█X       @@@@@       X                                         X█".ToCharArray(),
		"█X       @@@@@       X                                         X█".ToCharArray(),
		"█X       @@@@@       X                                         X█".ToCharArray(),
		"█X                   X                                         X█".ToCharArray(),
		"█X                   X                                         X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█                                             #                 █".ToCharArray(),
		"█                                             #                 █".ToCharArray(),
		"█                                             #                 █".ToCharArray(),
		"█                                             #                 █".ToCharArray(),
		"█                                             #                 █".ToCharArray(),
		"█                                             #                 █".ToCharArray(),
		"█                                             #                 █".ToCharArray(),
		"█                                             █~~~~~~~~~~~~~~~~~█".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█      @@@@@                                  █                 █".ToCharArray(),
		"█      @@@@@                                  █                 █".ToCharArray(),
		"█      @@@@@                                  █                 █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█                                             █        ●        █".ToCharArray(),
		"█                                             █                 █".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X##############################################################█".ToCharArray(),
		"█X~                                                             █".ToCharArray(),
		"█X~                                                             █".ToCharArray(),
		"█X~                                                             █".ToCharArray(),
		"█X~         █                                                   █".ToCharArray(),
		"█X~         █                                                   █".ToCharArray(),
		"█X~         █                                                   █".ToCharArray(),
		"█X~         █                                                   █".ToCharArray(),
		"█X~         █                                                   █".ToCharArray(),
		"█X~         █                                                   █".ToCharArray(),
		"█X~    ●    █                                                   █".ToCharArray(),
		"█X~         █                                                   █".ToCharArray(),
		"█X~         █                                                   █".ToCharArray(),
		"█████████████████████████████████████████████████             ███".ToCharArray(),
		"█                                               #             ~X█".ToCharArray(),
		"█      @@@@@                                    #             ~X█".ToCharArray(),
		"█      @@@@@                                    #             ~X█".ToCharArray(),
		"█      @@@@@                                    #             ~X█".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                            #########        X█".ToCharArray(),
		"█X                                                    ~        X█".ToCharArray(),
		"█X                                                    ~        X█".ToCharArray(),
		"█X     #####                                          ~        X█".ToCharArray(),
		"█X     # ● #                                          ~        X█".ToCharArray(),
		"█X     #####                                          ~        X█".ToCharArray(),
		"█X                                                    ~        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXX                             ~        X█".ToCharArray(),
		"█X                                                    ~        X█".ToCharArray(),
		"█X                                                    ~        X█".ToCharArray(),
		"█X                                                    ~        X█".ToCharArray(),
		"█X     @@@@@                                          ~        X█".ToCharArray(),
		"█X     @@@@@                                          ~        X█".ToCharArray(),
		"█X     @@@@@                                                   X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█X                                                             X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█████████████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"█████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█XXXXXXXXXX#####XXXXXXXXX#####XXXXXXXXX#####XXXXXXXXXXXX█".ToCharArray(),
		"█XXXXXXXXXX            ~X            #X               XX█".ToCharArray(),
		"█XXXXXXXXXX            ~X            #X             ● XX█".ToCharArray(),
		"█XXXXXXXXXX                                           XX█".ToCharArray(),
		"█X@@@@@         ~X            ~X            ~XXXXXXXXXXX█".ToCharArray(),
		"█X@@@@@         ~X            ~X            ~XXXXXXXXXXX█".ToCharArray(),
		"█X@@@@@         ~X#####XXXXXXXXX~~~~~XXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█████████████████████████████████████████████████████████".ToCharArray(),
	],
	[
		"██████████████████████████████████████████████████████████████████████".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X@@@@@              XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X@@@@@                         XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X@@@@@                                   XXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X                                                 XXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"█X        ~~~~~~~~~                                        XXXXXXXXXX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXX##########                                   XXXX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXX~~~~~~~~~                          XXX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX########                  XX█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX~~~~~~~~          X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX      ●  X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX        X█".ToCharArray(),
		"█XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX█".ToCharArray(),
		"██████████████████████████████████████████████████████████████████████".ToCharArray(),
	],
];

Console.OutputEncoding = Encoding.UTF8;
Stopwatch stopwatch = Stopwatch.StartNew();
bool closeRequested = false;
(int X, int Y) velocity = (0, 0);
int level = 0;
int updatesSinceSlideApplied = 0;
const int slideUpdateFrequency = 2;
int updatesSinceGravityApplied = 0;
const int gravityUpdateFrequency = 1;
Direction gravity = Direction.None;
PlayerState playerState = PlayerState.Neutral;
GameState gameState = GameState.Default;
(int X, int Y) PlayerPosition = GetStartingPlayerPositionFromLevel();

Console.Write("""

	     ██████╗ ██████╗  █████╗ ██╗   ██╗██╗████████╗██╗   ██╗
	    ██╔════╝ ██╔══██╗██╔══██╗██║   ██║██║╚══██╔══╝╚██╗ ██╔╝
	    ██║  ███╗██████╔╝███████║██║   ██║██║   ██║    ╚████╔╝ 
	    ██║   ██║██╔══██╗██╔══██║╚██╗ ██╔╝██║   ██║     ╚██╔╝  
	    ╚██████╔╝██║  ██║██║  ██║ ╚████╔╝ ██║   ██║      ██║   
	     ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝  ╚═══╝  ╚═╝   ╚═╝      ╚═╝   

	    Reach the goal (●) by using the [arrow keys] or [WASD] to
	    manipulate gravity. Watch out for spikes (X). Use the
	    [spacebar] to toggle (#) walls and (~) spaces.

	    Press [escape] to close the game at any time.

	    Press [enter] to begin...
	""");
Console.CursorVisible = false;
PressToContinue();
Console.Clear();

while (!closeRequested)
{
	Update();
	if (closeRequested)
	{
		break;
	}
	Render();
	SleepAfterRender();
}
Console.Clear();

void SleepAfterRender()
{
	TimeSpan sleep = TimeSpan.FromSeconds(1d / 20d) - stopwatch.Elapsed;
	if (sleep > TimeSpan.Zero)
	{
		Thread.Sleep(sleep);
	}
	stopwatch.Restart();
}

(int X, int Y) GetStartingPlayerPositionFromLevel()
{
	for (int i = 0; i < levels[level].Length; i++)
	{
		for (int j = 0; j < levels[level][i].Length; j++)
		{
			if (levels[level][i][j] is '@')
			{
				return (j + 2, i + 1);
			}
		}
	}
	throw new Exception($"Level {level} has no starting position.");
}

void Update()
{
	while (Console.KeyAvailable)
	{
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.W or ConsoleKey.UpArrow:
				if (gravity is not Direction.Up)
				{
					updatesSinceGravityApplied = int.MaxValue;
					gravity = Direction.Up;
				}
				break;
			case ConsoleKey.A or ConsoleKey.LeftArrow:
				if (gravity is not Direction.Left)
				{
					updatesSinceGravityApplied = int.MaxValue;
					gravity = Direction.Left;
				}
				break;
			case ConsoleKey.S or ConsoleKey.DownArrow:
				if (gravity is not Direction.Down)
				{
					updatesSinceGravityApplied = int.MaxValue;
					gravity = Direction.Down;
				}
				break;
			case ConsoleKey.D or ConsoleKey.RightArrow:
				if (gravity is not Direction.Right)
				{
					updatesSinceGravityApplied = int.MaxValue;
					gravity = Direction.Right;
				}
				break;
			case ConsoleKey.Spacebar:
				for (int j = 0; j < levels[level].Length; j++)
				{
					for (int k = 0; k < levels[level][j].Length; k++)
					{
						switch (levels[level][j][k])
						{
							case '#': levels[level][j][k] = '~'; break;
							case '~': levels[level][j][k] = '#'; break;
						}
					}
				}
				break;
			case ConsoleKey.Escape: closeRequested = true; return;
		}
	}

	if (updatesSinceGravityApplied >= gravityUpdateFrequency)
	{
		switch (gravity)
		{
			case Direction.Up:    velocity.Y--; break;
			case Direction.Left:  velocity.X--; break;
			case Direction.Down:  velocity.Y++; break;
			case Direction.Right: velocity.X++; break;
		}
		updatesSinceGravityApplied = 0;
	}
	else
	{
		updatesSinceGravityApplied++;
	}

	playerState = PlayerState.Neutral;

	int u = velocity.Y < 0 ? -velocity.Y : 0;
	int l = velocity.X < 0 ? -velocity.X : 0;
	int d = velocity.Y > 0 ?  velocity.Y : 0;
	int r = velocity.X > 0 ?  velocity.X : 0;

	if (velocity.Y < 0 && (gravity is Direction.Left && WallLeft() || gravity is Direction.Right && WallRight()))
	{
		playerState |= PlayerState.Sliding | PlayerState.Up | (PlayerState)gravity;
		if (updatesSinceSlideApplied >= slideUpdateFrequency)
		{
			velocity.Y++;
			updatesSinceSlideApplied = 0;
		}
		updatesSinceSlideApplied++;
	}
	else if (velocity.Y > 0 && (gravity is Direction.Left && WallLeft() || gravity is Direction.Right && WallRight()))
	{
		playerState |= PlayerState.Sliding | PlayerState.Down | (PlayerState)gravity;
		if (updatesSinceSlideApplied >= slideUpdateFrequency)
		{
			velocity.Y--;
			updatesSinceSlideApplied = 0;
		}
		updatesSinceSlideApplied++;
	}
	else if (velocity.X < 0 && (gravity is Direction.Up && WallUp() || gravity is Direction.Down && WallDown()))
	{
		playerState |= PlayerState.Sliding | PlayerState.Left | (PlayerState)gravity;
		if (updatesSinceSlideApplied >= slideUpdateFrequency)
		{
			velocity.X++;
			updatesSinceSlideApplied = 0;
		}
		updatesSinceSlideApplied++;
	}
	else if (velocity.X > 0 && (gravity is Direction.Up && WallUp() || gravity is Direction.Down && WallDown()))
	{
		playerState |= PlayerState.Sliding | PlayerState.Right | (PlayerState)gravity;
		if (updatesSinceSlideApplied >= slideUpdateFrequency)
		{
			velocity.X--;
			updatesSinceSlideApplied = 0;
		}
		updatesSinceSlideApplied++;
	}
	else
	{
		updatesSinceSlideApplied = 0;
	}

	while ((u > 0 || l > 0 || d > 0 || r > 0) && gameState is GameState.Default)
	{
		if (u > 0)
		{
			if (WallUp())
			{
				if (u > 1)
				{
					if (playerState.HasFlag(PlayerState.Sliding))
					{
						playerState = PlayerState.Neutral;
					}
					playerState |= PlayerState.Squash | PlayerState.Up;
				}
				velocity.Y = 0;
				u = 0;
			}
			else
			{
				PlayerPosition.Y--;
				u--;
			}
		}

		if (d > 0)
		{
			if (WallDown())
			{
				if (d > 1)
				{
					if (playerState.HasFlag(PlayerState.Sliding))
					{
						playerState = PlayerState.Neutral;
					}
					playerState |= PlayerState.Squash | PlayerState.Down;
				}
				velocity.Y = 0;
				d = 0;
			}
			else
			{
				PlayerPosition.Y++;
				d--;
			}
		}

		if (l > 0)
		{
			if (WallLeft())
			{
				if (l > 1)
				{
					if (playerState.HasFlag(PlayerState.Sliding))
					{
						playerState = PlayerState.Neutral;
					}
					playerState |= PlayerState.Squash | PlayerState.Left;
				}
				velocity.X = 0;
				l = 0;
			}
			else
			{
				PlayerPosition.X--;
				l--;
			}
		}

		if (r > 0)
		{
			if (WallRight())
			{
				if (r > 1)
				{
					if (playerState.HasFlag(PlayerState.Sliding))
					{
						playerState = PlayerState.Neutral;
					}
					playerState |= PlayerState.Squash | PlayerState.Right;
				}
				velocity.X = 0;
				r = 0;
			}
			else
			{
				PlayerPosition.X++;
				r--;
			}
		}

		for (int i = -1; i <= 1; i++)
		{
			for (int j = -2; j <= 2; j++)
			{
				char c = levels[level][i + PlayerPosition.Y][j + PlayerPosition.X];
				switch (c)
				{
					case 'X': gameState |= GameState.Died; break;
					case '●': gameState |= GameState.Won; break;
				}
			}
		}
	}

	if (gameState.HasFlag(GameState.Died))
	{
		Render();
		Console.WriteLine("You died. Press enter to retry level.");
		PressToContinue();
		PlayerPosition = GetStartingPlayerPositionFromLevel();
		gravity = Direction.None;
		velocity = (0, 0);
		gameState = GameState.Default;
	}
	else if (gameState.HasFlag(GameState.Won))
	{
		Render();
		if (level >= levels.Length - 1)
		{
			Console.WriteLine("You Won. You beat all the levels! Congratulations!");
			Console.WriteLine("Press enter to exit game...");
			PressToContinue();
			closeRequested = true;
			return;
		}
		Console.WriteLine("You Won. Press enter to move to the next level.");
		PressToContinue();
		Console.Clear();
		level++;
		PlayerPosition = GetStartingPlayerPositionFromLevel();
		gravity = Direction.None;
		velocity = (0, 0);
		gameState = GameState.Default;
	}
}

bool WallUp() =>
	levels[level][PlayerPosition.Y - 2][PlayerPosition.X - 2] is '█' or '#' ||
	levels[level][PlayerPosition.Y - 2][PlayerPosition.X - 1] is '█' or '#' ||
	levels[level][PlayerPosition.Y - 2][PlayerPosition.X]     is '█' or '#' ||
	levels[level][PlayerPosition.Y - 2][PlayerPosition.X + 1] is '█' or '#' ||
	levels[level][PlayerPosition.Y - 2][PlayerPosition.X + 2] is '█' or '#';

bool WallDown() =>
	levels[level][PlayerPosition.Y + 2][PlayerPosition.X - 2] is '█' or '#' ||
	levels[level][PlayerPosition.Y + 2][PlayerPosition.X - 1] is '█' or '#' ||
	levels[level][PlayerPosition.Y + 2][PlayerPosition.X]     is '█' or '#' ||
	levels[level][PlayerPosition.Y + 2][PlayerPosition.X + 1] is '█' or '#' ||
	levels[level][PlayerPosition.Y + 2][PlayerPosition.X + 2] is '█' or '#';

bool WallLeft() =>
	levels[level][PlayerPosition.Y - 1][PlayerPosition.X - 3] is '█' or '#' ||
	levels[level][PlayerPosition.Y][PlayerPosition.X - 3]     is '█' or '#' ||
	levels[level][PlayerPosition.Y + 1][PlayerPosition.X - 3] is '█' or '#';

bool WallRight() =>
	levels[level][PlayerPosition.Y - 1][PlayerPosition.X + 3] is '█' or '#' ||
	levels[level][PlayerPosition.Y][PlayerPosition.X + 3]     is '█' or '#' ||
	levels[level][PlayerPosition.Y + 1][PlayerPosition.X + 3] is '█' or '#';

void Render()
{
	string[] playerSprite = RenderPlayerState();
	StringBuilder render = new();
	render.AppendLine();
	for (int i = 0; i < levels[level].Length; i++)
	{
		render.Append(' ');
		render.Append(' ');
		for (int j = 0; j < levels[level][i].Length; j++)
		{
			char c = levels[level][i][j];
			if (c is '@')
			{
				c = ' ';
			}
			if (c is not 'X' and not '●' &&
				PlayerPosition.X - 2 <= j &&
				PlayerPosition.X + 2 >= j &&
				PlayerPosition.Y - 1 <= i &&
				PlayerPosition.Y + 1 >= i)
			{
				c = playerSprite[i - PlayerPosition.Y + 1][j - PlayerPosition.X + 2];
			}
			render.Append(c);
		}
		render.AppendLine();
	}
	render.AppendLine();
	render.AppendLine($"Level: {level}    Gravity: {RenderGravityIdentifier()}");
	Console.SetCursorPosition(0, 0);
	Console.Write(render);
	Console.CursorVisible = false;
}

string[] RenderPlayerState()
{
	return (playerState) switch
	{
		(PlayerState.Sliding | PlayerState.Up | PlayerState.Right) or
		(PlayerState.Sliding | PlayerState.Down | PlayerState.Left) =>
			[
				"╭──╮ ",
				"╰╮ ╰╮",
				" ╰──╯",
			],
		(PlayerState.Sliding | PlayerState.Down | PlayerState.Right) or
		(PlayerState.Sliding | PlayerState.Up | PlayerState.Left) =>
			[
				" ╭──╮",
				"╭╯ ╭╯",
				"╰──╯ ",
			],
		(PlayerState.Squash | PlayerState.Up | PlayerState.Right) =>
			[
				"╭───╮",
				"╰─╮ │",
				"  ╰─╯",
			],
		(PlayerState.Squash | PlayerState.Down | PlayerState.Right) =>
			[
				"  ╭─╮",
				"╭─╯ │",
				"╰───╯",
			],
		(PlayerState.Squash | PlayerState.Up | PlayerState.Left) =>
			[
				"╭───╮",
				"│ ╭─╯",
				"╰─╯  ",
			],
		(PlayerState.Squash | PlayerState.Down | PlayerState.Left) =>
			[
				"╭─╮  ",
				"│ ╰─╮",
				"╰───╯",
			],
		(PlayerState.Squash | PlayerState.Up) =>
			[
				"╭───╮",
				"╰───╯",
				"     ",
			],
		(PlayerState.Squash | PlayerState.Down) =>
			[
				"     ",
				"╭───╮",
				"╰───╯",
			],
		(PlayerState.Squash | PlayerState.Right) =>
			[
				"  ╭─╮",
				"  │ │",
				"  ╰─╯",
			],
		(PlayerState.Squash | PlayerState.Left) =>
			[
				"╭─╮  ",
				"│ │  ",
				"╰─╯  ",
			],
		_ =>
			[
				"╭───╮",
				"│   │",
				"╰───╯",
			],
	};
}

char RenderGravityIdentifier()
{
	return gravity switch
	{
		Direction.None  => '○',
		Direction.Up    => '^',
		Direction.Down  => 'v',
		Direction.Left  => '<',
		Direction.Right => '>',
		_ => throw new NotImplementedException(),
	};
}

void PressToContinue(ConsoleKey key = ConsoleKey.Enter)
{
	ConsoleKey input = default;
	while (input != key && !closeRequested)
	{
		input = Console.ReadKey(true).Key;
		if (input is ConsoleKey.Escape)
		{
			closeRequested = true;
			return;
		}
	}
}

internal enum Direction
{
	None  =      0,
	Up    = 1 << 0,
	Down  = 1 << 1,
	Left  = 1 << 2,
	Right = 1 << 3,
}

internal enum GameState
{
	Default =      0,
	Died    = 1 << 0,
	Won     = 1 << 1,
}

internal enum PlayerState
{
	Neutral   =      0,
	Up        = 1 << 0,
	Down      = 1 << 1,
	Left      = 1 << 2,
	Right     = 1 << 3,
	Sliding   = 1 << 4,
	Squash    = 1 << 5,
}