using System;
using System.Threading.Tasks;
using static Website.Games.Rock_Paper_Scissors.Rock_Paper_Scissors.Move;

namespace Website.Games.Rock_Paper_Scissors;

public class Rock_Paper_Scissors
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		int wins = 0;
		int draws = 0;
		int losses = 0;
		while (true)
		{
			await Console.Clear();
			await Console.WriteLine("Rock, Paper, Scissors");
			await Console.WriteLine();
		GetInput:
			await Console.Write("Choose [r]ock, [p]aper, [s]cissors, or [e]xit:");
			Move playerMove;
			switch ((await Console.ReadLine() ?? "").Trim().ToLower())
			{
				case "r" or "rock": playerMove = Rock; break;
				case "p" or "paper": playerMove = Paper; break;
				case "s" or "scissors": playerMove = Scissors; break;
				case "e" or "exit":
					await Console.Clear();
					await Console.WriteLine("Rock, Paper, Scissors was closed.");
					await Console.Refresh();
					return;
				default: await Console.WriteLine("Invalid Input. Try Again..."); goto GetInput;
			}
			Move computerMove = (Move)Random.Shared.Next(3);
			await Console.WriteLine($"The computer chose {computerMove}.");
			switch (playerMove, computerMove)
			{
				case (Rock, Paper) or (Paper, Scissors) or (Scissors, Rock):
					await Console.WriteLine("You lose.");
					losses++;
					break;
				case (Rock, Scissors) or (Paper, Rock) or (Scissors, Paper):
					await Console.WriteLine("You win.");
					wins++;
					break;
				default:
					await Console.WriteLine("This game was a draw.");
					draws++;
					break;
			}
			await Console.WriteLine($"Score: {wins} wins, {losses} losses, {draws} draws");
			await Console.WriteLine("Press Enter To Continue...");
			await Console.ReadLine();
		}
	}

	public enum Move
	{
		Rock = 0,
		Paper = 1,
		Scissors = 2,
	}
}
