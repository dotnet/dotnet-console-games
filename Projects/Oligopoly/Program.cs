using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Oligopoly;

public class Program
{
	private static bool CloseRequested { get; set; } = false;
	private static List<Company> Companies { get; set; } = null!;
	private static List<Event> Events { get; set; } = null!;
	private static List<Event> GlobalEvents { get; set; } = null!;
	private static Event CurrentEvent { get; set; } = null!;
	private static int PositiveEventChance { get; set; }
	private static int TurnCounter { get; set; }
	private static decimal Money = 10000m;
	private static decimal NetWorth { get; set; }
	private static decimal LosingNetWorth = 2000.00m;
	private static decimal WinningNetWorth = 50000.00m;

	public static void Main()
	{
		#region Trim Prevention

		// We need to call the default constructors so that they do not get
		// trimmed when compiling with the -p:PublishTrimmed=true option.
		// The default constructor is required for JSON deserialization.

		_ = new Company()
		{
			Name = nameof(Company.Name),
			Industry = nameof(Company.Industry),
			SharePrice = 42.42m,
			NumberOfShares = 42,
			Description = nameof(Company.Description)
		};

		_ = new Event()
		{
			Title = nameof(Event.Title),
			Description = nameof(Event.Description),
			CompanyName = nameof(Event.CompanyName),
			Effect = 42
		};

		#endregion

		try
		{
			MainMenuScreen();
		}
		finally
		{
			Console.ResetColor();
			Console.CursorVisible = true;
		}
	}

	private static void LoadEmbeddedResources()
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
		{
			using Stream? stream = assembly.GetManifestResourceStream("Oligopoly.GlobalEvent.json");
			GlobalEvents = JsonSerializer.Deserialize<List<Event>>(stream!)!;
		}
	}

	private static void MainMenuScreen()
	{
		while (!CloseRequested)
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
			int selectedIndex = HandleMenuWithOptions(prompt.ToString(),
				new string[]
				{
					"Play",
					"About",
					"Exit",
				});
			switch (selectedIndex)
			{
				case 0: IntroductionScreen(); break;
				case 1: AboutInfoScreen(); break;
				case 2: CloseRequested = true; break;
			}
		}
		Console.Clear();
		Console.WriteLine("Oligopoly was closed. Press any key to continue...");
		Console.CursorVisible = false;
		Console.ReadKey(true);
	}

	private static void GameSetupScreen()
	{
		string prompt = @"
 ██████╗  █████╗ ███╗   ███╗███████╗    ███████╗███████╗████████╗██╗   ██╗██████╗
██╔════╝ ██╔══██╗████╗ ████║██╔════╝    ██╔════╝██╔════╝╚══██╔══╝██║   ██║██╔══██╗
██║  ███╗███████║██╔████╔██║█████╗      ███████╗█████╗     ██║   ██║   ██║██████╔╝
██║   ██║██╔══██║██║╚██╔╝██║██╔══╝      ╚════██║██╔══╝     ██║   ██║   ██║██╔═══╝
╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗    ███████║███████╗   ██║   ╚██████╔╝██║
 ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝    ╚══════╝╚══════╝   ╚═╝    ╚═════╝ ╚═╝
         Customize the game to make it interesting for you to play ;)
