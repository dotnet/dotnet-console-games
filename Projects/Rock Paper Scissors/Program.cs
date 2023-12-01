using System;
using static Move;

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
	switch ((Console.ReadLine() ?? "").Trim().ToLower())
	{
		case "r" or "rock": playerMove = Rock; break;
		case "p" or "paper": playerMove = Paper; break;
		case "s" or "scissors": playerMove = Scissors; break;
		case "e" or "exit": Console.Clear(); return;
		default: Console.WriteLine("Invalid Input. Try Again..."); goto GetInput;
	}
	Move computerMove = (Move)Random.Shared.Next(3);
	Console.WriteLine($"The computer chose {computerMove}.");
	switch (playerMove, computerMove)
	{
		case (Rock, Paper) or (Paper, Scissors) or (Scissors, Rock):
			Console.WriteLine("You lose.");
			losses++;
			break;
		case (Rock, Scissors) or (Paper, Rock) or (Scissors, Paper):
			Console.WriteLine("You win.");
			wins++;
			break;
		default:
			Console.WriteLine("This game was a draw.");
			draws++;
			break;
	}
	Console.WriteLine($"Score: {wins} wins, {losses} losses, {draws} draws");
	Console.WriteLine("Press Enter To Continue...");
	Console.ReadLine();
}

enum Move
{
	Rock = 0,
	Paper = 1,
	Scissors = 2,
}
