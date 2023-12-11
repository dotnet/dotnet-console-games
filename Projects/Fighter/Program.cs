using System;
using System.Diagnostics;
using System.Threading;
using static Program.Action;

class Program
{
	static readonly int height = Console.WindowHeight;
	static readonly int width = Console.WindowWidth;

	static readonly int barWidth = (width - 9) / 2;
	const int Y = 6;

	static readonly TimeSpan sleep = TimeSpan.FromMilliseconds(10);
	static readonly TimeSpan timeSpanIdle = TimeSpan.FromMilliseconds(400);
	static readonly TimeSpan timeSpanPunch = TimeSpan.FromMilliseconds(100);
	static readonly TimeSpan timeSpanBlock = TimeSpan.FromMilliseconds(800);
	static readonly TimeSpan timeSpanJumpKick = TimeSpan.FromMilliseconds(100);
	static readonly TimeSpan timeSpanOwned = TimeSpan.FromMilliseconds(30);
	static readonly TimeSpan timeSpanGround = TimeSpan.FromMilliseconds(600);
	static readonly TimeSpan timeSpanGetUp = TimeSpan.FromMilliseconds(80);

	internal enum Action
	{
		Idle = 0,
		Punch = 1,
		Block = 2,
		JumpKick = 3,
		Owned = 4,
		Ground = 5,
		GetUp = 6,
	}

	class Fighter
	{
		public Action Action = Idle;
		public int Frame = 0;
		public int Position;
		public Stopwatch Stopwatch = new();
		public int MaxEnergy = 60;
		public int Energy = 40;
		public int MaxHealth = 10;
		public int Health = 10;
		public string[] IdleAnimation;
		public string[] PunchAnimation;
		public string[] BlockAnimation;
		public string[] JumpKickAnimation;
		public string[] OwnedAnimation;
		public string[] GroundAnimation;
		public string[] GetUpAnimation;

		public Fighter(int position, string[] idleAnimation, string[] punchAnimation, string[] blockAnimation, string[] jumpKickAnimation, string[] ownedAnimation, string[] groundAnimation, string[] getUpAnimation)
		{
			Position = position;
			IdleAnimation = idleAnimation;
			PunchAnimation = punchAnimation;
			BlockAnimation = blockAnimation;
			JumpKickAnimation = jumpKickAnimation;
			OwnedAnimation = ownedAnimation;
			GroundAnimation = groundAnimation;
			GetUpAnimation = getUpAnimation;
		}

	}

