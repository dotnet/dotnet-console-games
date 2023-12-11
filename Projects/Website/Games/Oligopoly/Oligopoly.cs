﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Website.Games.Oligopoly;

public class Oligopoly
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		bool CloseRequested = false;
		List<Company> Companies = null!;
		List<Event> Events = null!;
		Event CurrentEvent = null!;
		decimal Money = 10000.00m;
		const decimal LosingNetWorth = 2000.00m;
		const decimal WinningNetWorth = 50000.00m;

		try
		{
			await MainMenuScreen();
		}
		finally
		{
			Console.ResetColor();
			Console.CursorVisible = true;
		}

		void LoadEmbeddedResources()
		{
			Companies = JsonSerializer.Deserialize<List<Company>>(Resources.Company_json!)!;
			Events = JsonSerializer.Deserialize<List<Event>>(Resources.Event_json!)!;
		}

		async Task MainMenuScreen()
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
				int selectedIndex = await HandleMenuWithOptions(prompt.ToString(),
					[
					"Play",
					"About",
					"Exit",
					]);
				switch (selectedIndex)
				{
					case 0: await IntroductionScreen(); break;
					case 1: await AboutInfoScreen(); break;
					case 2: CloseRequested = true; break;
				}
			}
			await Console.Clear();
			await Console.WriteLine("Oligopoly was closed. Press any key to continue...");
			Console.CursorVisible = false;
			await Console.ReadKey(true);
		}

		async Task InitializeGame()
		{
			await Console.Clear();
			await Console.Write("loading...");
			await Console.Refresh();

			Money = 10000.00m;
			LoadEmbeddedResources();
		}

		async Task GameLoop()
		{
			while (!CloseRequested)
			{
				int selectedOption = -1;
				while (!CloseRequested && selectedOption is not 0)
				{
					StringBuilder prompt = RenderCompanyStocksTable();
					prompt.AppendLine();
					prompt.Append("Use up and down arrow keys and enter to select an option:");
					selectedOption = await HandleMenuWithOptions(prompt.ToString(),
						[
						"Wait For Market Change",
						"Buy",
						"Sell",
						"Information About Companies",
						]);
					switch (selectedOption)
					{
						case 1: await BuyOrSellStockScreen(true); break;
						case 2: await BuyOrSellStockScreen(false); break;
						case 3: await CompanyDetailsScreen(); break;
					}
				}

				await EventScreen();

				switch (CalculateNetWorth())
				{
					case >= WinningNetWorth: await PlayerWinsScreen(); return;
					case <= LosingNetWorth: await PlayerLosesScreen(); return;
				}
			}
		}

		async Task EventScreen()
		{
			CurrentEvent = Events[Random.Shared.Next(0, Events.Count)];

			foreach (Company currentCompany in Companies)
			{
				if (currentCompany.Name == CurrentEvent.CompanyName)
				{
					currentCompany.SharePrice += currentCompany.SharePrice * CurrentEvent.Effect / 100;
				}
			}

			StringBuilder prompt = RenderCompanyStocksTable();
			prompt.AppendLine();
			prompt.AppendLine($"{CurrentEvent.Title}");
			prompt.AppendLine();
			prompt.AppendLine($"{CurrentEvent.Description}");
			prompt.AppendLine();
			prompt.Append("Press any key to continue...");
			await Console.Clear();
			await Console.Write(prompt);
			Console.CursorVisible = false;
			CloseRequested = CloseRequested || (await Console.ReadKey(true)).Key is ConsoleKey.Escape;
		}

		async Task BuyOrSellStockScreen(bool isBuying)
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

				await Console.Clear();
				await Console.WriteLine(RenderCompanyStocksTable());
				await Console.WriteLine();
				await Console.WriteLine($"Use the arrow keys and enter to confirm how many shares to {(isBuying ? "buy" : "sell")}:");
				for (int i = 0; i < Companies.Count; i++)
				{
					if (i == index)
					{
						(Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
						await Console.WriteLine($" < {numberOfShares[i]}{(!isBuying ? $"/{Companies[index].NumberOfShares}" : "")} > {Companies[i].Name}");
						Console.ResetColor();
					}
					else
					{
						await Console.WriteLine($"   {numberOfShares[i]}{(!isBuying ? $"/{Companies[index].NumberOfShares}" : "")}   {Companies[i].Name}");
					}
				}
				await Console.WriteLine();
				await Console.WriteLine($"{(isBuying ? "Cost" : "Payout")}: {cost:C}");

				key = (await Console.ReadKey(true)).Key;
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

			await Console.Clear();
			await Console.WriteLine(RenderCompanyStocksTable());
			await Console.WriteLine($"You shares have been updated.");
			await Console.WriteLine();
			await Console.Write("Press any key to continue...");
			Console.CursorVisible = false;
			CloseRequested = CloseRequested || (await Console.ReadKey(true)).Key is ConsoleKey.Escape;
		}

		async Task CompanyDetailsScreen()
		{
			await Console.Clear();
			foreach (Company company in Companies)
			{
				await Console.WriteLine($"{company.Name} - {company.Description}");
				await Console.WriteLine();
			}
			await Console.Write("Press any key to exit the menu...");
			Console.CursorVisible = false;
			CloseRequested = CloseRequested || (await Console.ReadKey(true)).Key is ConsoleKey.Escape;
		}

		async Task IntroductionScreen()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine("    ┌────────────────────────────────────────────────────────────────────────────────┐");
			await Console.WriteLine("    │ Dear CEO,                                                                      │");
			await Console.WriteLine("    │                                                                                │");
			await Console.WriteLine("    │ Welcome to Oligopoly!                                                          │");
			await Console.WriteLine("    │                                                                                │");
			await Console.WriteLine("    │ On behalf of the board of directors of Oligopoly Investments, we would like to │");
			await Console.WriteLine("    │ congratulate you on becoming our new CEO. We are confident that you will lead  │");
			await Console.WriteLine("    │ our company to new heights of success and innovation. As CEO, you now have     │");
			await Console.WriteLine("    │ access to our exclusive internal software called Oligopoly, where you can      |");
			await Console.WriteLine("    │ track the latest news from leading companies and buy and sell their shares.    │");
			await Console.WriteLine("    │ This software will give you an edge over the competition and help you make     │");
			await Console.WriteLine("    │ important decisions for our company. To access the program, simply click the   │");
			await Console.WriteLine("    │ button at the bottom of this email. We look forward to working with you and    │");
			await Console.WriteLine("    │ supporting you in your new role.                                               │");
			await Console.WriteLine("    │                                                                                │");
			await Console.WriteLine("    │ Sincerely,                                                                     │");
			await Console.WriteLine("    │ The board of directors of Oligopoly Investments                                │");
			await Console.WriteLine("    └────────────────────────────────────────────────────────────────────────────────┘");
			await Console.WriteLine();
			await Console.Write("Press any key to continue...");
			Console.CursorVisible = false;
			CloseRequested = CloseRequested || (await Console.ReadKey(true)).Key is ConsoleKey.Escape;
			await InitializeGame();
			await GameLoop();
		}

		async Task PlayerWinsScreen()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine("    ┌────────────────────────────────────────────────────────────────────────────────┐");
			await Console.WriteLine("    │ Dear CEO,                                                                      │");
			await Console.WriteLine("    │                                                                                │");
			await Console.WriteLine("    │ On behalf of the board of directors of Oligopoly Investments, we would like to │");
			await Console.WriteLine("    │ express our gratitude and understanding for your decision to leave your post.  │");
			await Console.WriteLine("    │ You have been a remarkable leader and a visionary strategist, who played the   │");
			await Console.WriteLine("    │ stock market skillfully and increased our budget by five times. We are proud   │");
			await Console.WriteLine("    │ of your achievements and we wish you all the best in your future endeavors. As │");
			await Console.WriteLine("    │ a token of our appreciation, we are pleased to inform you that the company     │");
			await Console.WriteLine("    │ will pay you a bonus of $1 million. You deserve this reward for your hard work │");
			await Console.WriteLine("    │ and dedication. We hope you will enjoy it and remember us fondly. Thank you    │");
			await Console.WriteLine("    │ for your service and your contribution to Oligopoly Investments.               │");
			await Console.WriteLine("    │ You will be missed.                                                            │");
			await Console.WriteLine("    │                                                                                │");
			await Console.WriteLine("    │ Sincerely,                                                                     │");
			await Console.WriteLine("    │ The board of directors of Oligopoly Investments                                │");
			await Console.WriteLine("    └────────────────────────────────────────────────────────────────────────────────┘");
			await Console.WriteLine();
			await Console.WriteLine($"Your net worth is over {WinningNetWorth:C}.");
			await Console.WriteLine();
			await Console.WriteLine("You win! Congratulations!");
			await Console.WriteLine();
			await Console.Write("Press any key (except ENTER) to continue...");
			ConsoleKey key = ConsoleKey.Enter;
			while (!CloseRequested && key is ConsoleKey.Enter)
			{
				Console.CursorVisible = false;
				key = (await Console.ReadKey(true)).Key;
				CloseRequested = CloseRequested || key is ConsoleKey.Escape;
			}
		}

		async Task PlayerLosesScreen()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine("    ┌────────────────────────────────────────────────────────────────────────────────┐");
			await Console.WriteLine("    │ Dear former CEO,                                                               │");
			await Console.WriteLine("    │                                                                                │");
			await Console.WriteLine("    │ We regret to inform you that you are being removed from the position of CEO    │");
			await Console.WriteLine("    │ and fired from the company, effective immediately. The board of directors of   │");
			await Console.WriteLine("    │ Oligopoly Investments has decided to take this action because you have spent   │");
			await Console.WriteLine("    │ the budget allocated to you, and your investment turned out to be unprofitable │");
			await Console.WriteLine("    │ for the company. We appreciate your service and wish you all the best in your  │");
			await Console.WriteLine("    │ future endeavors.                                                              │");
			await Console.WriteLine("    │                                                                                │");
			await Console.WriteLine("    │ Sincerely,                                                                     │");
			await Console.WriteLine("    │ The board of directors of Oligopoly Investments                                │");
			await Console.WriteLine("    └────────────────────────────────────────────────────────────────────────────────┘");
			await Console.WriteLine();
			await Console.WriteLine($"Your net worth dropped below {LosingNetWorth:C}.");
			await Console.WriteLine();
			await Console.WriteLine("You Lose! Better luck next time...");
			await Console.WriteLine();
			await Console.Write("Press any key (except ENTER) to continue...");
			ConsoleKey key = ConsoleKey.Enter;
			while (!CloseRequested && key is ConsoleKey.Enter)
			{
				Console.CursorVisible = false;
				key = (await Console.ReadKey(true)).Key;
				CloseRequested = CloseRequested || key is ConsoleKey.Escape;
			}
		}

		async Task AboutInfoScreen()
		{
			await Console.Clear();
			await Console.WriteLine();
			await Console.WriteLine("    |");
			await Console.WriteLine("    | THANKS!");
			await Console.WriteLine("    |");
			await Console.WriteLine("    | No really, thank you for taking time to play this simple console game. It means a lot.");
			await Console.WriteLine("    |");
			await Console.WriteLine("    | This game was created by Semion Medvedev (Fuinny)");
			await Console.WriteLine("    |");
			await Console.WriteLine();
			await Console.WriteLine("Press any key to continue...");
			Console.CursorVisible = false;
			CloseRequested = CloseRequested || (await Console.ReadKey(true)).Key is ConsoleKey.Escape;
		}

		StringBuilder RenderCompanyStocksTable()
		{
			// table column widths
			const int c0 = 30, c1 = 10, c2 = 19, c3 = 17;

			StringBuilder gameView = new();
			gameView.AppendLine($"╔═{new('═', c0)}═╤═{new('═', c1)}═╤═{new('═', c2)}═╤═{new('═', c3)}═╗");
			gameView.AppendLine($"║ {"Company",-c0} │ {"Industry",c1} │ {"Share Price",c2} │ {"You Have",c3} ║");
			gameView.AppendLine($"╟─{new('─', c0)}─┼─{new('─', c1)}─┼─{new('─', c2)}─┼─{new('─', c3)}─╢");
			foreach (Company company in Companies)
			{
				gameView.AppendLine($"║ {company.Name,-c0} │ {company.Industry,c1} │ {company.SharePrice,c2:C} │ {company.NumberOfShares,c3} ║");
			}
			gameView.AppendLine($"╚═{new('═', c0)}═╧═{new('═', c1)}═╧═{new('═', c2)}═╧═{new('═', c3)}═╝");
			gameView.AppendLine($"Your money: {Money:C}    Your Net Worth: {CalculateNetWorth():C}");
			return gameView;
		}

		async Task<int> HandleMenuWithOptions(string prompt, string[] options)
		{
			int index = 0;
			ConsoleKey key = default;
			while (!CloseRequested && key is not ConsoleKey.Enter)
			{
				await Console.Clear();
				await Console.WriteLine(prompt);
				for (int i = 0; i < options.Length; i++)
				{
					string currentOption = options[i];
					if (i == index)
					{
						(Console.BackgroundColor, Console.ForegroundColor) = (Console.ForegroundColor, Console.BackgroundColor);
						await Console.WriteLine($"[*] {currentOption}");
						Console.ResetColor();
					}
					else
					{
						await Console.WriteLine($"[ ] {currentOption}");
					}
				}
				Console.CursorVisible = false;
				key = (await Console.ReadKey(true)).Key;
				switch (key)
				{
					case ConsoleKey.UpArrow: index = index is 0 ? options.Length - 1 : index - 1; break;
					case ConsoleKey.DownArrow: index = index == options.Length - 1 ? 0 : index + 1; break;
					case ConsoleKey.Escape: CloseRequested = true; break;
				}
			}
			return index;
		}

		decimal CalculateNetWorth()
		{
			decimal netWorth = Money;
			foreach (Company company in Companies)
			{
				netWorth += company.SharePrice * company.NumberOfShares;
			}
			return netWorth;
		}
	}
}
