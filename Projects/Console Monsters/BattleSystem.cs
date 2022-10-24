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
		

		bool playerTurn = true;
		bool battleOver = false;

		if (OpponentMonster.SpeedStat > PlayerMonster.SpeedStat)
		{
			playerTurn = false;
		}
		DrawStats(playerTurn, PlayerMonster, OpponentMonster);
		while (!battleOver)
		{
			if (playerTurn)
			{
				AttackingMonster = PlayerMonster;
				DefendingMonster = OpponentMonster;
				if (PlayerMonster.CurrentHP <= 0)
				{
					Console.Clear();
					Console.WriteLine("Player Lost");
					battleOver = true;
				}
				else if(PlayerMonster.CurrentEnergy <= 0)
				{
					playerTurn = false;
					DrawStats(playerTurn, PlayerMonster, OpponentMonster);
				}
				else
				{

					ConsoleHelper.PressToContinue();
					MoveBase playerMove = MoveBase.GetRandomMove();
					PlayerMonster.CurrentEnergy -= playerMove.EnergyTaken;
					OpponentMonster.CurrentHP -= (int)playerMove.FinalDamage;
					playerTurn = false;
					DrawStats(playerTurn, PlayerMonster, OpponentMonster);
				}
			}
			if (!playerTurn)
			{
				AttackingMonster = OpponentMonster;
				DefendingMonster = PlayerMonster;
				if (OpponentMonster.CurrentHP <= 0)
				{
					Console.Clear();
					Console.WriteLine("CPU Lost");
					battleOver = true;
				}
				else
				{
					ConsoleHelper.PressToContinue();
					MoveBase opponentMove = MoveBase.GetRandomMove();
					OpponentMonster.CurrentEnergy -= opponentMove.EnergyTaken;
					PlayerMonster.CurrentHP -= (int)opponentMove.FinalDamage;
					playerTurn = true;
					DrawStats(playerTurn, PlayerMonster, OpponentMonster);
				}
			}
		}

		static void DrawStats(bool playerTurn, MonsterBase PlayerMonster, MonsterBase OpponentMonster)
		{
			//TEMP
			Console.SetCursorPosition(63, 34);
			Console.WriteLine($"HP:{PlayerMonster.CurrentHP}  Energy:{PlayerMonster.CurrentEnergy}  ");

			Console.SetCursorPosition(102, 13);
			Console.WriteLine($"HP:{OpponentMonster.CurrentHP}  Energy:{OpponentMonster.CurrentEnergy}  ");

			Console.SetCursorPosition(35, 5);
			string turn;
			if (playerTurn)
			{
				turn = "Player Turn";
			}
			else
			{
				turn = "CPU Turn   ";
			}
			Console.WriteLine(turn);
		}

	}
}
