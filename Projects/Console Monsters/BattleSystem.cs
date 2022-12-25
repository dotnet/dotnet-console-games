using Console_Monsters.Moves;
using System.Diagnostics;

namespace Console_Monsters;

public class BattleSystem
{
	public static void Battle(MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		bool playerTurn = SelectStartingMonster(PlayerMonster, OpponentMonster), battleOver = false;

		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
		DrawSingleBattleText($"You Encountered A Wild {OpponentMonster.Name}", PlayerMonster, OpponentMonster, playerTurn);



		while (!battleOver)
		{
			// Who starts based on speed
			playerTurn = SelectStartingMonster(PlayerMonster, OpponentMonster);
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

			Turn(PlayerMonster, OpponentMonster, playerTurn);
			playerTurn = !playerTurn;
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

			if (CheckLostAllMonsters(PlayerMonster, OpponentMonster))
				break;
			WaitBetweenAction();

			Turn(PlayerMonster, OpponentMonster, playerTurn);
			playerTurn = !playerTurn;
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

			//Are all monsters dead on one side?
			battleOver = CheckLostAllMonsters(PlayerMonster, OpponentMonster);
		}
		//Battle is over, remove the losing monster from screen
		DrawSingleBattleText($"BATTLE OVER     Press [ENTER] to continue", PlayerMonster, OpponentMonster, playerTurn);
	}

	public static void Turn(MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		if (playerTurn)
		{
			BattleSelectionMenu(PlayerMonster, OpponentMonster, playerTurn);
			ConsoleKey key = Console.ReadKey(true).Key;
			switch (key.ToString())
			{
				case "D1": // Battle

					DrawMoveText(PlayerMonster, OpponentMonster, playerTurn);
					MoveBase playerMove = InputToMonsterMove(PlayerMonster);

					DrawSingleBattleText($"{PlayerMonster.Name} used {playerMove.Name}", PlayerMonster, OpponentMonster, playerTurn);

					PlayerMonster.CurrentEnergy -= playerMove.EnergyTaken;
					if (playerMove.DamageType != Enums.DamageType.Special && playerMove.BaseDamage != 0)
					{
						OpponentMonster.CurrentHP -=
							playerMove.CalculateDamage(PlayerMonster, OpponentMonster, playerMove);
					}
					else
					{
						playerMove.CalculateStatChange(PlayerMonster, OpponentMonster, playerMove);
					}
						

					if (OpponentMonster.CurrentHP < 0) OpponentMonster.CurrentHP = 0; // So the hp will not display under 0
					if (OpponentMonster.CurrentHP is < 1 or > 0) PlayerMonster.CurrentHP = 1; //So it doesn't display 0 when decimals.
					break;
				case "D2": // Monsters

					PlayerMonster = MonsterSelectionMenu(PlayerMonster, OpponentMonster, playerTurn);

					break;
				case "D3": // Inventory



					break;
				case "D4": // Run
					double escapeOdds = PlayerMonster.SpeedStat * 32 / (OpponentMonster.SpeedStat / 4) % 256;
					if (escapeOdds > 255 || Random.Shared.Next(0, 256) < escapeOdds)
					{
						//Esacpe
					}
					break;
				default:
					Turn(PlayerMonster, OpponentMonster, true);
					break;
			}
		}
		else
		{
			#warning Fix to individual Monster
			MoveBase opponentMove = MoveBase.GetRandomMove();

			DrawSingleBattleText($"{OpponentMonster.Name} used {opponentMove.Name}", PlayerMonster, OpponentMonster, playerTurn);
			OpponentMonster.CurrentEnergy -= opponentMove.EnergyTaken;
			if (opponentMove.DamageType != Enums.DamageType.Special && opponentMove.BaseDamage != 0)
			{
				PlayerMonster.CurrentHP -=
					opponentMove.CalculateDamage(OpponentMonster, PlayerMonster, opponentMove);
			}
			else
			{
				opponentMove.CalculateStatChange(OpponentMonster, PlayerMonster, opponentMove);
			}
			if (PlayerMonster.CurrentHP < 0) PlayerMonster.CurrentHP = 0; // So the hp will not display under 0
			if (PlayerMonster.CurrentHP < 1 || PlayerMonster.CurrentHP > 0) PlayerMonster.CurrentHP = 1; //So it doesn't display 0 when decimals.
		}
	}

