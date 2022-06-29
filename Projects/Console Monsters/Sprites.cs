namespace Console_Monsters;

public static class Sprites
{
	public const int Width = 7;
	public const int Height = 5;

	public const int BattleSpriteWidth = 70;
	public const int BattleSpriteHeight = 20;

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
		@" ┬──┬─╮" + "\n" +
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
		@"       "+ "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"#######" + "\n" +
		@"#######";
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

	#region Character

	public static readonly string[] RunRight = new[]
	{
		// 0
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ",
		// 1
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │_─┘ ",
		// 2
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │_─┘ ",
		// 3
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ",
		// 4
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  └─_│ ",
		// 5
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  └─_│ ",
	};

	public static readonly string[] RunLeft = new[]
	{
		// 0
		@" ╭══╮  " + '\n' +
		@" │' │  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │__│  ",
		// 1
		@" ╭══╮  " + '\n' +
		@" │' │  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │_─┘  ",
		// 2
		@" ╭══╮  " + '\n' +
		@" │' │  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │_─┘  ",
		// 3
		@" ╭══╮  " + '\n' +
		@" │' │  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │__│  ",
		// 4
		@" ╭══╮  " + '\n' +
		@" │' │  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" └─_│  ",
		// 5
		@" ╭══╮  " + '\n' +
		@" │' │  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" └─_│  ",
	};

	public static readonly string[] RunDown = new[]
	{
		// 0
		@" ╭═══╮ " + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ",
		// 1
		@" ╭═══╮ " + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" └─┤_│ ",
		// 2
		@" ╭═══╮ " + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" └─┤_│ ",
		// 3
		@" ╭═══╮ " + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ",
		// 4
		@" ╭═══╮ " + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_├─┘ ",
		// 5
		@" ╭═══╮ " + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_├─┘ ",
	};

	public static readonly string[] RunUp = new[]
	{
		// 0
		@" ╭═══╮ " + '\n' +
		@" │   │ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ",
		// 1
		@" ╭═══╮ " + '\n' +
		@" │   │ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" └─┤_│ ",
		// 2
		@" ╭═══╮ " + '\n' +
		@" │   │ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" └─┤_│ ",
		// 3
		@" ╭═══╮ " + '\n' +
		@" │   │ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ",
		// 4
		@" ╭═══╮ " + '\n' +
		@" │   │ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_├─┘ ",
		// 5
		@" ╭═══╮ " + '\n' +
		@" │   │ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_├─┘ ",
	};

	public static readonly string Idle1 =
		@" ╭═══╮ " + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";

	public static readonly string Idle2 =
		@" ╭═══╮ " + '\n' +
		@" │-_-│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";

	public static readonly string[] IdlePlayer =
		Enumerable.Repeat(Idle1, 100).Concat(Enumerable.Repeat(Idle2, 10)).ToArray();

	#endregion

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
