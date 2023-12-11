//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class DirtWormTrio : MonsterBase
{
	public DirtWormTrio()
	{
		Name = "Dirt Worm Trio";
	}

	public override string[] Sprite => (
		" ╭───╮       " + '\n' +
		" │‾o‾│ ╭───╮ " + '\n' +
		" │   │ │o_o│ " + '\n' +
		" │  ╭┴─┴╮  │ " + '\n' +
		" │  │^_^│  │ " + '\n' +
		" │  │   │  │ " + '\n' +
		" │  │   │  │ " + '\n' +
		"─┴──┴───┴──┴─").Split('\n');
}
