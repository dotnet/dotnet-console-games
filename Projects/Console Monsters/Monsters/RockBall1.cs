namespace Console_Monsters.Monsters;

internal class RockBall1 : MonsterBase
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
