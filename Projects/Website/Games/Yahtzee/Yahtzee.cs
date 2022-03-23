using System;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;

namespace Website.Games.Yahtzee;

public class Yahtzee
{
	public readonly BlazorConsole Console = new();

	public async Task Run()
	{
		const string welcome =
@"                                                 
  See the README for instructions if needed.     
  Press [enter] to begin...                      
                                                 
                                                 
                                                 ";

		const string rollDice =
@"                                                 
  Press [space] to roll the dice...              
                                                 
                                                 
                                                 
                                                 ";

		const string rollDiceSmall =
@"                                                 
  Press [space] to roll the dice...              
                                                 
                                                 
                                                 ";

		const string blank =
@"                                                 
                                                 
                                                 
                                                 
                                                 
                                                 ";

		const string chooseDice2 =
@"                                                 
  Choose dice to re-roll. You have 2 re-rolls    
  remaining. Use [left & right & up arrow keys]  
  to select dice, and [enter] to confirm.        ";

		const string chooseDice1 =
@"                                                 
  Choose dice to re-roll. You have 1 re-rolls    
  remaining. Use [left & right & up arrow keys]  
  to select dice, and [enter] to confirm.        ";

		const string selectScore =
@"                                                 
  Select the item on the score card to use these 
  dice rolls for. Use [up & down arrow keys] to  
  select, and [enter] to confirm.                
                                                 
                                                 ";

		const string selectScoreInvalid =
@"                                                 
  Invalid selection. Each score item may only be 
  used once. Press [enter] to continue...        
                                                 
                                                 
                                                 ";

		const string yahtzeeBonus =
@"                                                 
  You got a Yahtzee Bonus! It has                
  been marked on your scorecard.                 
  Press [enter] to continue...                   
                                                 
                                                 ";

		const string upperBonusSuccess =
@"                                                 
  You scored at least 63 in the upper section of 
  the score sheet. You get the Aces-Sices Bonus. 
  Press [enter] to continue...                   
                                                 
                                                 ";

const string upperBonusFail =
@"                                                 
  You did not score at least 63 in the upper     
  section of the score sheet. You do not get the 
  Aces-Sices Bonus. Press [enter] to continue... 
                                                 
                                                 ";

const string gameComplete =
@"                                                 
  Game complete. Your total score has been       
  calculated on your scorecard.                  
  Play again [enter] or quit [escape]?           
                                                 
                                                 ";

		const int minWidth = 50;
		const int minHeight = 32;

		Random random = new();
		int[] dice = { 1, 2, 3, 4, 5 };
		bool[] locked = new bool[dice.Length];
		int diceSelection = 0;
		int scoreSelection = 0;
		int?[] scores = new int?[16];
		bool escape = false;

		try
		{
			if (OperatingSystem.IsWindows())
			{
				try
				{
					int width = Console.WindowWidth;
					int height = Console.WindowHeight;
					if (width < minWidth || height < minHeight)
					{
						Console.SetWindowSize(minWidth, minHeight);
						Console.SetBufferSize(minWidth, minHeight);
					}
				}
				catch
				{
					// Is mayonaise and instrument?
				}
			}
			Restart:
			Array.Fill(scores, null);
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			await Console.Clear();
			await Render(false, false, false);
			await Console.Write(welcome);
			await PressToContinue(ConsoleKey.Enter);
			while (!escape && scores.Contains(null))
			{
				await PlayRound();
				if (escape) return;
				if (IsYahtzee() && scores[12] > 0)
				{
					scores[14] = scores[14] is null ? 100 : scores[14] + 100;
					await Render(false, false, false);
					await Console.Write(yahtzeeBonus);
					await PressToContinue(ConsoleKey.Enter);
					if (escape) return;
				}
				await ScoreSelection();
			}
			if (escape) return;
			await Render(false, false, false);
			await Console.Write(gameComplete);
			PlayAgainCheck:
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.Enter: goto Restart;
				case ConsoleKey.Escape: escape = true; break;
				default: goto PlayAgainCheck;
			}
		}
		finally
		{
			Console.ResetColor();
			await Console.Clear();
			await Console.Write("Yahtzee was closed.");
			await Console.Refresh();
		}

