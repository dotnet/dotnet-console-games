using System;
using System.Threading;

namespace Oligopoly;

public class Menu
{
	protected int SelectedIndex;
	protected int OutputDelay;
	protected string Prompt;
	protected string[] Options;

	/// <summary>
	/// Initializes a new instance of the Menu class with given prompt, options and output delay.
	/// </summary>
	/// <param name="prompt">The prompt to display above the menu.</param>
	/// <param name="options">The options to display in the menu.</param>
	/// <param name="outputDelay">The text output delay. Have to be a positive integer or zero!</param>
	public Menu(string prompt, string[] options, int outputDelay)
	{
		Prompt = prompt;
		Options = options;
		OutputDelay = outputDelay;
		SelectedIndex = 0;
	}

	/// <summary>
	/// Displays menu to the console and redraws it when user select other option.
	/// </summary>
	protected virtual void DisplayMenu()
	{
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
	public virtual int RunMenu()
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
}