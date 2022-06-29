namespace Console_Monsters.Monsters;

internal class Snake : MonsterBase
{
	public Snake()
	{
		Sprite = (
			"               ╭────╮    " + '\n' +
			"               │ ^_^│    " + '\n' +
			"    ╭───╮ ╭───╮╰─╮╭─╯    " + '\n' +
			"    ││‾││_││‾││__││      " + '\n' +
			"    ╰╯ ╰───╯ ╰────╯      ").Split('\n');
	}
}
