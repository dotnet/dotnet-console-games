using System;
using System.Globalization;
using System.Linq;

Exception? exception = null;

const string welcome = """
	                                                 
	  See the README for instructions if needed.     
	  Press [enter] to begin...                      
	                                                 
	                                                 
	                                                 
	""";

const string rollDice = """
	                                                 
	  Press [space] to roll the dice...              
	                                                 
	                                                 
	                                                 
	                                                 
	""";

const string rollDiceSmall = """
	                                                 
	  Press [space] to roll the dice...              
	                                                 
	                                                 
	                                                 
	""";

const string blank = """
	                                                 
	                                                 
	                                                 
	                                                 
	                                                 
	                                                 
	""";

const string chooseDice2 = """
	                                                 
	  Choose dice to re-roll. You have 2 re-rolls    
	  remaining. Use [left & right & up arrow keys]  
	  to select dice, and [enter] to confirm.        
	""";

const string chooseDice1 = """
	                                                 
	  Choose dice to re-roll. You have 1 re-rolls    
	  remaining. Use [left & right & up arrow keys]  
	  to select dice, and [enter] to confirm.        
	""";

const string selectScore = """
	                                                 
	  Select the item on the score card to use these 
	  dice rolls for. Use [up & down arrow keys] to  
	  select, and [enter] to confirm.                
	                                                 
	                                                 
	""";

const string selectScoreInvalid = """
	                                                 
	  Invalid selection. Each score item may only be 
	  used once. Press [enter] to continue...        
	                                                 
	                                                 
	                                                 
	""";

const string yahtzeeBonus = """
	                                                 
	  You got a Yahtzee Bonus! It has                
	  been marked on your scorecard.                 
	  Press [enter] to continue...                   
	                                                 
	                                                 
	""";

const string upperBonusSuccess = """
	                                                 
	  You scored at least 63 in the upper section of 
	  the score sheet. You get the Aces-Sices Bonus. 
	  Press [enter] to continue...                   
	                                                 
	                                                 
	""";

const string upperBonusFail = """
	                                                 
	  You did not score at least 63 in the upper     
	  section of the score sheet. You do not get the 
	  Aces-Sices Bonus. Press [enter] to continue... 
	                                                 
	                                                 
	""";

const string gameComplete = """
	                                                 
	  Game complete. Your total score has been       
	  calculated on your scorecard.                  
	  Play again [enter] or quit [escape]?           
	                                                 
	                                                 
	""";

const int minWidth = 50;
const int minHeight = 32;

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
	Console.Clear();
	Render(false, false, false);
	Console.Write(welcome);
	PressToContinue(ConsoleKey.Enter);
	while (!escape && scores.Contains(null))
	{
		PlayRound();
		if (escape) return;
		if (IsYahtzee() && scores[12] > 0)
		{
			scores[14] = scores[14] is null ? 100 : scores[14] + 100;
			Render(false, false, false);
			Console.Write(yahtzeeBonus);
			PressToContinue(ConsoleKey.Enter);
			if (escape) return;
		}
		ScoreSelection();
	}
	if (escape) return;
	Render(false, false, false);
	Console.Write(gameComplete);
	PlayAgainCheck:
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.Enter: goto Restart;
		case ConsoleKey.Escape: escape = true; break;
		default: goto PlayAgainCheck;
	}
}
catch (Exception e)
{
	exception = e;
	throw;
}
finally
{
	Console.ResetColor();
	Console.Clear();
	Console.WriteLine(exception?.ToString() ?? "Yahtzee was closed.");
}

void Render(bool selectingDice, bool selectingScore, bool successiveRoll)
{
	Console.CursorVisible = false;
	EnsureConsoleSize();
	if (escape) return;
	Console.CursorVisible = false;
	Console.SetCursorPosition(0, 0);
	Console.WriteLine();
	Console.WriteLine("  Yahtzee");
	Console.WriteLine($"  ╒════════ Score Sheet ════════╕");
	Console.WriteLine($"  ├─────── Upper Section ───────┤");
	RenderSelectableScoreSheetLine(selectingScore, 00, "Aces...............");
	RenderSelectableScoreSheetLine(selectingScore, 01, "Twos...............");
	RenderSelectableScoreSheetLine(selectingScore, 02, "Threes.............");
	RenderSelectableScoreSheetLine(selectingScore, 03, "Fours..............");
	RenderSelectableScoreSheetLine(selectingScore, 04, "Fives..............");
	RenderSelectableScoreSheetLine(selectingScore, 05, "Sixes..............");
	Console.WriteLine($"  │   Aces-Sixes Bonus...[{RenderScore(scores[06])}] │");
	Console.WriteLine($"  ├─────── Lower Section ───────┤");
	RenderSelectableScoreSheetLine(selectingScore, 07, "3 of a Kind........");
	RenderSelectableScoreSheetLine(selectingScore, 08, "4 of a Kind........");
	RenderSelectableScoreSheetLine(selectingScore, 09, "Full House.........");
	RenderSelectableScoreSheetLine(selectingScore, 10, "Small Straight.....");
	RenderSelectableScoreSheetLine(selectingScore, 11, "Large Straight.....");
	RenderSelectableScoreSheetLine(selectingScore, 12, "Yahtzee............");
	RenderSelectableScoreSheetLine(selectingScore, 13, "Chance.............");
	Console.WriteLine($"  │   Yahtzee Bonus......[{RenderScore(scores[14])}] │");
	Console.WriteLine($"  │   Total..............[{RenderScore(scores[15])}] │");
	Console.WriteLine($"  └─────────────────────────────┘");
	Console.WriteLine("        ╔═══╗╔═══╗╔═══╗╔═══╗╔═══╗");
	Console.Write("  Dice: ");
	for (int i = 0; i < dice.Length; i++)
	{
		Console.Write($"║ {dice[i].ToString(CultureInfo.InvariantCulture)} ║");
	}
	Console.WriteLine();
	Console.WriteLine("        ╚═══╝╚═══╝╚═══╝╚═══╝╚═══╝");
	if (selectingDice)
	{
		Console.Write("        ");
		for (int i = 0; i < dice.Length; i++)
		{
			Console.Write(!locked[i] ? "^roll" : "     ");
		}
		Console.WriteLine();
		if (!successiveRoll)
		{
			Console.Write("        ");
			Console.ForegroundColor = ConsoleColor.Yellow;
			for (int i = 0; i < dice.Length; i++)
			{
				Console.Write(i == diceSelection ? "^^^^^" : "     ");
			}
			Console.ForegroundColor = ConsoleColor.White;
		}
	}
}

