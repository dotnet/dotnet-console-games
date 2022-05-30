namespace Rock_Paper_Scissors;

using System;
using System.Linq;

public class HumanPlayer : IPlay
{
	public int Wins { get; set; }
	public int Draws { get; set; }
	public int Losses { get; set; }

	public Move GetMove()
	{
		Move? move = null;
		while (move is null)
		{
			Console.Write("Choose [r]ock, [p]aper, [s]cissors, or [e]xit:");
			var input = Console.ReadLine()?.ToLower();

			move = Move.All.FirstOrDefault(m => m.Name == input || m.ShortName == input);
		}
		return move;
	}

	public void Focus()
	{
		if (!Console.IsOutputRedirected) //TODO workaround for Console Testing Helper
			Console.Clear();
	}

	public void Tell(string response) => Console.WriteLine(response);

	public void Wait()
	{
		if (!Console.IsOutputRedirected) //TODO workaround for Console Testing Helper
			Console.ReadKey();
	}
}
