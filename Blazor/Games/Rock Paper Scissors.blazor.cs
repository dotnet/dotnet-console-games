using System.Diagnostics;
using Console = Blazor.Console<Blazor.Games.Rock_Paper_Scissors>;

namespace Blazor.Games;

public class Rock_Paper_Scissors
{
	public static async void Run()
	{
		Random random = new();
		int wins = 0;
		int draws = 0;
		int losses = 0;
		while (true)
		{
			Console.Clear();
			Console.WriteLine("Rock, Paper, Scissors");
			Console.WriteLine();
		GetInput:
			Console.Write("Choose [r]ock, [p]aper, [s]cissors, or [e]xit:");
			Move playerMove;
			switch ((await Console.ReadLine()).ToLower())
			{
				case "rock" or "r": playerMove = Move.Rock; break;
				case "paper" or "p": playerMove = Move.Paper; break;
				case "scissors" or "s": playerMove = Move.Scissors; break;
				case "exit" or "e": Console.Clear(); return;
				default: Console.WriteLine("Invalid Input. Try Again..."); goto GetInput;
			}
			Move computerMove = (Move)random.Next(3);
			Console.WriteLine($"The computer chose {computerMove}.");
			switch (playerMove, computerMove)
			{
				case (Move.Rock, Move.Paper):
				case (Move.Paper, Move.Scissors):
				case (Move.Scissors, Move.Rock):
					Console.WriteLine("You lose.");
					losses++;
					break;
				case (Move.Rock, Move.Scissors):
				case (Move.Paper, Move.Rock):
				case (Move.Scissors, Move.Paper):
					Console.WriteLine("You win.");
					wins++;
					break;
				default:
					Console.WriteLine("This game was a draw.");
					draws++;
					break;
			}
			Console.WriteLine($"Score: {wins} wins, {losses} losses, {draws} draws");
			Console.WriteLine($"Press Enter To Continue...");
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
