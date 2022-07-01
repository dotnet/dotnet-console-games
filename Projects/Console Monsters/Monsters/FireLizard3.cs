namespace Console_Monsters.Monsters;

internal class FireLizard3 : MonsterBase
{
	public FireLizard3()
	{
		Sprite = (
			@"     ╭─╮─╮      " + '\n' +
			@"    ╭╯ ╰╮╰╮     " + '\n' +
			@"   ╭╯   ╰╮│╭───╮" + '\n' +
			@"   ╰╮    │││‾o‾│" + '\n' +
			@"╰╮  ╰─╮  │││   │" + '\n' +
			@"╰╮╰╮  ╰╮ ╰┴╯│ ││" + '\n' +
			@"╰╮╰╮╰╮ ╰──╮ ╰─╯│" + '\n' +
			@"   ╰──────╯   ╭╯" + '\n' +
			@"         ╰┬╮ ╭╯ " + '\n' +
			@"          ╰╰─╯  ").Split('\n');
		Name = "Fire Lizard Large";
	}
}