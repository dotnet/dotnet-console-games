﻿using System;
using System.Linq;
using System.Text;
using System.Threading;
using static Website.Games.Console_Monsters.Statics;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Items;
using Website.Games.Console_Monsters.Maps;
using Website.Games.Console_Monsters.Monsters;
using Website.Games.Console_Monsters.Bases;
using Website.Games.Console_Monsters.Characters;
using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Screens.Menus;
using Website.Games.Console_Monsters.Enums;
using Website.Games.Console_Monsters.Utilities;
using System.Collections.Generic;
using Towel;
using static Towel.Statics;
using System.Threading.Tasks;

namespace Website.Games.Console_Monsters;

public class Player
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

	public (int I, int J) InteractTile
	{
		get
		{
			var (tileI, tileJ) = MapBase.WorldToTile(I, J);
			if (Animation == IdleDown)
			{
				return (tileI, tileJ + 1);
			}
			if (Animation == IdleUp)
			{
				return (tileI, tileJ - 1);
			}
			if (Animation == IdleLeft)
			{
				return (tileI - 1, tileJ);
			}
			if (Animation == IdleRight)
			{
				return (tileI + 1, tileJ);
			}
			throw new NotImplementedException();
		}
	}


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

	#region IdleLeft
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
	#endregion

	#region IdleRight
	public static readonly string IdleRight1 =
		@"  ╭══╮ " + '\n' +
		@"  │ '│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ";
	public static readonly string IdleRight2 =
		@"  ╭══╮ " + '\n' +
		@"  │ -│ " + '\n' +
		@"  ╰──╯ " + '\n' +
		@"  │||│ " + '\n' +
		@"  │__│ ";
	public static readonly string[] IdleRight =
		Enumerable.Repeat(IdleRight1, 100).Concat(Enumerable.Repeat(IdleRight2, 10)).ToArray();
	#endregion

	#region IdleUp
	public static readonly string IdleUp1 =
		@" ╭═══╮ " + '\n' +
		@" │   │ " + '\n' +
		@"╭╰───╯╮" + '\n' +
		@"│├───┤│" + '\n' +
		@" │_|_│ ";
	//#warning TODO: need another idle sprite
	public static readonly string[] IdleUp =
		Enumerable.Repeat(IdleUp1, 100).Concat(Enumerable.Repeat(IdleUp1, 10)).ToArray();
	#endregion

	#region IdleDown
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

	#endregion
}
