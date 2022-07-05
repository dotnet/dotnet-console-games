namespace Console_Monsters.Monsters;

public class Butterfly1 : MonsterBase
{
	public Butterfly1()
	{
		Name = "Butterfly Larva";
	}

	public override string[] Sprite => (
		"╭┴─┴╮" + '\n' +
		"│^_^│" + '\n' +
		"╰┬─┬╯" + '\n' +
		" ├ ┤ " + '\n' +
		" ├ ┤ " + '\n' +
		" ╰╥╯ ").Split('\n');
}
