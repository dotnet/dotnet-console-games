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

namespace Website.Games.Console_Monsters.Items;

public class Key : ItemBase
{
	public override string? Name => "Key";

	public override string? Description => "It opens something";

	public override string Sprite =>
		@"  ═╗   " + "\n" +
		@"  ═╣   " + "\n" +
		@"   ║   " + "\n" +
		@"  ╔╩╗  " + "\n" +
		@"  ╚═╝  ";

	public static readonly Key Instance = new();
}
