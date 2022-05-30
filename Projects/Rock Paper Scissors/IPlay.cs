using System;

namespace Rock_Paper_Scissors;

public interface IPlay
{
	public int Wins { get; set; }
	public int Draws { get; set; }
	public int Losses { get; set; }

	public Move GetMove();
	public void Focus();

	public void Tell(string response);

	public void Wait();

	public void Score(Move yourMove, Move opponentMove)
	{
		Tell($"The opponent chose {opponentMove.Name}.");

		switch (yourMove.Verses(opponentMove))
		{
			case > 0:
				Tell("You win.");
				Wins++;
				break;
			case < 0:
				Tell("You lose.");
				Losses++;
				break;
			default:
				Tell("This game was a draw.");
				Draws++;
				break;
		}
	}
}
