namespace Console_Monsters.Monsters;

internal class FireLizard3 : MonsterBase
{
	public FireLizard3()
	{
		Name = "Fire Lizard Large";
	}

	public override string[] Sprite => (
		"     ╭─╮─╮      " + '\n' +
		"    ╭╯ ╰╮╰╮     " + '\n' +
		"   ╭╯   ╰╮│╭───╮" + '\n' +
		"   ╰╮    │││‾o‾│" + '\n' +
		"╰╮  ╰─╮  │││   │" + '\n' +
		"╰╮╰╮  ╰╮ ╰┴╯│ ││" + '\n' +
		"╰╮╰╮╰╮ ╰──╮ ╰─╯│" + '\n' +
		"   ╰──────╯   ╭╯" + '\n' +
		"         ╰┬╮ ╭╯ " + '\n' +
		"          ╰╰─╯  ").Split('\n');
}