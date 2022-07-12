namespace Console_Monsters.Monsters;

public class Elephant : MonsterBase
{
	public Elephant()
	{
		Name = "Elephant";
	}

	public override string[] Sprite => (
		@"  ╭───╮      " + '\n' +
		@" C│^ ^│Ↄ───╮ " + '\n' +
		@"╒╕╰┤├┬╯    ├╮" + '\n' +
		@"╰╰─╯╯╰╮╮─╮╮╯ " + '\n' +
		@"      └└ ┘┘  ").Split('\n');
}
