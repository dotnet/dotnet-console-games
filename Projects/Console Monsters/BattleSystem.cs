using Console_Monsters.Moves;
using System.Diagnostics;

namespace Console_Monsters;

public class BattleSystem
{
	public struct BattleMonsters
	{
		public MonsterBase Player;
		public MonsterBase Opponent;
	}

	public static void Battle(BattleMonsters monsters)
	{
		bool battleOver = false;
		MonsterBase losingMonster = new _EmptyMonster();

		BattleScreen.DrawStats(monsters.Player, monsters.Opponent);
		DrawSingleBattleText($"You Encountered A Wild {monsters.Opponent.Name}", monsters);

		while (!battleOver)
		{
			BattleScreen.DrawStats(monsters.Player, monsters.Opponent);

			Turn(monsters);
			BattleScreen.DrawStats(monsters.Player, monsters.Opponent);

			(battleOver, losingMonster) = CheckLostAllMonsters(monsters);
			if (battleOver) break;

			WaitTimeBetweenChange();

			Turn(monsters);
			BattleScreen.DrawStats(monsters.Player, monsters.Opponent);

			//Are all monsters dead on one side?
			(battleOver, losingMonster) = CheckLostAllMonsters(monsters);
		}
		//BATLE IS OVER
		WaitTimeBetweenChange();

		PromptBattleText = new string[]
		{
			$"{losingMonster.Name} Fainted",
			$"BATTLE OVER     Press [ENTER] to continue",
		};

		//Battle is over, remove the losing monster from screen
		if (losingMonster == monsters.Opponent)
			BattleScreen.Render(monsters.Player, new _EmptyMonster());
		else
			BattleScreen.Render(new _EmptyMonster(), monsters.Opponent);

		ConsoleHelper.PressToContinue();
	}

	public static void Turn(BattleMonsters monsters)
	{
		MoveBase playerMove = new _EmptyMove();
		BattleSelectionMenu(monsters);
		ConsoleKey key = Console.ReadKey(true).Key;
		switch (key.ToString())
		{
			case "D1": // Battle

				PromptBattleText = new string[]
				{
					$"1. {monsters.Player.MoveSet[0].Name}  3. {monsters.Player.MoveSet[2].Name}",
					$"2. {monsters.Player.MoveSet[1].Name}  4. {monsters.Player.MoveSet[3].Name}"
				};

				BattleScreen.Render(monsters.Player, monsters.Opponent);
				BattleScreen.DrawStats(monsters.Player, monsters.Opponent);

				playerMove = InputToMonsterMove(monsters);

				break;
			case "D2": // Monsters
				//FIX
				monsters.Player = MonsterSelectionMenu(monsters);

				break;
			case "D3": // Inventory
				//FIX

				break;
			case "D4": // Run
				double escapeOdds = monsters.Player.SpeedStat * 32 / (monsters.Player.SpeedStat / 4) % 256;
				if (escapeOdds > 255 || Random.Shared.Next(0, 256) < escapeOdds)
				{
					//Esacpe FIX
				}
				break;
			default:
				Turn(monsters);
				break;
		}
		//OPPONENT MONSTER
		MoveBase opponentMove = MoveBase.GetRandomMove();

		bool playerTurn = SelectStartingMonster(monsters);

		PromptBattleText = new string[]
		{
			$" ",
		};
		BattleScreen.Render(monsters.Player, monsters.Opponent);
		BattleScreen.DrawStats(monsters.Player, monsters.Opponent);

		WaitTimeBetweenChange();

		if (playerTurn)
		{
			PlayerAttack(monsters, playerMove);
			CheckLostAllMonsters(monsters);
			OpponentAttack(monsters, opponentMove);
		} else
		{
			OpponentAttack(monsters, opponentMove);
			CheckLostAllMonsters(monsters);
			PlayerAttack(monsters, playerMove);
		}
		
	}

	public static void PlayerAttack(BattleMonsters monsters, MoveBase playerMove)
	{
		if (playerMove != new _EmptyMove())
		{
			DrawSingleBattleText($"{monsters.Player.Name} used {playerMove.Name}", monsters);

			monsters.Player.CurrentEnergy -= playerMove.EnergyTaken;
			if (playerMove.DamageType != Enums.DamageType.Special && playerMove.BaseDamage != 0)
			{
				monsters.Opponent.CurrentHP -=
					playerMove.CalculateDamage(monsters.Player, monsters.Opponent, playerMove);
			}
			else
			{
				playerMove.CalculateStatChange(monsters.Player, monsters.Opponent, playerMove);
			}

			if (monsters.Opponent.CurrentHP < 0) monsters.Opponent.CurrentHP = 0; // So the hp will not display under 0
			if (monsters.Opponent.CurrentHP is < 1 and > 0) monsters.Opponent.CurrentHP = 1; //So it doesn't display 0 when decimals.
		}
	}
	public static void OpponentAttack(BattleMonsters monsters, MoveBase opponentMove)
	{
		DrawSingleBattleText($"{monsters.Opponent.Name} used {opponentMove.Name}", monsters);

		monsters.Opponent.CurrentEnergy -= opponentMove.EnergyTaken;
		if (opponentMove.DamageType != Enums.DamageType.Special && opponentMove.BaseDamage != 0)
		{
			monsters.Player.CurrentHP -=
				opponentMove.CalculateDamage(monsters.Opponent, monsters.Player, opponentMove);
		}
		else
		{
			opponentMove.CalculateStatChange(monsters.Opponent, monsters.Player, opponentMove);
		}

		if (monsters.Player.CurrentHP < 0) monsters.Player.CurrentHP = 0; // So the hp will not display under 0
		if (monsters.Player.CurrentHP is < 1 and > 0) monsters.Player.CurrentHP = 1; //So it doesn't display 0 when decimals.
	}

