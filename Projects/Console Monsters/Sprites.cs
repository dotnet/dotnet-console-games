namespace Console_Monsters;

public static class Sprites
{
	public const int Width = 7;
	public const int Height = 5;

	public const int BattleSpriteWidth = 70;
	public const int BattleSpriteHeight = 20;

	#region Interior

	public const string InteriorWallNE =
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ ╰───" + "\n" +
		@" ╰─────" + "\n" +
		@"       ";
	public const string InteriorWallNW =
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"───╯ │ " + "\n" +
		@"─────╯ " + "\n" +
		@"       ";
	public const string InteriorWallSW =
		@"       " + "\n" +
		@"─────╮ " + "\n" +
		@"───╮ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ ";
	public const string InteriorWallSE =
		@"       " + "\n" +
		@" ╭─────" + "\n" +
		@" │ ╭───" + "\n" +
		@" │ │   " + "\n" +
		@" │ │   ";
	public const string InteriorWallEWHigh =
		@"       " + "\n" +
		@"───────" + "\n" +
		@"───────" + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string InteriorWallEWLow =
		@"       " + "\n" +
		@"       " + "\n" +
		@"───────" + "\n" +
		@"───────" + "\n" +
		@"       ";
	public const string InteriorWallNSLeft =
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ │   " + "\n" +
		@" │ │   ";
	public const string InteriorWallNSRight =
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ " + "\n" +
		@"   │ │ ";

	#endregion

	#region Buildings

	public const string BuildingSmall =
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/-----\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string VetSmall = // REMOVE? (Make it bigger like the pokecenter?)
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/-Vet-\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string Store = // REMOVE? (Make it bigger like the pokemart?)
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/Store\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string Door =
		@"╔═════╗" + "\n" +
		@"║ ■■■ ║" + "\n" +
		@"║    o║" + "\n" +
		@"║     ║" + "\n" +
		@"╚═════╝";
	public const string TopRoofLeft =
		@"       " + "\n" +
		@"   ////" + "\n" +
		@"  /////" + "\n" +
		@" //////" + "\n" +
		@"///////";
	public const string TopRoofRight =
		@"       " + "\n" +
		@"\\\\   " + "\n" +
		@"\\\\\  " + "\n" +
		@"\\\\\\ " + "\n" +
		@"\\\\\\\";
	public const string MiddleRoof =
		@"       " + "\n" +
		@"|||||||" + "\n" +
		@"|||||||" + "\n" +
		@"|||||||" + "\n" +
		@"|||||||";
	public const string BuildingLeft =
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      ";
	public const string BuildingBaseLeft =
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"│      " + "\n" +
		@"└──────";
	public const string BuildingRight =
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │";
	public const string BuildingBaseRight =
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"──────┘";
	public const string MiddleWindow =
		@"       " + "\n" +
		@"       " + "\n" +
		@" ▐█ ▐█ " + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string LowWindowSideLeft =
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │" + "\n" +
		@"      │";
	public const string LowWindow =
		@"       " + "\n" +
		@"       " + "\n" +
		@" ▐█ ▐█ " + "\n" +
		@"       " + "\n" +
		@"───────";

	#endregion

	#region Objects

	public const string Sign = // Text TBC
		@" ┬──┬─┐" + "\n" +
		@"╭┴──┴╮│" + "\n" +
		@"│Sign││" + "\n" +
		@"╰────╯│" + "\n" +
		@"      │";
	public const string Fence =
		@"       " + "\n" +
		@"╔═════╗" + "\n" +
		@"╠═════╣" + "\n" +
		@"║     ║" + "\n" +
		@"╩     ╩";
	public const string FenceLow =
		@"       " + "\n" +
		@"       " + "\n" +
		@"╦═════╦" + "\n" +
		@"╬═════╬" + "\n" +
		@"╩     ╩";

	#endregion

	#region Wall

	public const string Wall_0000 =
		@"╔═════╗" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"╚═════╝";
	public const string Wall_0001 =
		@"══════╗" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"══════╝";
	public const string Wall_0010 =
		@"╔═════╗" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║";
	public const string Wall_0011 =
		@"══════╗" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"╗█████║";
	public const string Wall_0100 =
		@"╔══════" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"╚══════";
	public const string Wall_0101 =
		@"═══════" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"═══════";
	public const string Wall_0110 =
		@"╔══════" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║█████╔";
	public const string Wall_0111 =
		@"═══════" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"╗█████╔";
	public const string Wall_1000 =
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"╚═════╝";
	public const string Wall_1001 =
		@"╝█████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"══════╝";
	public const string Wall_1010 =
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║" + "\n" +
		@"║█████║";
	public const string Wall_1011 =
		@"╝█████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"██████║" + "\n" +
		@"╗█████║";
	public const string Wall_1100 =
		@"║█████╚" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"╚══════";
	public const string Wall_1101 =
		@"╝█████╚" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"═══════";
	public const string Wall_1110 =
		@"║█████╚" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║██████" + "\n" +
		@"║█████╔";
	public const string Wall_1111 =
		@"╝█████╚" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"███████" + "\n" +
		@"╗█████╔";

