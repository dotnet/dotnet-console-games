using System.Linq;
using System.Reflection;

namespace Console_Monsters.Bases;

public abstract class MonsterBase
{
	public string? Name { get; set; }

	public int Level { get; set; }

	public List<CMType>? MonsterType { get; set; }

	public int ExperiencePoints { get; set; }

	public double BaseHP { get; set; }
	public double CurrentHP { get; set; }
	public double MaximumHP { get; set; }

	public double BaseEnergy { get; set; }
	public double CurrentEnergy { get; set; }
	public double MaximumEnergy { get; set; }

	public int Evolution { get; set; }

	public abstract string[] Sprite { get; }

	//// In case we want the monster to follow the player in the map view.
	//public abstract string[] SmallSprite { get; }

	public int AttackStat { get; set; }

	public int SpeedStat { get; set; }

	public int DefenseStat { get; set; }

	public int AccuracyStat { get; set; } = 100;

	public int EvasionStat { get; set; } = 100;

	public List<MoveBase>? MoveSet { get; set; }

	public string? Description { get; set; }

	public string? StatusCondition { get; set; }

	public static MonsterBase GetRandom()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		Type[] monsterTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(MonsterBase)).ToArray();
		Type monsterType = monsterTypes[Random.Shared.Next(monsterTypes.Length)];
		MonsterBase monster = (MonsterBase)Activator.CreateInstance(monsterType)!;
		return monster;
	}
	public static void WildMonster(MonsterBase OpponentMonster)
	{
		OpponentMonster.Level = SetRandomLevelForWildMonster(partyMonsters);
		OpponentMonster.MaximumHP = SetMaxHPFromBase(OpponentMonster.BaseHP, OpponentMonster.Level);
		OpponentMonster.CurrentHP = OpponentMonster.MaximumHP;
	}

	public static int SetRandomLevelForWildMonster(List<MonsterBase> playerMonsters)
	{
		List<int> monsterLvls = new();
		for (int i = 0; i < playerMonsters.Count; i++)
			monsterLvls.Add(playerMonsters[i].Level);

		int level = Random.Shared.Next(monsterLvls.AsQueryable().Min()-4, monsterLvls.AsQueryable().Max()+5);

		return level;
	}

	public static double SetMaxHPFromBase(double baseHP, int level)
	{
		//int HPStatExp   // IN CASE WE HAVE IV's ------ https://bulbapedia.bulbagarden.net/wiki/Stat
		double maxHP = (((baseHP * 2 + (Math.Sqrt(1) / 4)) * level) / 100) + level + 10;

		return maxHP;
	}

	public static double SetMaxEnergyFromBase(double baseEN, int level)
	{
		double maxEN = (((baseEN * 2 + (Math.Sqrt(1) / 4)) * level) / 20) + level + 10;

		return maxEN;
	}

	public static double SetMaxOtherStatFromBase(double baseHP, int level)
	{
		double maxHP = (((baseHP * 2) * level) / 100) + 5;

		return maxHP;
	}
}
