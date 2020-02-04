using System;
using System.Diagnostics;
using System.Threading;

class Program
{
	static readonly int height = Console.WindowHeight;
	static readonly int width = Console.WindowWidth;

	const int Y = 5;

	static readonly TimeSpan sleep = TimeSpan.FromMilliseconds(10);
	static readonly TimeSpan timeSpanIdle = TimeSpan.FromMilliseconds(400);
	static readonly TimeSpan timeSpanPunch = TimeSpan.FromMilliseconds(100);
	static readonly TimeSpan timeSpanBlock = TimeSpan.FromMilliseconds(400);
	static readonly TimeSpan timeSpanJumpKick = TimeSpan.FromMilliseconds(100);
	static readonly TimeSpan timeSpanOwned = TimeSpan.FromMilliseconds(50);
	static readonly TimeSpan timeSpanGetUp = TimeSpan.FromMilliseconds(80);

	static readonly Random random = new Random();

	enum Action
	{
		Idle,
		Punch,
		Block,
		JumpKick,
		Owned,
		GetUp,
	}

	class Fighter
	{
		public Action Action = Action.Idle;
		public int Frame = 0;
		public int Position;
		public Stopwatch Stopwatch = new Stopwatch();
		public int Energy = 30;
		public int Health = 10;
		public string[] IdleAnimation;
		public string[] PunchAnimation;
		public string[] BlockAnimation;
		public string[] JumpKickAnimation;
		public string[] OwnedAnimation;
		public string[] GetUpAnimation;
	}