	static void Main()
	{
		Console.Clear();
		Console.CursorVisible = false;

		Fighter player = new(
			position: width / 3,
			idleAnimation: Ascii.Player.IdleAnimation,
			punchAnimation: Ascii.Player.PunchAnimation,
			blockAnimation: Ascii.Player.BlockAnimation,
			jumpKickAnimation: Ascii.Player.JumpKickAnimation,
			ownedAnimation: Ascii.Player.OwnedAnimation,
			groundAnimation: Ascii.Player.GroundAnimation,
			getUpAnimation: Ascii.Player.GetUpAnimation);

		Fighter enemy = new(
			position: (width / 3) * 2,
			idleAnimation: Ascii.Enemy.IdleAnimation,
			punchAnimation: Ascii.Enemy.PunchAnimation,
			blockAnimation: Ascii.Enemy.BlockAnimation,
			jumpKickAnimation: Ascii.Enemy.JumpKickAnimation,
			ownedAnimation: Ascii.Enemy.OwnedAnimation,
			groundAnimation: Ascii.Enemy.GroundAnimation,
			getUpAnimation: Ascii.Enemy.GetUpAnimation);

		player.Stopwatch.Restart();
		enemy.Stopwatch.Restart();
		Console.SetCursorPosition(player.Position, Y);
		Render(Ascii.Player.IdleAnimation[player.Frame]);
		Console.SetCursorPosition(enemy.Position, Y);
		Render(Ascii.Enemy.IdleAnimation[enemy.Frame]);

		Console.SetCursorPosition(0, Y + 6);
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

			#region Helpers

			static void Trigger(Fighter fighter, Action action)
			{
				if (!(fighter.Energy >= action switch
				{
					Punch => 10,
					JumpKick => 20,
					Block => 0,
					_ => throw new NotImplementedException(),
				})) return;

				Console.SetCursorPosition(fighter.Position, Y);
				Erase(fighter.IdleAnimation[fighter.Frame]);
				fighter.Action = action;
				fighter.Frame = 0;
				fighter.Energy = Math.Max(action switch
				{
					Punch => fighter.Energy - 10,
					JumpKick => fighter.Energy - 20,
					Block => fighter.Energy,
					_ => throw new NotImplementedException(),
				}, 0);

				Console.SetCursorPosition(fighter.Position, Y);
				Render(action switch
				{
					Idle => fighter.IdleAnimation[fighter.Frame],
					Punch => fighter.PunchAnimation[fighter.Frame],
					Block => fighter.BlockAnimation[fighter.Frame],
					JumpKick => fighter.JumpKickAnimation[fighter.Frame],
					Owned => fighter.OwnedAnimation[fighter.Frame],
					GetUp => fighter.GetUpAnimation[fighter.Frame],
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
						if (player.Action is Idle)
						{
							Trigger(player, Punch);
							skipPlayerUpdate = true;
						}
						break;
					case ConsoleKey.D:
						if (player.Action is Idle)
						{
							Trigger(player, Block);
							skipPlayerUpdate = true;
						}
						break;
					case ConsoleKey.S:
						if (player.Action is Idle)
						{
							Trigger(player, JumpKick);
							skipPlayerUpdate = true;
						}
						break;
					//case ConsoleKey.Q:
					//	if (player.Action is Idle)
					//	{
					//		Trigger(player, Owned);
					//		skipPlayerUpdate = true;
					//	}
					//	break;
					case ConsoleKey.LeftArrow:
						if (player.Action is Idle)
						{
							int newPosition = Math.Min(Math.Max(player.Position - 1, 0), enemy.Position - 4);
							if (newPosition != player.Position && player.Energy >= 2)
							{
								Move(player, newPosition);
								skipPlayerUpdate = true;
								player.Energy = Math.Max(player.Energy - 1, 0);
							}
						}
						break;
					case ConsoleKey.RightArrow:
						if (player.Action is Idle)
						{
							int newPosition = Math.Min(Math.Max(player.Position + 1, 0), enemy.Position - 4);
							if (newPosition != player.Position && player.Energy >= 2)
							{
								Move(player, newPosition);
								skipPlayerUpdate = true;
								player.Energy = Math.Max(player.Energy - 1, 0);
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

			if (enemy.Action is Idle)
			{
				if (enemy.Position - player.Position <= 5 && Random.Shared.Next(10) is 0)
				{
					Trigger(enemy, Punch);
					skipEnemyUpdate = true;
				}
				else if (enemy.Position - player.Position <= 5 && Random.Shared.Next(10) is 0)
				{
					Trigger(enemy, JumpKick);
					skipEnemyUpdate = true;
				}
				else if (enemy.Position - player.Position <= 5 && Random.Shared.Next(7) is 0 && player.Energy >= 9)
				{
					Trigger(enemy, Block);
					skipEnemyUpdate = true;
				}
				else if (Random.Shared.Next(10) is 0 && enemy.Energy >= 2 && (enemy.Energy == enemy.MaxEnergy || Random.Shared.Next(enemy.MaxEnergy - enemy.Energy + 3) is 0))
				{
					int newPosition = Math.Min(Math.Max(enemy.Position - 1, player.Position + 4), width - 9);
					if (enemy.Position != newPosition)
					{
						Move(enemy, newPosition);
						skipEnemyUpdate = true;
						enemy.Energy = Math.Max(enemy.Energy - 1, 0);
					}
				}
				else if (Random.Shared.Next(13) is 0 && enemy.Energy >= 2 && (enemy.Energy == enemy.MaxEnergy || Random.Shared.Next(enemy.MaxEnergy - enemy.Energy + 3) is 0))
				{
					int newPosition = Math.Min(Math.Max(enemy.Position + 1, player.Position + 4), width - 9);
					if (enemy.Position != newPosition)
					{
						Move(enemy, newPosition);
						skipEnemyUpdate = true;
						enemy.Energy = Math.Max(enemy.Energy - 1, 0);
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

			void Update(Fighter fighter)
			{
				if (fighter.Action is Idle && fighter.Stopwatch.Elapsed > timeSpanIdle)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.IdleAnimation[fighter.Frame]);
					fighter.Frame = fighter.Frame is 0 ? 1 : 0;
					Console.SetCursorPosition(fighter.Position, Y);
					Render(fighter.IdleAnimation[fighter.Frame]);
					fighter.Stopwatch.Restart();
					fighter.Energy = Math.Min(fighter.Energy + 1, fighter.MaxEnergy);
				}
				else if (fighter.Action is Punch && fighter.Stopwatch.Elapsed > timeSpanPunch)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.PunchAnimation[fighter.Frame]);
					fighter.Frame++;

					Fighter opponent = fighter == player ? enemy : player;
					if (Math.Abs(opponent.Position - fighter.Position) <= 5 &&
						2 >= fighter.Frame  && fighter.Frame <= 3 &&
						opponent.Action is not Block &&
						opponent.Action is not GetUp &&
						opponent.Action is not Ground &&
						opponent.Action is not Owned)
					{
						opponent.Health -= 4;
						Console.SetCursorPosition(opponent.Position, Y);
						Erase(opponent.Action switch
						{
							Punch => opponent.PunchAnimation[opponent.Frame],
							Idle => opponent.IdleAnimation[opponent.Frame],
							JumpKick => opponent.JumpKickAnimation[opponent.Frame],
							_ => throw new NotImplementedException(),
						});
						opponent.Action = Owned;
						opponent.Frame = 0;
						Console.SetCursorPosition(opponent.Position, Y);
						Render(opponent.OwnedAnimation[opponent.Frame]);
						opponent.Stopwatch.Restart();
					}

					if (fighter.Frame >= fighter.PunchAnimation.Length)
					{
						fighter.Action = Idle;
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
				else if (fighter.Action is Block && fighter.Stopwatch.Elapsed > timeSpanBlock)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.BlockAnimation[fighter.Frame]);
					fighter.Action = Idle;
					fighter.Frame = 0;
					Console.SetCursorPosition(fighter.Position, Y);
					Render(fighter.IdleAnimation[fighter.Frame]);
					fighter.Stopwatch.Restart();
				}
				else if (fighter.Action is JumpKick && fighter.Stopwatch.Elapsed > timeSpanJumpKick)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.JumpKickAnimation[fighter.Frame]);
					fighter.Frame++;

					Fighter opponent = fighter == player ? enemy : player;
					if (Math.Abs(opponent.Position - fighter.Position) <= 5 &&
						fighter.Frame is 5 &&
						opponent.Action is not GetUp &&
						opponent.Action is not Ground &&
						opponent.Action is not Owned)
					{
						opponent.Health -= opponent.Action is Block ? 4 : 8;
						Console.SetCursorPosition(opponent.Position, Y);
						Erase(opponent.Action switch
						{
							Punch => opponent.PunchAnimation[opponent.Frame],
							Idle => opponent.IdleAnimation[opponent.Frame],
							JumpKick => opponent.JumpKickAnimation[opponent.Frame],
							Block => opponent.BlockAnimation[opponent.Frame],
							_ => throw new NotImplementedException(),
						});
						opponent.Action = Owned;
						opponent.Frame = 0;
						Console.SetCursorPosition(opponent.Position, Y);
						Render(opponent.OwnedAnimation[opponent.Frame]);
						opponent.Stopwatch.Restart();
					}

					if (fighter.Frame >= fighter.JumpKickAnimation.Length)
					{
						fighter.Action = Idle;
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
				else if (fighter.Action is Owned && fighter.Stopwatch.Elapsed > timeSpanOwned)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.OwnedAnimation[fighter.Frame]);
					fighter.Frame++;
					if (fighter.Frame >= fighter.OwnedAnimation.Length)
					{
						fighter.Action = Ground;
						fighter.Frame = 0;
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.GroundAnimation[fighter.Frame]);
					}
					else
					{
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.OwnedAnimation[fighter.Frame]);
					}
					fighter.Stopwatch.Restart();
				}
				else if (fighter.Action is Ground && fighter.Stopwatch.Elapsed > timeSpanGround)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.GroundAnimation[fighter.Frame]);
					fighter.Frame++;
					if (fighter.Frame >= fighter.GroundAnimation.Length)
					{
						fighter.Action = GetUp;
						fighter.Frame = 0;
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.GetUpAnimation[fighter.Frame]);
					}
					else
					{
						Console.SetCursorPosition(fighter.Position, Y);
						Render(fighter.GroundAnimation[fighter.Frame]);
					}
					fighter.Stopwatch.Restart();
				}
				else if (fighter.Action is GetUp && fighter.Stopwatch.Elapsed > timeSpanGetUp)
				{
					Console.SetCursorPosition(fighter.Position, Y);
					Erase(fighter.GetUpAnimation[fighter.Frame]);
					fighter.Frame++;
					if (fighter.Frame >= fighter.GetUpAnimation.Length)
					{
						fighter.Action = Idle;
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

			#region Render Player (to make sure player is always on top)

			Console.SetCursorPosition(player.Position, Y);
			Render(player.Action switch
			{
				Idle => player.IdleAnimation[player.Frame],
				Punch => player.PunchAnimation[player.Frame],
				Block => player.BlockAnimation[player.Frame],
				JumpKick => player.JumpKickAnimation[player.Frame],
				Owned => player.OwnedAnimation[player.Frame],
				Ground => player.GroundAnimation[player.Frame],
				GetUp => player.GetUpAnimation[player.Frame],
				_ => throw new NotImplementedException(),
			});

			#endregion

			#region Health + Energy Bars
			{
				// player
				char[] playerHealthBar = new char[barWidth];
				int playerHealthBarLevel = (int)((player.Health / (float)player.MaxHealth) * barWidth);
				for (int i = 0; i < barWidth; i++)
				{
					playerHealthBar[i] = i <= playerHealthBarLevel ? '█' : ' ';
				}
				// enemy
				char[] enemyHealthBar = new char[barWidth];
				int enemyHealthBarLevel = (int)((enemy.Health / (float)enemy.MaxHealth) * barWidth);
				for (int i = 0; i < barWidth; i++)
				{
					enemyHealthBar[barWidth - i - 1] = i <= enemyHealthBarLevel ? '█' : ' ';
				}
				// render
				string healthBars = " HP " + new string(playerHealthBar) + " " + new string(enemyHealthBar) + " HP ";
				Console.SetCursorPosition(0, 1);
				Console.Write(healthBars);
			}
			{
				// player
				char[] playerEnergyBar = new char[barWidth];
				int playerEnergyBarLevel = (int)((player.Energy / (float)player.MaxEnergy) * barWidth);
				for (int i = 0; i < barWidth; i++)
				{
					playerEnergyBar[i] = i <= playerEnergyBarLevel ? '█' : ' ';
				}
				// enemy
				char[] enemyEnergyBar = new char[barWidth];
				int enemyEnergyBarLevel = (int)((enemy.Energy / (float)enemy.MaxEnergy) * barWidth);
				for (int i = 0; i < barWidth; i++)
				{
					enemyEnergyBar[barWidth - i - 1] = i <= enemyEnergyBarLevel ? '█' : ' ';
				}
				// render
				string energyBars = " EN " + new string(playerEnergyBar) + " " + new string(enemyEnergyBar) + " EN ";
				Console.SetCursorPosition(0, 3);
				Console.Write(energyBars);
			}
			#endregion

			if (player.Health <= 0 && player.Action is Ground)
			{
				Console.SetCursorPosition(0, Y + 8);
				Console.Write("You Lose.");
				break;
			}
			if (enemy.Health <= 0 && enemy.Action is Ground)
			{
				Console.SetCursorPosition(0, Y + 8);
				Console.Write("You Win.");
				break;
			}

			Thread.Sleep(sleep);
		}
		Console.ReadLine();
	}

	#region Render & Erase

	static void Render(string @string, bool renderSpace = false)
	{
		int x = Console.CursorLeft;
		int y = Console.CursorTop;
		foreach (char c in @string)
			if (c is '\n')
				Console.SetCursorPosition(x, ++y);
			else if (Console.CursorLeft < width - 1 && (c is not ' ' || renderSpace))
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
			else if (Console.CursorLeft < width - 1 && c is not ' ')
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
			public static readonly string[] IdleAnimation =
			[
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
			];

			public static readonly string[] BlockAnimation =
			[
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"    o_|  " + '\n' +
				@"    |-'  " + '\n' +
				@"    |    " + '\n' +
				@"   / /   ",
			];

			public static readonly string[] PunchAnimation =
			[
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
			];

			public static readonly string[] JumpKickAnimation =
			[
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
			];

			public static readonly string[] OwnedAnimation =
			[
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
				@"         " + '\n' +
				@"  o___/\ ",
			];

			public static readonly string[] GroundAnimation =
			[
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"  o___/\ ",
			];

			public static readonly string[] GetUpAnimation =
			[
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
			];
		}

		public static class Enemy
		{
			public static readonly string[] IdleAnimation =
			[
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
			];

			public static readonly string[] BlockAnimation =
			[
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"  |_o    " + '\n' +
				@"  '-|    " + '\n' +
				@"    |    " + '\n' +
				@"   \ \   ",
			];

			public static readonly string[] PunchAnimation =
			[
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
			];

			public static readonly string[] JumpKickAnimation =
			[
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"  O_     " + '\n' +
				@" _|\|    " + '\n' +
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
			];

			public static readonly string[] OwnedAnimation =
			[
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"  ___O   " + '\n' +
				@"  -'/    " + '\n' +
				@"   /\    " + '\n' +
				@"   \ \   ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"    \\   " + '\n' +
				@"   __\O  " + '\n' +
				@"  /\__   " + '\n' +
				@"  \      ",
				// 2
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"    \\   " + '\n' +
				@"  /\__O  " + '\n' +
				@"  /      ",
				// 3
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@" /\___o ",
			];

			public static readonly string[] GroundAnimation =
			[
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@" /\___o  ",
			];

			public static readonly string[] GetUpAnimation =
			[
				// 0
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"  __     " + '\n' +
				@"   /__o  ",
				// 1
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"   \     " + '\n' +
				@"   /__o  ",
				// 2
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"   |     " + '\n' +
				@"   |     " + '\n' +
				@"    \_o  ",
				// 3
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"   /|    " + '\n' +
				@"    \_o  " + '\n' +
				@"      /\ ",
				// 4
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"    /-\  " + '\n' +
				@"  // /o/ " + '\n' +
				@"         ",
				// 5
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"   /|o\  " + '\n' +
				@"  /      " + '\n' +
				@"  \\     ",
				// 6
				@"         " + '\n' +
				@"    _    " + '\n' +
				@"   /O__  " + '\n' +
				@"   /     " + '\n' +
				@"  /\     " + '\n' +
				@"  \ \    ",
				// 7
				@"         " + '\n' +
				@"         " + '\n' +
				@"         " + '\n' +
				@"   o     " + '\n' +
				@"   >\>   " + '\n' +
				@"   <<    ",
			];
		}

		#endregion
	}
}
