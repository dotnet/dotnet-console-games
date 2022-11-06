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

	public double CalculateDamage(MonsterBase attackingMonster, MonsterBase defendingMonster, MoveBase move)
	{
		//Critical Damage Chance
		Random random = new();
		int critical = 1;

		//OG is 256 / 256, but for playablity sake it will be higher until further changes

		int num = random.Next(0, 501);
		if(num < random.Next(0, 501) && move.DamageType != Enums.DamageType.Special)
		{ 
			critical = (attackingMonster.Level * 2 + 5) / attackingMonster.Level + 5;
		}

		if (attackingMonster.AttackStat > 255)
			attackingMonster.AttackStat /= 4;
		if (defendingMonster.DefenseStat > 255)
			defendingMonster.DefenseStat /= 4;

		double dmg = (((2 * attackingMonster.Level * critical / 5 + 2)
					* BaseDamage * attackingMonster.AttackStat / defendingMonster.DefenseStat) / 50 + 2)
					* BattleRandom.Next(217, 256) / 255;

		return dmg;
	}
}
