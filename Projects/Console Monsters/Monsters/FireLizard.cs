namespace Console_Monsters.Monsters;

internal class FireLizard : MonsterBase
{
	public FireLizard()
	{
		Sprite = (
			@" ╰╮             " + '\n' +
			@" ╰╮╰╮      ╭───╮" + '\n' +
			@" ╰╮╰╮╰╮    │^_^│" + '\n' +
			@"    ╰╮╰────╯  ╭╯" + '\n' +
			@"     ╰┬╮ ╭─┬╮ │ " + '\n' +
			@"      ╰╰─╯ ╰╰─╯ ").Split('\n');
	}
}