	#endregion

	#region Nature

	public const string Water =
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~";
	public const string Gate =
		@"▀▄▀▄▀▄▀" + "\n" +
		@"▀▄▀▄▀▄▀" + "\n" +
		@"▀▄▀▄▀▄▀" + "\n" +
		@"▀▄▀▄▀▄▀" + "\n" +
		@"▀▄▀▄▀▄▀";
	public const string Tree =
		@" /‾‾‾\ " + "\n" +
		@"/‾\ /‾\" + "\n" +
		@"\_____/" + "\n" +
		@"  | |  " + "\n" +
		@"  / \  ";
	public const string Tree2 =
		@"  (@@) " + "\n" +
		@" (@@@@)" + "\n" +
		@"   ||  " + "\n" +
		@"   ||  " + "\n" +
		@"   ||  ";
	public const string GrassDec =
		@" .   . " + "\n" +
		@".  . . " + "\n" +
		@" .   . " + "\n" +
		@".  .  ." + "\n" +
		@" .   . ";
	public const string Grass =
		@" v   v " + "\n" +
		@"v  v  v" + "\n" +
		@"  v  v " + "\n" +
		@"v  v  v" + "\n" +
		@" v  v  ";
	public const string Grass2 =
		@" ╷ ╷ v " + "\n" +
		@"╷ v ╷ ╷" + "\n" +
		@" ╷ ╷ v " + "\n" +
		@"v ╷ ╷ ╷" + "\n" +
		@" ╷ v ╷ ";
	public const string Grass3 =
		@"╵╷╵╷╵╷╵" + "\n" +
		@"╵╷╵╷╵╷╵" + "\n" +
		@"╵╷╵╷╵╷╵" + "\n" +
		@"╵╷╵╷╵╷╵" + "\n" +
		@"╵╷╵╷╵╷╵";
	public const string HalfRock =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"#######" + "\n" +
		@"#######";
	public const string HalfRockGrass =
		@" .  .  " + "\n" +
		@".  . . " + "\n" +
		@" .   . " + "\n" +
		@"#######" + "\n" +
		@"#######";
	public const string HalfRockStairs =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"▬▬▬▬▬▬▬" + "\n" +
		@"▬▬▬▬▬▬▬";
	public const string HalfRockStairsGrass =
		@" .  .  " + "\n" +
		@".  . . " + "\n" +
		@" .   . " + "\n" +
		@"▬▬▬▬▬▬▬" + "\n" +
		@"▬▬▬▬▬▬▬";
	public const string ArrowDown =
		@"  |-|  " + "\n" +
		@"  | |  " + "\n" +
		@" _| |_ " + "\n" +
		@" \   / " + "\n" +
		@"  \ /  ";
	public const string ArrowUp =
		@"  / \  " + "\n" +
		@" /_ _\ " + "\n" +
		@"  | |  " + "\n" +
		@"  | |  " + "\n" +
		@"  |_|  ";
	public const string Mountains =
		@" /_\   " + "\n" +
		@"/   /_\" + "\n" +
		@"/_\/   " + "\n" +
		@"   \   " + "\n" +
		@"    \   ";
	public const string Mountain =
		@"       " + "\n" +
		@"   /\  " + "\n" +
		@"  /--\ " + "\n" +
		@" /    \" + "\n" +
		@"/      ";
	public const string Mountain2 =
		@"       " + "\n" +
		@"   /\  " + "\n" +
		@"  /\/\ " + "\n" +
		@" /    \" + "\n" +
		@"/      ";
	public const string Mountain3 =
		@"       " + "\n" +
		@"   /\  " + "\n" +
		@"  /**\ " + "\n" +
		@" /    \" + "\n" +
		@"/      ";

	#endregion

	#region NPCs
	public static readonly string NPC1 =
		@"/_____\" + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"╰├───┤╯" + '\n' +
		@" │_|_│ ";
	#endregion

	#region Items
	public const string HealthPotion =
		@" [╤═╤] " + "\n" +
		@" ╭╯ ╰╮ " + "\n" +
		@" │   │ " + "\n" +
		@" │   │ " + "\n" +
		@" ╰───╯ ";
	public const string CaptureDevice =
		@"       " + "\n" +
		@" ╭───╮ " + "\n" +
		@" ╞═●═╡ " + "\n" +
		@" ╰───╯ " + "\n" +
		@"       ";
	#endregion

	public const string Box =
		@"       " + "\n" +
		@" ╭───╮ " + "\n" +
		@" ╞═●═╡ " + "\n" +
		@" ╰───╯ " + "\n" +
		@"       ";

	public const string Open =
		@"       " + "\n" +
		@"       " + "\n" +	
		@"       " + "\n" +
		@"       " + "\n" +
		@"       ";

	public const string Error =
		@"╔═════╗" + "\n" +
		@"║error║" + "\n" +
		@"║error║" + "\n" +
		@"║error║" + "\n" +
		@"╚═════╝";
}
