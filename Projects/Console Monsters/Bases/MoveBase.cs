using System.Reflection;

namespace Console_Monsters.Bases;

public abstract class MoveBase
{
	public string? Name { get; set; }

	public double BaseDamage { get; set; }

	public int EnergyTaken { get; set; }

	public DamageType? DamageType { get; }

	public Element? Element { get; }

	public string? Description { get; set; }

	public static MoveBase GetRandomMove()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		Type[] moveTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(MoveBase)).ToArray();
		Type moveType = moveTypes[Random.Shared.Next(moveTypes.Length)];
		MoveBase move = (MoveBase)Activator.CreateInstance(moveType)!;
		return move;
	}

	public double CalculateDamage(MonsterBase attackingMonster, MonsterBase defendingMonster)
	{
		double dmg = (((2 * attackingMonster.Level / 5 + 2)
					* BaseDamage * attackingMonster.AttackStat / defendingMonster.DefenseStat) / 50 + 2)
					* BattleRandom.Next(85, 101) / 100;

		return dmg;
	}
}
