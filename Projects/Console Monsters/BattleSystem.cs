namespace Console_Monsters;

public class BattleSystem
{
	// TEMP FOR DEVELOPMENT, YES VERY MESSY, PLEASE FIX
	public static FireLizard FireLizard { get; set; } = new();
	public static Turtle Turtle { get; set; } = new();

	public static MonsterBase PlayerMonster { get; set; } = Turtle;
	public static MonsterBase OpponentMonster { get; set; } = FireLizard;

	public static MonsterBase? AttackingMonster { get; set; }
	public static MonsterBase? DefendingMonster { get; set; }

	public static void Battle()
	{
		bool playerTurn = true;
		bool battleOver = false;

		if (OpponentMonster.SpeedStat > PlayerMonster.SpeedStat)
		{
			playerTurn = false;
		}
		DrawStats(playerTurn);
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
				else
				{

					ConsoleHelper.PressToContinue();
					MoveBase playerMove = MoveBase.GetRandomMove();
					PlayerMonster.CurrentEnergy -= playerMove.EnergyTaken;
					OpponentMonster.CurrentHP -= (int)playerMove.FinalDamage;
					playerTurn = false;
					DrawStats(playerTurn);
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
					DrawStats(playerTurn);
				}
			}
		}

		static void DrawStats(bool playerTurn)
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
