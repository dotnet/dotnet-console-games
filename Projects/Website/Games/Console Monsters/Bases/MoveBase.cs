using System.Reflection;

using System;
using System.Linq;
//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Enums;
using Towel;
using static Towel.Statics;

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
