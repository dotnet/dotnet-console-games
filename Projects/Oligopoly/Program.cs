using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading;

namespace Oligopoly;

public class Program
{
	public static List<Company> Companies;
	public static List<Event> Events;

	public static void Main()
	{
		RunMainMenu();
	}

	private static void RunMainMenu()
	{
		string prompt = @"
 ██████╗ ██╗     ██╗ ██████╗  ██████╗ ██████╗  ██████╗ ██╗  ██╗   ██╗
██╔═══██╗██║     ██║██╔════╝ ██╔═══██╗██╔══██╗██╔═══██╗██║  ╚██╗ ██╔╝
██║   ██║██║     ██║██║  ███╗██║   ██║██████╔╝██║   ██║██║   ╚████╔╝ 
██║   ██║██║     ██║██║   ██║██║   ██║██╔═══╝ ██║   ██║██║    ╚██╔╝  
╚██████╔╝███████╗██║╚██████╔╝╚██████╔╝██║     ╚██████╔╝███████╗██║   
 ╚═════╝ ╚══════╝╚═╝ ╚═════╝  ╚═════╝ ╚═╝      ╚═════╝ ╚══════╝╚═╝   
        --Use up and down arrow keys to select an option--                                                                     
";
		string[] options = { "Play", "About", "Exit" };

		Menu mainMenu = new(prompt, options, 0);

		int selectedOption = mainMenu.RunMenu();

		switch (selectedOption)
		{
			case 0:
				RunSkipMenu();
				break;
			case 1:
				DisplayAboutInfo();
				RunMainMenu();
				break;
			case 2:
				ExitGame();
				break;
		}
	}

	private static void RunSkipMenu()
	{
		string prompt = @"Welcome to Oligopoly!
Do you want to read the introductory letter or do you want to jump right into the gameplay?

";
		string[] options = { "Read introductory letter", "Skip introductory letter" };

		Menu skipMenu = new(prompt, options, 0);

		int selectedOption = skipMenu.RunMenu();

		switch (selectedOption)
		{
			case 0:
				RunStartMenu();
				break;
			case 1:
				RunGameMenu();
				break;
		}
	}

	private static void RunStartMenu()
	{
		string prompt = @"Dear, new CEO
    On behalf of the board of directors of Oligopoly Investments, we would like to congratulate you on becoming our new CEO. We are confident that you will lead our company to new heights of success and innovation.
    As CEO, you now have access to our exclusive internal software called Oligopoly, where you can track the latest news from leading companies and buy and sell their shares. This software will give you an edge over the competition and help you make important decisions for our company. To access the program, simply click the button at the bottom of this email.
    We look forward to working with you and supporting you in your new role.

Sincerely,
The board of directors of Oligopoly Investments

";
		string[] options = { "Get Access" };

		Menu startMenu = new(prompt, options, 15);

		int selectedOption = startMenu.RunMenu();

		if (selectedOption is 0)
		{
			Console.Clear();
			RunGameMenu();
		}
	}

	private static void RunGameMenu()
	{
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			{
				using Stream? stream = assembly.GetManifestResourceStream("Oligopoly.Company.json");
				Companies = JsonSerializer.Deserialize<List<Company>>(stream!)!;
			}
			{
				using Stream? stream = assembly.GetManifestResourceStream("Oligopoly.Event.json");
				Events = JsonSerializer.Deserialize<List<Event>>(stream!)!;
			}
		}

		double money = 10000;
		bool isGameEnded = false;
		Random random = new();

