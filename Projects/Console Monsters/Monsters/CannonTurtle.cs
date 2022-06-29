namespace Console_Monsters.Monsters;

internal class TurtleCannon : MonsterBase
{
	public TurtleCannon()
	{
		Sprite = (
			"        ╭─────╮        " + '\n' +
			"     (O)│ ‾o‾ │(O)     " + '\n' +
			"    ╭─╨─╯╔═══╗╰─╨─╮    " + '\n' +
			"    │ ╭╮╔╝   ╚╗╭╮ │    " + '\n' +
			"    ╰─╯╔╝     ╚╗╰─╯    " + '\n' +
			"       ╚╗     ╔╝       " + '\n' +
			"      ╭╯╚╗   ╔╝╰╮      " + '\n' +
			"      │ ╭╚═══╝╮ │      " + '\n' +
			"      ╰─╯     ╰─╯      ").Split('\n');
	}
}