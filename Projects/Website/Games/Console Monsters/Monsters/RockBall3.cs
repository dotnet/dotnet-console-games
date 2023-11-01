//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class RockBall3 : MonsterBase
{
	public RockBall3()
	{
		Name = "Rock Ball";
	}

	public override string[] Sprite => (
		"╭───╮             ╭───╮" + '\n' +
		"╰╮ ╮╯  ╭───────╮  ╰╭ ╭╯" + '\n' +
		" │ │╭─╮│       │╭─╮│ │ " + '\n' +
		" │ ╰╯ ╰┤  ‾o‾  ├╯ ╰╯ │ " + '\n' +
		" ╰─────┤       ├─────╯ " + '\n' +
		"       ╰───────╯       ").Split('\n');
}
