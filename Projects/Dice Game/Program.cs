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
	Console.WriteLine("Press any key to roll the dice:");

	Console.ReadKey(true);

	int playerRandomNum = random.Next(1, 7);
	Console.WriteLine("You rolled a " + playerRandomNum);

	Console.WriteLine("....");
	System.Threading.Thread.Sleep(1000);

	int rivalRandomNum = random.Next(1, 7);
	Console.WriteLine("Rival rolled a " + rivalRandomNum);

	if (playerRandomNum > rivalRandomNum)
	{
		playerPoints++;
		Console.WriteLine("The Player has won this round.");
	}
	else if (playerRandomNum < rivalRandomNum)
	{
		rivalPoints++;
		Console.WriteLine("The Rival has won this round.");
	}
	else
	{
		Console.WriteLine("This round is a draw!");
	}

	Console.WriteLine($"The score is now - Player : {playerPoints}. Rival : {rivalPoints}.");
	Console.WriteLine();
}

if (playerPoints > rivalPoints)
{
	Console.WriteLine("You have won!");
}
else if (playerPoints < rivalPoints)
{
	Console.WriteLine("You have lost!");
}
else
{
	Console.WriteLine("This game is a draw.");
}

Console.ReadKey(true);
