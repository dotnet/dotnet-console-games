using System.Linq;

namespace Animal_Trainer
{
	public static class Sprites
	{
		public const string Open =
			@"       " + "\n" +
			@"       " + "\n" +
			@"       " + "\n" +
			@"       ";
		public const string Town =
			@"╔══ ══╗" + "\n" +
			@"║▀▀ ▀▀║" + "\n" +
			@"║▄▄ ▄▄║" + "\n" +
			@"╚══ ══╝";
		public const string Castle =
			@"╔══ ══╗" + "\n" +
			@"║╔═══╗║" + "\n" +
			@"║╚═══╝║" + "\n" +
			@"╚══ ══╝";
		public const string Building =
			@" /---\ " + "\n" +
			@"/-----\" + "\n" +
			@"|     |" + "\n" +
			@"|  █  |";
		public const string Inn =
			@" /---\ " + "\n" +
			@"/-Inn-\" + "\n" +
			@"|     |" + "\n" +
			@"|  █  |";
		public const string Store =
			@" /---\ " + "\n" +
			@"/Store\" + "\n" +
			@"|     |" + "\n" +
			@"|  █  |";
		public const string Chest =
			@"       " + "\n" +
			@"  _._  " + "\n" +
			@" |___| " + "\n" +
			@"       ";
		public const string EmptyChest =
			@"       " + "\n" +
			@"       " + "\n" +
			@" |___| " + "\n" +
			@"       ";
		public const string Water =
			@"~~~~~~~" + "\n" +
			@"~~~~~~~" + "\n" +
			@"~~~~~~~" + "\n" +
			@"~~~~~~~";
		public const string Wall_0000 =
			@"╔═════╗" + "\n" +
			@"║█████║" + "\n" +
			@"║█████║" + "\n" +
			@"╚═════╝";
		public const string Wall_0001 =
			@"══════╗" + "\n" +
			@"██████║" + "\n" +
			@"██████║" + "\n" +
			@"══════╝";
		public const string Wall_0010 =
			@"╔═════╗" + "\n" +
			@"║█████║" + "\n" +
			@"║█████║" + "\n" +
			@"║█████║";
		public const string Wall_0011 =
			@"══════╗" + "\n" +
			@"██████║" + "\n" +
			@"██████║" + "\n" +
			@"╗█████║";
		public const string Wall_0100 =
			@"╔══════" + "\n" +
			@"║██████" + "\n" +
			@"║██████" + "\n" +
			@"╚══════";
		public const string Wall_0101 =
			@"═══════" + "\n" +
			@"███████" + "\n" +
			@"███████" + "\n" +
			@"═══════";
		public const string Wall_0110 =
			@"╔══════" + "\n" +
			@"║██████" + "\n" +
			@"║██████" + "\n" +
			@"║█████╔";
		public const string Wall_0111 =
			@"═══════" + "\n" +
			@"███████" + "\n" +
			@"███████" + "\n" +
			@"╗█████╔";
		public const string Wall_1000 =
			@"║█████║" + "\n" +
			@"║█████║" + "\n" +
			@"║█████║" + "\n" +
			@"╚═════╝";
		public const string Wall_1001 =
			@"╝█████║" + "\n" +
			@"██████║" + "\n" +
			@"██████║" + "\n" +
			@"══════╝";
		public const string Wall_1010 =
			@"║█████║" + "\n" +
			@"║█████║" + "\n" +
			@"║█████║" + "\n" +
			@"║█████║";
		public const string Wall_1011 =
			@"╝█████║" + "\n" +
			@"██████║" + "\n" +
			@"██████║" + "\n" +
			@"╗█████║";
		public const string Wall_1100 =
			@"║█████╚" + "\n" +
			@"║██████" + "\n" +
			@"║██████" + "\n" +
			@"╚══════";
		public const string Wall_1101 =
			@"╝█████╚" + "\n" +
			@"███████" + "\n" +
			@"███████" + "\n" +
			@"═══════";
		public const string Wall_1110 =
			@"║█████╚" + "\n" +
			@"║██████" + "\n" +
			@"║██████" + "\n" +
			@"║█████╔";
		public const string Wall_1111 =
			@"╝█████╚" + "\n" +
			@"███████" + "\n" +
			@"███████" + "\n" +
			@"╗█████╔";
		public const string Gate =
			@"▀▄▀▄▀▄▀" + "\n" +
			@"▀▄▀▄▀▄▀" + "\n" +
			@"▀▄▀▄▀▄▀" + "\n" +
			@"▀▄▀▄▀▄▀";
		public const string Tree =
			@" /---\ " + "\n" +
			@" \___/ " + "\n" +
			@"  | |  " + "\n" +
			@"  / \  ";
		public const string Tree2 =
			@"  (@@) " + "\n" +
			@" (@@@@)" + "\n" +
			@"   ||  " + "\n" +
			@"   ||  ";
		public const string Barrels1 =
			@"       " + "\n" +
			@"  /---\ " + "\n" +
			@"/-\/-\|" + "\n" +
			@"\ /\ //";
		public const string Barrels2 =
			@"       " + "\n" +
			@"/---\  " + "\n" +
			@"|/-\/-\" + "\n" +
			@"\\ /\ /";
		public const string Barrels3 =
			@"       " + "\n" +
			@" /---\ " + "\n" +
			@"/--\/-\" + "\n" +
			@"\  /\ /";
		public const string Fence =
			@"       " + "\n" +
			@"       " + "\n" +
			@"#######" + "\n" +
			@"#######";
		public const string ArrowDown =
			@"  |-|  " + "\n" +
			@" _| |_ " + "\n" +
			@" \   / " + "\n" +
			@"  \ /  ";
		public const string ArrowUp =
			@"  / \  " + "\n" +
			@" /_ _\ " + "\n" +
			@"  | |  " + "\n" +
			@"  |_|  ";
		public const string Mountains =
			@" /_\   " + "\n" +
			@"/   /_\" + "\n" +
			@"/_\/   " + "\n" +
			@"   \   ";
		public const string Mountain =
			@"   /\  " + "\n" +
			@"  /--\ " + "\n" +
			@" /    \" + "\n" +
			@"/      ";
		public const string Mountain2 =
			@"   /\  " + "\n" +
			@"  /\/\ " + "\n" +
			@" /    \" + "\n" +
			@"/      ";
		public const string Mountain3 =
			@"   /\  " + "\n" +
			@"  /**\ " + "\n" +
			@" /    \" + "\n" +
			@"/      ";
		public const string Guard =
			@" ^  O  " + "\n" +
			@" |--|> " + "\n" +
			@" |  |  " + "\n" +
			@" | | | ";
		public const string King =
			@"  'O'  " + "\n" +
			@"  /|\  " + "\n" +
			@"   |   " + "\n" +
			@"  | |  ";
		public const string Error =
			@"╔═════╗" + "\n" +
			@"║error║" + "\n" +
			@"║error║" + "\n" +
			@"╚═════╝";

