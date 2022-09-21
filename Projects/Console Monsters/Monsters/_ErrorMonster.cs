namespace Console_Monsters.Monsters;

public class _ErrorMonster : MonsterBase
{
	public _ErrorMonster()
	{
		Name = "No Monster";
	}

	public override string[] Sprite => (
		"╔═════╗" + "\n" +
		"║ERROR║" + "\n" +
		"║ERROR║" + "\n" +
		"║ERROR║" + "\n" +
		"╚═════╝").Split('\n');
}
