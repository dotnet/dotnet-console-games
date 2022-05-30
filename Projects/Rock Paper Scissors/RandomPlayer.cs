namespace Rock_Paper_Scissors;

using System;
using System.Linq;

public class RandomPlayer : IPlay
{
	protected Random rand;

	public int Wins { get; set; }
	public int Draws { get; set; }
	public int Losses { get; set; }

	public RandomPlayer()
	{
		rand = Random.Shared;
	}

	public Move GetMove()
	{
		if (Losses == 100)
			return Move.GiveUp; //a fail safe when battling bots, could be better done elsewhere

		return Move.All
		.Where(move => move != Move.GiveUp)
		.Skip(rand.Next(0, Move.All.Length - 1))
		.First();
	}

	public void Focus(){}
	public void Tell(string response) { }
	public void Wait() { }
	
}