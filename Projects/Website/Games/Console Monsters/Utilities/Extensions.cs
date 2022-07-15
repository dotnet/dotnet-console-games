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

namespace Website.Games.Console_Monsters.Utilities;

public static class Extensions
{
	[System.Diagnostics.DebuggerHidden]
	public static T Get<T>(this T[,] array2D, (int Index1, int Index2) indeces) => array2D[indeces.Index1, indeces.Index2];

	[System.Diagnostics.DebuggerHidden]
	public static (T, T) Reverse<T>(this (T, T) tuple) => (tuple.Item2, tuple.Item1);
}