		async Task Render(bool selectingDice, bool selectingScore, bool successiveRoll)
		{
			Console.CursorVisible = false;
			await EnsureConsoleSize();
			if (escape) return;
			Console.CursorVisible = false;
			await Console.SetCursorPosition(0, 0);
			await Console.WriteLine();
			await Console.WriteLine("  Yahtzee");
			await Console.WriteLine($"  ╒════════ Score Sheet ════════╕");
			await Console.WriteLine($"  ├─────── Upper Section ───────┤");
			await RenderSelectableScoreSheetLine(selectingScore, 00, "Aces...............");
			await RenderSelectableScoreSheetLine(selectingScore, 01, "Twos...............");
			await RenderSelectableScoreSheetLine(selectingScore, 02, "Threes.............");
			await RenderSelectableScoreSheetLine(selectingScore, 03, "Fours..............");
			await RenderSelectableScoreSheetLine(selectingScore, 04, "Fives..............");
			await RenderSelectableScoreSheetLine(selectingScore, 05, "Sixes..............");
			await Console.WriteLine($"  │   Aces-Sixes Bonus...[{RenderScore(scores[06])}] │");
			await Console.WriteLine($"  ├─────── Lower Section ───────┤");
			await RenderSelectableScoreSheetLine(selectingScore, 07, "3 of a Kind........");
			await RenderSelectableScoreSheetLine(selectingScore, 08, "4 of a Kind........");
			await RenderSelectableScoreSheetLine(selectingScore, 09, "Full House.........");
			await RenderSelectableScoreSheetLine(selectingScore, 10, "Small Straight.....");
			await RenderSelectableScoreSheetLine(selectingScore, 11, "Large Straight.....");
			await RenderSelectableScoreSheetLine(selectingScore, 12, "Yahtzee............");
			await RenderSelectableScoreSheetLine(selectingScore, 13, "Chance.............");
			await Console.WriteLine($"  │   Yahtzee Bonus......[{RenderScore(scores[14])}] │");
			await Console.WriteLine($"  │   Total..............[{RenderScore(scores[15])}] │");
			await Console.WriteLine($"  └─────────────────────────────┘");
			await Console.WriteLine("        ╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗");
			await Console.Write("  Dice: ");
			for (int i = 0; i < dice.Length; i++)
			{
				await Console.Write($"║ {dice[i].ToString(CultureInfo.InvariantCulture)} ║");
			}
			await Console.WriteLine();
			await Console.WriteLine("        ╚═══╝╚═══╝╚═══╝╚═══╝╚═══╝");
			if (selectingDice)
			{
				await Console.Write("        ");
				for (int i = 0; i < dice.Length; i++)
				{
					await Console.Write(!locked[i] ? @"^roll" : "     ");
				}
				await Console.WriteLine();
				if (!successiveRoll)
				{
					await Console.Write("        ");
					Console.ForegroundColor = ConsoleColor.Yellow;
					for (int i = 0; i < dice.Length; i++)
					{
						await Console.Write(i == diceSelection ? @"^^^^^" : "     ");
					}
					Console.ForegroundColor = ConsoleColor.White;
				}
			}
		}