		public static readonly string[] RunRight = new[]
		{
			// 0
			@"   O   " + '\n' +
			@"   |_  " + '\n' +
			@"   |>  " + '\n' +
			@"  /|   ",
			// 1
			@"   O   " + '\n' +
			@"  <|L  " + '\n' +
			@"   |_  " + '\n' +
			@"   |/  ",
			// 2
			@"   O   " + '\n' +
			@"  L|L  " + '\n' +
			@"   |_  " + '\n' +
			@"  /  | ",
			// 3
			@"  _O   " + '\n' +
			@" | |L  " + '\n' +
			@"   /─  " + '\n' +
			@"  /  \ ",
			// 4
			@"  __O  " + '\n' +
			@" / /\_ " + '\n' +
			@"__/\   " + '\n' +
			@"    \  ",
			// 5
			@"   _O  " + '\n' +
			@"  |/|_ " + '\n' +
			@"  /\   " + '\n' +
			@" /  |  ",
			// 6
			@"    O  " + '\n' +
			@"  </L  " + '\n' +
			@"   \   " + '\n' +
			@"   /|  ",
		};

		public static readonly string[] RunLeft = new[]
		{
			// 0
			@"   O   " + '\n' +
			@"  _|   " + '\n' +
			@"  <|   " + '\n' +
			@"   |\  ",
			// 1
			@"   O   " + '\n' +
			@"  >|>  " + '\n' +
			@"  _|   " + '\n' +
			@"  \|   ",
			// 2
			@"   O   " + '\n' +
			@"  >|>  " + '\n' +
			@"  _|   " + '\n' +
			@" |  \  ",
			// 3
			@"   O_  " + '\n' +
			@"  >| | " + '\n' +
			@"  ─\   " + '\n' +
			@" /  \  ",
			// 4
			@"  O__  " + '\n' +
			@" _/\ \ " + '\n' +
			@"   /\__" + '\n' +
			@"  /    ",
			// 5
			@"  O_   " + '\n' +
			@" _|\|  " + '\n' +
			@"   /\  " + '\n' +
			@"  |  \ ",
			// 6
			@"  O    " + '\n' +
			@"  >\>  " + '\n' +
			@"   /   " + '\n' +
			@"  |\   ",
		};