		// Start of the game cycle.
		while (!isGameEnded)
		{
			int currentEvent = random.Next(0, Events.Count);

			if (Events[currentEvent].Type is "Positive")
			{
				foreach (Company currentCompany in Companies)
				{
					if (currentCompany.Ticker == Events[currentEvent].Target)
					{
						currentCompany.SharePrice = Math.Round(currentCompany.SharePrice + currentCompany.SharePrice * Events[currentEvent].Effect / 100, 2);
					}
				}
			}
			else if (Events[currentEvent].Type is "Negative")
			{
				foreach (Company currentCompany in Companies)
				{
					if (currentCompany.Ticker == Events[currentEvent].Target)
					{
						currentCompany.SharePrice = Math.Round(currentCompany.SharePrice - currentCompany.SharePrice * Events[currentEvent].Effect / 100, 2);
					}
				}
			}

			string prompt = "\nUse up and down arrow keys to select an option: \n";
			string[] options = { "Skip", "Buy", "Sell", "More About Companies" };
			GameMenu gameMenu = new(prompt, options, 0, currentEvent, money);

			int selectedOption = gameMenu.RunMenu();

			switch (selectedOption)
			{
				case 0:
					break;
				case 1:
					RunActionMenu(ref money, currentEvent, true);
					break;
				case 2:
					RunActionMenu(ref money, currentEvent, false);
					break;
				case 3:
					DisplayMoreAboutCompaniesMenu();
					gameMenu.RunMenu();
					break;
			}

			// Check for win or loss.
			if (money <= 0)
			{
				isGameEnded = true;
				RunEndMenu(false);
			}
			else if (money >= 50000)
			{
				isGameEnded = true;
				RunEndMenu(true);
			}
		}
	}

	/// <summary>
	/// Runs action menu, that is buying or selling menu.
	/// </summary>
	/// <param name="money">The amount of money that user currently has.</param>
	/// <param name="currentEvent">The index of current generated event.</param>
	/// <param name="isBuying">Flag that determines the mode of the method. True - buying, false - selling.</param>
	private static void RunActionMenu(ref double money, int currentEvent, bool isBuying)
	{
		string actionPrompt = "Select a company: \n";
		string amountPrompt = "Select an amount: \n";
		string[] actionOptions = new string[Companies.Count];
		string[] amountOptions = { "Increase (+)", "Decrease (-)", "Enter" };

		for (int i = 0; i < actionOptions.Length; i++)
		{
			actionOptions[i] = Companies[i].Name;
		}

		GameMenu actionMenu = new(actionPrompt, actionOptions, 0, currentEvent, money);
		GameMenu amountMenu = new(amountPrompt, amountOptions, 0, currentEvent, money);

		int selectedCompany = actionMenu.RunMenu();
		int amountOfShares = amountMenu.RunAmountMenu();

		if (isBuying)
		{
			Companies[selectedCompany].ShareAmount += amountOfShares;
			money -= amountOfShares * Companies[selectedCompany].SharePrice;

			Console.WriteLine($"You have bought {amountOfShares} shares of {Companies[selectedCompany].Name} company.");
			Thread.Sleep(2500);
		}
		else
		{
			if (amountOfShares <= Companies[selectedCompany].ShareAmount)
			{
				Companies[selectedCompany].ShareAmount -= amountOfShares;
				money += amountOfShares * Companies[selectedCompany].SharePrice;

				Console.WriteLine($"You have sold {amountOfShares} shares of {Companies[selectedCompany].Name} company.");
				Thread.Sleep(2500);
			}
			else
			{
				Console.WriteLine("Entered not a valid value");
				Thread.Sleep(2500);
			}
		}
	}

	private static void DisplayMoreAboutCompaniesMenu()
	{
		Console.Clear();

		foreach (Company company in Companies)
		{
			Console.WriteLine($"{company.Name} - {company.Description}\n");
		}

		Console.WriteLine("Press any key to exit the menu...");
		Console.ReadKey(true);
	}

	/// <summary>
	/// Runs end menu.
	/// </summary>
	/// <param name="isWinner">Flag that determines the mode of the method. True - for a winner, false - for a loser.</param>
	private static void RunEndMenu(bool isWinner)
	{
		Console.Clear();

		if (isWinner)
		{
			string prompt = @"Dear CEO,
    On behalf of the board of directors of Oligopoly Investments, we would like to express our gratitude and understanding for your decision to leave your post. You have been a remarkable leader and a visionary strategist, who played the stock market skillfully and increased our budget by five times. We are proud of your achievements and we wish you all the best in your future endeavors.
    As a token of our appreciation, we are pleased to inform you that the company will pay you a bonus of $1 million. You deserve this reward for your hard work and dedication. We hope you will enjoy it and remember us fondly.
    Thank you for your service and your contribution to Oligopoly Investments. You will be missed.

Sincerely,
The board of directors of Oligopoly Investments

";
			string[] options = { "Play Again", "Return to Main Menu" };

			Menu EndMenu = new(prompt, options, 15);

			int selectedOption = EndMenu.RunMenu();

			switch (selectedOption)
			{
				case 0:
					RunGameMenu();
					break;
				case 1:
					RunMainMenu();
					break;
			}
		}
		else
		{
			string prompt = @"Dear, former CEO
    We regret to inform you that you are being removed from the position of CEO and fired from the company, effective immediately.
    The board of directors of Oligopoly Investments has decided to take this action because you have spent the budget allocated to you, and your investment turned out to be unprofitable for the company.
    We appreciate your service and wish you all the best in your future endeavors.

Sincerely,
The board of directors of Oligopoly Investments

";
			string[] options = { "Play Again", "Return to Main Menu" };

			Menu EndMenu = new(prompt, options, 15);

			int selectedOption = EndMenu.RunMenu();

			switch (selectedOption)
			{
				case 0:
					RunGameMenu();
					break;
				case 1:
					RunMainMenu();
					break;
			}
		}
	}

	private static void DisplayAboutInfo()
	{
		Console.Clear();
		Console.WriteLine(@"THANKS!
No really, thank you for taking time to play this simple console game. It means a lot.

This game was created by Semion Medvedev (Fuinny)
My GitHub profile: https://github.com/Fuinny

Press any key to exit the menu...");
		Console.ReadKey(true);
	}

	private static void ExitGame()
	{
		Console.Clear();
		Console.WriteLine("Press any key to exit the game...");
		Console.ReadKey(true);
		Environment.Exit(0);
	}
}