		async Task RenderSelectableScoreSheetLine(bool selectingScore, int index, string line)
		{
			await Console.Write($"  │ ");
			if (selectingScore && scoreSelection == index)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				await Console.Write('>');
			}
			else
			{
				await Console.Write(" ");
			}
			await Console.Write($" {line}[{RenderScore(scores[index])}]");
			Console.ForegroundColor = ConsoleColor.White;
			await Console.WriteLine($" │");
		}

		string RenderScore(int? score) =>
			score switch
			{
				null   => "    ",
				< 10   => "   " + score.Value.ToString(CultureInfo.InvariantCulture),
				< 100  => "  "  + score.Value.ToString(CultureInfo.InvariantCulture),
				< 1000 => " "   + score.Value.ToString(CultureInfo.InvariantCulture),
				_      =>         score.Value.ToString(CultureInfo.InvariantCulture),
			};

		async Task RollDice(bool selectingDice)
		{
			TimeSpan rollTime = TimeSpan.FromSeconds(1.5);
			DateTime start = DateTime.Now;
			while (DateTime.Now - start < rollTime)
			{
				while (await Console.KeyAvailable())
				{
					if ((await Console.ReadKey(true)).Key is ConsoleKey.Escape)
					{
						escape = true;
						return;
					}
				}
				for (int i = 0; i < dice.Length; i++)
				{
					if (!locked[i])
					{
						dice[i] = random.Next(1, 7);
					}
				}
				await Render(selectingDice, false, false);
				await Console.Write(blank);
				await Console.Refresh();
			}
		}

		async Task PlayRound()
		{
			Array.Fill(locked, false);
			await Render(false, false, false);
			await Console.Write(rollDice);
			await PressToContinue(ConsoleKey.Spacebar);
			if (escape) return;
			await RollDice(false);
			if (escape) return;
			Array.Fill(locked, true);
			await DiceSelection(chooseDice2);
			if (!locked.Contains(false) || escape) return;
			await Render(true, false, true);
			await Console.Write(rollDiceSmall);
			await PressToContinue(ConsoleKey.Spacebar);
			if (escape) return;
			await RollDice(false);
			if (escape) return;
			Array.Fill(locked, true);
			await DiceSelection(chooseDice1);
			if (!locked.Contains(false) || escape) return;
			await Render(true, false, true);
			await Console.Write(rollDiceSmall);
			await PressToContinue(ConsoleKey.Spacebar);
			if (escape) return;
			await RollDice(false);
			if (escape) return;
		}

		async Task DiceSelection(string message)
		{
			diceSelection = 0;
		GetInput:
			await Render(true, false, false);
			await Console.Write(message);
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.LeftArrow: diceSelection = Math.Max(diceSelection - 1, 0); goto GetInput;
				case ConsoleKey.RightArrow: diceSelection = Math.Min(diceSelection + 1, dice.Length - 1); goto GetInput;
				case ConsoleKey.UpArrow: locked[diceSelection] = !locked[diceSelection]; goto GetInput;
				case ConsoleKey.Enter: break;
				case ConsoleKey.Escape: escape = true; return;
				default: goto GetInput;
			}
		}

		async Task ScoreSelection()
		{
			scoreSelection = 0;
		GetInput:
			await Render(false, true, false);
			await Console.Write(selectScore);
			switch ((await Console.ReadKey(true)).Key)
			{
				case ConsoleKey.UpArrow:
					scoreSelection = Math.Max(scoreSelection - 1, 0);
					if (scoreSelection is 06) scoreSelection -= 1;
					goto GetInput;
				case ConsoleKey.DownArrow:
					scoreSelection = Math.Min(scoreSelection + 1, 13);
					if (scoreSelection is 06) scoreSelection += 1;
					goto GetInput;
				case ConsoleKey.Enter:
					if (scores[scoreSelection] is null)
					{
						scores[scoreSelection] = scoreSelection switch
						{
							00 => dice.Count(v => v is 1),
							01 => dice.Count(v => v is 2) * 2,
							02 => dice.Count(v => v is 3) * 3,
							03 => dice.Count(v => v is 4) * 4,
							04 => dice.Count(v => v is 5) * 5,
							05 => dice.Count(v => v is 6) * 6,
							07 => Is3OfAKind() ? dice.Sum() : 0,
							08 => Is4OfAKind() ? dice.Sum() : 0,
							09 => IsFullHouse() ? 25 : 0,
							10 => IsSmallStraight() ? 30 : 0,
							11 => IsLargeStraight() ? 40 : 0,
							12 => IsYahtzee() ? 50 : 0,
							13 => dice.Sum(),
							_  => throw new Exception("invalid score selection"),
						};
						if (scores[06] is null && !scores[..06].Contains(null))
						{
							scores[06] = scores[..06].Sum() >= 63 ? 35 : 0;
							await Render(false, false, false);
							await Console.Write(scores[06] is 0 ? upperBonusFail : upperBonusSuccess);
							await PressToContinue(ConsoleKey.Enter);
							if (escape) return;
						}
						if (scores[14] is null && !scores[..14].Contains(null))
						{
							scores[14] = 0;
						}
						if (!scores[..15].Contains(null))
						{
							scores[15] = scores.Sum();
						}
						break;
					}
					else
					{
						await Render(false, true, false);
						await Console.Write(selectScoreInvalid);
						await PressToContinue(ConsoleKey.Enter);
						if (escape) return;
						goto GetInput;
					}
				case ConsoleKey.Escape:
					escape = true;
					return;
				default:
					goto GetInput;
			}
		}

		async Task PressToContinue(ConsoleKey key)
		{
		GetInput:
			ConsoleKey input = (await Console.ReadKey(true)).Key;
			if (input is ConsoleKey.Escape)
			{
				escape = true;
				return;
			}
			if (input != key)
			{
				goto GetInput;
			}
		}

		bool IsFullHouse()
		{
			int[] values = new int[6];
			for (int i = 0; i < dice.Length; i++)
			{
				values[dice[i] - 1]++;
			}
			return values.Contains(2) && values.Contains(3);
		}

		bool Is3OfAKind() => GetMaxMatches() >= 3;

		bool Is4OfAKind() => GetMaxMatches() >= 4;

		bool IsYahtzee() => GetMaxMatches() is 5;

		int GetMaxMatches()
		{
			int[] values = new int[6];
			for (int i = 0; i < dice.Length; i++)
			{
				values[dice[i] - 1]++;
			}
			return values.Max();
		}

		bool IsSmallStraight() => GetMaxDiceInARow() >= 4;

		bool IsLargeStraight() => GetMaxDiceInARow() >= 5;

		int GetMaxDiceInARow()
		{
			int[] values = new int[6];
			for (int i = 0; i < dice.Length; i++)
			{
				values[dice[i] - 1]++;
			}
			int maxInARow = 0;
			int inARow = 0;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i] > 0)
				{
					inARow++;
				}
				else
				{
					maxInARow = Math.Max(maxInARow, inARow);
					inARow = 0;
				}
			}
			return Math.Max(maxInARow, inARow);
		}

		async Task EnsureConsoleSize()
		{
			int width = Console.WindowWidth;
			int height = Console.WindowHeight;
			while (!escape && (width < minWidth || height < minHeight))
			{
				await Console.Clear();
				await Console.WriteLine("Increase console size and press [enter]...");
				bool enter = false;
				while (!escape && !enter)
				{
					switch ((await Console.ReadKey(true)).Key)
					{
						case ConsoleKey.Enter:
							enter = true;
							break;
						case ConsoleKey.Escape:
							escape = true;
							break;
					}
				}
				width = Console.WindowWidth;
				height = Console.WindowHeight;
				await Console.Clear();
			}
		}
	}
}
