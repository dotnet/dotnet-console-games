namespace Console_Monsters.Monsters;

public class Turtle : MonsterBase
{
	public Turtle()
	{
		Name = "Turtle";
	}

	public override string[] Sprite => (
		"   ╭───╮   " + '\n' +
		"   │^_^│   " + '\n' +
		"╭──╔═══╗──╮" + '\n' +
		"╰─╔╝   ╚╗─╯" + '\n' +
		" ╭╚╗   ╔╝╮ " + '\n' +
		" ╰─╚═══╝─╯ ").Split('\n');
}