//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

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