";
		string[] difficultiesOptions = { "Easy", "Normal", "Hard" };
		string[] gameModesOptions = { "Standard", "Random", "Custom" };
		string[] difficultiesDescriptions =
		{
			"60% chance that the next market event will be positive",
			"50% chance that the next market event will be positive/negative",
			"60% change that the next market event will be negative"
		};
		string[] gameModesDescriptions =
		{
			"Just standard mode, nothing out of the ordinary",
			"Your money and company shares prices will be randomly generated",
			"You can set the starting amount of your money"
		};

		TurnCounter = 1;
		LoadEmbeddedResources();
		int selectedMode = HandleMenuWithOptions(prompt, gameModesOptions, gameModesDescriptions);
		int selectedDifficulty = HandleMenuWithOptions(prompt, difficultiesOptions, difficultiesDescriptions);

		switch (selectedDifficulty)
		{
			case 0: PositiveEventChance = 60; break;
			case 1: PositiveEventChance = 50; break;
			case 2: PositiveEventChance = 40; break;
		}

		switch (selectedMode)
		{
			case 1:
				foreach (Company company in Companies)
				{
					company.SharePrice = Random.Shared.Next(100, 5001);
				}
				Money = Random.Shared.Next(1000, 30001);
				break;
			case 2:
				Money = MoneySetupScreen();
				break;
		}
	}

	private static void GameLoop()
	{
		while (!CloseRequested)
		{
			CalculateNetWorth();

			int selectedOption = -1;
			while (!CloseRequested && selectedOption is not 0)
			{
				StringBuilder prompt = RenderCompanyStocksTable();
				prompt.AppendLine();
				prompt.Append("Use up and down arrow keys and enter to select an option:");
				selectedOption = HandleMenuWithOptions(prompt.ToString(), new string[]
					{
						"Wait For Market Change",
						"Buy",
						"Sell",
						"Information About Companies",
					});
				switch (selectedOption)
				{
					case 1: BuyOrSellStockScreen(true); break;
					case 2: BuyOrSellStockScreen(false); break;
					case 3: CompanyDetailsScreen(); break;
				}
			}

			UpdateSharePrices();
			EventScreen();

			if (NetWorth > WinningNetWorth)
			{
				PlayerWinsScreen();
				return;
			}
			else if (NetWorth < LosingNetWorth)
			{
				PlayerLosesScreen();
				return;
			}

			TurnCounter++;
		}
	}

	private static void EventScreen()
	{
		StringBuilder prompt = RenderCompanyStocksTable();
		prompt.AppendLine();

		bool isPositive = Random.Shared.Next(0, 101) <= PositiveEventChance;

		if (TurnCounter % 50 == 0)
		{
			if (isPositive)
			{
				List<Event> positiveGlobalEvents = GlobalEvents.Where(e => e.Effect > 0).ToList();
				CurrentEvent = positiveGlobalEvents[Random.Shared.Next(0, positiveGlobalEvents.Count)];
			}
			else
			{
				List<Event> negativeGlobalEvents = GlobalEvents.Where(e => e.Effect < 0).ToList();
				CurrentEvent = negativeGlobalEvents[Random.Shared.Next(0, negativeGlobalEvents.Count)];
			}
		}
		else
		{
			if (isPositive)
			{
				List<Event> positiveEvents = Events.Where(e => e.Effect > 0).ToList();
				CurrentEvent = positiveEvents[Random.Shared.Next(0, positiveEvents.Count)];
			}
			else
			{
				List<Event> negativeEvents = Events.Where(e => e.Effect < 0).ToList();
				CurrentEvent = negativeEvents[Random.Shared.Next(0, negativeEvents.Count)];
			}
		}

		prompt.AppendLine();
		prompt.AppendLine($"{CurrentEvent.Title}");
		prompt.AppendLine();
		prompt.AppendLine($"{CurrentEvent.Description}");
		prompt.AppendLine();
		prompt.Append("Press any key to continue...");
		Console.Clear();
		Console.Write(prompt);
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
	}

	private static void BuyOrSellStockScreen(bool isBuying)
	{
		int[] numberOfShares = new int[Companies.Count];
		int index = 0;
		ConsoleKey key = default;
		while (!CloseRequested && key is not ConsoleKey.Enter)
		{
			// calculate the current cost of the transaction
			decimal cost = 0.00m;
			for (int i = 0; i < Companies.Count; i++)
			{
				cost += numberOfShares[i] * Companies[i].SharePrice;
			}

			Console.Clear();
			Console.WriteLine(RenderCompanyStocksTable());
			Console.WriteLine();
			Console.WriteLine($"Use the arrow keys and enter to confirm how many shares to {(isBuying ? "buy" : "sell")}:");
			for (int i = 0; i < Companies.Count; i++)
			{
				if (i == index)
				{
					(Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
					Console.WriteLine($" < {numberOfShares[i]}{(!isBuying ? $"/{Companies[i].NumberOfShares}" : "")} > {Companies[i].Name}");
					Console.ResetColor();
				}
				else
				{
					Console.WriteLine($"   {numberOfShares[i]}{(!isBuying ? $"/{Companies[i].NumberOfShares}" : "")}   {Companies[i].Name}");
				}
			}
			Console.WriteLine();
			Console.WriteLine($"{(isBuying ? "Cost" : "Payout")}: {cost:C}");

			key = Console.ReadKey(true).Key;
			switch (key)
			{
				case ConsoleKey.Escape: CloseRequested = true; return;
				case ConsoleKey.UpArrow: index = index is 0 ? Companies.Count - 1 : index - 1; break;
				case ConsoleKey.DownArrow: index = index == Companies.Count - 1 ? 0 : index + 1; break;
				case ConsoleKey.RightArrow:
					if (isBuying)
					{
						if (cost + Companies[index].SharePrice <= Money)
						{
							numberOfShares[index]++;
						}
					}
					else
					{
						if (numberOfShares[index] < Companies[index].NumberOfShares)
						{
							numberOfShares[index]++;
						}
					}
					break;
				case ConsoleKey.LeftArrow:
					if (numberOfShares[index] > 0)
					{
						numberOfShares[index]--;
					}
					break;

			}
		}

		if (CloseRequested)
		{
			return;
		}

		if (isBuying)
		{
			for (int i = 0; i < Companies.Count; i++)
			{
				Money -= numberOfShares[i] * Companies[i].SharePrice;
				Companies[i].NumberOfShares += numberOfShares[i];
			}
		}
		else
		{
			for (int i = 0; i < Companies.Count; i++)
			{
				Money += numberOfShares[i] * Companies[i].SharePrice;
				Companies[i].NumberOfShares -= numberOfShares[i];
			}
		}

		Console.Clear();
		Console.WriteLine(RenderCompanyStocksTable());
		Console.WriteLine($"You shares have been updated.");
		Console.WriteLine();
		Console.Write("Press any key to continue...");
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
	}

	private static int MoneySetupScreen()
	{
		bool isEnd = false;
		int index = 0, customMoney = 1000;
		string[] options = { "(+) Increase", "(-) Decrease", "Done"};
		ConsoleKey key = default;
		while (!CloseRequested && !isEnd)
		{
			Console.Clear();
			Console.WriteLine($"Your starting amount of money will be set to {customMoney:C}");
			Console.WriteLine("Use up and down arrow keys and enter to select an option:\n");
			for (int i = 0; i < options.Length; i++)
			{
				string currentOption = options[i];
				if (i == index)
				{
					(Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
					Console.WriteLine($"[*] {currentOption}");
					Console.ResetColor();
				}
				else
				{
					Console.WriteLine($"[ ] {currentOption}");
				}
			}

			Console.CursorVisible = false;
			key = Console.ReadKey().Key;
			switch (key)
			{
				case ConsoleKey.UpArrow: index = index is 0 ? options.Length - 1 : index - 1; break;
				case ConsoleKey.DownArrow: index = index == options.Length - 1 ? 0 : index + 1; break;
				case ConsoleKey.Enter:
					if (index == 0)
					{
						customMoney += 100;
					}
					else if (index == 1)
					{
						if (customMoney - 100 >= 1000)
						{
							customMoney -= 100;
						}	
					}
					else
					{
						isEnd = true;
					}
				break;
				case ConsoleKey.Escape: CloseRequested = true; break;
			}
		}
		return customMoney;
	}

	private static void CompanyDetailsScreen()
	{
		Console.Clear();
		foreach (Company company in Companies)
		{
			Console.WriteLine($"{company.Name} - {company.Description}");
			Console.WriteLine();
		}
		Console.Write("Press any key to exit the menu...");
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
	}

	private static void IntroductionScreen()
	{
		Console.Clear();
		Console.WriteLine(@"
          ██╗    ██╗███████╗██╗      ██████╗ ██████╗ ███╗   ███╗███████╗
          ██║    ██║██╔════╝██║     ██╔════╝██╔═══██╗████╗ ████║██╔════╝
          ██║ █╗ ██║█████╗  ██║     ██║     ██║   ██║██╔████╔██║█████╗  
          ██║███╗██║██╔══╝  ██║     ██║     ██║   ██║██║╚██╔╝██║██╔══╝  
          ╚███╔███╔╝███████╗███████╗╚██████╗╚██████╔╝██║ ╚═╝ ██║███████╗
           ╚══╝╚══╝ ╚══════╝╚══════╝ ╚═════╝ ╚═════╝ ╚═╝     ╚═╝╚══════╝
╔════════════════════════════════════════════════════════════════════════════════╗
║ Dear new CEO,                                                                  ║
║                                                                                ║
║ Welcome to Oligopoly!                                                          ║
║                                                                                ║
║ On behalf of the board of directors of Oligopoly Investments, we would like to ║
║ congratulate you on becoming our new CEO. We are confident that you will lead  ║
║ our company to new heights of success and innovation. As CEO, you now have     ║
║ access to our exclusive internal software called Oligopoly, where you can      ║
║ track the latest news from leading companies and buy and sell their shares.    ║
║ This software will give you an edge over the competition and help you make     ║
║ important decisions for our company. To access the program, simply click the   ║
║ button at the bottom of this email. We look forward to working with you and    ║
║ supporting you in your new role.                                               ║
║                                                                                ║
║ Sincerely,                                                                     ║
║ The board of directors of Oligopoly Investments                                ║
╚════════════════════════════════════════════════════════════════════════════════╝
");
		Console.Write("Press any key to continue...");
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
		GameSetupScreen();
		GameLoop();
	}

	private static void PlayerWinsScreen()
	{
		Console.Clear();
		Console.WriteLine(@$"
          ██╗   ██╗ ██████╗ ██╗   ██╗    ██╗    ██╗██╗███╗   ██╗
          ╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║    ██║██║████╗  ██║
           ╚████╔╝ ██║   ██║██║   ██║    ██║ █╗ ██║██║██╔██╗ ██║
            ╚██╔╝  ██║   ██║██║   ██║    ██║███╗██║██║██║╚██╗██║
             ██║   ╚██████╔╝╚██████╔╝    ╚███╔███╔╝██║██║ ╚████║
             ╚═╝    ╚═════╝  ╚═════╝      ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝
╔════════════════════════════════════════════════════════════════════════════════╗
║ Dear CEO,                                                                      ║
║                                                                                ║
║ On behalf of the board of directors of Oligopoly Investments, we would like to ║
║ express our gratitude and understanding for your decision to leave your post.  ║
║ You have been a remarkable leader and a visionary strategist, who played the   ║
║ stock market skillfully and increased our budget by five times. We are proud   ║
║ of your achievements and we wish you all the best in your future endeavors. As ║
║ a token of our appreciation, we are pleased to inform you that the company     ║
║ will pay you a bonus of $1 million. You deserve this reward for your hard work ║
║ and dedication. We hope you will enjoy it and remember us fondly. Thank you    ║
║ for your service and your contribution to Oligopoly Investments.               ║
║ You will be missed.                                                            ║
║                                                                                ║
║ Sincerely,                                                                     ║
║ The board of directors of Oligopoly Investments                                ║
╚════════════════════════════════════════════════════════════════════════════════╝

Your Net Worth is over {WinningNetWorth}$
You have played {TurnCounter} turns
Congratulations!
");
		Console.Write("Press any key (except ENTER) to continue...");
		ConsoleKey key = ConsoleKey.Enter;
		while (!CloseRequested && key is ConsoleKey.Enter)
		{
			Console.CursorVisible = false;
			key = Console.ReadKey(true).Key;
			CloseRequested = CloseRequested || key is ConsoleKey.Escape;
		}
	}

	private static void PlayerLosesScreen()
	{
		Console.Clear();
		Console.WriteLine($@"
          ██╗   ██╗ ██████╗ ██╗   ██╗    ██╗      ██████╗ ███████╗███████╗
          ╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║     ██╔═══██╗██╔════╝██╔════╝
           ╚████╔╝ ██║   ██║██║   ██║    ██║     ██║   ██║███████╗█████╗  
            ╚██╔╝  ██║   ██║██║   ██║    ██║     ██║   ██║╚════██║██╔══╝  
             ██║   ╚██████╔╝╚██████╔╝    ███████╗╚██████╔╝███████║███████╗
             ╚═╝    ╚═════╝  ╚═════╝     ╚══════╝ ╚═════╝ ╚══════╝╚══════╝
╔════════════════════════════════════════════════════════════════════════════════╗
║ Dear former CEO,                                                               ║
║                                                                                ║
║ We regret to inform you that you are being removed from the position of CEO    ║
║ and fired from the company, effective immediately. The board of directors of   ║
║ Oligopoly Investments has decided to take this action because you have spent   ║
║ the budget allocated to you, and your investment turned out to be unprofitable ║
║ for the company. We appreciate your service and wish you all the best in your  ║
║ future endeavors.                                                              ║
║                                                                                ║
║ Sincerely,                                                                     ║
║ The board of directors of Oligopoly Investments                                ║
╚════════════════════════════════════════════════════════════════════════════════╝

Your Net Worth dropped below {LosingNetWorth}$
You have played {TurnCounter} turns
Better luck next time...
");
		Console.Write("Press any key (except ENTER) to continue...");
		ConsoleKey key = ConsoleKey.Enter;
		while (!CloseRequested && key is ConsoleKey.Enter)
		{
			Console.CursorVisible = false;
			key = Console.ReadKey(true).Key;
			CloseRequested = CloseRequested || key is ConsoleKey.Escape;
		}
	}

	private static void AboutInfoScreen()
	{
		Console.Clear();
		Console.WriteLine($@"
          ████████╗██╗  ██╗ █████╗ ███╗   ██╗██╗  ██╗███████╗██╗
          ╚══██╔══╝██║  ██║██╔══██╗████╗  ██║██║ ██╔╝██╔════╝██║
             ██║   ███████║███████║██╔██╗ ██║█████╔╝ ███████╗██║
             ██║   ██╔══██║██╔══██║██║╚██╗██║██╔═██╗ ╚════██║╚═╝
             ██║   ██║  ██║██║  ██║██║ ╚████║██║  ██╗███████║██╗
             ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝╚══════╝╚═╝
╔══════════════════════════════════════════════════════════════════════════════════╗
║No really, thank you for taking time to play this simple CLI game. It means a lot.║
║If you find any bug or have an idea how to improve the game, please let me know :D║
║                                                                                  ║
║This game was created by Semion Medvedev (Fuinny)                                 ║
╚══════════════════════════════════════════════════════════════════════════════════╝
");
		Console.WriteLine("Press any key to continue...");
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
	}

	private static StringBuilder RenderCompanyStocksTable()
	{
		// table column widths
		const int c0 = 30, c1 = 10, c2 = 19, c3 = 17;

		StringBuilder gameView = new();
		gameView.AppendLine($"╔═{new('═', c0)}═╦═{new('═', c1)}═╦═{new('═', c2)}═╦═{new('═', c3)}═╗");
		gameView.AppendLine($"║ {"Company",-c0} ║ {"Industry",c1} ║ {"Share Price",c2} ║ {"You Have",c3} ║");
		gameView.AppendLine($"╠═{new('═', c0)}═╬═{new('═', c1)}═╬═{new('═', c2)}═╬═{new('═', c3)}═╣");
		foreach (Company company in Companies)
		{
			gameView.AppendLine($"║ {company.Name,-c0} ║ {company.Industry,c1} ║ {company.SharePrice,c2:C} ║ {company.NumberOfShares,c3} ║");
		}
		gameView.AppendLine($"╚═{new('═', c0)}═╩═{new('═', c1)}═╩═{new('═', c2)}═╩═{new('═', c3)}═╝");
		gameView.AppendLine($"Your money: {Money:C}    Your Net Worth: {NetWorth:C}    Current Turn: {TurnCounter}");
		return gameView;
	}

	private static int HandleMenuWithOptions(string prompt, string[] options, string[] descriptions = null!)
	{
		int index = 0;
		ConsoleKey key = default;
		while (!CloseRequested && key is not ConsoleKey.Enter)
		{
			Console.Clear();
			Console.WriteLine(prompt);
			for (int i = 0; i < options.Length; i++)
			{
				string currentOption = options[i];
				if (i == index)
				{
					(Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
					Console.WriteLine($"[*] {currentOption}");
					Console.ResetColor();
				}
				else
				{
					Console.WriteLine($"[ ] {currentOption}");
				}
			}
			if (descriptions != null)
			{
				Console.WriteLine("\nDescription:");
				Console.WriteLine(descriptions[index]);
			}
			Console.CursorVisible = false;
			key = Console.ReadKey().Key;
			switch (key)
			{
				case ConsoleKey.UpArrow: index = index is 0 ? options.Length - 1 : index - 1; break;
				case ConsoleKey.DownArrow: index = index == options.Length - 1 ? 0 : index + 1; break;
				case ConsoleKey.Escape: CloseRequested = true; break;
			}
		}
		return index;
	}

	private static void CalculateNetWorth()
	{
		NetWorth = Money;
		foreach (Company company in Companies)
		{
			NetWorth += company.SharePrice * company.NumberOfShares;
		}
	}

	private static void UpdateSharePrices()
	{
		for (int i = 0; i < Companies.Count; i++)
		{
			Random random = new Random();
			int effect = random.Next(0, 2);

			switch (effect)
			{
				case 0:
					Companies[i].SharePrice += Companies[i].SharePrice * Random.Shared.Next(1, 4) / 100;
					break;
				case 1:
					Companies[i].SharePrice += Companies[i].SharePrice * Random.Shared.Next(-3, 0) / 100;
					break;
			}
		}
	}
}