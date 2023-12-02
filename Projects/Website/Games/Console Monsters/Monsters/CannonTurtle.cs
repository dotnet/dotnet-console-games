//using Website.Games.Console_Monsters.Screens;
using Website.Games.Console_Monsters.Bases;

namespace Website.Games.Console_Monsters.Monsters;

public class TurtleCannon : MonsterBase
{
	public TurtleCannon()
	{
		Name = "Turtle Cannon";
	}

	public override string[] Sprite => (
		"    ╭─────╮    " + '\n' +
		" (O)│ ‾o‾ │(O) " + '\n' +
		"╭─╨─╯╔═══╗╰─╨─╮" + '\n' +
		"│ ╭╮╔╝   ╚╗╭╮ │" + '\n' +
		"╰─╯╔╝     ╚╗╰─╯" + '\n' +
		"   ╚╗     ╔╝   " + '\n' +
		"  ╭╯╚╗   ╔╝╰╮  " + '\n' +
		"  │ ╭╚═══╝╮ │  " + '\n' +
		"  ╰─╯     ╰─╯  ").Split('\n');
}