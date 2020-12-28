using System;

Random random = new Random();
int wins = 0;
int losses = 0;
while (true)
{
	Console.Clear();
	Console.WriteLine("Rock, Paper, Scissors");
	Console.WriteLine();
GetInput:
	Console.Write("Choose [r]ock, [p]aper, [s]cissors, or [e]xit:");
	Move playerMove;
	switch(Console.ReadLine().ToLower())
	{
		case "rock"     or "r": playerMove = Move.Rock;     break;
		case "paper"    or "p": playerMove = Move.Paper;    break;
		case "scissors" or "s": playerMove = Move.Scissors; break;
		case "exit"     or "e": Console.Clear(); return;
		default: Console.WriteLine("Invalid Input. Try Again..."); goto GetInput;
	}
	Move computerMove = (Move)random.Next(3);
	Console.WriteLine($"The computer chose {computerMove}.");
	switch (playerMove, computerMove)
	{
		case (Move.Rock,     Move.Paper):
		case (Move.Paper,    Move.Scissors):
		case (Move.Scissors, Move.Rock):
			Console.WriteLine($"You lose.");
			losses++;
			break;
		case (Move.Rock,     Move.Scissors):
		case (Move.Paper,    Move.Rock):
		case (Move.Scissors, Move.Paper):
			Console.WriteLine($"You win.");
			wins++;
			break;
		default:
			Console.WriteLine("This game was a draw.");
			break;
	}
	Console.WriteLine($"Score: {wins} wins, {losses} losses");
	Console.WriteLine($"Press Enter To Continue...");
	Console.ReadLine();
}

enum Move
{
	Rock = 0,
	Paper = 1,
	Scissors = 2,
}
