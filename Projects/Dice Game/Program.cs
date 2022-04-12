using System;

int playerRandomNum;
int rivalRandomNum;

int playerPoints = 0;
int rivalPoints = 0;

Random random = new Random();

for (int i = 0; i < 10; i++)
{
	Console.WriteLine("Press any key to roll the dice:");

	Console.ReadKey();

	playerRandomNum = random.Next(1, 7);
	Console.WriteLine("You rolled a " + playerRandomNum);

	Console.WriteLine("....");
	System.Threading.Thread.Sleep(1000);

	rivalRandomNum = random.Next(1, 7);
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

	Console.WriteLine("The score is now - Player : " + playerPoints + ". Rival : " + rivalPoints + ".");
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

Console.ReadKey();
