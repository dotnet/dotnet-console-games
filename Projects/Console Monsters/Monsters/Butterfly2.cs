namespace Console_Monsters.Monsters;

public class Butterfly2 : MonsterBase
{
	public Butterfly2()
	{
		Name = "Butterfly Cacoon";
	}

	public override string[] Sprite => (
		"  ╭─╮  " + '\n' +
		" ╭╯╯╰╮ " + '\n' +
		"╭╯o_o╰╮" + '\n' +
		"╰╮╮ ╭╭╯" + '\n' +
		" ╰╮╮╭╯ " + '\n' +
		"  ╰─╯  ").Split('\n');
}