		// would be nice to give up/down their own animations, but
		// for now they are just copies of left/right
		public static readonly string[] RunDown = (string[])RunRight.Clone();
		public static readonly string[] RunUp   = (string[])RunLeft.Clone();

		public static readonly string IdleLeft1 =
			@"   O   " + '\n' +
			@"  )|J  " + '\n' +
			@"   |   " + '\n' +
			@"  / )  ";

		public static readonly string IdleLeft2 =
			@"   o   " + '\n' +
			@"  J))  " + '\n' +
			@"   |   " + '\n' +
			@"  ( \  ";

		public static readonly string[] IdleLeft =
			Enumerable.Repeat(IdleLeft1, 10).Concat(Enumerable.Repeat(IdleLeft2, 10)).ToArray();

		public static readonly string IdleRight1 =
			@"   O   " + '\n' +
			@"  L|(  " + '\n' +
			@"   |   " + '\n' +
			@"  ( \  ";

		public static readonly string IdleRight2 =
			@"   o   " + '\n' +
			@"  ((L  " + '\n' +
			@"   |   " + '\n' +
			@"  / )  ";

		public static readonly string[] IdleRight =
			Enumerable.Repeat(IdleRight1, 10).Concat(Enumerable.Repeat(IdleRight2, 10)).ToArray();

		// would be nice to give up/down their own animations, but
		// for now they are just copies of left/right
		public static readonly string[] IdleDown = (string[])IdleRight.Clone();
		public static readonly string[] IdleUp   = (string[])IdleLeft.Clone();

		public static readonly string[] FallLeft = new string[]
		{
			// 0
			@"  O___ " + '\n' +
			@"   \`- " + '\n' +
			@"   /\  " + '\n' +
			@"  / /  ",
			// 1
			@"  O___ " + '\n' +
			@"   \`- " + '\n' +
			@"   /\  " + '\n' +
			@"  / /  ",
			// 2
			@"  //   " + '\n' +
			@" O/__  " + '\n' +
			@"  __/\ " + '\n' +
			@"     / ",
			// 3
			@"  //   " + '\n' +
			@" O/__  " + '\n' +
			@"  __/\ " + '\n' +
			@"     / ",
			// 4
			@"       " + '\n' +
			@"  //   " + '\n' +
			@" O/__/\" + '\n' +
			@"      \",
			// 5
			@"       " + '\n' +
			@"  //   " + '\n' +
			@" O/__/\" + '\n' +
			@"      \",
			// 6
			@"       " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@" o___/\",
			// 7
			@"       " + '\n' +
			@"       " + '\n' +
			@"       " + '\n' +
			@" o___/\",
		};

