using System;
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

internal class Mushroom : ItemBase
{
	public override string? Name => "Mushroom";

	public override string? Description => "It is a mushroom";

	public override string Sprite =>
		@"   __  " + "\n" +
		@"  / `\ " + "\n" +
		@" (___:)" + "\n" +
		@"  """""""" " + "\n" +
		@"   ||  ";

	public static readonly Mushroom Instance = new();
}
