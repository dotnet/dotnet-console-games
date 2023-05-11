using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Oligopoly;

public class Program
{
	public static bool CloseRequested { get; set; } = false;
	public static List<Company> Companies { get; private set; } = null!;
	public static List<Event> Events { get; private set; } = null!;

	public static void Main()
	{
		try
		{
			Console.CursorVisible = false;
			LoadEmbeddedResources();
			RunMainMenu();
		}
		finally
		{
			Console.CursorVisible = true;
		}
	}

	public static void LoadEmbeddedResources()
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

	private static void RunMainMenu()
	{
		StringBuilder prompt = new();
		prompt.AppendLine();
		prompt.AppendLine("     ██████╗ ██╗     ██╗ ██████╗  ██████╗ ██████╗  ██████╗ ██╗  ██╗   ██╗");
		prompt.AppendLine("    ██╔═══██╗██║     ██║██╔════╝ ██╔═══██╗██╔══██╗██╔═══██╗██║  ╚██╗ ██╔╝");
		prompt.AppendLine("    ██║   ██║██║     ██║██║  ███╗██║   ██║██████╔╝██║   ██║██║   ╚████╔╝ ");
		prompt.AppendLine("    ██║   ██║██║     ██║██║   ██║██║   ██║██╔═══╝ ██║   ██║██║    ╚██╔╝  ");
		prompt.AppendLine("    ╚██████╔╝███████╗██║╚██████╔╝╚██████╔╝██║     ╚██████╔╝███████╗██║   ");
		prompt.AppendLine("     ╚═════╝ ╚══════╝╚═╝ ╚═════╝  ╚═════╝ ╚═╝      ╚═════╝ ╚══════╝╚═╝   ");
		prompt.AppendLine();
		prompt.Append("You can exit the game at any time by pressing ESCAPE.");
		prompt.AppendLine();
		prompt.Append("Use up and down arrow keys and enter to select an option:");
		Menu mainMenu = new(prompt.ToString(),
			new string[]
			{
				"Play",
				"About",
				"Exit"
			});

		while (!CloseRequested)
		{
			int selectedOption = mainMenu.RunMenu();
			switch (selectedOption)
			{
				case 0:
					RunStartMenu();
					break;
				case 1:
					DisplayAboutInfo();
					break;
				case 2:
					CloseRequested = true;
					break;
			}
		}
		Console.Clear();
		Console.WriteLine("Oligopoly was closed. Press any key to continue...");
		Console.CursorVisible = false;
		Console.ReadKey(true);
	}

