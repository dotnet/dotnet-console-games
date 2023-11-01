//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class FireLizard2 : MonsterBase
{
	public FireLizard2()
	{
		Name = "Fire Lizard Medium";
	}

	public override string[] Sprite => (
		"╰╮       ╭───╮" + '\n' +
		"╰╮╰╮     │o_o│" + '\n' +
		"╰╮╰╮╰╮ ╭─╯  ╭╯" + '\n' +
		"   ╰╮╰─╯╰─╯╭╯ " + '\n' +
		"    ╰──┬╮ ╭╯  " + '\n' +
		"       ╰╰─╯   ").Split('\n');
}