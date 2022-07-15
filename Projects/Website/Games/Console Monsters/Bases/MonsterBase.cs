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

public abstract class MonsterBase
{
	public string? Name { get; set; }

	public int Level { get; set; }

	public int ExperiencePoints { get; set; }

	public int CurrentHP { get; set; }

	public int MaximumHP { get; set; }

	public int CurrentEnergy { get; set; }

	public int MaximumEnergy { get; set; }

	public int Evolution { get; set; }

	public abstract string[] Sprite { get; }

	public int AttackStat { get; set; }

	public int SpeedStat { get; set; }

	public int DefenseStat { get; set; }

	public List<string>? MoveSet { get; set; }

	public string? Description { get; set; }

	//public AnimalType Type { get; set; }

	//public string? StatusCondition { get; set; }


	//public static MonsterBase GetRandom(){}

	public static MonsterBase GetRandom()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		System.Type[] monsterTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(MonsterBase)).ToArray();
		System.Type monsterType = monsterTypes[Random.Shared.Next(monsterTypes.Length)];
		MonsterBase monster = (MonsterBase)Activator.CreateInstance(monsterType)!;
		return monster;
	}
}
