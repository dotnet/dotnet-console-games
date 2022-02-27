using System;

string board = @"
  ╔═════╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═══╤═════╗
  ║     │   │   │   │   │   │   │   │   │     ║
  ║     │   │   │   │   │   │   │   │   │     ║
  ╟─────┼───┴───┴───┴───┴───┴───┴───┴───┼─────╢
  ║     │                               │     ║
  ╟─────┤                               ├─────╢
  ║     │                               │     ║
  ╟─────┤                               ├─────╢
  ║     │     *   ────────────>   │     │     ║
  ╟─────┤     │                   │     ├─────╢
  ║     │     │   Roll And Move   │     │     ║
  ╟─────┤     │                   │     ├─────╢
  ║     │     │   <────────────   v     │     ║
  ╟─────┤                               ├─────╢
  ║     │                               │     ║
  ╟─────┤                               ├─────╢
  ║     │                               │     ║
  ╟─────┼───┬───┬───┬───┬───┬───┬───┬───┼─────╢
  ║     │   │   │   │   │   │   │   │   │     ║
  ║     │   │   │   │   │   │   │   │   │     ║
  ╚═════╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═══╧═════╝
";

string newGame = @"
  ***************** New  Game *****************
  Be the first player to circle the board.     
  Press [enter] to begin...                    ";

string turn_a = @"
  It is your turn. Press [enter] to roll your  
  dice...                                      
                                               ";

string roll_a = @"
  ╔═══╗  You roll a {0} and move your pawn {0}     
  ║ {0} ║  spaces. Press [enter] to continue...  
  ╚═══╝                                        ";

string roll_b = @"
  ╔═══╗  Your opponent rolls a {0} and moves his 
  ║ {0} ║  pawn {0} spaces. Press [enter] to       
  ╚═══╝  continue...                           ";

string last_turn_b = @"
  You reach the goal, but your opponent gets   
  one more move to try for a tie game. Press   
  [enter] to continue...                       ";

string tie = @"
  ================= Tie Game! =================
  You and your opponent circled the board. Play
  again [enter] or quit [escape]?              ";

string win = @"
  ================= You  Win! =================
  You circled the board before your opponent.  
  Play again [enter] or quit [escape]?         ";

string lose = @"
  ================= You Lose! =================
  Your opponent circled the board before you.  
  Play again [enter] or quit [escape]?         ";

ConsoleColor color_a = ConsoleColor.Blue;
(int Top, int Left)[] spots_a =
{
	/* top    */ (02, 04), (02, 10), (02, 14), (02, 18), (02, 22), (02, 26), (02, 30), (02, 34), (02, 38),
	/* right  */ (02, 44), (05, 44), (07, 44), (09, 44), (11, 44), (13, 44), (15, 44), (17, 44),
	/* bottom */ (20, 44), (20, 38), (20, 34), (20, 30), (20, 26), (20, 22), (20, 18), (20, 14), (20, 10),
	/* left   */ (20, 04), (17, 04), (15, 04), (13, 04), (11, 04), (09, 04), (07, 04), (05, 04), (02, 04),
};

ConsoleColor color_b = ConsoleColor.Red;
(int Top, int Left)[] spots_b =
{
	/* top    */ (03, 06), (03, 10), (03, 14), (03, 18), (03, 22), (03, 26), (03, 30), (03, 34), (03, 38),
	/* right  */ (03, 42), (05, 42), (07, 42), (09, 42), (11, 42), (13, 42), (15, 42), (17, 42),
	/* bottom */ (19, 42), (19, 38), (19, 34), (19, 30), (19, 26), (19, 22), (19, 18), (19, 14), (19, 10),
	/* left   */ (19, 06), (17, 06), (15, 06), (13, 06), (11, 06), (09, 06), (07, 06), (05, 06), (03, 06),
};

Random random = new();
bool escape = false;
while (!escape)
{
	Console.ForegroundColor = ConsoleColor.White;
	Console.BackgroundColor = ConsoleColor.Black;
	Console.Clear();
	Console.Write(board);
	int player_a = 0;
	int player_b = 0;
	RenderPixel('■', spots_a[player_a], color_a);
	RenderPixel('■', spots_b[player_b], color_b);
	Prompt(newGame);
	while (!escape && player_a < spots_a.Length - 1 && player_b < spots_b.Length - 1)
	{
		Prompt(turn_a);
		if (escape) break;
		string move_a = MovePlayer(ref player_a, spots_a, color_a);
		Prompt(string.Format(roll_a, move_a));
		if (escape) break;
		if (player_a >= spots_a.Length - 1) Prompt(last_turn_b);
		if (escape) break;
		string move_b = MovePlayer(ref player_b, spots_b, color_b);
		Prompt(string.Format(roll_b, move_b));
	}
	if (escape) break;
	switch (player_a >= spots_a.Length - 1, player_b >= spots_b.Length - 1)
	{
		case (true,  true): Prompt(tie);  break;
		case (true, false): Prompt(win);  break;
		case (false, true): Prompt(lose); break;
	}
}
Console.ResetColor();
Console.CursorVisible = true;
Console.Clear();
Console.Write("Roll And Move was closed.");

void Prompt(string message)
{
	Console.SetCursorPosition(0, 22);
	Console.Write(message);
	PressEnterToContinue();
}

string MovePlayer(ref int player, (int Top, int Left)[] spots, ConsoleColor color)
{
	RenderPixel(' ', spots[player], ConsoleColor.White);
	int roll = random.Next(6) + 1;
	player = Math.Min(spots.Length - 1, player + roll);
	RenderPixel('■', spots[player], color);
	return roll.ToString(System.Globalization.CultureInfo.InvariantCulture);
}

void RenderPixel(char c, (int Top, int Left) spot, ConsoleColor color)
{
	Console.SetCursorPosition(spot.Left, spot.Top);
	Console.ForegroundColor = color;
	Console.Write(c);
	Console.ForegroundColor = ConsoleColor.White;
}

void PressEnterToContinue()
{
	while (true)
	{
		Console.CursorVisible = false;
		switch (Console.ReadKey(true).Key)
		{
			case ConsoleKey.Enter: return;
			case ConsoleKey.Escape: escape = true; return;
		}
	}
}