	private static void RunStartMenu()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine("    ┌────────────────────────────────────────────────────────────────────────────────┐");
		Console.WriteLine("    │ Dear CEO,                                                                      │");
		Console.WriteLine("    │                                                                                │");
		Console.WriteLine("    │ Welcome to Oligopoly!                                                          │");
		Console.WriteLine("    │                                                                                │");
		Console.WriteLine("    │ On behalf of the board of directors of Oligopoly Investments, we would like to │");
		Console.WriteLine("    │ congratulate you on becoming our new CEO. We are confident that you will lead  │");
		Console.WriteLine("    │ our company to new heights of success and innovation. As CEO, you now have     │");
		Console.WriteLine("    │ access to our exclusive internal software called Oligopoly, where you can      |");
		Console.WriteLine("    │ track the latest news from leading companies and buy and sell their shares.    │");
		Console.WriteLine("    │ This software will give you an edge over the competition and help you make     │");
		Console.WriteLine("    │ important decisions for our company. To access the program, simply click the   │");
		Console.WriteLine("    │ button at the bottom of this email. We look forward to working with you and    │");
		Console.WriteLine("    │ supporting you in your new role.                                               │");
		Console.WriteLine("    │                                                                                │");
		Console.WriteLine("    │ Sincerely,                                                                     │");
		Console.WriteLine("    │ The board of directors of Oligopoly Investments                                │");
		Console.WriteLine("    └────────────────────────────────────────────────────────────────────────────────┘");
		Console.WriteLine();
		Console.WriteLine("Press any key to continue...");
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
		RunGameMenu();
	}

	private static void RunGameMenu()
	{
		double money = 10000;
		bool isGameEnded = false;
		Random random = new();

		// Start of the game cycle.
		while (!CloseRequested && !isGameEnded)
		{
			int currentEvent = random.Next(0, Events.Count);

			if (Events[currentEvent].Type is "Positive")
			{
				foreach (Company currentCompany in Companies)
				{
					if (currentCompany.Name == Events[currentEvent].CompanyName)
					{
						currentCompany.SharePrice = Math.Round(currentCompany.SharePrice + currentCompany.SharePrice * Events[currentEvent].Effect / 100, 2);
					}
				}
			}
			else if (Events[currentEvent].Type is "Negative")
			{
				foreach (Company currentCompany in Companies)
				{
					if (currentCompany.Name == Events[currentEvent].CompanyName)
					{
						currentCompany.SharePrice = Math.Round(currentCompany.SharePrice - currentCompany.SharePrice * Events[currentEvent].Effect / 100, 2);
					}
				}
			}

			string prompt = "Use up and down arrow keys and enter to select an option:";
			string[] options = { "Skip", "Buy", "Sell", "More About Companies" };
			GameMenu gameMenu = new(prompt, options, currentEvent, money);

			int selectedOption = -1;
			while (!CloseRequested && selectedOption is not 0)
			{
				selectedOption = gameMenu.RunMenu();
				switch (selectedOption)
				{
					case 1:
						RunActionMenu(ref money, currentEvent, true);
						break;
					case 2:
						RunActionMenu(ref money, currentEvent, false);
						break;
					case 3:
						DisplayMoreAboutCompaniesMenu();
						break;
				}
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

		GameMenu actionMenu = new(actionPrompt, actionOptions, currentEvent, money);
		GameMenu amountMenu = new(amountPrompt, amountOptions, currentEvent, money);

		int selectedCompany = actionMenu.RunMenu();
		int amountOfShares = amountMenu.RunAmountMenu();

		if (isBuying)
		{
			Companies[selectedCompany].ShareAmount += amountOfShares;
			money -= amountOfShares * Companies[selectedCompany].SharePrice;

			Console.WriteLine($"You have bought {amountOfShares} shares of {Companies[selectedCompany].Name} company.");
			Console.WriteLine("Press any key to continue...");
			Console.CursorVisible = false;
			CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
		}
		else
		{
			if (amountOfShares <= Companies[selectedCompany].ShareAmount)
			{
				Companies[selectedCompany].ShareAmount -= amountOfShares;
				money += amountOfShares * Companies[selectedCompany].SharePrice;

				Console.WriteLine($"You have sold {amountOfShares} shares of {Companies[selectedCompany].Name} company.");
				Console.WriteLine("Press any key to continue...");
				Console.CursorVisible = false;
				CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
			}
			else
			{
				Console.WriteLine("Entered not a valid value");
				Console.WriteLine("Press any key to continue...");
				Console.CursorVisible = false;
				CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
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
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
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
			Console.WriteLine();
			Console.WriteLine("    ┌────────────────────────────────────────────────────────────────────────────────┐");
			Console.WriteLine("    │ Dear CEO,                                                                      │");
			Console.WriteLine("    │                                                                                │");
			Console.WriteLine("    │ On behalf of the board of directors of Oligopoly Investments, we would like to │");
			Console.WriteLine("    │ express our gratitude and understanding for your decision to leave your post.  │");
			Console.WriteLine("    │ You have been a remarkable leader and a visionary strategist, who played the   │");
			Console.WriteLine("    │ stock market skillfully and increased our budget by five times. We are proud   │");
			Console.WriteLine("    │ of your achievements and we wish you all the best in your future endeavors. As │");
			Console.WriteLine("    │ a token of our appreciation, we are pleased to inform you that the company     │");
			Console.WriteLine("    │ will pay you a bonus of $1 million. You deserve this reward for your hard work │");
			Console.WriteLine("    │ and dedication. We hope you will enjoy it and remember us fondly. Thank you    │");
			Console.WriteLine("    │ for your service and your contribution to Oligopoly Investments.               │");
			Console.WriteLine("    │ You will be missed.                                                            │");
			Console.WriteLine("    │                                                                                │");
			Console.WriteLine("    │ Sincerely,                                                                     │");
			Console.WriteLine("    │ The board of directors of Oligopoly Investments                                │");
			Console.WriteLine("    └────────────────────────────────────────────────────────────────────────────────┘");
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.CursorVisible = false;
			CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
		}
		else
		{
			Console.WriteLine();
			Console.WriteLine("    ┌────────────────────────────────────────────────────────────────────────────────┐");
			Console.WriteLine("    │ Dear former CEO,                                                               │");
			Console.WriteLine("    │                                                                                │");
			Console.WriteLine("    │ We regret to inform you that you are being removed from the position of CEO    │");
			Console.WriteLine("    │ and fired from the company, effective immediately. The board of directors of   │");
			Console.WriteLine("    │ Oligopoly Investments has decided to take this action because you have spent   │");
			Console.WriteLine("    │ the budget allocated to you, and your investment turned out to be unprofitable │");
			Console.WriteLine("    │ for the company. We appreciate your service and wish you all the best in your  │");
			Console.WriteLine("    │ future endeavors.                                                              │");
			Console.WriteLine("    │                                                                                │");
			Console.WriteLine("    │ Sincerely,                                                                     │");
			Console.WriteLine("    │ The board of directors of Oligopoly Investments                                │");
			Console.WriteLine("    └────────────────────────────────────────────────────────────────────────────────┘");
			Console.WriteLine();
			Console.WriteLine("Press any key to continue...");
			Console.CursorVisible = false;
			CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
		}
	}

	private static void DisplayAboutInfo()
	{
		Console.Clear();
		Console.WriteLine("THANKS!");
		Console.WriteLine();
		Console.WriteLine("No really, thank you for taking time to play this simple console game. It means a lot.");
		Console.WriteLine();
		Console.WriteLine("This game was created by Semion Medvedev (Fuinny)");
		Console.WriteLine();
		Console.WriteLine("Press any key to continue...");
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
	}
}