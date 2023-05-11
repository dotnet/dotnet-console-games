using System;

namespace Oligopoly;

public class Menu
{
	protected int SelectedIndex;
	protected string Prompt;
	protected string[] Options;

	/// <summary>
	/// Initializes a new instance of the Menu class with given prompt, options and output delay.
	/// </summary>
	/// <param name="prompt">The prompt to display above the menu.</param>
	/// <param name="options">The options to display in the menu.</param>
	/// <param name="outputDelay">The text output delay. Have to be a positive integer or zero!</param>
	public Menu(string prompt, string[] options)
	{
		Prompt = prompt;
		Options = options;
		SelectedIndex = 0;
	}

	/// <summary>
	/// Displays menu to the console and redraws it when user select other option.
	/// </summary>
	protected virtual void DisplayMenu()
	{
		Console.WriteLine(Prompt);
		for (int i = 0; i < Options.Length; i++)
		{
			string currentOption = Options[i];
			if (i == SelectedIndex)
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
	}

	/// <summary>
	/// Runs the menu.
	/// </summary>
	/// <returns>An integer that represents the selected option.</returns>
	public virtual int RunMenu()
	{
		ConsoleKey keyPressed = default;
		while (!Program.CloseRequested && keyPressed is not ConsoleKey.Enter)
		{
			Console.Clear();
			DisplayMenu();
			Console.CursorVisible = false;
			keyPressed = Console.ReadKey().Key;
			switch (keyPressed)
			{
				case ConsoleKey.UpArrow:
					SelectedIndex = SelectedIndex is 0 ? Options.Length - 1 : SelectedIndex - 1;
					break;
				case ConsoleKey.DownArrow:
					SelectedIndex = SelectedIndex == Options.Length - 1 ? 0 : SelectedIndex + 1;
					break;
				case ConsoleKey.Escape:
					Program.CloseRequested = true;
					break;
			}
		}
		return SelectedIndex;
	}
}