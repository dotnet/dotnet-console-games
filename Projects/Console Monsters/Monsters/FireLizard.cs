namespace Console_Monsters.Monsters;

internal class FireLizard : MonsterBase
{
	public FireLizard()
	{
		Name = "Fire Lizard Small";
	}

	public override string[] Sprite => (
		"╰╮             " + '\n' +
		"╰╮╰╮      ╭───╮" + '\n' +
		"╰╮╰╮╰╮    │^_^│" + '\n' +
		"   ╰╮╰────╯  ╭╯" + '\n' +
		"    ╰┬╮ ╭─┬╮ │ " + '\n' +
		"     ╰╰─╯ ╰╰─╯ ").Split('\n');
}