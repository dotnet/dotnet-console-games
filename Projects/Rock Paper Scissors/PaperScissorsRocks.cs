using System;
using System.Linq;

namespace Rock_Paper_Scissors;
public class PaperScissorsRocks
{
	public IPlay[] Players;
	public PaperScissorsRocks(IPlay you, IPlay opponent)
	{
		Players = new[] { you, opponent };
	}

	public bool PlayRound()
	{
		var moves = Players.Select(p =>
		{
			p.Focus();
			p.Tell("Rock, Paper, Scissors\n");
			return p.GetMove();
		}).ToArray();

		if (moves.Contains(Move.GiveUp))
			return false;

		for (int i = 0; i < Players.Length; i++)
		{
			var p = Players[i];
			p.Score(moves[i], moves[^ (1+i)]);
			p.Tell($"Score: {p.Wins} wins, {p.Losses} losses, {p.Draws} draws");
			p.Tell($"Press Enter To Continue...");
			p.Wait();
		}

		return true;
	}
}
