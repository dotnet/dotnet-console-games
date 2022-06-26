namespace Animal_Trainer;

public static class Sprites
{
	public const int Width = 7;
	public const int Height = 5;

	public const string Open =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string BuildingSmall =
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/-----\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string BuildingLowLargeDoorLeft =
		@" /-----" + "\n" +
		@"/------" + "\n" +
		@"|      " + "\n" +
		@"|      " + "\n" +
		@"|  █   ";
	public const string BuildingLowLargeDoorRight =
		@"-----\ " + "\n" +
		@"------\" + "\n" +
		@"      |" + "\n" +
		@"      |" + "\n" +
		@"   █  |";
	public const string BuildingLowLargeLeft =
		@" /-----" + "\n" +
		@"/------" + "\n" +
		@"|      " + "\n" +
		@"|      " + "\n" +
		@"| ▀ ▀  ";
	public const string BuildingLowLargeRight =
		@"-----\ " + "\n" +
		@"------\" + "\n" +
		@"      |" + "\n" +
		@"      |" + "\n" +
		@" ▀ ▀  |";
	public const string BuildingLargeDoorLeft =
		@"|      " + "\n" +
		@"|      " + "\n" +
		@"|      " + "\n" +
		@"|      " + "\n" +
		@"|  █   ";
	public const string BuildingLargeDoorRight =
		@"      |" + "\n" +
		@"      |" + "\n" +
		@"      |" + "\n" +
		@"      |" + "\n" +
		@"   █  |";
	public const string BuildingLargeLeft =
		@"|      " + "\n" +
		@"|      " + "\n" +
		@"|      " + "\n" +
		@"|      " + "\n" +
		@"|  ▀ ▀ ";
	public const string BuildingLargeRight =
		@"      |" + "\n" +
		@"      |" + "\n" +
		@"      |" + "\n" +
		@"      |" + "\n" +
		@"  ▀ ▀ |";
	public const string BuildingLargeTopLeft =
		@"  /----" + "\n" +
		@" /-----" + "\n" +
		@"/------" + "\n" +
		@"| ▄ ▄  " + "\n" +
		@"|      ";
	public const string BuildingLargeTopRight =
		@"----\  " + "\n" +
		@"-----\ " + "\n" +
		@"------\" + "\n" +
		@"  ▄ ▄ |" + "\n" +
		@"      |";
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
	public const string Chest =  // REMOVE?
		@"       " + "\n" +
		@"       " + "\n" +
		@"  _._  " + "\n" +
		@" |___| " + "\n" +
		@"       ";
	public const string EmptyChest = // REMOVE?
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@" |___| " + "\n" +
		@"       ";
	public const string Water =
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~" + "\n" +
		@"~~~~~~~";
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
	public const string Barrels1 = // REMOVE?
		@"       " + "\n" +
		@"       " + "\n" +
		@"  /---\" + "\n" +
		@"/-\/-\|" + "\n" +
		@"\ /\ //";
	public const string Barrels2 = // REMOVE?
		@"       " + "\n" +
		@"       " + "\n" +
		@"/---\  " + "\n" +
		@"|/-\/-\" + "\n" +
		@"\\ /\ /";
	public const string Barrels3 = // REMOVE?
		@"       " + "\n" +
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/--\/-\" + "\n" +
		@"\  /\ /";
	public const string Sign = // Text TBC
		@" ┬──┬─╮" + "\n" +
		@"╭┴──┴╮│" + "\n" +
		@"│ʣʨʢʬ││" + "\n" +
		@"╰────╯│" + "\n" +
		@"      │";
	public const string GrassDec =
		@"░░░░░░░" + "\n" +
		@"░░░░░░░" + "\n" +
		@"░░░░░░░" + "\n" +
		@"░░░░░░░" + "\n" +
		@"░░░░░░░";
	public const string Grass =
		@" v   v " + "\n" +
		@"v  v  v" + "\n" +
		@"  v  v " + "\n" +
		@"v  v  v" + "\n" +
		@" v  v  ";
	public const string Grass1 =
		@" . . . " + "\n" +
		@". . . ." + "\n" +
		@" . . . " + "\n" +
		@". . . ." + "\n" +
		@" . . . ";
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
	public const string Fence = // Redesign?
		@"#######" + "\n" +
		@"#_____#" + "\n" +
		@"#     #" + "\n" +
		@"#     #" + "\n" +
		@"#     #";
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
	public const string Error =
		@"╔═════╗" + "\n" +
		@"║error║" + "\n" +
		@"║error║" + "\n" +
		@"║error║" + "\n" +
		@"╚═════╝";

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
}
