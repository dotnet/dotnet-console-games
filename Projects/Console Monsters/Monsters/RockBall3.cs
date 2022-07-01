namespace Console_Monsters.Monsters;

internal class RockBall3 : MonsterBase
{
	public RockBall3()
	{
		Name = "Rock Ball";
	}

	public override string[] Sprite => (
		"╭─╮   ╭───────╮   ╭─╮" + '\n' +
		"│ │╭─╮│       │╭─╮│ │" + '\n' +
		"│ ╰╯ ╰┤  ‾o‾  ├╯ ╰╯ │" + '\n' +
		"╰─────┤       ├─────╯" + '\n' +
		"      ╰───────╯      ").Split('\n');
}
