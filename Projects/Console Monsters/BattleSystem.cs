namespace Console_Monsters;

public class BattleSystem
{
	// TEMP FOR DEVELOPMENT, YES VERY MESSY, PLEASE FIX
	public static MonsterBase? AttackingMonster { get; set; } = new _ErrorMonster();
	public static MonsterBase? DefendingMonster { get; set; } = new _ErrorMonster();

	public static void Battle(MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		// Temp
		OpponentMonster.CurrentHP = 20;
		OpponentMonster.MaximumHP = 20;
		OpponentMonster.CurrentEnergy = 40;
		OpponentMonster.DefenseStat = 10;
		OpponentMonster.AttackStat = 10;
		OpponentMonster.SpeedStat = 9;

		bool playerTurn = false, battleOver = false;

		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
		SetBattleText($"You Encountered A Wild {OpponentMonster.Name}", PlayerMonster, OpponentMonster);
		
		while (!battleOver)
		{
			// Who starts based on speed
			playerTurn = SelectStartingMonster(PlayerMonster, OpponentMonster);
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
			
			Turn(PlayerMonster, OpponentMonster, playerTurn);
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
			ConsoleHelper.PressToContinue();

			Turn(PlayerMonster, OpponentMonster, playerTurn);
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
			ConsoleHelper.PressToContinue();
			
			//Are all monsters dead on one side?
			battleOver = CheckLostAllMonsters();
		}
		//Battle is over, remove the losing monster from screen
		BattleScreen.Render(PlayerMonster, OpponentMonster);
	}

	public static void Turn(MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		if (playerTurn)
		{
			AttackingMonster = PlayerMonster;
			DefendingMonster = OpponentMonster;
			MoveBase playerMove = InputToMonsterMove(PlayerMonster);

			PlayerMonster.CurrentEnergy -= playerMove.EnergyTaken;
			OpponentMonster.CurrentHP -= playerMove.FinalDamage;

		}
		else
		{
			AttackingMonster = OpponentMonster;
			DefendingMonster = PlayerMonster;
			MoveBase opponentMove = MoveBase.GetRandomMove();

			OpponentMonster.CurrentEnergy -= opponentMove.EnergyTaken;
			PlayerMonster.CurrentHP -= opponentMove.FinalDamage;
		}
		playerTurn = !playerTurn;
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

	public static bool CheckLostAllMonsters()
	{
		// Credit: Ero#1111
		return partyMonsters.All(m => m.CurrentHP <= 0) || trainerMonsters.All(m => m.CurrentHP <= 0);
	}

	public static void SetBattleText(string prompt, MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		PromptBattleText = new string[]
		{
			//$"{AsciiGenerator.ToAscii($"You Encountered A Wild {OpponentMonster.Name}")}",
			//$"You Encountered A Wild {OpponentMonster.Name}!"
			prompt,
		};
		BattleScreen.Render(PlayerMonster, OpponentMonster);
		ConsoleHelper.PressToContinue();

		PromptBattleText = null;
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
			_ => throw new Exception("bug"),
		};
		return move;
	}
}