	public static bool SelectStartingMonster(BattleMonsters monsters)
	{
#warning ADD Move priority

		if (monsters.Player.SpeedStat > monsters.Opponent.SpeedStat)
		{
			return true;
		}
		//In case they have the same Speed Stat
		else if (monsters.Player.SpeedStat == monsters.Opponent.SpeedStat)
		{
			return Random.Shared.Next(2) == 0;
		}
		return false;
	}

	public static void BattleSelectionMenu(BattleMonsters monsters)
	{
		PromptBattleText = new string[]
		{
			$"1. Battle   3. Inventory",
			$"2. Monsters 4. Run"
		};

		BattleScreen.Render(monsters.Player, monsters.Opponent);
		BattleScreen.DrawStats(monsters.Player, monsters.Opponent);
	}

	public static MonsterBase MonsterSelectionMenu(BattleMonsters monsters)
	{
		string empty = string.Empty;

#warning FIX LOOK
		PromptBattleText = new string[]
		{
			$"1. {partyMonsters[0].Name} 2. {(partyMonsters.Count >= 2 ? partyMonsters[1].Name : empty)} 3. {(partyMonsters.Count >= 3 ? partyMonsters[2].Name : empty)}",
			$"4. {(partyMonsters.Count >= 4 ? partyMonsters[3].Name : empty)} 5. {(partyMonsters.Count >= 5 ? partyMonsters[4].Name : empty)} 6. {(partyMonsters.Count >= 6 ? partyMonsters[5].Name : empty)}"
		};

		BattleScreen.Render(monsters.Player, monsters.Opponent);
		BattleScreen.DrawStats(monsters.Player, monsters.Opponent);

		ConsoleKey key = Console.ReadKey(true).Key;
		monsters.Player = key.ToString() switch
		{
			"D1" => partyMonsters[0],
			"D2" => partyMonsters[1],
			"D3" => partyMonsters[2],
			"D4" => partyMonsters[3],
			"D5" => partyMonsters[4],
			"D6" => partyMonsters[5],
			_ => MonsterSelectionMenu(monsters),
		};
		return monsters.Player;
	}

	public static void DrawSingleBattleText(string prompt, BattleMonsters monsters)
	{
		PromptBattleText = new string[]
		{
			//$"{AsciiGenerator.ToAscii($"{prompt}")}",
			prompt,
		};
		BattleScreen.Render(monsters.Player, monsters.Opponent);
		BattleScreen.DrawStats(monsters.Player, monsters.Opponent);

		WaitTimeBetweenChange();

		PromptBattleText = new string[]
		{
			" ",
		};
		BattleScreen.Render(monsters.Player, monsters.Opponent);
	}

	public static MoveBase InputToMonsterMove(BattleMonsters monsters)
	{
		ConsoleKey key = Console.ReadKey(true).Key;
		MoveBase move = key.ToString() switch
		{
			"D1" => monsters.Player.MoveSet[0],
			"D2" => monsters.Player.MoveSet[1],
			"D3" => monsters.Player.MoveSet[2],
			"D4" => monsters.Player.MoveSet[3],
			_ => InputToMonsterMove(monsters),
		};
		return move;
	}

	public static void WaitTimeBetweenChange()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		TimeSpan timeTaken = stopwatch.Elapsed;
		int timeToWait = FastText ? 1 : 3;

		while (timeTaken.TotalSeconds <= timeToWait)
		{
			timeTaken = stopwatch.Elapsed;
		}
	}

	#region Check Lost Monster
	public static (bool, MonsterBase) CheckLostAllMonsters(BattleMonsters monsters) // Temp
	{
		if (monsters.Player.CurrentHP <= 0)
			return (true, monsters.Player);

		else if (monsters.Opponent.CurrentHP <= 0)
			return (true, monsters.Opponent);
		else
			return (false, new _EmptyMonster());
	}
	#endregion
}
