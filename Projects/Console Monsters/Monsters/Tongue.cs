namespace Console_Monsters.Monsters;

internal class Tongue : MonsterBase
{
	public Tongue()
	{
		Name = "Tongue";
	}

	public override string[] Sprite => (
		"  ╭─────╮    " + '\n' +
		"  │ ⱺ_ⱺ │    " + '\n' +
		"╭─╰─┤ ├─╯─╮  " + '\n' +
		"│ ╷╭┤ ├╮╷ │  " + '\n' +
		"╰─╯││ ││╰─╯╭╮" + '\n' +
		" ╭╯╰┤ ├╯╰╮─╯│" + '\n' +
		" │ ╭┤ ├╮ ├──╯" + '\n' +
		" ╰─╯│ │╰─╯   " + '\n' +
		"    │ │      " + '\n' +
		"    ╯─╰      ").Split('\n');
}
