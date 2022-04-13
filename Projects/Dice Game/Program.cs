using System;

int playerPoints = 0;
int rivalPoints = 0;

Random random = new Random();

Console.WriteLine("Dice Game");
Console.WriteLine();
Console.WriteLine("In this game you and a computer Rival will play 10 rounds");
Console.WriteLine("where you will each roll a 6-sided dice, and the player");
Console.WriteLine("with the highest dice value will win the round. The player");
Console.WriteLine("who wins the most rounds wins the game. Good luck!");
Console.WriteLine();
Console.Write("Press any key to start...");
Console.ReadKey(true);
Console.WriteLine();
Console.WriteLine();
for (int i = 0; i < 10; i++)
{
	Console.WriteLine($"Round {i + 1}");
	int rivalRandomNum = random.Next(1, 7);
	Console.WriteLine("Rival rolled a " + rivalRandomNum);
	Console.Write("Press any key to roll the dice...");
	Console.ReadKey(true);
	Console.WriteLine();
	int playerRandomNum = random.Next(1, 7);
	Console.WriteLine("You rolled a " + playerRandomNum);
	if (playerRandomNum > rivalRandomNum)
	{
		playerPoints++;
		Console.WriteLine("You won this round.");
	}
	else if (playerRandomNum < rivalRandomNum)
	{
		rivalPoints++;
		Console.WriteLine("The Rival won this round.");
	}
	else
	{
		Console.WriteLine("This round is a draw!");
	}
	Console.WriteLine($"The score is now - You : {playerPoints}. Rival : {rivalPoints}.");
	Console.Write("Press any key to continue...");
	Console.ReadKey(true);
	Console.WriteLine();
	Console.WriteLine();
}
Console.WriteLine("Game over.");
Console.WriteLine($"The score is now - You : {playerPoints}. Rival : {rivalPoints}.");
if (playerPoints > rivalPoints)
{
	Console.WriteLine("You won!");
}
else if (playerPoints < rivalPoints)
{
	Console.WriteLine("You lost!");
}
else
{
	Console.WriteLine("This game is a draw.");
}
Console.Write("Press any key to exit...");
Console.ReadKey(true);
