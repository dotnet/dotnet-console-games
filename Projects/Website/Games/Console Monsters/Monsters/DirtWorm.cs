//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class DirtWorm : MonsterBase
{
	public DirtWorm()
	{
		Name = "Dirt Worm";
	}

	public override string[] Sprite => (
		" ╭───╮ " + '\n' +
		" │^_^│ " + '\n' +
		" │   │ " + '\n' +
		" │   │ " + '\n' +
		"─┴───┴─").Split('\n');
}
