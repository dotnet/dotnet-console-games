namespace Console_Monsters;

public class Character
{
	/// <summary>Horizontal position in pixel coordinates.</summary>
	public int I { get; set; }
	/// <summary>Vertical position in pixel coordinates.</summary>
	public int J { get; set; }
	/// <summary>Currently active animation.</summary>
	public string[] Animation { get; set; } = IdleDown;
	/// <summary>The current frame of the active animation.</summary>
	public int AnimationFrame {get; set; } = 0;
	/// <summary>The render state of the character based on the <see cref="Animation"/> and <see cref="AnimationFrame"/>.</summary>
	public string Render => Animation[AnimationFrame % Animation.Length];

	public bool IsIdle =>
		Animation == IdleDown ||
		Animation == IdleUp   ||
		Animation == IdleLeft ||
		Animation == IdleRight;


	#region Player Sprites

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

	public static readonly string IdleLeft1 =
		@" ╭══╮  " + '\n' +
		@" │' │  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │__│  ";
	public static readonly string IdleLeft2 =
		@" ╭══╮  " + '\n' +
		@" │- │  " + '\n' +
		@" ╰──╯  " + '\n' +
		@" │||│  " + '\n' +
		@" │__│  ";
	public static readonly string[] IdleLeft =
		Enumerable.Repeat(IdleLeft1, 100).Concat(Enumerable.Repeat(IdleLeft2, 10)).ToArray();

	public static readonly string IdleRight1 =
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ";
	public static readonly string IdleRight2 =
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ";
	public static readonly string[] IdleRight =
		Enumerable.Repeat(IdleRight1, 100).Concat(Enumerable.Repeat(IdleRight2, 10)).ToArray();

	public static readonly string IdleUp1 =
		@" ╭═══╮ " + '\n' +
		@" │   │ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
	#warning TODO: need another idle sprite
	public static readonly string[] IdleUp =
		Enumerable.Repeat(IdleUp1, 100).Concat(Enumerable.Repeat(IdleUp1, 10)).ToArray();

	public static readonly string IdleDown1 =
		@" ╭═══╮ " + '\n' +
		@" │'_'│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";

	public static readonly string IdleDown2 =
		@" ╭═══╮ " + '\n' +
		@" │-_-│ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";

	public static readonly string[] IdleDown =
		Enumerable.Repeat(IdleDown1, 100).Concat(Enumerable.Repeat(IdleDown2, 10)).ToArray();

	#endregion
}
