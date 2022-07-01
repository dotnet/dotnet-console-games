namespace Console_Monsters.Monsters;

internal class TurtleCannon : MonsterBase
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