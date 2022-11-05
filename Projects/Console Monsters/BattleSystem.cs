using Console_Monsters.Moves;
using System.Diagnostics;

namespace Console_Monsters;

public class BattleSystem
{
	// TEMP FOR DEVELOPMENT, YES VERY MESSY, PLEASE FIX
	public static MonsterBase? AttackingMonster { get; set; } = new _ErrorMonster();
	public static MonsterBase? DefendingMonster { get; set; } = new _ErrorMonster();

	public static void Battle(MonsterBase PlayerMonster, MonsterBase OpponentMonster)
	{
		bool playerTurn = SelectStartingMonster(PlayerMonster, OpponentMonster), battleOver = false;

		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
		DrawBattleText($"You Encountered A Wild {OpponentMonster.Name}", PlayerMonster, OpponentMonster, playerTurn);



		while (!battleOver)
		{
			// Who starts based on speed
			playerTurn = SelectStartingMonster(PlayerMonster, OpponentMonster);
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

			Turn(PlayerMonster, OpponentMonster, playerTurn);
			playerTurn = !playerTurn;
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

			if (CheckLostAllMonsters(OpponentMonster)) break;
			WaitBetweenAction();

			Turn(PlayerMonster, OpponentMonster, playerTurn);
			playerTurn = !playerTurn;
			BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);

			//Are all monsters dead on one side?
			battleOver = CheckLostAllMonsters(OpponentMonster);
		}
		//Battle is over, remove the losing monster from screen
		DrawBattleText($"", PlayerMonster, OpponentMonster, playerTurn);
	}

	public static void Turn(MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		if (playerTurn)
		{
			AttackingMonster = PlayerMonster;
			DefendingMonster = OpponentMonster;
			int attackStat = AttackingMonster.AttackStat;

			DrawMoveText(PlayerMonster, OpponentMonster, playerTurn);
			MoveBase playerMove = InputToMonsterMove(PlayerMonster);

			DrawBattleText($"{PlayerMonster.Name} used {playerMove.Name}", PlayerMonster, OpponentMonster, playerTurn);
			PlayerMonster.CurrentEnergy -= playerMove.EnergyTaken;
			OpponentMonster.CurrentHP -= playerMove.CalculateDamage(PlayerMonster, OpponentMonster);
		}
		else
		{
			AttackingMonster = OpponentMonster;
			DefendingMonster = PlayerMonster;

			MoveBase opponentMove = new Punch();

			DrawBattleText($"{OpponentMonster.Name} used {opponentMove.Name}", PlayerMonster, OpponentMonster, playerTurn);
			OpponentMonster.CurrentEnergy -= opponentMove.EnergyTaken;
			PlayerMonster.CurrentHP -= opponentMove.CalculateDamage(OpponentMonster, PlayerMonster);
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
	public static bool CheckLostAllMonsters(MonsterBase OpponentMonster)
	{
		return CheckLostAllPlayerMonsters() && CheckLostAllOpponentMonsters(OpponentMonster);
	}

	public static bool CheckLostAllPlayerMonsters()
	{
		// Credit: Ero#1111   -   Commented out due to trainerMonsters being empty for the time being
		//return partyMonsters.All(m => m.CurrentHP <= 0) || trainerMonsters.All(m => m.CurrentHP <= 0);
		return partyMonsters.All(m => m.CurrentHP <= 0);
	}
	public static bool CheckLostAllOpponentMonsters(MonsterBase OpponentMonster)
	{
		return OpponentMonster.CurrentHP <= 0;
	}

	public static void DrawMoveText(MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
	{
		PromptBattleText = new string[]
		{
			$"1.{PlayerMonster.MoveSet[0].Name}  3.{PlayerMonster.MoveSet[2].Name}",
			$"2.{PlayerMonster.MoveSet[1].Name}"
		};
		BattleScreen.Render(PlayerMonster, OpponentMonster);
		BattleScreen.DrawStats(playerTurn, PlayerMonster, OpponentMonster);
	}
	public static void DrawBattleText(string prompt, MonsterBase PlayerMonster, MonsterBase OpponentMonster, bool playerTurn)
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
}
