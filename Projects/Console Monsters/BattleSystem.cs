namespace Console_Monsters;

public class BattleSystem
{
	// TEMP FOR DEVELOPMENT, YES VERY MESSY, PLEASE FIX

	public static MonsterBase? AttackingMonster { get; set; }
	public static MonsterBase? DefendingMonster { get; set; }

	public static void Battle(MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		//Temp
		OpponentMonster.CurrentHP = 20;
		OpponentMonster.CurrentEnergy = 40;
		OpponentMonster.DefenseStat = 10;
		OpponentMonster.AttackStat = 10;
		OpponentMonster.SpeedStat = 9;


		bool playerTurn = false;
		bool battleOver = false;

		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

		PromptBattleText = new string[]
		{
			$"You Encountered A Wild {OpponentMonster.Name}"
		};
		BattleScreen.Render(PlayerMonster, OpponentMonster);
		ConsoleHelper.PressToContinue();
		PromptBattleText = null;
		BattleScreen.Render(PlayerMonster, OpponentMonster);
		

		while (!battleOver)
		{
			playerTurn = false;
			if (PlayerMonster.SpeedStat > OpponentMonster.SpeedStat)
			{
				playerTurn = true;
			}
			//In case they have the same Speed Stat
			else if(PlayerMonster.SpeedStat == OpponentMonster.SpeedStat)
			{
				playerTurn = Random.Shared.Next(2) == 0;
			}
			


			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
			battleOver = CheckLostAllMonster();
		}

		static bool CheckLostAllMonster()
		{
			if (partyMonsters.All(x => x.CurrentHP <= 0))
			{
				return true;
			}
			if (trainerMonsters.All(x => x.CurrentHP <= 0))
			{
				return true;
			}
			return false;
		}
	}
}