		public static readonly string[] PunchRight = new string[]
		{
			// 0
			@" _o_.  " + '\n' +
			@" (|    " + '\n' +
			@"  |    " + '\n' +
			@" > \   ",
			// 0
			@" _o_.  " + '\n' +
			@" (|    " + '\n' +
			@"  |    " + '\n' +
			@" > \   ",
			// 1
			@"  o__. " + '\n' +
			@" (|    " + '\n' +
			@"  |    " + '\n' +
			@" / >   ",
			// 1
			@"  o__. " + '\n' +
			@" (|    " + '\n' +
			@"  |    " + '\n' +
			@" / >   ",
			// 2
			@"  O___." + '\n' +
			@" L(    " + '\n' +
			@"  |    " + '\n' +
			@" / >   ",
			// 2
			@"  O___." + '\n' +
			@" L(    " + '\n' +
			@"  |    " + '\n' +
			@" / >   ",
			// 3
			@"   o_  " + '\n' +
			@"  L( \ " + '\n' +
			@"   |   " + '\n' +
			@"  > \  ",
			// 3
			@"   o_  " + '\n' +
			@"  L( \ " + '\n' +
			@"   |   " + '\n' +
			@"  > \  ",
			// 4
			@"   o_  " + '\n' +
			@"  L( > " + '\n' +
			@"   |   " + '\n' +
			@"  > \  ",
			// 4
			@"   o_  " + '\n' +
			@"  L( > " + '\n' +
			@"   |   " + '\n' +
			@"  > \  ",
			// 5
			@"   o   " + '\n' +
			@"  (|)  " + '\n' +
			@"   |   " + '\n' +
			@"  / \  ",
		};

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

		public static readonly string[] GetUpAnimationRight = new string[]
		{
			// 0
			@"       " + '\n' +
			@"       " + '\n' +
			@"    __ " + '\n' +
			@" o__\  ",
			// 1
			@"       " + '\n' +
			@"       " + '\n' +
			@"    __ " + '\n' +
			@" o__\  ",
			// 2
			@"       " + '\n' +
			@"       " + '\n' +
			@"    /  " + '\n' +
			@" o__\  ",
			// 3
			@"       " + '\n' +
			@"       " + '\n' +
			@"    /  " + '\n' +
			@" o__\  ",
			// 4
			@"       " + '\n' +
			@"    |  " + '\n' +
			@"    |  " + '\n' +
			@" o_/   ",
			// 5
			@"       " + '\n' +
			@"    |  " + '\n' +
			@"    |  " + '\n' +
			@" o_/   ",
			// 6
			@"       " + '\n' +
			@"    |\ " + '\n' +
			@" o_/   " + '\n' +
			@" /\    ",
			// 7
			@"       " + '\n' +
			@"    |\ " + '\n' +
			@" o_/   " + '\n' +
			@" /\    ",
			// 8
			@"       " + '\n' +
			@"  /-\  " + '\n' +
			@"/o/ // " + '\n' +
			@"       ",
			// 9
			@"       " + '\n' +
			@"  /-\  " + '\n' +
			@"/o/ // " + '\n' +
			@"       ",
			// 10
			@"       " + '\n' +
			@" /o|\  " + '\n' +
			@"     \ " + '\n' +
			@"    // ",
			// 11
			@"       " + '\n' +
			@" /o|\  " + '\n' +
			@"     \ " + '\n' +
			@"    // ",
			// 12
			@" __O\  " + '\n' +
			@"    \  " + '\n' +
			@"    /\ " + '\n' +
			@"   / / ",
			// 13
			@" __O\  " + '\n' +
			@"    \  " + '\n' +
			@"    /\ " + '\n' +
			@"   / / ",
			// 14
			@"       " + '\n' +
			@"    o  " + '\n' +
			@"  </<  " + '\n' +
			@"   >>  ",
			// 15
			@"       " + '\n' +
			@"    o  " + '\n' +
			@"  </<  " + '\n' +
			@"   >>  ",
		};
	}
}
