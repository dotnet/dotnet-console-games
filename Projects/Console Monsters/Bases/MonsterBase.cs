using System.Reflection;

namespace Console_Monsters.Bases;

public abstract class MonsterBase
{
	public string? Name { get; set; }

	public int Level { get; set; }

	public string? MonsterType { get; set; }

	public int ExperiencePoints { get; set; }

	public double CurrentHP { get; set; }

	public double MaximumHP { get; set; }

	public int CurrentEnergy { get; set; }

	public int MaximumEnergy { get; set; }

	public int Evolution { get; set; }

	public abstract string[] Sprite { get; }

	//// In case we want the monster to follow the player in the map view.
	//public abstract string[] SmallSprite { get; }

	public int AttackStat { get; set; }

	public int SpeedStat { get; set; }

	public int DefenseStat { get; set; }

	public List<MoveBase>? MoveSet { get; set; }

	public string? Description { get; set; }

	//public string? StatusCondition { get; set; }


	//public static MonsterBase GetRandom(){}

	public static MonsterBase GetRandom()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		Type[] monsterTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(MonsterBase)).ToArray();
		Type monsterType = monsterTypes[Random.Shared.Next(monsterTypes.Length)];
		MonsterBase monster = (MonsterBase)Activator.CreateInstance(monsterType)!;
		return monster;
	}
}
