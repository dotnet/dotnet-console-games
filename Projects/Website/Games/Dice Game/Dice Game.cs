using System;
using System.Threading.Tasks;

namespace Website.Games.Dice_Game;

public class Dice_Game
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		int playerPoints = 0;
		int rivalPoints = 0;

		Random random = new();

		await Console.WriteLine("Dice Game");
		await Console.WriteLine();
		await Console.WriteLine("In this game you and a computer Rival will play 10 rounds");
		await Console.WriteLine("where you will each roll a 6-sided dice, and the player");
		await Console.WriteLine("with the highest dice value will win the round. The player");
		await Console.WriteLine("who wins the most rounds wins the game. Good luck!");
		await Console.WriteLine();
		await Console.Write("Press any key to start...");
		await Console.ReadKey(true);
		await Console.WriteLine();
		await Console.WriteLine();
		for (int i = 0; i < 10; i++)
		{
			await Console.WriteLine($"Round {i + 1}");
			int rivalRandomNum = random.Next(1, 7);
			await Console.WriteLine("Rival rolled a " + rivalRandomNum);
			await Console.Write("Press any key to roll the dice...");
			await Console.ReadKey(true);
			await Console.WriteLine();
			int playerRandomNum = random.Next(1, 7);
			await Console.WriteLine("You rolled a " + playerRandomNum);
			if (playerRandomNum > rivalRandomNum)
			{
				playerPoints++;
				await Console.WriteLine("You won this round.");
			}
			else if (playerRandomNum < rivalRandomNum)
			{
				rivalPoints++;
				await Console.WriteLine("The Rival won this round.");
			}
			else
			{
				await Console.WriteLine("This round is a draw!");
			}
			await Console.WriteLine($"The score is now - You : {playerPoints}. Rival : {rivalPoints}.");
			await Console.Write("Press any key to continue...");
			await Console.ReadKey(true);
			await Console.WriteLine();
			await Console.WriteLine();
		}
		await Console.WriteLine("Game over.");
		await Console.WriteLine($"The score is now - You : {playerPoints}. Rival : {rivalPoints}.");
		if (playerPoints > rivalPoints)
		{
			await Console.WriteLine("You won!");
		}
		else if (playerPoints < rivalPoints)
		{
			await Console.WriteLine("You lost!");
		}
		else
		{
			await Console.WriteLine("This game is a draw.");
		}
		await Console.Write("Press any key to exit...");
		await Console.ReadKey(true);
		await Console.Clear();
		await Console.Write("Dice Game was closed.");
		await Console.Refresh();
	}
}
