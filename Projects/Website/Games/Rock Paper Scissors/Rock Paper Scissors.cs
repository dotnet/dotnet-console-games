using System;
using System.Threading.Tasks;

namespace Website.Games.Rock_Paper_Scissors;

public class Rock_Paper_Scissors
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		Random random = new();
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
			switch ((await Console.ReadLine()).ToLower())
			{
				case "rock" or "r": playerMove = Move.Rock; break;
				case "paper" or "p": playerMove = Move.Paper; break;
				case "scissors" or "s": playerMove = Move.Scissors; break;
				case "exit" or "e":
					await Console.Clear();
					await Console.WriteLine("Rock, Paper, Scissors was closed.");
					await Console.Refresh();
					return;
				default: await Console.WriteLine("Invalid Input. Try Again..."); goto GetInput;
			}
			Move computerMove = (Move)random.Next(3);
			await Console.WriteLine($"The computer chose {computerMove}.");
			switch (playerMove, computerMove)
			{
				case (Move.Rock, Move.Paper):
				case (Move.Paper, Move.Scissors):
				case (Move.Scissors, Move.Rock):
					await Console.WriteLine("You lose.");
					losses++;
					break;
				case (Move.Rock, Move.Scissors):
				case (Move.Paper, Move.Rock):
				case (Move.Scissors, Move.Paper):
					await Console.WriteLine("You win.");
					wins++;
					break;
				default:
					await Console.WriteLine("This game was a draw.");
					draws++;
					break;
			}
			await Console.WriteLine($"Score: {wins} wins, {losses} losses, {draws} draws");
			await Console.WriteLine($"Press Enter To Continue...");
			await Console.ReadLine();
		}
	}

	enum Move
	{
		Rock = 0,
		Paper = 1,
		Scissors = 2,
	}
}
