using System;
using System.Collections.Generic;
using System.Linq;

public class Move
{
	public static Move Rock { get; } = new Move("rock", "r", new[] { "scissors" });
	public static Move Scissors { get; } = new Move("scissors", "s", new[] { "paper" });
	public static Move Paper { get; } = new Move("paper", "p", new[] { "rock" });
	public static Move GiveUp { get; } = new Move("exit", "e", Array.Empty<string>());
	public static Move[] All { get; } = new[] { Rock, Paper, Scissors, GiveUp };

	private Move(string name, string shortName, string[] beats)
	{
		Name = name;
		ShortName = shortName;
		Beats = beats.ToDictionary(k => k, k => true);
	}

	public string Name { get; }
	public string ShortName { get; }
	protected Dictionary<string, bool> Beats = new();
	public MoveResult Verses(Move otherMove)
	{
		if (this == otherMove)
			return MoveResult.Draw;
		if (Beats.ContainsKey(otherMove.Name))
			return MoveResult.Win;
		return MoveResult.Lose;
	}
}

public enum MoveResult
{
	Lose = -1,
	Draw = 0,
	Win = 1,
}