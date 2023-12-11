//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class RockBall2 : MonsterBase
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