void RenderSelectableScoreSheetLine(bool selectingScore, int index, string line)
{
	Console.Write($"  │ ");
	if (selectingScore && scoreSelection == index)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.Write('>');
	}
	else
	{
		Console.Write(" ");
	}
	Console.Write($" {line}[{RenderScore(scores[index])}]");
	Console.ForegroundColor = ConsoleColor.White;
	Console.WriteLine($" │");
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

void RollDice(bool selectingDice)
{
	TimeSpan rollTime = TimeSpan.FromSeconds(1.5);
	DateTime start = DateTime.Now;
	while (DateTime.Now - start < rollTime)
	{
		while (Console.KeyAvailable)
		{
			if (Console.ReadKey(true).Key is ConsoleKey.Escape)
			{
				escape = true;
				return;
			}
		}
		for (int i = 0; i < dice.Length; i++)
		{
			if (!locked[i])
			{
				dice[i] = Random.Shared.Next(1, 7);
			}
		}
		Render(selectingDice, false, false);
		Console.Write(blank);
	}
}

void PlayRound()
{
	Array.Fill(locked, false);
	Render(false, false, false);
	Console.Write(rollDice);
	PressToContinue(ConsoleKey.Spacebar);
	if (escape) return;
	RollDice(false);
	if (escape) return;
	Array.Fill(locked, true);
	DiceSelection(chooseDice2);
	if (!locked.Contains(false) || escape) return;
	Render(true, false, true);
	Console.Write(rollDiceSmall);
	PressToContinue(ConsoleKey.Spacebar);
	if (escape) return;
	RollDice(false);
	if (escape) return;
	Array.Fill(locked, true);
	DiceSelection(chooseDice1);
	if (!locked.Contains(false) || escape) return;
	Render(true, false, true);
	Console.Write(rollDiceSmall);
	PressToContinue(ConsoleKey.Spacebar);
	if (escape) return;
	RollDice(false);
	if (escape) return;
}

void DiceSelection(string message)
{
	diceSelection = 0;
GetInput:
	Render(true, false, false);
	Console.Write(message);
	switch (Console.ReadKey(true).Key)
	{
		case ConsoleKey.LeftArrow: diceSelection = Math.Max(diceSelection - 1, 0); goto GetInput;
		case ConsoleKey.RightArrow: diceSelection = Math.Min(diceSelection + 1, dice.Length - 1); goto GetInput;
		case ConsoleKey.UpArrow: locked[diceSelection] = !locked[diceSelection]; goto GetInput;
		case ConsoleKey.Enter: break;
		case ConsoleKey.Escape: escape = true; return;
		default: goto GetInput;
	}
}

void ScoreSelection()
{
	scoreSelection = 0;
GetInput:
	Render(false, true, false);
	Console.Write(selectScore);
	switch (Console.ReadKey(true).Key)
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
					Render(false, false, false);
					Console.Write(scores[06] is 0 ? upperBonusFail : upperBonusSuccess);
					PressToContinue(ConsoleKey.Enter);
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
				Render(false, true, false);
				Console.Write(selectScoreInvalid);
				PressToContinue(ConsoleKey.Enter);
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

void PressToContinue(ConsoleKey key)
{
GetInput:
	ConsoleKey input = Console.ReadKey(true).Key;
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

void EnsureConsoleSize()
{
	int width = Console.WindowWidth;
	int height = Console.WindowHeight;
	while (!escape && (width < minWidth || height < minHeight))
	{
		Console.Clear();
		Console.WriteLine("Increase console size and press [enter]...");
		bool enter = false;
		while (!escape && !enter)
		{
			switch (Console.ReadKey(true).Key)
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
		Console.Clear();
	}
}