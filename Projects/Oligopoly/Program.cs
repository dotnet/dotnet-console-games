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
	private static int TurnCounter { get; set; } = 1;
	private static decimal Money { get; set; }
	private const decimal LosingNetWorth = 2000.00m;
	private const decimal WinningNetWorth = 50000.00m;

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

	private static void InitializeGame()
	{
		Money = 10000.00m;
		LoadEmbeddedResources();
	}

	private static void GameLoop()
	{
		while (!CloseRequested)
		{
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

			switch (CalculateNetWorth())
			{
				case >= WinningNetWorth: PlayerWinsScreen(); return;
				case <= LosingNetWorth: PlayerLosesScreen(); return;
			}

			TurnCounter++;
		}
	}

	private static void EventScreen()
	{
		StringBuilder prompt = RenderCompanyStocksTable();
		prompt.AppendLine();

		if (TurnCounter % 50 == 0)
		{
			CurrentEvent = GlobalEvents[Random.Shared.Next(0, GlobalEvents.Count)];

			foreach (Company currentCompany in Companies)
			{
				currentCompany.SharePrice += currentCompany.SharePrice * CurrentEvent.Effect / 100;
			}

			prompt.AppendLine($"{CurrentEvent.Title.ToUpper()}");
		}
		else
		{
			CurrentEvent = Events[Random.Shared.Next(0, Events.Count)];

			foreach (Company currentCompany in Companies)
			{
				if (currentCompany.Name == CurrentEvent.CompanyName)
				{
					currentCompany.SharePrice += currentCompany.SharePrice * CurrentEvent.Effect / 100;
				}
			}

			prompt.AppendLine($"{CurrentEvent.Title}");
		}

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
		Console.WriteLine();
		Console.WriteLine("    ╔════════════════════════════════════════════════════════════════════════════════╗");
		Console.WriteLine("    ║ Dear CEO,                                                                      ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ Welcome to Oligopoly!                                                          ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ On behalf of the board of directors of Oligopoly Investments, we would like to ║");
		Console.WriteLine("    ║ congratulate you on becoming our new CEO. We are confident that you will lead  ║");
		Console.WriteLine("    ║ our company to new heights of success and innovation. As CEO, you now have     ║");
		Console.WriteLine("    ║ access to our exclusive internal software called Oligopoly, where you can      ║");
		Console.WriteLine("    ║ track the latest news from leading companies and buy and sell their shares.    ║");
		Console.WriteLine("    ║ This software will give you an edge over the competition and help you make     ║");
		Console.WriteLine("    ║ important decisions for our company. To access the program, simply click the   ║");
		Console.WriteLine("    ║ button at the bottom of this email. We look forward to working with you and    ║");
		Console.WriteLine("    ║ supporting you in your new role.                                               ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ Sincerely,                                                                     ║");
		Console.WriteLine("    ║ The board of directors of Oligopoly Investments                                ║");
		Console.WriteLine("    ╚════════════════════════════════════════════════════════════════════════════════╝");
		Console.WriteLine();
		Console.Write("Press any key to continue...");
		Console.CursorVisible = false;
		CloseRequested = CloseRequested || Console.ReadKey(true).Key is ConsoleKey.Escape;
		InitializeGame();
		GameLoop();
	}

	private static void PlayerWinsScreen()
	{
		Console.Clear();
		Console.WriteLine();
		Console.WriteLine("    ╔════════════════════════════════════════════════════════════════════════════════╗");
		Console.WriteLine("    ║ Dear CEO,                                                                      ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ On behalf of the board of directors of Oligopoly Investments, we would like to ║");
		Console.WriteLine("    ║ express our gratitude and understanding for your decision to leave your post.  ║");
		Console.WriteLine("    ║ You have been a remarkable leader and a visionary strategist, who played the   ║");
		Console.WriteLine("    ║ stock market skillfully and increased our budget by five times. We are proud   ║");
		Console.WriteLine("    ║ of your achievements and we wish you all the best in your future endeavors. As ║");
		Console.WriteLine("    ║ a token of our appreciation, we are pleased to inform you that the company     ║");
		Console.WriteLine("    ║ will pay you a bonus of $1 million. You deserve this reward for your hard work ║");
		Console.WriteLine("    ║ and dedication. We hope you will enjoy it and remember us fondly. Thank you    ║");
		Console.WriteLine("    ║ for your service and your contribution to Oligopoly Investments.               ║");
		Console.WriteLine("    ║ You will be missed.                                                            ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ Sincerely,                                                                     ║");
		Console.WriteLine("    ║ The board of directors of Oligopoly Investments                                ║");
		Console.WriteLine("    ╚════════════════════════════════════════════════════════════════════════════════╝");
		Console.WriteLine();
		Console.WriteLine($"Your net worth is over {WinningNetWorth:C}.");
		Console.WriteLine();
		Console.WriteLine("You win! Congratulations!");
		Console.WriteLine();
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
		Console.WriteLine();
		Console.WriteLine("    ╔════════════════════════════════════════════════════════════════════════════════╗");
		Console.WriteLine("    ║ Dear former CEO,                                                               ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ We regret to inform you that you are being removed from the position of CEO    ║");
		Console.WriteLine("    ║ and fired from the company, effective immediately. The board of directors of   ║");
		Console.WriteLine("    ║ Oligopoly Investments has decided to take this action because you have spent   ║");
		Console.WriteLine("    ║ the budget allocated to you, and your investment turned out to be unprofitable ║");
		Console.WriteLine("    ║ for the company. We appreciate your service and wish you all the best in your  ║");
		Console.WriteLine("    ║ future endeavors.                                                              ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ Sincerely,                                                                     ║");
		Console.WriteLine("    ║ The board of directors of Oligopoly Investments                                ║");
		Console.WriteLine("    ╚════════════════════════════════════════════════════════════════════════════════╝");
		Console.WriteLine();
		Console.WriteLine($"Your net worth dropped below {LosingNetWorth:C}.");
		Console.WriteLine();
		Console.WriteLine("You Lose! Better luck next time...");
		Console.WriteLine();
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
		Console.WriteLine();
		Console.WriteLine("    ╔════════════════════════════════════════════════════════════════════════════════╗");
		Console.WriteLine("    ║ THANKS!                                                                        ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ No really, thank you for taking time to play this simple console game.         ║");
		Console.WriteLine("    ║ It means a lot :D                                                              ║");
		Console.WriteLine("    ║                                                                                ║");
		Console.WriteLine("    ║ This game was created by Semion Medvedev (Fuinny)                              ║");
		Console.WriteLine("    ╚════════════════════════════════════════════════════════════════════════════════╝");
		Console.WriteLine();
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
		gameView.AppendLine($"Your money: {Money:C}    Your Net Worth: {CalculateNetWorth():C}    Current Turn: {TurnCounter}");
		return gameView;
	}

	private static int HandleMenuWithOptions(string prompt, string[] options)
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

	private static decimal CalculateNetWorth()
	{
		decimal netWorth = Money;
		foreach (Company company in Companies)
		{
			netWorth += company.SharePrice * company.NumberOfShares;
		}
		return netWorth;
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