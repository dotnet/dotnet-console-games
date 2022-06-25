using System.Linq;

namespace Animal_Trainer;

public static class Sprites
{
	public const string Open =
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"       " + "\n" +
		@"       ";
	public const string Town =
		@"╔══ ══╗" + "\n" +
		@"║▀▀ ▀▀║" + "\n" +
		@"║▀▀ ▀▀║" + "\n" +
		@"║▄▄ ▄▄║" + "\n" +
		@"╚══ ══╝";
	public const string Castle =
		@"╔══ ══╗" + "\n" +
		@"║╔═══╗║" + "\n" +
		@"║║   ║║" + "\n" +
		@"║╚═══╝║" + "\n" +
		@"╚══ ══╝";
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
	public const string InnSmall =
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/-Inn-\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string Store =
		@"       " + "\n" +
		@" /---\ " + "\n" +
		@"/Store\" + "\n" +
		@"|     |" + "\n" +
		@"|  █  |";
	public const string Chest =
		@"       " + "\n" +
		@"       " + "\n" +
		@"  _._  " + "\n" +
		@" |___| " + "\n" +
		@"       ";
	public const string EmptyChest =
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
		@" /---\ " + "\n" +
		@" \___/ " + "\n" +
		@"  | |  " + "\n" +
		@"  | |  " + "\n" +
		@"  / \  ";
	public const string Tree2 =
		@"  (@@) " + "\n" +
		@" (@@@@)" + "\n" +
		@"   ||  " + "\n" +
		@"   ||  " + "\n" +
		@"   ||  ";
	public const string Barrels1 =
		@"       " + "\n" +
		@"       " + "\n" +
		@"  /---\" + "\n" +
		@"/-\/-\|" + "\n" +
		@"\ /\ //";
	public const string Barrels2 =
		@"       " + "\n" +
		@"       " + "\n" +
		@"/---\  " + "\n" +
		@"|/-\/-\" + "\n" +
		@"\\ /\ /";
	public const string Barrels3 =
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
	public const string Fence =
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
		@"  │ˍˍ│ " + '\n' +
		@"  │|\_\ ",
		// 1
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │ˍˍ│ " + '\n' +
		@"  │ˍˍ│ ",
		// 2
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │ˍˍ│ " + '\n' +
		@"  \_\│ ",
		// 3
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │ˍˍ│ " + '\n' +
		@"  │ˍˍ│ ",
	};

	public static readonly string[] RunLeft = new[]
	{
		// 0
		@"  ╭══╮ " + '\n' +
		@"  │' │ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │ˍˍ│ " + '\n' +
		@" /ˍ/|│ ",
		// 1
		@"  ╭══╮ " + '\n' +
		@"  │' │ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │ˍˍ│ " + '\n' +
		@"  │ˍˍ│ ",
		// 2
		@"  ╭══╮ " + '\n' +
		@"  │' │ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │ˍˍ│ " + '\n' +
		@"  │/ˍ/ ",
		// 3
		@"  ╭══╮ " + '\n' +
		@"  │' │ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │ˍˍ│ " + '\n' +
		@"  │ˍˍ│ ",
	};

	// would be nice to give up/down their own animations, but
	// for now they are just copies of left/right
	public static readonly string[] RunDown = (string[])RunRight.Clone();
	public static readonly string[] RunUp = (string[])RunLeft.Clone();

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

	// would be nice to give up/down their own animations, but
	// for now they are just copies of left/right
	public static readonly string[] Idle = (string[])IdlePlayer.Clone();


	public static readonly string[] IdleBoar = new[]
	{
		// 0
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 1
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 2
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ', ,' ",
		// 3
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ', ,' ",
		// 4
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ', ,' ",
		// 5
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ', ,' ",
		// 6
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ', ,' ",
		// 7
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,' ', ",
		// 8
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,' ', ",
		// 9
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,' ', ",
		// 10
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,' ', ",
		// 11
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,' ', ",
		// 12
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 13
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 14
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 15
		@" ^..^  " + '\n' +
		@"(oo)\  " + '\n' +
		@"  O  )~" + '\n' +
		@" '' ,, ",
		// 16
		@" ^..^  " + '\n' +
		@"(oo)\  " + '\n' +
		@"  O  )~" + '\n' +
		@" '' ,, ",
		// 17
		@" ^..^  " + '\n' +
		@"(oo)\  " + '\n' +
		@"  O  )~" + '\n' +
		@" '' ,, ",
		// 18
		@" ^..^  " + '\n' +
		@"(oo)\  " + '\n' +
		@"  O  )~" + '\n' +
		@" '' ,, ",
		// 19
		@" ^..^  " + '\n' +
		@"(oo)\  " + '\n' +
		@"  O  )~" + '\n' +
		@" '' ,, ",
		// 20
		@"       " + '\n' +
		@"^--^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 21
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 22
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 23
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 24
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 25
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 26
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
		// 27
		@"       " + '\n' +
		@"^..^   " + '\n' +
		@"(oo) )~" + '\n' +
		@" ,, ,, ",
	};
}
