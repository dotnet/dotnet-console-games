namespace Console_Monsters;

public class BattleSystem
{
	// TEMP FOR DEVELOPMENT, YES VERY MESSY, PLEASE FIX
	public static MonsterBase? AttackingMonster { get; set; }
	public static MonsterBase? DefendingMonster { get; set; }

	public static void Battle(MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		// Temp
		OpponentMonster.CurrentHP = 20;
		OpponentMonster.CurrentEnergy = 40;
		OpponentMonster.DefenseStat = 10;
		OpponentMonster.AttackStat = 10;
		OpponentMonster.SpeedStat = 9;

		bool playerTurn = false, battleOver = false;

		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

		PromptBattleText = new string[]
		{
			$"You Encountered A Wild {OpponentMonster.Name}!"
		};

		BattleScreen.Render(PlayerMonster, OpponentMonster);
		ConsoleHelper.PressToContinue();

		PromptBattleText = new string[]
		{
			" "
		};
		BattleScreen.Render(PlayerMonster, OpponentMonster);
		
		while (!battleOver)
		{
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

			Round(PlayerMonster, OpponentMonster, playerTurn);

			//Are all monsters dead on one side?
			battleOver = CheckLostAllMonsters();
			ConsoleHelper.PressToContinue();
		}

	}

	public static void Round(MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		// Who starts based on speed
		playerTurn = SelectStartingMonster(PlayerMonster, OpponentMonster);

		if (playerTurn)
		{

		}
		else
		{

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

	public static bool CheckLostAllMonsters()
	{
		// Credit: Ero#1111
		return partyMonsters.All(m => m.CurrentHP <= 0) || trainerMonsters.All(m => m.CurrentHP <= 0);
	}
}