	public static bool SelectStartingMonster(MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		if (PlayerMonster.SpeedStat > OpponentMonster.SpeedStat)
		{
			return true;
		}
		//In case they have the same Speed Stat
		else if (PlayerMonster.SpeedStat == OpponentMonster.SpeedStat)
		{
			return Random.Shared.Next(2) == 0;
		}
		return false;
	}

	public static void BattleSelectionMenu(MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		PromptBattleText = new string[]
		{
			$"1. Battle   3. Inventory",
			$"2. Monsters 4. Run"
		};

		BattleScreen.Render(PlayerMonster, OpponentMonster);
		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
	}

	public static MonsterBase MonsterSelectionMenu(MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		string empty = string.Empty;

		PromptBattleText = new string[]
		{
			$"1. {partyMonsters[0].Name} 2. {(partyMonsters.Count >= 2 ? partyMonsters[1].Name : empty)} 3. {(partyMonsters.Count >= 3 ? partyMonsters[2].Name : empty)}",
			$"4. {(partyMonsters.Count >= 4 ? partyMonsters[3].Name : empty)} 5. {(partyMonsters.Count >= 5 ? partyMonsters[4].Name : empty)} 6. {(partyMonsters.Count >= 6 ? partyMonsters[5].Name : empty)}"
		};

		BattleScreen.Render(PlayerMonster, OpponentMonster);
		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

		ConsoleKey key = Console.ReadKey(true).Key;
		PlayerMonster = key.ToString() switch
		{
			"D1" => partyMonsters[0],
			"D2" => partyMonsters[1],
			"D3" => partyMonsters[2],
			"D4" => partyMonsters[3],
			"D5" => partyMonsters[4],
			"D6" => partyMonsters[5],
			_ => MonsterSelectionMenu(PlayerMonster, OpponentMonster, playerTurn),
		};
		return PlayerMonster;
	}

	public static void DrawMoveText(MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		PromptBattleText = new string[]
		{
			$"1. {PlayerMonster.MoveSet[0].Name}  3. {PlayerMonster.MoveSet[2].Name}",
			$"2. {PlayerMonster.MoveSet[1].Name}  4. {PlayerMonster.MoveSet[3].Name}"
		};

		BattleScreen.Render(PlayerMonster, OpponentMonster);
		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
	}

	public static void DrawSingleBattleText(string prompt, MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		PromptBattleText = new string[]
		{
			//$"{AsciiGenerator.ToAscii($"You Encountered A Wild {OpponentMonster.Name}")}",
			prompt,
		};
		BattleScreen.Render(PlayerMonster, OpponentMonster);
		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

		WaitBetweenAction();

		PromptBattleText = new string[]
		{
			" ",
		};
		BattleScreen.Render(PlayerMonster, OpponentMonster);
	}

	public static MoveBase InputToMonsterMove(MonsterBase PlayerMonster)
	{
		ConsoleKey key = Console.ReadKey(true).Key;
		MoveBase move = key.ToString() switch
		{
			"D1" => PlayerMonster.MoveSet[0],
			"D2" => PlayerMonster.MoveSet[1],
			"D3" => PlayerMonster.MoveSet[2],
			"D4" => PlayerMonster.MoveSet[3],
			_ => InputToMonsterMove(PlayerMonster),
		};
		return move;
	}

	public static void WaitBetweenAction()
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
	public static bool CheckLostAllMonsters(MonsterBase PlayerMonster, MonsterBase OpponentMonster) // Temp
	{
		if (CheckLostCurrentPlayerMonster(PlayerMonster))
			return true;
		else if (CheckLostCurrentOpponentMonsters(OpponentMonster))
			return true;
		else
			return false;
	}
	public static bool CheckLostCurrentPlayerMonster(MonsterBase PlayerMonster) //Temp
	{
		return PlayerMonster.CurrentHP <= 0;
	}
	public static bool CheckLostCurrentOpponentMonsters(MonsterBase OpponentMonster)
	{
		return OpponentMonster.CurrentHP <= 0;
	}
	/*public static bool CheckLostAllPlayerMonsters(MonsterBase PlayerMonster)
	{
		//return partyMonsters.All(m => m.CurrentHP <= 0);
	}
	public static bool CheckLostAllOpponentMonsters(MonsterBase OpponentMonster)
	{
		trainerMonsters.All(m => m.CurrentHP <= 0
	} */
	#endregion
}
