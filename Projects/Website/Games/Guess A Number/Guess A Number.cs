using System;
using System.Text;
using System.Threading.Tasks;

namespace Website.Games.Guess_A_Number;

public class Guess_A_Number
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		int value = Random.Shared.Next(1, 101);
		while (true)
		{
			await Console.Write("Guess a number (1-100): ");
			bool valid = int.TryParse((await Console.ReadLine()).Trim(), out int input);
			if (!valid) await Console.WriteLine("Invalid.");
			else if (input == value) break;
			else await Console.WriteLine($"Incorrect. Too {(input < value ? "Low" : "High")}.");
		}
		await Console.WriteLine("You guessed it!");
		await Console.Write("Press any key to exit...");
		await Console.ReadKey(true);
		await Console.Clear();
		await Console.Write("Guess A Number was closed.");
		await Console.Refresh();
	}
}
