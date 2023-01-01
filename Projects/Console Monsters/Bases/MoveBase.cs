using System.Reflection;

namespace Console_Monsters.Bases;

public abstract class MoveBase
{
	public string? Name { get; set; }

	public int Priority { get; set; }

	public double BaseDamage { get; set; }

	public int EnergyTaken { get; set; }

	public DamageType? DamageType { get; set; }

	public CMType? Element { get; set; }

	public string? Description { get; set; }

	public static MoveBase GetRandomMove()
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		System.Type[] moveTypes = assembly.GetTypes().Where(t => t.BaseType == typeof(MoveBase)).ToArray();
		System.Type moveType = moveTypes[Random.Shared.Next(moveTypes.Length)];
		MoveBase move = (MoveBase)Activator.CreateInstance(moveType)!;
		return move;
	}

	public double CalculateDamage(MonsterBase attackingMonster, MonsterBase defendingMonster, MoveBase move)
	{
		//Critical Damage Chance
		Random random = new();
		int critical = 1;
		double dmg = 0;

		//OG is 256 / 256, but for playability it will be higher until further change.
		int num = random.Next(0, 1001);
		if (num < random.Next(0, 1001))
			critical = (attackingMonster.Level * 2 + 5) / attackingMonster.Level + 5;

		if (attackingMonster.AttackStat > 255)
			attackingMonster.AttackStat /= 4;
		if (defendingMonster.DefenseStat > 255)
			defendingMonster.DefenseStat /= 4;

		dmg = (((2 * attackingMonster.Level * critical / 5 + 2)
				* BaseDamage * attackingMonster.AttackStat / defendingMonster.DefenseStat) / 50 + 2)
			* random.Next(217, 256) / 255;

		return dmg;
	}

	public void CalculateStatChange(MonsterBase attackingMonster, MonsterBase defendingMonster, MoveBase move)
	{

	}
}
