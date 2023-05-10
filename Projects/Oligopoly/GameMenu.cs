﻿using System;
using System.Threading;

namespace Oligopoly;

public class GameMenu : Menu
{
	private int CurrentEvent;
	private double Money;

	/// <summary>
	/// Initializes a new instance of the GameMenu class with given parameters.
	/// </summary>
	/// <param name="prompt">The prompt to display above the menu.</param>
	/// <param name="options">The options to display in the menu.</param>
	/// <param name="outputDelay">The text output delay. Have to be a positive integer or zero.</param>
	/// <param name="currentEvent">The integer that represents current event.</param>
	public GameMenu(string prompt, string[] options, int outputDelay, int currentEvent, double money)
		: base(prompt, options, outputDelay)
	{
		CurrentEvent = currentEvent;
		Money = money;
	}

	/// <summary>
	/// Displays menu to the console and redraws it when user select other option.
	/// </summary>
	protected override void DisplayMenu()
	{
		// column widths
		const int c0 = 30;
		const int c1 = 8;
		const int c2 = 10;
		const int c3 = 19;
		const int c4 = 17;

		Console.WriteLine($"╔═{new('═', c0)}═╤═{new('═', c1)}═╤═{new('═', c2)}═╤═{new('═', c3)}═╤═{new('═', c4)}═╗");
		Console.WriteLine($"║ {"Company", -c0} │ {"Ticker", c1} │ {"Industry", c2} │ {"Share Price", c3} │ {"You Have", c4} ║");
		Console.WriteLine($"╟─{new('─', c0)}─┼─{new('─', c1)}─┼─{new('─', c2)}─┼─{new('─', c3)}─┼─{new('─', c4)}─╢");
		foreach (Company company in Program.Companies)
		{
			Console.WriteLine($"║ {company.Name, -c0} │ {company.Ticker, c1} │ {company.Industry, c2} │ {company.SharePrice, c3} │ {company.ShareAmount, c4} ║");
		}
		Console.WriteLine($"╚═{new('═', c0)}═╧═{new('═', c1)}═╧═{new('═', c2)}═╧═{new('═', c3)}═╧═{new('═', c4)}═╝");
		Console.WriteLine();

		Console.WriteLine($"{Program.Events[CurrentEvent].Title}");
		Console.WriteLine();
		Console.WriteLine($"{Program.Events[CurrentEvent].Content}");
		Console.WriteLine();

		Console.WriteLine($"You have: {Math.Round(Money, 2)}$");
		//Console.WriteLine();

		foreach (char symbol in Prompt)
		{
			Thread.Sleep(OutputDelay);
			Console.Write(symbol);
		}

		for (int i = 0; i < Options.Length; i++)
		{
			string currentOption = Options[i];
			string prefix;

			if (i == SelectedIndex)
			{
				prefix = "*";
				Console.ForegroundColor = ConsoleColor.Black;
				Console.BackgroundColor = ConsoleColor.White;
			}
			else
			{
				prefix = " ";
				Console.ForegroundColor = ConsoleColor.White;
				Console.BackgroundColor = ConsoleColor.Black;
			}

			Console.WriteLine($"[{prefix}] {currentOption}");
		}

		Console.ResetColor();
	}

	/// <summary>
	/// Runs the menu.
	/// </summary>
	/// <returns>An integer that represents the selected option.</returns>
	public override int RunMenu()
	{
		// Set output delay to 0.
		// This is necessary so that the menu does not draw with a delay when updating the console.
		OutputDelay = 0;

		ConsoleKey keyPressed = default;
		while (keyPressed is not ConsoleKey.Enter)
		{
			Console.Clear();
			DisplayMenu();
			keyPressed = Console.ReadKey().Key;
			if (keyPressed is ConsoleKey.UpArrow)
			{
				SelectedIndex = SelectedIndex is 0 ? Options.Length - 1 : SelectedIndex - 1;
			}
			else if (keyPressed is ConsoleKey.DownArrow)
			{
				SelectedIndex = SelectedIndex == Options.Length - 1 ? 0 : SelectedIndex + 1;
			}
		}

		return SelectedIndex;
	}

	/// <summary>
	/// Runs the amount menu.
	/// </summary>
	/// <returns>An integer, that represents the amount of shares.</returns>
	public int RunAmountMenu()
	{
		// Set output delay to 0.
		// This is necessary so that the menu does not draw with a delay when updating the console.
		OutputDelay = 0;

		ConsoleKey keyPressed = default;
		int amount = 0;
		while (keyPressed is not ConsoleKey.Enter || SelectedIndex is not 2)
		{
			Console.Clear();
			DisplayMenu();
			Console.WriteLine($"Current amount: {amount}");
			keyPressed = Console.ReadKey().Key;
			switch (keyPressed)
			{
				case ConsoleKey.UpArrow:
					SelectedIndex = SelectedIndex is 0 ? Options.Length - 1 : SelectedIndex - 1;
					break;
				case ConsoleKey.DownArrow:
					SelectedIndex = SelectedIndex == Options.Length - 1 ? 0 : SelectedIndex + 1;
					break;
				case ConsoleKey.Enter:
					if (SelectedIndex is 0)
					{
						amount++;
					}
					else if (SelectedIndex is 1 && amount > 0)
					{
						amount--;
					}
					break;
			}
		}

		return amount;
	}
}