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

namespace Website.Games.Console_Monsters.Bases;

public abstract class ItemBase
{
	public abstract string? Name { get; }

	public abstract string? Description { get; }

	public abstract string Sprite { get; }

	// we may want to change the following but doing this for now...

	public static bool operator ==(ItemBase a, ItemBase b) => a.GetType() == b.GetType();
	public static bool operator !=(ItemBase a, ItemBase b) => !(a == b);
	public override bool Equals(object? obj) => obj is ItemBase item && this == item;
	public override int GetHashCode() => this.GetType().GetHashCode();
}
