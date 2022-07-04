namespace Console_Monsters.Monsters;

public class Butterfly3 : MonsterBase
{
	public Butterfly3()
	{
		Name = "Butterfly";
	}

	public override string[] Sprite => (
		"       ╭┴─┴╮       " + '\n' +
		" ╭╭─╮╮ │‾o‾│ ╭╭─╮╮ " + '\n' +
		"╭╭╯|╰╮╮╰┬─┬╯╭╭╯|╰╮╮" + '\n' +
		"╭╯♦|♦╰─╮├ ┤╭─╯♦|♦╰╮" + '\n' +
		"╰╮♦|♦╭─╯├ ┤╰─╮♦|♦╭╯" + '\n' +
		"╰╰╮|╭╯╯ ├ ┤ ╰╰╮|╭╯╯" + '\n' +
		" ╰╰─╯╯  ├ ┤  ╰╰─╯╯ " + '\n' +
		"        ╰╥╯        ").Split('\n');
}
