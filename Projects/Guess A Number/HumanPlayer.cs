﻿namespace Guess_A_Number;
using System;

public class HumanPlayer
{
	public int GetInt(string prompt, string badInputPrompt = "Invalid!")
	{
		var input = 0;
		var validInput = false;
		while (!validInput)
		{
			Console.Write($"{prompt}: ");
			validInput = int.TryParse(Console.ReadLine(), out input);
			if (!validInput)
				Console.WriteLine(badInputPrompt);
		}

		return input;
	}

	public void Tell(string response) => Console.WriteLine(response);

	public void Wait()
	{
		if (!Console.IsOutputRedirected) //TODO workaround for Console Testing Helper
			Console.ReadKey(intercept: true);
	}
}