namespace Console_Monsters.Monsters;

public class RockBall1 : MonsterBase
{
	public RockBall1()
	{
		Name = "Rock Ball";
	}

	public override string[] Sprite => (
		"  ╭─────╮  " + '\n' +
		"╭─┤ ^_^ ├─╮" + '\n' +
		"╰─╰─────╯─╯").Split('\n');
}