	static void Main()
	{
		Console.WriteLine("This game is still under development.");
		Console.WriteLine("Press Enter To Continue...");
		Console.ReadLine();
		Console.Clear();

		Console.CursorVisible = false;

		Fighter player = new Fighter()
		{
			Position = 30,
			IdleAnimation = Ascii.Player.IdleAnimation,
			PunchAnimation = Ascii.Player.PunchAnimation,
			BlockAnimation = Ascii.Player.BlockAnimation,
			JumpKickAnimation = Ascii.Player.JumpKickAnimation,
			OwnedAnimation = Ascii.Player.OwnedAnimation,
			GetUpAnimation = Ascii.Player.GetUpAnimation,
		};

		Fighter enemy = new Fighter()
		{
			Position = 60,
			IdleAnimation = Ascii.Enemy.IdleAnimation,
			PunchAnimation = Ascii.Enemy.PunchAnimation,
			BlockAnimation = Ascii.Enemy.BlockAnimation,
			JumpKickAnimation = Ascii.Enemy.JumpKickAnimation,
			//OwnedAnimation = Ascii.Enemy.OwnedAnimation,
			//GetUpAnimation = Ascii.Enemy.GetUpAnimation,
		};

		player.Stopwatch.Restart();
		enemy.Stopwatch.Restart();
		Console.SetCursorPosition(player.Position, Y);
		Render(Ascii.Player.IdleAnimation[player.Frame]);
		Console.SetCursorPosition(enemy.Position, Y);
		Render(Ascii.Enemy.IdleAnimation[enemy.Frame]);

		Console.SetCursorPosition(0, 11);
		for (int i = 0; i < width; i++)
		{
			Console.Write('=');
		}

		while (true)
		{
			#region Console Resize

			if (Console.WindowHeight != height || Console.WindowWidth != width)
			{
				Console.Clear();
				Console.Write("Console resized. Fighter was closed.");
				return;
			}

			#endregion

			bool skipPlayerUpdate = false;
			bool skipEnemyUpdate = false;

			#region Trigger Action

			static void Trigger(Fighter fighter, Action action)
			{
				Console.SetCursorPosition(fighter.Position, Y);
				Erase(fighter.IdleAnimation[fighter.Frame]);
				fighter.Action = action;
				fighter.Frame = 0;
				Console.SetCursorPosition(fighter.Position, Y);
				Render(action switch
				{
					Action.Idle => fighter.IdleAnimation[fighter.Frame],
					Action.Punch => fighter.PunchAnimation[fighter.Frame],
					Action.Block => fighter.BlockAnimation[fighter.Frame],
					Action.JumpKick => fighter.JumpKickAnimation[fighter.Frame],
					Action.Owned => fighter.OwnedAnimation[fighter.Frame],
					Action.GetUp => fighter.GetUpAnimation[fighter.Frame],
					_ => throw new NotImplementedException(),
				});
				fighter.Stopwatch.Restart();
			}

			static void Move(Fighter fighter, int location)
			{
				Console.SetCursorPosition(fighter.Position, Y);
				Erase(fighter.IdleAnimation[fighter.Frame]);
				fighter.Position = location;
				Console.SetCursorPosition(fighter.Position, Y);
				Render(fighter.IdleAnimation[fighter.Frame]);
			}

			#endregion

			#region PLayer Input

			if (Console.KeyAvailable)
			{
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.F:
						if (player.Action == Action.Idle)
						{
							Trigger(player, Action.Punch);
							skipPlayerUpdate = true;
						}
						break;
					case ConsoleKey.D:
						if (player.Action == Action.Idle)
						{
							Trigger(player, Action.Block);
							skipPlayerUpdate = true;
						}
						break;
					case ConsoleKey.S:
						if (player.Action == Action.Idle)
						{
							Trigger(player, Action.JumpKick);
							skipPlayerUpdate = true;
						}
						break;
					case ConsoleKey.Q:
						if (player.Action == Action.Idle)
						{
							Trigger(player, Action.Owned);
							skipPlayerUpdate = true;
						}
						break;
					case ConsoleKey.LeftArrow:
						if (player.Action == Action.Idle)
						{
							int newPosition = Math.Min(Math.Max(player.Position - 1, 0), enemy.Position - 4);
							if (newPosition != player.Position)
							{
								Move(player, newPosition);
								skipPlayerUpdate = true;
							}
						}
						break;
					case ConsoleKey.RightArrow:
						if (player.Action == Action.Idle)
						{
							int newPosition = Math.Min(Math.Max(player.Position + 1, 0), enemy.Position - 4);
							if (newPosition != player.Position)
							{
								Move(player, newPosition);
								skipPlayerUpdate = true;
							}
						}
						break;
					case ConsoleKey.Escape:
						Console.Clear();
						Console.Write("Fighter was closed.");
						return;
				}
			}
			while (Console.KeyAvailable)
			{
				Console.ReadKey(true);
			}

			#endregion

			#region Enemy AI

			if (enemy.Action == Action.Idle)
			{
				if (enemy.Position - player.Position <= 5 && random.Next(7) == 0)
				{
					Trigger(enemy, Action.Block);
					skipEnemyUpdate = true;
				}
				else if (enemy.Position - player.Position <= 5 && random.Next(10) == 0)
				{
					Trigger(enemy, Action.Punch);
					skipEnemyUpdate = true;
				}
				else if (enemy.Position - player.Position <= 5 && random.Next(10) == 0)
				{
					Trigger(enemy, Action.JumpKick);
					skipEnemyUpdate = true;
				}
				else if (random.Next(10) == 0)
				{
					int newPosition = Math.Min(Math.Max(enemy.Position - 1, player.Position + 4), width - 9);
					if (enemy.Position != newPosition)
					{
						Move(enemy, newPosition);
						skipEnemyUpdate = true;
					}
				}
				else if (random.Next(10) == 0)
				{
					int newPosition = Math.Min(Math.Max(enemy.Position + 1, player.Position + 4), width - 9);
					if (enemy.Position != newPosition)
					{
						Move(enemy, newPosition);
						skipEnemyUpdate = true;
					}
				}
			}

			#endregion

			#region Update Fighter

			if (!skipPlayerUpdate)
			{
				Update(player);
			}

			if (!skipEnemyUpdate)
			{
				Update(enemy);
			}

			static void Update(Fighter fighter)
			{
				if (fighter.Action == Action.Idle && fighter.Stopwatch.Elapsed > timeSpanIdle)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.IdleAnimation[fighter.Frame]);
					fighter.Frame = fighter.Frame is 0 ? 1 : 0;
					Console.SetCursorPosition(fighter.Position, Y);
					Render(fighter.IdleAnimation[fighter.Frame]);
					fighter.Stopwatch.Restart();
				}
				else if (fighter.Action == Action.Punch && fighter.Stopwatch.Elapsed > timeSpanPunch)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.PunchAnimation[fighter.Frame]);
					fighter.Frame++;
					if (fighter.Frame >= fighter.PunchAnimation.Length)
					{
						fighter.Action = Action.Idle;
						fighter.Frame = 0;
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.IdleAnimation[fighter.Frame]);
					}
					else
					{
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.PunchAnimation[fighter.Frame]);
					}
					fighter.Stopwatch.Restart();
				}
				else if (fighter.Action == Action.Block && fighter.Stopwatch.Elapsed > timeSpanBlock)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.BlockAnimation[fighter.Frame]);
					fighter.Action = Action.Idle;
					fighter.Frame = 0;
					Console.SetCursorPosition(fighter.Position, Y);
					Render(fighter.IdleAnimation[fighter.Frame]);
					fighter.Stopwatch.Restart();
				}
				else if (fighter.Action == Action.JumpKick && fighter.Stopwatch.Elapsed > timeSpanJumpKick)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.JumpKickAnimation[fighter.Frame]);
					fighter.Frame++;
					if (fighter.Frame >= fighter.JumpKickAnimation.Length)
					{
						fighter.Action = Action.Idle;
						fighter.Frame = 0;
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.IdleAnimation[fighter.Frame]);
					}
					else
					{
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.JumpKickAnimation[fighter.Frame]);
					}
					fighter.Stopwatch.Restart();
				}
				else if (fighter.Action == Action.Owned && fighter.Stopwatch.Elapsed > timeSpanOwned)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.OwnedAnimation[fighter.Frame]);
					fighter.Frame++;
					if (fighter.Frame >= fighter.OwnedAnimation.Length)
					{
						fighter.Action = Action.GetUp;
						fighter.Frame = 0;
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.GetUpAnimation[fighter.Frame]);
					}
					else
					{
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.OwnedAnimation[fighter.Frame]);
					}
					fighter.Stopwatch.Restart();
				}
				else if (fighter.Action == Action.GetUp && fighter.Stopwatch.Elapsed > timeSpanGetUp)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.GetUpAnimation[fighter.Frame]);
					fighter.Frame++;
					if (fighter.Frame >= fighter.GetUpAnimation.Length)
					{
						fighter.Action = Action.Idle;
						fighter.Frame = 0;
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.IdleAnimation[fighter.Frame]);
					}
					else
					{
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.GetUpAnimation[fighter.Frame]);
					}
					fighter.Stopwatch.Restart();
				}
			}

			#endregion

			Thread.Sleep(sleep);
		}
	}

	#region Render & Erase

	static void Render(string @string, bool renderSpace = false)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c is '\n')
				Console.SetCursorPosition(x, ++y);
			else if (Console.CursorLeft < width - 1 && (!(c is ' ') || renderSpace))
				Console.Write(c);
			else if (Console.CursorLeft < width - 1 && Console.CursorTop < height - 1)
				Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
	}

	static void Erase(string @string)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c is '\n')
				Console.SetCursorPosition(x, ++y);
			else if (Console.CursorLeft < width - 1 && !(c is ' '))
				Console.Write(' ');
			else if (Console.CursorLeft < width - 1 && Console.CursorTop < height - 1)
				Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
	}

	#endregion

	static class Ascii
	{
		#region Ascii

		public static class Player
		{
			public static readonly string[] IdleAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"    O    " + '\n' +
				@"   L|(   " + '\n' +
				@"    |    " + '\n' +
				@"   ( \   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"   ((L   " + '\n' +
				@"    |    " + '\n' +
				@"   / )   ",
			};

			public static readonly string[] BlockAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o_|  " + '\n' +
				@"    |-'  " + '\n' +
				@"    |    " + '\n' +
				@"   / /   ",
			};

			public static readonly string[] PunchAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"   _o_.  " + '\n' +
				@"   (|    " + '\n' +
				@"    |    " + '\n' +
				@"   > \   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o__. " + '\n' +
				@"   (|    " + '\n' +
				@"    |    " + '\n' +
				@"   / >   ",
				// 2
				@"         " + '\n' +
				@"         " + '\n' +
				@"    O___." + '\n' +
				@"   L(    " + '\n' +
				@"    |    " + '\n' +
				@"   / >   ",
				// 3
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o_   " + '\n' +
				@"   L( \  " + '\n' +
				@"    |    " + '\n' +
				@"   > \   ",
				// 4
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o_   " + '\n' +
				@"   L( >  " + '\n' +
				@"    |    " + '\n' +
				@"   > \   ",
				// 5
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"   (|)   " + '\n' +
				@"    |    " + '\n' +
				@"   / \   ",
			};

			public static readonly string[] JumpKickAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"    _O   " + '\n' +
				@"   |/|_  " + '\n' +
				@"   /\    " + '\n' +
				@"  /  |   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"     o   " + '\n' +
				@"   </<   " + '\n' +
				@"    >>   ",
				// 2
				@"         " + '\n' +
				@"         " + '\n' +
				@"    O    " + '\n' +
				@"   <|<   " + '\n' +
				@"    |    " + '\n' +
				@"   /|    ",
				// 3
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"   L|<   " + '\n' +
				@"    >    " + '\n' +
				@"    |    " + '\n' +
				@"         ",
				// 4
				@"         " + '\n' +
				@"   _o_   " + '\n' +
				@"   L|_   " + '\n' +
				@"    |/   " + '\n' +
				@"    |    " + '\n' +
				@"         ",
				// 5
				@"         " + '\n' +
				@"   _o_   " + '\n' +
				@"   <|___." + '\n' +
				@"    |    " + '\n' +
				@"    |    " + '\n' +
				@"         ",
				// 6
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"   (<_   " + '\n' +
				@"    |/   " + '\n' +
				@"    |    " + '\n' +
				@"         ",
				// 7
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"     o   " + '\n' +
				@"   </<   " + '\n' +
				@"    >>   ",
			};

			public static readonly string[] OwnedAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"   O___  " + '\n' +
				@"    \`-  " + '\n' +
				@"    /\   " + '\n' +
				@"   / /   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"   //    " + '\n' +
				@"  O/__   " + '\n' +
				@"   __/\  " + '\n' +
				@"      /  ",
				// 2
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"   //    " + '\n' +
				@"  O/__/\ " + '\n' +
				@"       \  ",
				// 3
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"   //    " + '\n' +
				@"  O/__/\ ",
				// 3
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"  o___/\ ",
			};

			public static readonly string[] GetUpAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"     __  " + '\n' +
				@"  o__\   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"     /   " + '\n' +
				@"  o__\   ",
				// 2
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"     |   " + '\n' +
				@"     |   " + '\n' +
				@"  o_/    ",
				// 3
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"     |\  " + '\n' +
				@"  o_/    " + '\n' +
				@"  /\     ",
				// 4
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"   /-\   " + '\n' +
				@" /o/ //  " + '\n' +
				@"         ",
				// 5
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"  /o|\   " + '\n' +
				@"      \  " + '\n' +
				@"     //  ",
				// 6
				@"         " + '\n' +
				@"    _    " + '\n' +
				@"  __O\   " + '\n' +
				@"     \   " + '\n' +
				@"     /\  " + '\n' +
				@"    / /  ",
				// 7
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"     o   " + '\n' +
				@"   </<   " + '\n' +
				@"    >>   ",
			};
		}

		public static class Enemy
		{
			public static readonly string[] IdleAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"    O    " + '\n' +
				@"   )|J   " + '\n' +
				@"    |    " + '\n' +
				@"   / )   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"   J))   " + '\n' +
				@"    |    " + '\n' +
				@"   ( \   ",
			};

			public static readonly string[] BlockAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"  |_o    " + '\n' +
				@"  '-|    " + '\n' +
				@"    |    " + '\n' +
				@"   \ \   ",
			};

			public static readonly string[] PunchAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"  ._o_   " + '\n' +
				@"    |)   " + '\n' +
				@"    |    " + '\n' +
				@"   / <   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@" .__o    " + '\n' +
				@"    |)   " + '\n' +
				@"    |    " + '\n' +
				@"   < \   ",
				// 2
				@"         " + '\n' +
				@"         " + '\n' +
				@".___O    " + '\n' +
				@"    )J   " + '\n' +
				@"    |    " + '\n' +
				@"   < \   ",
				// 3
				@"         " + '\n' +
				@"         " + '\n' +
				@"   _o    " + '\n' +
				@"  / )J   " + '\n' +
				@"    |    " + '\n' +
				@"   / <   ",
				// 4
				@"         " + '\n' +
				@"         " + '\n' +
				@"   _o    " + '\n' +
				@"  < )J   " + '\n' +
				@"    |    " + '\n' +
				@"   / <   ",
				// 5
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"   (|)   " + '\n' +
				@"    |    " + '\n' +
				@"   / \   ",
			};

			public static readonly string[] JumpKickAnimation = new string[]
			{
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"  O_   " + '\n' +
				@" _|\|  " + '\n' +
				@"   /\    " + '\n' +
				@"  |  \   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"    >\>  " + '\n' +
				@"    <<   ",
				// 2
				@"         " + '\n' +
				@"         " + '\n' +
				@"    O    " + '\n' +
				@"   >|>   " + '\n' +
				@"    |    " + '\n' +
				@"    |\   ",
				// 3
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"   >|J   " + '\n' +
				@"    <    " + '\n' +
				@"    |    " + '\n' +
				@"         ",
				// 4
				@"         " + '\n' +
				@"   _o_   " + '\n' +
				@"   _|J   " + '\n' +
				@"   \|    " + '\n' +
				@"    |    " + '\n' +
				@"         ",
				// 5
				@"         " + '\n' +
				@"   _o_   " + '\n' +
				@".___|>   " + '\n' +
				@"    |    " + '\n' +
				@"    |    " + '\n' +
				@"         ",
				// 6
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"   _>)   " + '\n' +
				@"   \|    " + '\n' +
				@"    |    " + '\n' +
				@"         ",
				// 7
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o    " + '\n' +
				@"    >\>   " + '\n' +
				@"    <<   ",
			};
		}

		#endregion
	}
}
