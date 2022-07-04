namespace Console_Monsters.Monsters;

public class Butterfly : MonsterBase
{
	public Butterfly()
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
