namespace Console_Monsters.Monsters;

public class _ErrorMonster : MonsterBase
{
	public _ErrorMonster()
	{
		Name = "No Monster";
		AttackStat = 0;
		DefenseStat = 0;
		SpeedStat = 0;
	}

	public override string[] Sprite => (
		"╔═════╗" + "\n" +
		"║ERROR║" + "\n" +
		"║ERROR║" + "\n" +
		"║ERROR║" + "\n" +
		"╚═════╝").Split('\n');
}
