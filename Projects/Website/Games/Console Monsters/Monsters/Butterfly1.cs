//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class Butterfly1 : MonsterBase
{
	public Butterfly1()
	{
		Name = "Butterfly Larva";
	}

	public override string[] Sprite => (
		"╭┴─┴╮" + '\n' +
		"│^_^│" + '\n' +
		"╰┬─┬╯" + '\n' +
		" ├ ┤ " + '\n' +
		" ├ ┤ " + '\n' +
		" ╰╥╯ ").Split('\n');
}
