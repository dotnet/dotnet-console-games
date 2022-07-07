namespace Console_Monsters;

public class BattleSystem
{
	// TEMP FOR DEVELOPMENT
	static FireLizard fireLizard = new();
	static Turtle turtle = new();

	public static MonsterBase PlayerMonster = turtle;
	public static MonsterBase OpponentMonster = fireLizard;
	public static void Battle()
	{
		bool playerTurn = true;
		bool battleOver = false;

		if (OpponentMonster.SpeedStat > PlayerMonster.SpeedStat)
		{
			playerTurn = false;
		}
		while (!battleOver)
		{
			while (playerTurn)
			{
				MoveBase playerMove = MoveBase.GetRandomMove();
				PlayerMonster.CurrentEnergy -= playerMove.EnergyTaken;
				OpponentMonster.CurrentHP -= (int)playerMove.FinalDamage;
				if (PlayerMonster.CurrentHP <= 0)
				{
					Console.Clear();
					Console.Write("Player Lost");
				}
				else
				{
					playerTurn = false;
				}
				PressEnterToContiue();
			}
			while (!playerTurn)
			{
				MoveBase opponentMove = MoveBase.GetRandomMove();
				OpponentMonster.CurrentEnergy -= opponentMove.EnergyTaken;
				PlayerMonster.CurrentHP -= (int)opponentMove.FinalDamage;
				if (PlayerMonster.CurrentHP <= 0)
				{
					Console.Clear();
					Console.Write("Player Lost");
				}
				else
				{
					playerTurn = true;
				}
				PressEnterToContiue();
			}
		}
	}
}
