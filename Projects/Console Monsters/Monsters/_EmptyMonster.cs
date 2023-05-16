namespace Console_Monsters.Monsters;

public class _EmptyMonster : MonsterBase
{
	public _EmptyMonster()
	{
		Name = "Empty Monster";
		AttackStat = 0;
		DefenseStat = 0;
		SpeedStat = 0;
	}

	public override string[] Sprite => (
		"       " + "\n" +
		"       " + "\n" +
		"       " + "\n" +
		"       " + "\n" +
		"       ").Split('\n');
}
