using System.Reflection;

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

public abstract class MoveBase
{
	public string? Name { get; set; }

	public double BaseDamge { get; set; }

	public double FinalDamage { get; set; }

	public int EnergyTaken { get; set; }

	public DamageType? DamageType { get; }

	public Element? Element { get; }

	public string? Description { get; set; }

	public static MoveBase GetRandomMove()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		System.Type[] moveTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(MoveBase)).ToArray();
		System.Type moveType = moveTypes[Random.Shared.Next(moveTypes.Length)];
		MoveBase move = (MoveBase)Activator.CreateInstance(moveType)!;
		return move;
	}
}
