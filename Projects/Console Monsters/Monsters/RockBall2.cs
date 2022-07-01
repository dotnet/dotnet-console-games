namespace Console_Monsters.Monsters;

internal class RockBall2 : MonsterBase
{
	public RockBall2()
	{
		Name = "Rock Ball 2";
	}

	public override string[] Sprite => (
		"╭─╮ ╭───────╮ ╭─╮" + '\n' +
		"│ ╰─┤  o_o  ├─╯ │" + '\n' +
		"╰───┤       ├───╯" + '\n' +
		"    ╰───────╯    ").Split('\n');
}
