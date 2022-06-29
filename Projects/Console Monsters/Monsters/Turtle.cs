namespace Console_Monsters.Monsters;

internal class Turtle : MonsterBase
{
	public Turtle()
	{
		Sprite = (
			"       ╭───╮       " + '\n' +
			"       │^_^│       " + '\n' +
			"    ╭──╔═══╗──╮    " + '\n' +
			"    ╰─╔╝   ╚╗─╯    " + '\n' +
			"     ╭╚╗   ╔╝╮     " + '\n' +
			"     ╰─╚═══╝─╯     ").Split('\n');
	}
}