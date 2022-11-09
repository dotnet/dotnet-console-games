namespace Console_Monsters.Monsters;

public class FireLizard3 : MonsterBase
{
	public FireLizard3()
	{
		Name = "Fire Dragon";
	}

	public override string[] Sprite => (
		"     ╭─╮─╮      " + '\n' +
		"    ╭╯ ╰╮╰╮     " + '\n' +
		"   ╭╯   ╰╮│╭───╮" + '\n' +
		"   ╰╮    │││‾o‾│" + '\n' +
		"╰╮  ╰─╮  │││   │" + '\n' +
		"╰╮╰╮  ╰╮ ╰┴╯│ ││" + '\n' +
		"╰╮╰╮╰╮ ╰──╮ ╰─╯│" + '\n' +
		"   ╰─────┬╯   ╭╯" + '\n' +
		"         ╰┬╮ ╭╯ " + '\n' +
		"          ╰╰─╯  ").Split('\n');
}